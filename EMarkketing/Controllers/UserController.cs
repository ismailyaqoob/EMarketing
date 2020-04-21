using EMarkketing.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMarkketing.Controllers
{
    public class UserController : Controller
    {
        dbecommerceEntities con = null;
        public UserController()
        {
            con = new dbecommerceEntities();
        }

        // GET: User
        public ActionResult Index(int? page)
        {
            int pagesize = 9, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = con.tbl_category.Where(x => x.cat_status == 1).OrderByDescending(x => x.cat_id).ToList();
            IPagedList<tbl_category> stu = list.ToPagedList(pageindex, pagesize);
            return View(stu);
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(tbl_user f_data, HttpPostedFileBase imgfile)
        {
            if (ModelState.IsValid)
            {
                string path = UploadFile(imgfile);
                if (path != null)
                {
                    tbl_user u = new tbl_user();
                    u.u_name = f_data.u_name;
                    u.u_email = f_data.u_email;
                    u.u_contact = f_data.u_contact;
                    u.u_password = f_data.u_password;
                    u.u_image = path;
                    con.tbl_user.Add(u);
                    con.SaveChanges();
                    return RedirectToAction("Login");
                }
                Response.Write("<script>alert('File must be of .jpeg, .jpg, .png');</script>");
            }
            return View();
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(tbl_user f_data)
        {
            tbl_user e_data = con.tbl_user.Where(x => x.u_email == f_data.u_email && x.u_password == f_data.u_password).SingleOrDefault();
            if (e_data != null)
            {
                Session["u_id"] = e_data.u_id.ToString();
                return RedirectToAction("Index");
            }
            ViewBag.error = "Invalid Username or Password";
            return View();
        }

        [HttpGet]
        public ActionResult CreateAd()
        {
            if (Session["u_id"] != null)
            {
                List<tbl_category> li = con.tbl_category.ToList();
                ViewBag.CategoryList = new SelectList(li, "cat_id", "cat_name");
                return View();
            }
            return RedirectToAction("Login");

        }

        [HttpPost]
        public ActionResult CreateAd(tbl_product f_data, HttpPostedFileBase imgfile)
        {
            string path = UploadFile(imgfile);
            if (path != null)
            {
                tbl_product pro = new tbl_product();
                pro.pro_name = f_data.pro_name;
                pro.pro_price = f_data.pro_price;
                pro.pro_des = f_data.pro_des;
                pro.u_image = path;
                pro.pro_fk_cat = f_data.pro_fk_cat;
                pro.pro_fk_user = Convert.ToInt32(Session["u_id"].ToString());
                con.tbl_product.Add(pro);
                con.SaveChanges();
                return RedirectToAction("Index");
            }
            List<tbl_category> li = con.tbl_category.ToList();
            ViewBag.CategoryList = new SelectList(li, "cat_id", "cat_name");
            Response.Write("<script>alert('File must be of .jpeg, .jpg, .png');</script>");
            return View();
        }

        [HttpGet]
        public ActionResult ads(int? id, int? page)
        {
            int pagesize = 9, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = con.tbl_product.Where(x => x.pro_fk_cat == id).OrderByDescending(x => x.pro_id).ToList();
            IPagedList<tbl_product> stu = list.ToPagedList(pageindex, pagesize);
            return View(stu);
        }

        [HttpPost]
        public ActionResult ads(int? id, int? page, string search)
        {
            int pagesize = 9, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = con.tbl_product.Where(x => x.pro_name.Contains(search)).OrderByDescending(x => x.pro_id).ToList();
            IPagedList<tbl_product> stu = list.ToPagedList(pageindex, pagesize);
            return View(stu);
        }

        public ActionResult ViewAd(int? id)
        {
            ViewAd ad = new ViewAd();
            tbl_product pro = con.tbl_product.Where(x => x.pro_id == id).SingleOrDefault();
            ad.pro_name = pro.pro_name;
            ad.pro_id = pro.pro_id;
            ad.pro_price = pro.pro_price;
            ad.pro_image = pro.u_image;
            ad.pro_des = pro.pro_des;
            tbl_user u = con.tbl_user.Where(x => x.u_id == pro.pro_fk_user).SingleOrDefault();
            ad.pro_fk_user = u.u_id;
            ad.u_name = u.u_name;
            ad.u_image = u.u_image;
            ad.u_contact = u.u_contact;
            tbl_category cat = con.tbl_category.Where(x => x.cat_id == pro.pro_fk_cat).SingleOrDefault();
            ad.cat_name = cat.cat_name;
            return View(ad);
        }

        public ActionResult SignOut()
        {
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteAd(int? id)
        {
            tbl_product pro = con.tbl_product.Where(x => x.pro_id == id).SingleOrDefault();
            if (pro != null)
            {
                con.tbl_product.Remove(pro);
                con.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        private string UploadFile(HttpPostedFileBase file)
        {
            Random r = new Random();
            string path = null;
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
