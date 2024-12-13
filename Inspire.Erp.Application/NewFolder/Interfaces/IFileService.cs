using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.File;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.NewFolder.Interfaces
{
  public  interface IFileService
    {
        Task<Response<byte[]>> GeneratePdf(AddPDFResponse model);

        Task<Response<string>> CreatePDFFromHtml(AddPDFResponse model);
        Task<Response<string>> CreateFileFromExtension(GetFileFromExtensionResponse model);
        Task<Response<MemoryStream>> DownloadFile(string fileUrl);
         Task<Response<string>> GetContentType(string path);
        Task<bool> CheckFileExist(string url);
    }
}
