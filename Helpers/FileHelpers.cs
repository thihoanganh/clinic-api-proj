using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SS10_WebApplication_MVC_DB.Helpers
{
    public class FileHelpers
    {
        public static string GenerateFileName(string contentType)
        {
            var fileName = Guid.NewGuid().ToString().Replace("-", "");
            var ext = contentType.Split(new char[] { '/' })[1];
            return fileName + "." + ext;
        }
    }
}
