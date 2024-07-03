using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebPortal.Models;

namespace WebPortal.Controllers

{
    public class MaintenanceController : Controller 

    {
        readonly booking_dbEntities db = new booking_dbEntities();
        [HttpGet]
         public ActionResult Editpage(string code)
        {
            ViewBag.Message = "";
            ViewBag.code = code;
            if (code == "ABT")
            {
                ViewBag.pagetitle = db.PageInfoes.Where(x => x.Code ==  code).FirstOrDefault().Name;
                ViewBag.page = db.PageInfoes.Where(x => x.Code == code).FirstOrDefault();
            }
            else if(code == "OS")
            {
                ViewBag.pagetitle = db.PageInfoes.Where(x => x.Code == code).FirstOrDefault().Name;
                ViewBag.page = db.PageInfoes.Where(x => x.Code == code).FirstOrDefault();
            }
            return View();
        }

        [HttpPost]
        public ActionResult Editpage(PageInfo page)
        {
            var db = new booking_dbEntities();
            ViewBag.pagetitle = page.Title;
            ViewBag.page = page;

            var updatedpage = db.PageInfoes.Where(x=>x.Code == page.Code).FirstOrDefault();
            try
            {
                updatedpage.Title = page.Title;
                updatedpage.Subtitle = page.Subtitle;
                updatedpage.Content = page.Content;
                updatedpage.Subcontent = page.Subcontent;
                updatedpage.LastModifiedDate = DateTime.Now;
                updatedpage.LastModifiedBy = "Webservice";

                db.PageInfoes.AddOrUpdate(updatedpage);
                db.SaveChanges();
                ViewBag.Message = "Page Updated";
                ViewBag.aboutus = updatedpage;
                return View();
               
            }catch (Exception ex)
            {
                ViewBag.Message = "Could not save the record.( " + ex.Message + ")";
            }
            return View();
        }
    }
}
