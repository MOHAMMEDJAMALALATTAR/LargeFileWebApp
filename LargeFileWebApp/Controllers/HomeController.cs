using LargeFileWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.IO;

namespace LargeFileWebApp.Controllers
{
    public class HomeController : Controller
    {

        private readonly IHostingEnvironment Environment;   
         

        public HomeController(IHostingEnvironment _environment)
        {
            Environment=_environment;
        }
        

         
        [RequestFormLimits(MultipartBodyLengthLimit =104857600)]
        public IActionResult Index(List<IFormFile> postedFiles)
        {
            string WWWPAth = this.Environment.WebRootPath;
            string ContentPath=this.Environment.ContentRootPath;
            string path = Path.Combine(this.Environment.WebRootPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            foreach (IFormFile postedfile in postedFiles)
            {
                string FileName = Path.GetFileName(postedfile.FileName);
                using (FileStream stream = new FileStream(Path.Combine(path,FileName),FileMode.Create ))
                {
                    ViewBag.Message += string.Format("<br>{0}<br/> Files Uploaded.</br>",FileName);
                    postedfile.CopyTo(stream);
                }

            }
            return View(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }
 
    }
}
