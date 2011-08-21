using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebToolkit.Zip;
using System.IO;

namespace Samples.Web.Controllers
{
    public class ZipController : Controller
    {
        //
        // GET: /Zip/

        public ActionResult Index()
        {
            string path = Server.MapPath("~/Content");
            string[] files = Directory.GetFiles(path, "*.txt");
            string pathToUnzip = string.Concat(path, "\\Unzipped");
            string destinationFile = string.Concat(path, "\\test.zip");

            if (System.IO.File.Exists(destinationFile))
                System.IO.File.Delete(destinationFile);

            if (Directory.Exists(pathToUnzip))
                Directory.Delete(pathToUnzip, true);

            Directory.CreateDirectory(pathToUnzip);

            DateTime firstWrittenTime = DateTime.Now;

            Utility.Compress(files, destinationFile);
            ViewBag.CompressTest = System.IO.File.Exists(destinationFile);

            Utility.Extract(destinationFile, pathToUnzip);
            DateTime lastWrittenTime = Directory.GetLastWriteTime(pathToUnzip);
            ViewBag.ExtractTest = lastWrittenTime > firstWrittenTime;
            return View();
        }

    }
}
