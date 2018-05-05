using CvSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CvSite.Controllers
{
    public class PartialsController : Controller
    {
        CVSiteDB db = new CVSiteDB();
        // GET: Partials
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult Home()
        {
            var res = db.Resumes.ToList();
            return PartialView(res);
        }
        public PartialViewResult SosyalMedya()
        {
            var sos = db.Socials.ToList();
            return PartialView(sos);
        }
        public PartialViewResult Navigation()
        {
            return PartialView();
        }
        public PartialViewResult About()
        {
            ViewBag.hakkimda = db.Resumes.ToList().Take(1);
            return PartialView();
        }
       
        public PartialViewResult Education()
        {
            ViewBag.edu = db.Educations.ToList();
            return PartialView();
        }
        public PartialViewResult Experience()
        {
            ViewBag.expe = db.Experiences.ToList();
            return PartialView();
        }
        public PartialViewResult Resume()
        {
            return PartialView();
        }
        public PartialViewResult Skills()
        {
            ViewBag.skills = db.Skills.ToList();
            return PartialView();
        }
        public PartialViewResult Works()
        {
            ViewBag.workKategori = db.ProjectCategories.ToList();
            ViewBag.work = db.Projects.ToList();
            return PartialView();
        }
        public PartialViewResult Blog()
        {
            return PartialView();
        }
        public PartialViewResult Contact()
        {
            return PartialView();
        }
        public PartialViewResult Footer()
        {
            return PartialView();
        }
    }
}