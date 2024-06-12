using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPortal.Models;


namespace WebPortal.Controllers

{
    public class UserController : Controller

    {

        public ActionResult Register()
        {
           return View();
        }

        [HttpPost]
        public ActionResult Register(UserStatu userstatus)
        {

            try
            {
                booking_dbEntities db = new booking_dbEntities();
                userstatus.CreatedBy = "Webservice";
                userstatus.LastModifiedBy = "Webservice";
                userstatus.CreatedDate = DateTime.Now;
                userstatus.LastModifiedDate = DateTime.Now;

                db.UserStatus.Add(userstatus);
                db.SaveChanges();

                ViewBag.Message = "Userstatus Saved";
            }
            catch(Exception ex)
            {
                ViewBag.Message = "Could not save the record.( " + ex.Message + ")";
            }

            return View();

        }

        public ActionResult UserstatusList() 
        {
            
            var entities = new booking_dbEntities();

            var userstatuslist = entities.UserStatus.ToList();
            return View(userstatuslist);

        }

        private booking_dbEntities db = new booking_dbEntities();

        [HttpGet]
        public ActionResult Userstatusedit(int id)

        {
            
            UserStatu userstatus = db.UserStatus.Find(keyValues: id);
            return View(userstatus);

        }

        [HttpPost]

        public ActionResult Userstatusedit( UserStatu userstatus)
        {
            if (ModelState.IsValid)
            {
                userstatus.CreatedBy = "Webservice";
                userstatus.LastModifiedBy = "Webservice";
                userstatus.CreatedDate = DateTime.Now;
                userstatus.LastModifiedDate = DateTime.Now;

                db.UserStatus.AddOrUpdate(userstatus);
                db.SaveChanges();
                return RedirectToAction("UserstatusList");

            }
            return View(userstatus);
        }

        [HttpGet]
        public ActionResult Userstatusdelete(int id)
        {
            UserStatu userstatus = db.UserStatus.Find(keyValues: id);
            db.UserStatus.Remove(userstatus);
            db.SaveChanges();
            return RedirectToAction("UserstatusList");

        }
        
        
       // public ActionResult Userstatusdelete(UserStatu userstatus) 
        //{
        //    db.UserStatus.Remove(userstatus);
        //    db.SaveChanges();
        //    return RedirectToAction("UserstatusList");
        //}
    }
}
