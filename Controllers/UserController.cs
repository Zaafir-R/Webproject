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

        public ActionResult UserRegister()
        {
            var entities = new booking_dbEntities();

            var userstatuslist = entities.UserStatus.ToList();
            return View(userstatuslist);  
        }

        [HttpPost]
        public ActionResult UserRegister(User User)
        {
            try
            {
                booking_dbEntities db = new booking_dbEntities();
                User.Username = User.Email;
                User.CreatedBy = "Webservice";
                User.LastModifiedBy = "Webservice";
                User.CreatedDate = DateTime.Now;
                User.LastModifiedDate = DateTime.Now;

                db.Users.Add(User);
                db.SaveChanges();
                ViewBag.Message = "User Saved";
            }
            catch(Exception ex)
            {
                ViewBag.Message = "Could not save the record.( " + ex.Message + ")";
            }
            return RedirectToAction("Userslist");
        }

        public ActionResult UsersList()
        { 
            var entities = new booking_dbEntities();

            var Userlist = entities.Users.ToList();
            return View(Userlist);

        }

        private booking_dbEntities db = new booking_dbEntities();

        [HttpGet]
        public ActionResult Useredit(int id)
        {
            var userstatuslist = db.UserStatus.ToList();
           
            User User = db.Users.Find(keyValues: id);
            ViewBag.UserStatuslist = userstatuslist; 
            ViewBag.User = User;
            return View();
        }

        [HttpPost]

        public ActionResult Useredit(User User)
        {
            if (ModelState.IsValid)
            {
                User.Username = User.Firstname + User.Surname;
                User.Password = "123456789";
                User.CreatedBy = "Webservice";
                User.LastModifiedBy = "Webservice";
                User.CreatedDate = DateTime.Now;
                User.LastModifiedDate = DateTime.Now;

                db.Users.AddOrUpdate(User);
                db.SaveChanges();
                return RedirectToAction("UsersList");

            }
            return View(User);
        }

        [HttpGet]
        public ActionResult Userdelete(int id)
        {
            User User = db.Users.Find(keyValues: id);
            db.Users.Remove(User);
            db.SaveChanges();
            return RedirectToAction("UsersList");

        }
        
        
       // public ActionResult Userstatusdelete(UserStatu userstatus) 
        //{
        //    db.UserStatus.Remove(userstatus);
        //    db.SaveChanges();
        //    return RedirectToAction("UserstatusList");
        //}
    }
}
