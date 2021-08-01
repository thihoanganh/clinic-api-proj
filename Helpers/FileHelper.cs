using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Helpers
{
    public class FileHelper
    {
        private readonly IWebHostEnvironment _evn;
        public FileHelper(IWebHostEnvironment evn)
        {
            _evn = evn;
        }

        /// <summary>
        /// return guid name of files uploaded
        /// </summary>
        /// <returns></returns>
        public List<string> UploadFiles(List<IFormFile> targetFiles, string subPath)
        {
            try
            {
                var fnames = new List<String>();
                targetFiles.ForEach(file =>
                {
                    var FileNameGuid = GenerateFileName(file.ContentType);
                    var path = Path.Combine(_evn.WebRootPath, subPath, FileNameGuid);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    fnames.Add(FileNameGuid);
                });
                return fnames;

            }
            catch (Exception)
            {

                return null;
            }

        }

        public string UploadFile(IFormFile targetFile, string subPath)
        {
            try
            {
                var FileNameGuid = GenerateFileName(targetFile.ContentType);
                var path = Path.Combine(_evn.WebRootPath, subPath, FileNameGuid);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    targetFile.CopyTo(fileStream);
                }
                return FileNameGuid;
            }
            catch (Exception)
            {

                return null;
            }

        }

        private string GenerateFileName(string contentType)
        {
            var fileName = Guid.NewGuid().ToString().Replace("-", "");
            var ext = contentType.Split(new char[] { '/' })[1];
            return fileName + "." + ext;
        }
    }
}

