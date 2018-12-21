using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace PSU.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            try
            {
                if(file.ContentLength>0)
                {
                    string _fileName = TimeStamp() + "_" + Path.GetFileName(file.FileName),
                        _path = Path.Combine(Server.MapPath("~/App_Data/Uploads"), _fileName),
                        line;
                    file.SaveAs(_path);
                    int numLines = 0;

                    StreamReader reader = new StreamReader(_path);
                    while ((line = reader.ReadLine()) != null) {
                        numLines++;
                    }
                    reader.Close();
                    ViewBag.Message = numLines;
                }
            }
            catch (Exception e)
            {
                throw e;
                
            }

            return View();
        }

        public string TimeStamp()
        {
            return DateTime.Now.Year.ToString()+
                DateTime.Now.Month.ToString() +
                DateTime.Now.Day.ToString() +
                DateTime.Now.Hour.ToString() +
                DateTime.Now.Minute.ToString() +
                DateTime.Now.Second.ToString();
        }
    }
}