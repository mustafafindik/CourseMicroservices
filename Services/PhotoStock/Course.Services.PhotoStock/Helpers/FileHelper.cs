using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Course.Services.PhotoStock.Helpers
{
    public class FileHelper
    {
        public async static Task<string> Task<Add>(IFormFile image, CancellationToken cancellationToken)
        {
            try
            {
                string directory = Environment.CurrentDirectory + @"\wwwroot\";
                var fileName = CreateNewFileName(image.FileName);

                string path = Path.Combine(directory, "photos");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    await image.CopyToAsync(stream, cancellationToken);
                }

                var filePath = Path.Combine(path, fileName);
                var url = "photos/" + fileName;
                return url;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public static string CreateNewFileName(string fileName)
        {
            string[] file = fileName.Split('.');
            string extension = file[1];
            string newFileName = string.Format(@"{0}." + extension, Guid.NewGuid());
            return newFileName;
        }

        public static bool Delete(string photoUrl)
        {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);
                File.Delete(path);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }


        public static bool FileExist(string photoUrl)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);
            if (!System.IO.File.Exists(path))
            {
                return false;
            }
            return true;
        }
    }
}

