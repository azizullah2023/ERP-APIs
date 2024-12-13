using DinkToPdf;
using DinkToPdf.Contracts;
using Inspire.Erp.Application.NewFolder.Interfaces;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.File;
using Microsoft.AspNetCore.StaticFiles;
using Spire.Pdf;
using Spire.Pdf.Conversion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.NewFolder.Implementations
{
   public class FileService: IFileService
    {
        private IConverter _converter;
        public FileService(IConverter converter)
        {
            _converter = converter;
        }


        public async Task<Response<byte[]>>  GeneratePdf(AddPDFResponse model)
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",
                //Out = model.OutputPath
            };
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = model.Html,
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = model.CssPath },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };
            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

           //var pdfFile= _converter.Convert(pdf);

          return Response < byte[] >.Success(await Task.FromResult(_converter.Convert(pdf)), "Generated Successfully!.");
        }
        public async Task<Response<string>> CreatePDFFromHtml(AddPDFResponse model)
        {
            try
            {
                var globalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 10 },
                    DocumentTitle = "PDF Report",
                    Out = model.OutputPath
                };
                var objectSettings = new ObjectSettings
                {
                    PagesCount = true,
                    HtmlContent = model.Html,
                    WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = model.CssPath },
                    HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                    FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
                };
                var pdf = new HtmlToPdfDocument()
                {
                    GlobalSettings = globalSettings,
                    Objects = { objectSettings }
                };
                _converter.Convert(pdf);
                return Response<string>.Success(await Task.FromResult(model.OutputPath), "Generated Successfully!.");
            }
            catch (Exception ex)
            {
                return Response<string>.Fail("", ex.Message);
            }
        }
        public async Task<Response<string>> CreateFileFromExtension(GetFileFromExtensionResponse model)
        {
            try
            {
                String outputFileName = Directory.GetCurrentDirectory()+@$"/Assets/Files/{model.FileName}";
            PdfDocument pdf = new PdfDocument();
            pdf.LoadFromFile(model.Path);
            PdfStandardsConverter standardsConverter = new PdfStandardsConverter(model.Path);

                switch (model.Extension)
                {
                case "DOCX":
                    outputFileName += ".docx";
                        await CheckFileExist(outputFileName);
                    pdf.SaveToFile(outputFileName, FileFormat.DOCX);
                    break;
                case "DOC":
                    outputFileName += ".doc";
                        await CheckFileExist(outputFileName);
                        pdf.SaveToFile(outputFileName, FileFormat.DOC);
                    break;
                case "XPS":
                    outputFileName += ".xps";
                        await CheckFileExist(outputFileName);
                        pdf.SaveToFile(outputFileName, FileFormat.XPS);
                    break;
                case "XLSX":
                    outputFileName += ".xlsx";
                        await CheckFileExist(outputFileName);
                        pdf.SaveToFile(outputFileName, FileFormat.XLSX);
                    break;
            }

                return Response<string>.Success(await Task.FromResult(outputFileName), "Generated Successfully!.");
            }
            catch (Exception ex)
            {
                return Response<string>.Fail("", ex.Message);
            }
        }
        public async Task<Response<MemoryStream>> DownloadFile(string fileUrl)
        {
            try
            {
                var filePath = fileUrl;
                if (!System.IO.File.Exists(filePath))
                {
                    return Response<MemoryStream>.Fail(new MemoryStream(), "File not found.");
                }
                    var memory = new MemoryStream();
                await using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                return Response<MemoryStream>.Success(memory, "Generated Successfully!.");
            }
            catch (Exception ex)
            {
                return Response<MemoryStream>.Fail(new MemoryStream(), ex.Message);
            }
        }
        public async Task<Response<string>> GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return Response<string>.Success(await Task.FromResult(contentType),"");
        }
        public async Task<bool> CheckFileExist(string url)
        {
            await Task.Run(() =>
            {
                if (File.Exists(url))
                {
                    File.Delete(url);
                }
            });
            return true;

        }
    }
}
