using CvSite.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace CvSite.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        CVSiteDB db = new CVSiteDB();
        public ActionResult Index()
        {
            return View();
        }

        #region Users
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost, AllowAnonymous]
        public ActionResult Login(FormCollection form)
        {
            string username = form["username"].ToString();
            string password = form["password"].ToString();
            var md5pass = Crypto.Hash(password,"MD5");
            User user = db.Users.Where(x => x.userKulAdi == username && x.userSifre == md5pass && x.userActive == true).FirstOrDefault();
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.userKulAdi, false);
                Session["AdSoyad"] = user.userAd + " " + user.userSoyad;
                Session["Username"] = user.userKulAdi;
                Session["UserId"] = user.user_id;
                Session["Rol"] = user.userRole;
                Session["uyeid"] = user.user_id;
                if (user.userRole == "Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                return RedirectToAction("Index", "Home");
            }
            TempData["Message"] = Alert("Hatalı giriş yaptınız.", false);
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return Redirect("/");
        }
        public ActionResult Users()
        {
            List<User> userlist = db.Users.ToList();
            return View(userlist);
        }
        public ActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateUser(User newitem,string userSifre)
        {
            var md5pass = userSifre;
            if (ModelState.IsValid)
            {
               
                    newitem.userSifre = Crypto.Hash(md5pass,"MD5");
                    db.Users.Add(newitem);
                    db.SaveChanges();
                    Session["uyeid"] = newitem.user_id;
                    Session["Username"] = newitem.userKulAdi;
                    return RedirectToAction("Users", "Admin");
            }
            return View(newitem);

        }
        public ActionResult EditUser(int? id)
        {
            User item = db.Users.Where(x => x.user_id == id).FirstOrDefault();
            if (Convert.ToInt32(Session["uyeid"]) != item.user_id)
            {
                return RedirectToAction("Users", "Admin");
            }
            return View(item);
        }
        [HttpPost]
        public ActionResult EditUser(User uye, string userSifre, int id)
        {
            if (ModelState.IsValid)
            {
                var md5pass = userSifre;

                var uyes = db.Users.Where(u => u.user_id == id).SingleOrDefault();
                uyes.userAd = uye.userAd;
                uyes.userSoyad = uye.userSoyad;
                uyes.userEmail = uye.userEmail;
                uyes.userKulAdi = uye.userKulAdi;
                uyes.userSifre =Crypto.Hash(md5pass, "MD5");
                uyes.userRole = uye.userRole;
                uyes.userActive = uye.userActive;
                db.SaveChanges();
                TempData["Message"] = Alert("Kullanıcı güncellendi.", true);
                return RedirectToAction("Users", "Admin", new { id = uyes.user_id });
            }
            else
            {
                TempData["Message"] = Alert("Hata oluştu! Kullanıcı güncellenemedi.", false);

            }
            return RedirectToAction("EditUser", "Admin");
        }
        public ActionResult DeleteUser(int id)
        {
            User item = db.Users.Where(x => x.user_id == id).FirstOrDefault();
            if (item != null)
            {
                db.Users.Remove(item);
                db.SaveChanges();
                TempData["Message"] = Alert("Kullanıcı silindi", true);
            }
            else
            {
                TempData["Message"] = Alert("Hata oluştu! Kullanıcı silinemedi.", false);
            }
            return RedirectToAction("Users", "Admin");
        }
        public JsonResult UserState(int id)
        {
            User user = db.Users.Where(x => x.user_id == id).FirstOrDefault();
            user.userActive = !user.userActive;
            //user.Job = db.Job.Where(x=>x.Id == user.Job.Id).FirstOrDefault();
            user.userSifre = user.userSifre;
            db.SaveChanges();
            return Json(1);
        }
        #endregion
        public string Alert(string message, bool? type = null)
        {
            string tip;
            switch (type)
            {
                case false: tip = "danger"; break;
                case true: tip = "success"; break;
                default:
                    tip = "info";
                    break;
            }
            string msg = "<div class='alert alert-" + tip + " alert-dismissible'><button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button>" + message + "</div>";
            return msg;
        }
    }

}