using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Inspire.Erp.Application.Master.Implementations;
using Inspire.Erp.Application.Master.Interfaces;
using Inspire.Erp.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NPOI.HPSF;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OfficeOpenXml;

namespace Inspire.Erp.Web.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private IUpload uploadService;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;



        public WeatherForecastController(ILogger<WeatherForecastController> logger, IUpload _uploadService)
        {
            _logger = logger;
            uploadService = _uploadService;
        }

        [HttpGet]
        public IEnumerable<dynamic> Get()
        {
            var rng = new Random();

            _logger.LogInformation("you are enering log weather forecast");
            return Enumerable.Range(1, 5).Select(index => new
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        [Route("import")]
        public async Task<IActionResult> OnPostUploadAsync(IFormFile file)
        {
            if (CheckIfExcelFile(file))
            {
                await WriteFile(file);
            }
            else
            {
                return BadRequest(new { message = "Invalid file extension" });
            }

            return Ok();
        }
        private bool CheckIfExcelFile(IFormFile file)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            return (extension == ".xlsx" || extension == ".xls"); // Change the extension based on your need
        }
        private async Task<bool> WriteFile(IFormFile file)
        {
            bool isSaveSuccess = false;
            string fileName;
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                fileName = DateTime.Now.Ticks + extension; //Create a new Name for the file due to security reasons.

                var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files");

                if (!Directory.Exists(pathBuilt))
                {
                    Directory.CreateDirectory(pathBuilt);
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files",
                   fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                isSaveSuccess = true;
            }
            catch (Exception e)
            {
                //log error
            }

            return isSaveSuccess;
        }

        [HttpPost]
        [Route("excelimport")]
        public ActionResult OnPostUpload(List<IFormFile> files)
        {
            try
            {
                var file = files.FirstOrDefault();
                var inputstream = file.OpenReadStream();

                XSSFWorkbook workbook = new XSSFWorkbook(inputstream);

                ISheet sheet = workbook.GetSheetAt(0);
                // Example: var firstCellRow = (int)sheet.GetRow(0).GetCell(0).NumericCellValue;

                for (int rowIdx = 2; rowIdx <= sheet.LastRowNum; rowIdx++)
                {
                    IRow currentRow = sheet.GetRow(rowIdx);



                    //business logic & saving data to DB                        
                }
            }
            catch (Exception ex)
            {

            }
            return Ok();
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("readexcelfile")]
        public async Task<IActionResult> ReadExcelFileAsync(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
                return Content("File Not Selected");
            foreach (var file in files)
            {
                string fileExtension = Path.GetExtension(file.FileName);
                if (fileExtension != ".xls" && fileExtension != ".xlsx")
                    return Content("File Not Selected");
                if (file.Length <= 0)
                    return BadRequest();



                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (var package = new ExcelPackage(stream))
                    {

                        try
                        {

                            ExcelWorksheets worksheet = package.Workbook.Worksheets;
                            foreach (var x in worksheet)
                            {
                                var rowCount = x.Dimension.Rows;
                                List<ImportTimeSheet> importList = new List<ImportTimeSheet>();
                                for (int row = 2; row < rowCount; row++)
                                {
                                    if (x.Cells[row, 1] != null && x.Cells[row, 1].ToString().Trim() != "")
                                    {
                                        var data = x.Cells[row, 1].Text;
                                        importList.Add(new ImportTimeSheet
                                        {
                                            Date = x.Cells[row, 1].Text,
                                            Task = x.Cells[row, 2].Text,
                                            StartTime = x.Cells[row, 3].Text,
                                            EndTime = x.Cells[row, 4].Text,
                                            Hours = x.Cells[row, 5].Text,
                                            ExpectedHours = x.Cells[row, 6].Text
                                        });
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                this.uploadService.InsertCity(importList);

                            }

                        }
                        catch (Exception ex)
                        {
                           
                            throw ex;
                        } finally
                        {
                            //this.uploadService.dispose();
                        }



                    }
                    return Ok(true);
                }
            }


            return Ok(true);
        }
 
        [HttpGet]
        [Route("excelprint")]
        public async Task<IActionResult> DemoExcelPrint()
        {
            var comlumHeadrs = new string[]
        {
            "Book Id",
            "Name",
        };

            byte[] result;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Current Book"); //Worksheet name
                using (var cells = worksheet.Cells[1, 1, 1, 5])
                {
                    cells.Style.Font.Bold = true;
                }

                for (var i = 0; i < comlumHeadrs.Count(); i++)
                {
                    worksheet.Cells[1, i + 1].Value = comlumHeadrs[i];
                }

                //Add values
                var j = 2;
                for (int i= 0; i < 10;i++)
                {
                    worksheet.Cells["A" + j].Value = "Book_Id"+i;
                    worksheet.Cells["B" + j].Value = "Book_Name"+i;
                    j++;
                }

                result = package.GetAsByteArray();

                var excelFile = new FileContentResult(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    FileDownloadName = "book-export.xlsx"
                };

                return excelFile;
            }
        }
    }
}