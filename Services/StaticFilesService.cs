using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace PersonalBlog.Services
{
    public class StaticFilesService
    {
        private readonly IWebHostEnvironment hostingEnvironment;

        public StaticFilesService(IWebHostEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        public string SaveFile(IFormFile file, string path)
        {
            string fullPath = Path.Combine(hostingEnvironment.WebRootPath, path, file.FileName);
            string localPath = Path.Combine(path, file.FileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }          

            return localPath;
        }
    }
}
