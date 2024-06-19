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

            ViewBag.Userstatus = entities.UserStatus.ToList();
            ViewBag.Userroles = entities.UserRoles.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult UserRegister(User user)
        { 
            booking_dbEntities db = new booking_dbEntities();
            ViewBag.Userstatus = db.UserStatus.ToList();
            ViewBag.Userroles = db.UserRoles.ToList();

            try
            {
                user.Username = user.Email;
                user.CreatedBy = "Webservice";
                user.LastModifiedBy = "Webservice";
                user.CreatedDate = DateTime.Now;
                user.LastModifiedDate = DateTime.Now;
                user.UserStatu = db.UserStatus.Where(a => a.UserStatusId == user.UserStatusId).FirstOrDefault();
                user.UserRole = db.UserRoles.Where(a => a.UserRoleId == user.UserRoleId).FirstOrDefault();

                var obj = db.Users.Where(a => a.Email.Equals(user.Email));
                if (!obj.Any())
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    ViewBag.Message = "User Saved";
                }
                else
                {
                    ViewBag.Message = user.Email +" already Exists";
                }
                
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

        public ActionResult Useredit(User user)
        {
            if (ModelState.IsValid)
            {
                var updatedUser = db.Users.Where(x => x.Username == user.Email).FirstOrDefault();

                //User.Username = User.Email;
                updatedUser.Firstname = user.Firstname;
                updatedUser.Surname = user.Surname;
                updatedUser.Email = user.Email;
                updatedUser.UserStatusId = user.UserStatusId;
                updatedUser.CreatedBy = "Webservice";
                updatedUser.LastModifiedBy = "Webservice";
                updatedUser.CreatedDate = DateTime.Now;
                updatedUser.LastModifiedDate = DateTime.Now;
               // User.UserRole = db.UserRoles.Where(x => x.Code == "CLT").FirstOrDefault();

                db.Users.AddOrUpdate(updatedUser);
                db.SaveChanges();
                return RedirectToAction("UsersList");

            }
            return View(user);
        }

        [HttpGet]
        public ActionResult Userdelete(int id)
        {
            User User = db.Users.Find(keyValues: id);
            db.Users.Remove(User);
            db.SaveChanges();
            return RedirectToAction("UsersList");

        }

        [HttpGet]
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

                Session["Currentuser"] = obj;
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


        [HttpGet]
        public ActionResult Signup()
        {
            return View(new SignupViewModel());
        }


        [HttpPost]
        public ActionResult Signup(SignupViewModel model) 
        {
            booking_dbEntities db = new booking_dbEntities();
            try
            {
                
                if (ModelState.IsValid)
                {
                    ViewBag.SignupViewModel = model;
                    

                    User user = new User();
                    user.Firstname = model.Firstname;
                    user.Surname = model.Surname;
                    user.Username = model.Email;
                    user.Email = model.Email;
                    user.Phone = model.Phone;
                    user.CreatedBy = "Webservice";
                    user.LastModifiedBy = "Webservice";
                    user.CreatedDate = DateTime.Now;
                    user.LastModifiedDate = DateTime.Now;
                    user.UserStatusId = 2;
                    user.Password = model.Password;
                    user.UserRole = db.UserRoles.Where(x => x.Code == "CLT").FirstOrDefault();
                    user.UserRoleId = user.UserRole.UserRoleId;

                   var obj = db.Users.Where(a => a.Email.Equals(user.Email));
                    if (!obj.Any())
                    {
                        db.Users.Add(user);
                        db.SaveChanges();
                        ViewBag.Message = "User Saved";
                    }
                    else
                    {
                        ViewBag.Message = user.Email + " already Exists";
                    }
                    
                }
                else {

                    ViewBag.Errors = GetModelStateErrors(ModelState);

                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Could not save the record.( " + ex.Message + ")";
            }
            return View();
        }

        public static List<string> GetModelStateErrors(ModelStateDictionary modelState)
        {
            var query = from state in modelState.Values
                        from error in state.Errors
                        select error.ErrorMessage;

            var errorList = query.ToList();
            return errorList;
        }

        public ActionResult UserBookings()
        {
            User user = (WebPortal.Models.User)Session["Currentuser"];
            var db = new booking_dbEntities();
            var bookings = db.Appointments.Where(x => x.UserId == user.UserId).ToList();
            return View(bookings);
        }

        [HttpGet]
        public ActionResult MakeBooking()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MakeBooking(Appointment booking)
        {
            User user = (WebPortal.Models.User)Session["Currentuser"];
            var db = new booking_dbEntities();
            booking = new Appointment();
            try
            {
                if (ModelState.IsValid) 
                {
                    
                    booking.UserId = user.UserId;
                    booking.Lastmodified = DateTime.Now;
                    booking.AppointmentStatusId = 2;
                    booking.AppointmentStatu = db.AppointmentStatus.Where(a => a.AppointmentStatusId == booking.AppointmentStatusId).FirstOrDefault();
                    booking.User = db.Users.Where(a => a.UserId == booking.UserId).FirstOrDefault();
                    booking.DateandTime = DateTime.Now;
                    db.Appointments.Add(booking);
                    db.SaveChanges();
                    ViewBag.Message = "User Saved";

                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Could not save the record.( " + ex.Message + ")";
            }
            return View();
        }
        public ActionResult UserProfile()
        {
            return View();
        }
    }


}
