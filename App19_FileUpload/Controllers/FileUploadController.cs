using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.XPath;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App19_FileUpload.Controllers
{
    public class FileUploadController : Controller
    {

        private readonly IWebHostEnvironment _env;

        public FileUploadController(IWebHostEnvironment env)
        {
            _env = env;
        }
        public IActionResult Index()
        {
            string webroot = _env.WebRootPath;
            string folder = "ProductImages";
            string fdir = Path.Combine(webroot, folder);

            string[] files = Directory.GetFiles(fdir);

            List<string> imageFiles = new List<string>();

            foreach (var item in files)
            {
                string ext = Path.GetExtension(item).ToLower();
                if (ext == ".jpg" || ext == ".JPG" || ext == ".jpeg" || ext == ".JPEG" || ext == ".gif")
                {
                    string imgfile = "/ProductImages/"+ Path.GetFileName(item);


                    imageFiles.Add(imgfile);

                }
            }

            return View(imageFiles);
        }
        public IActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile myfile)
        {
            string msg = "";
            string imgfile = "";
            if (myfile != null)
            {
                if (myfile.Length>0)
                {

                    long fsize = 1024 * 1024;
                    if (myfile.Length > fsize)
                    {
                        ViewBag.msg = "File Size Must Be 1 MB or less";
                        return View();
                    }

                    

                    string webroot = _env.WebRootPath;
                    string folder = "ProductImages";
                    string filename = Path.GetFileName(myfile.FileName);
                    string fileext = Path.GetExtension(myfile.FileName).ToLower();
                    string filetoupload = Path.Combine(webroot, folder,filename);

                    bool Isexist = System.IO.File.Exists(filetoupload);
                    if (Isexist)
                    {
                        ViewBag.msg = "File Already exists";
                        //return View();
                    }




                    if (fileext ==".jpg" || fileext == ".JPG" || fileext == ".jpeg" || fileext == ".JPEG" || fileext == ".ico")
                    {

                        using (var stream = new FileStream(filetoupload, FileMode.Create))
                        {
                            await myfile.CopyToAsync(stream);
                            msg += "File has been Uploaded successfully";
                        }

                      //  imgfile = "/ProductImages/" + Path.GetFileName(filetoupload);


                    }


                    else
                    {
                        ViewBag.msg = " This " + fileext + " File Format Not allowed";
                        
                    }
                }
                else

                {
                    msg += "<br/>File has no Content";
                }

            }
            else
            {
                msg += "<br/>File Content Is Null";
            }
            //ViewBag.image = imgfile;
            ViewBag.allimages = getUploadedImages();
            ViewBag.msg = msg;
            return View();
        }



        private List<string> getUploadedImages()
        {
            string webroot = _env.WebRootPath;
            string folder = "ProductImages";
            string fdir = Path.Combine(webroot, folder);

            string[] files = Directory.GetFiles(fdir);

            List<string> imageFiles = new List<string>();

            foreach (var item in files)
            {
                string ext = Path.GetExtension(item).ToLower();
                if (ext == ".jpg" || ext == ".JPG" || ext == ".jpeg" || ext == ".JPEG" || ext == ".gif")
                {
                    string imgfile = "/ProductImages/" + Path.GetFileName(item);


                    imageFiles.Add(imgfile);

                }
            }

            return imageFiles;
        }


    }
}
