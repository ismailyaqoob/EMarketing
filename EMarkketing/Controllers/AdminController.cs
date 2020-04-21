using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMarkketing.Models;
using System.IO;
using PagedList;

namespace EMarkketing.Controllers
{
    public class AdminController : Controller
    {
        dbecommerceEntities con = null;
        //control rever
        public AdminController()
        {
            con = new dbecommerceEntities();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(tbl_admin f_data)
        {
            if (ModelState.IsValid)
            {
                tbl_admin c_data = con.tbl_admin.Where(x => x.ad_username == f_data.ad_username && x.ad_password == f_data.ad_password).SingleOrDefault();
                if (c_data != null)
                {
                    Session["ad_id"] = c_data.ad_id.ToString();
                    return RedirectToAction("ViewCategory");
                }
                ViewBag.error = "Invalid Username or Password";
            }
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (Session["ad_id"] != null)
            {
                return View();
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult Create(tbl_category f_data,HttpPostedFileBase imgfile)
        {
            if (ModelState.IsValid)
            {
                string path=UploadFile(imgfile);
                if (path != null)
                {
                    tbl_category cat = new tbl_category();
                    cat.cat_name = f_data.cat_name;
                    cat.cat_image = path;
                    cat.cat_status = 1;
                    cat.cat_fk_ad = Convert.ToInt32(Session["ad_id"].ToString());
                    con.tbl_category.Add(cat);
                    con.SaveChanges();
                    return RedirectToAction("ViewCategory");
                }
                Response.Write("<script>alert('File must be of .jpeg, .jpg, .png');</script>");
            }
            return View();
        }

        public ActionResult ViewCategory(int?page)
        {
            if (Session["ad_id"]!=null) 
            {
                int pagesize = 9, pageindex = 1;
                pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
                var list = con.tbl_category.Where(x => x.cat_status == 1).OrderByDescending(x => x.cat_id).ToList();
                IPagedList<tbl_category> stu = list.ToPagedList(pageindex, pagesize);
                return View(stu);
            }
            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Login");
        }

        public ActionResult Delete(int?id)
        {
            tbl_category cat = con.tbl_category.Where(x => x.cat_id == id).SingleOrDefault();
            con.tbl_category.Remove(cat);
            con.SaveChanges();
            return RedirectToAction("ViewCategory");
        }
        //image
        private string UploadFile(HttpPostedFileBase file)
        {
            Random r = new Random();
            string path=null;
            int rand_num = r.Next();
            if (file != null && file.ContentLength > 0)
            {
                string extension = Path.GetExtension(file.FileName);
                if (extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".png"))
                {
                    try
                    {
                        path = Path.Combine(Server.MapPath(@"~\Content\Uploads\"), rand_num + Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        path = @"~\Content\Uploads\" + rand_num + Path.GetFileName(file.FileName);
                    }
                    catch (Exception ex)
                    {
                        path = null;
                    }
                }
            }
            else
            {
                Response.Write("<script>alert('Plesase upload an image');</script>");
            }
            return path;
        }
    }
}