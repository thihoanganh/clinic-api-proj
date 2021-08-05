using Clinic_Web_Api.Entities;
using Clinic_Web_Api.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
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
        public List<Attachment> UploadFiles(List<IFormFile> targetFiles, string subPath)
        {
            try
            {
                var fnames = new List<Attachment>();
                targetFiles.ForEach(file =>
                {
                    var FileNameGuid = GenerateFileName(file.ContentType);
                    var path = Path.Combine(_evn.WebRootPath, subPath, FileNameGuid);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    fnames.Add(new Attachment() { Type = file.ContentType, Size = GetSize(Convert.ToDecimal(file.Length)), Name = FileNameGuid, OriginName = file.FileName });
                });
                return fnames;

            }
            catch (Exception)
            {

                return null;
            }

        }

        public Attachment UploadFile(IFormFile targetFile, string subPath)
        {
            try
            {
                var FileNameGuid = GenerateFileName(targetFile.ContentType);
                var path = Path.Combine(_evn.WebRootPath, subPath, FileNameGuid);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    targetFile.CopyTo(fileStream);
                }
                return new Attachment() { Type = targetFile.ContentType, Name = FileNameGuid, OriginName = targetFile.Name, Size = GetSize(Convert.ToDecimal(targetFile.Length)) };
            }
            catch (Exception)
            {

                return null;
            }

        }

        public string UploadPoster(IFormFile targetFile, string subPath)
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



        public bool FileValidate(long size, string[] accepts, IFormFile file)
        {
            FileInfo finfo = new FileInfo(file.FileName);
            var ext = finfo.Extension.ToLower();
            if (accepts.Contains(ext) && file.Length <= size)
            {
                return true;
            }


            return false;
        }

        public Stream Zip(List<Attachment> atms)
        {
            var ms = new MemoryStream();

            using (var zip = new ZipArchive(ms, ZipArchiveMode.Create, true))
            {
                atms.ForEach(atm =>
                {
                    var path = Path.Combine(_evn.WebRootPath, "lecture/attach", atm.Name);
                    var entry = zip.CreateEntry(atm.OriginName);
                    using (var fileStream = new FileStream(path, FileMode.Open))
                    using (var entryStream = entry.Open())
                    {
                        fileStream.CopyTo(entryStream);
                    }
                });
            }
            ms.Position = 0;
            return ms;
        }

        public string GetSize(decimal bytes)
        {
            string[] sizes = { "Byte", "KB", "MB", "GB", "TB" };
            int order = 0;
            while (bytes >= 1024 && order < sizes.Length - 1)
            {
                order++;
                bytes = decimal.Divide(bytes, 1024);
            }
            return String.Format("{0:0.0#} {1}", bytes, sizes[order]);
        }
        private string GenerateFileName(string contentType)
        {
            var fileName = Guid.NewGuid().ToString().Replace("-", "");
            var ext = contentType.Split(new char[] { '/' })[1];
            return fileName + "." + ext;
        }


    }
}

