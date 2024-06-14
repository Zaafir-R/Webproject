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
            catch (Exception ex)
            {
                ViewBag.Message = "Could not save the record.( " + ex.Message + ")";
            }
            return View();
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
                User.Username = User.Email;
                
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


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Login(User user)
        {
            booking_dbEntities db = new booking_dbEntities();


            var obj = db.Users.FirstOrDefault(a => a.Username.Equals(user.Username) && a.Password.Equals(user.Password));
            if (obj != null)
            {
                Session["UserId"] = obj.UserId.ToString();
                Session["Username"] = obj.Username.ToString();
                Session["Firstname"] = obj.Firstname.ToString();
                Session["Surname"] = obj.Surname.ToString();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password.");
            }

            return View(user);

        }

        public ActionResult Logout()
        {
            Session["UserId"] = null;
            Session["Username"] = null;
            Session["Firstname"] = null;
            Session["Surname"] = null;
            return RedirectToAction("login");

        }

        public ActionResult Signup()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Signup(User User) 
        {
            try
            {
                booking_dbEntities db = new booking_dbEntities();
                User.Username = User.Email;
                User.CreatedBy = "Webservice";
                User.LastModifiedBy = "Webservice";
                User.CreatedDate = DateTime.Now;
                User.LastModifiedDate = DateTime.Now;
                User.UserStatusId = 2;

                db.Users.Add(User);
                db.SaveChanges();
                ViewBag.Message = "User Saved";
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Could not save the record.( " + ex.Message + ")";
            }
            return View();
        }
    }


}
