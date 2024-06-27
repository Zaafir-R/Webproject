using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPortal.Models;

namespace WebPortal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
           
           
            if (Session["UserID"] != null)
            {
                booking_dbEntities db = new booking_dbEntities();
                User user = (WebPortal.Models.User)Session["Currentuser"];
                var userbookings = db.Appointments.Where(x => x.UserId == user.UserId).ToList();
                ViewBag.confirmed = userbookings.Where(x => x.AppointmentStatu.Code == "CON").Count();
                ViewBag.cancelled = userbookings.Where(x => x.AppointmentStatu.Code == "CAN").Count();
                ViewBag.pending = userbookings.Where(x => x.AppointmentStatu.Code == "PEN").Count();
                ViewBag.deleted = userbookings.Where(x => x.isDeleted == true).Count();
                return View();
            }
            else
            {
                return Redirect("User/Login");
            }
        }



        public ActionResult AdminIndex()
        {
            
            if (Session["UserID"] != null)
            {
                booking_dbEntities db = new booking_dbEntities();
                User user = (WebPortal.Models.User)Session["Currentuser"];
                
                ViewBag.confirmed = db.Appointments.Where(x => x.AppointmentStatu.Code == "CON"&&x.isDeleted==false).Count();
                ViewBag.cancelled = db.Appointments.Where(x => x.AppointmentStatu.Code == "CAN"&& x.isDeleted == false).Count();
                ViewBag.pending = db.Appointments.Where(x => x.AppointmentStatu.Code == "PEN"&&x.isDeleted == false).Count();
                ViewBag.deleted = db.Appointments.Where(x => x.isDeleted == true).Count();
                return View();
            }
            else
            {
                return Redirect("User/Login");
            }
        }

    }
    
}