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

        #region kullanıcı işlemleri
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
            var md5pass = Crypto.Hash(password, "MD5");
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
        public ActionResult CreateUser(User newitem, string userSifre)
        {
            var md5pass = userSifre;
            if (ModelState.IsValid)
            {

                newitem.userSifre = Crypto.Hash(md5pass, "MD5");
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
                uyes.userSifre = Crypto.Hash(md5pass, "MD5");
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

        #region Özgeçmiş işlemleri
        public ActionResult Resumes()
        {
            List<Resume> resume = db.Resumes.ToList();
            return View(resume);
        }
        public ActionResult CreateResume()
        {

            return View();
        }
        [HttpPost]
        public ActionResult CreateResume(Resume resume, HttpPostedFileBase Foto)
        {
            if (ModelState.IsValid)
            {
                if (Foto != null)
                {
                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(Foto.FileName);

                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(800, 350);
                    img.Save("~/Uploads/UyeFoto/" + newfoto);
                    resume.res_resim = "/Uploads/UyeFoto/" + newfoto;


                }


                resume.res_user_id = Convert.ToInt32(Session["uyeid"]);
                db.Resumes.Add(resume);
                db.SaveChanges();

                return RedirectToAction("Resumes", "Admin");


            }

            return View(resume);
        }
        public ActionResult EditResume(int id)
        {
            Resume resume = db.Resumes.Where(r => r.res_id == id).FirstOrDefault();
            if (resume == null)
            {
                return HttpNotFound();
            }
            return View(resume);
        }
        [HttpPost]
        public ActionResult EditResume(Resume newitem, HttpPostedFileBase Foto, int id)
        {

            Resume item = db.Resumes.Where(x => x.res_id == id).SingleOrDefault();
            if (item != null)
            {
                if (Foto != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(item.res_resim)))
                    {
                        System.IO.File.Delete(Server.MapPath(item.res_resim));
                    }
                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(Foto.FileName);

                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(800, 350);
                    img.Save("~/Uploads/UyeFoto/" + newfoto);
                    item.res_resim = "/Uploads/UyeFoto/" + newfoto;

                }
                item.res_hakkimda = newitem.res_hakkimda;
                item.res_dogum_tarihi = newitem.res_dogum_tarihi;
                item.res_adres = newitem.res_adres;

                db.SaveChanges();
                TempData["Message"] = Alert("Keşisel Bilgiler güncellendi.", true);
            }
            else
            {
                TempData["Message"] = Alert("Hata oluştu! Keşisel Bilgiler güncellenemedi.", false);
            }
            return RedirectToAction("Resumes", "Admin");
        }
        public ActionResult DeleteResume(int id)
        {
            Resume item = db.Resumes.Where(x => x.res_id == id).FirstOrDefault();
            if (item != null)
            {
                if (item == null)
                {
                    return HttpNotFound();
                }
                if (System.IO.File.Exists(Server.MapPath(item.res_resim)))
                {
                    System.IO.File.Delete(Server.MapPath(item.res_resim));
                }


                db.Resumes.Remove(item);
                db.SaveChanges();
                TempData["Message"] = Alert("Kişisel Bilgiler silindi", true);
            }
            else
            {
                TempData["Message"] = Alert("Hata oluştu! Kişisel Bilgiler silinemedi.", false);
            }
            return RedirectToAction("Resumes", "Admin");
        }
        #endregion

        #region Eğtim Bilgileri

        public ActionResult Educations()
        {
            List<Education> item = db.Educations.ToList();
            return View(item);
        }
        public ActionResult CreateEducation()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateEducation(Education newitem)
        {
            db.Educations.Add(newitem);
            db.SaveChanges();
            return RedirectToAction("Educations", "Admin");
        }
        public ActionResult EditEducation(int id)
        {
            Education item = db.Educations.Where(x => x.edu_id == id).FirstOrDefault();
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }
        [HttpPost]
        public ActionResult EditEducation(Education newitem)
        {
            Education item = db.Educations.Where(x => x.edu_id == newitem.edu_id).FirstOrDefault();
            if (item != null)
            {
                item.edu_okul_adi = newitem.edu_okul_adi;
                item.edu_bolum = newitem.edu_bolum;
                item.edu_giris_tarihi = newitem.edu_giris_tarihi;
                item.edu_cikis_tarihi = newitem.edu_cikis_tarihi;
                db.SaveChanges();
                TempData["Message"] = Alert("Eğtim Bilgileri  güncellendi.", true);
            }
            else
            {
                TempData["Message"] = Alert("Hata oluştu! Eğtim Bilgileri güncellenemedi.", false);
            }
            return RedirectToAction("Educations", "Admin");
        }
        public ActionResult DeleteEducation(int id)
        {
            Education item = db.Educations.Where(x => x.edu_id == id).FirstOrDefault();
            if (item != null)
            {
                db.Educations.Remove(item);
                db.SaveChanges();
                TempData["Message"] = Alert("Eğtim Bilgileri silindi", true);
            }
            else
            {
                TempData["Message"] = Alert("Hata oluştu! Eğtim Bilgileri silinemedi.", false);
            }
            return RedirectToAction("Educations", "Admin");
        }
        #endregion

        #region Tecrübe Bilgileri
        public ActionResult Experiences()
        {
            List<Experience> item = db.Experiences.ToList();
            return View(item);
        }
        public ActionResult CreateExperiences()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateExperiences(Experience newitem)
        {
            db.Experiences.Add(newitem);
            db.SaveChanges();
            return RedirectToAction("Experiences", "Admin");
        }
        public ActionResult EditExperiences(int id)
        {
            Experience item = db.Experiences.Where(x => x.expe_id == id).FirstOrDefault();
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }
        [HttpPost]
        public ActionResult EditExperiences(Experience newitem)
        {
            Experience item = db.Experiences.Where(x => x.expe_id == newitem.expe_id).FirstOrDefault();
            if (item != null)
            {
                item.expe_ad = newitem.expe_ad;
                item.expe_link = newitem.expe_link;
                item.expe_konum = newitem.expe_konum;
                item.expe_pozisyon = newitem.expe_pozisyon;
                item.expe_giris_tarihi = newitem.expe_giris_tarihi;
                item.expe_cikis_tarihi = newitem.expe_cikis_tarihi;
                db.SaveChanges();
                TempData["Message"] = Alert("Tecrübe Bilgileri güncellendi.", true);
            }
            else
            {
                TempData["Message"] = Alert("Hata oluştu! Tecrübe Bilgileri güncellenemedi.", false);
            }
            return RedirectToAction("Experiences", "Admin");
        }
        public ActionResult DeleteExperiences(int id)
        {
            Experience item = db.Experiences.Where(x => x.expe_id == id).FirstOrDefault();
            if (item != null)
            {
                db.Experiences.Remove(item);
                db.SaveChanges();
                TempData["Message"] = Alert("Tecrübe Bilgileri silindi", true);
            }
            else
            {
                TempData["Message"] = Alert("Hata oluştu! Tecrübe Bilgileri silinemedi.", false);
            }
            return RedirectToAction("Experiences", "Admin");
        }

        #endregion
        #region Yetenek Bilgileri

        public ActionResult Skills()
        {
            List<Skill> item = db.Skills.ToList();
            return View(item);
        }
        public ActionResult CreateSkills()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateSkills(Skill newitem)
        {
            db.Skills.Add(newitem);
            db.SaveChanges();
            return RedirectToAction("Skills", "Admin");
        }
        public ActionResult EditSkills(int id)
        {
            Skill item = db.Skills.Where(x => x.skill_id == id).FirstOrDefault();
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }
        [HttpPost]
        public ActionResult EditSkills(Skill newitem)
        {
            Skill item = db.Skills.Where(x => x.skill_id == newitem.skill_id).FirstOrDefault();
            if (item != null)
            {
                item.skill_ad = newitem.skill_ad;
                item.skill_oran = newitem.skill_oran;
                db.SaveChanges();
                TempData["Message"] = Alert("Yetenek Bilgileri güncellendi.", true);
            }
            else
            {
                TempData["Message"] = Alert("Hata oluştu! Yetenek Bilgileri güncellenemedi.", false);
            }
            return RedirectToAction("Skills", "Admin");
        }
        public ActionResult DeleteSkills(int id)
        {
            Skill item = db.Skills.Where(x => x.skill_id == id).FirstOrDefault();
            if (item != null)
            {
                db.Skills.Remove(item);
                db.SaveChanges();
                TempData["Message"] = Alert("Yetenek Bilgileri silindi", true);
            }
            else
            {
                TempData["Message"] = Alert("Hata oluştu! Yetenek Bilgileri silinemedi.", false);
            }
            return RedirectToAction("Skills", "Admin");
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