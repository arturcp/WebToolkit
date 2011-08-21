using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebToolkit.FTP;

namespace Samples.Web.Controllers
{
    public class FtpController : Controller
    {
        //
        // GET: /Ftp/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Show()
        {
            string host = Request["host"];
            string user = Request["user"];
            string password = Request["password"];
            string path = Request["path"];
            string action = Request["action"];
            Utility ftp = new Utility(host, user, password);
            if (action == "Listar")
                List(ftp, path);
            else if (action == "Upload")
                Upload(ftp, path);
            else if (action == "Rename")
                Rename(ftp, path);
            else if (action == "Delete")
                Delete(ftp, path);
            return View();
        }

        private void List(Utility ftp, string path)
        {
            ViewBag.List = ftp.GetFiles(path);
        }

        private void Rename(Utility ftp, string path)
        {
            ftp.RenameFile(path, "TextFile1.txt", "TextFile2.txt");
        }

        private void Upload(Utility ftp, string path)
        {
            string destinationPath = Server.MapPath("~/Content/TextFile1.txt");
            ftp.UploadFile(path, destinationPath);
        }

        private void Delete(Utility ftp, string path)
        {
            ftp.DeleteFile(path, "TextFile2.txt");
        }
    }
}
