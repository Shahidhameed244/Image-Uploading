using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class ImageController : Controller
    {
        DBACCESS obj = new DBACCESS();
        // GET: Image
        [HttpGet]
        public ActionResult ImageUpload()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ImageDownload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ImageDownload(Img f)
        {

            if (ModelState.IsValid)
            {
                foreach (HttpPostedFileBase file in f.files )
                {
                    if (file != null)
                    {
                        
                        f.path = new List<string>();
                        var InputFileName = Path.GetFileName(file.FileName);
                        var ServerSavePath = Path.Combine(Server.MapPath("~/UploadedFiles/") + InputFileName);
                        file.SaveAs(ServerSavePath);
                        f.pathp = ("/UploadedFiles/" + InputFileName);
                        ViewBag.UploadStatus = f.files.Count().ToString() + " Image uploaded successfully.";

                       
                        string q = "insert into images values  ('" + f.pathp + "')";
                        obj.InsertUpdateDelete(q);
                        obj.CloseCon();
                    }
                }
            }
            return View(f);
        }

        public ActionResult DisplayImages()
        {
            
            List<Img> list = new List<Img>();
            string q = "select imgpath from images";
            obj.OpenCon();
            obj.cmd =new SqlCommand(q,obj.con);
            SqlDataReader sdr =obj.cmd.ExecuteReader();
            while (sdr.Read())
            {
                Img e = new Img();
                e.pathp = sdr["imgpath"].ToString();
                list.Add(e);
            }
            sdr.Close();
            obj.CloseCon();




            return View(list);



        }
    }
}