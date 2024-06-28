using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebPortal.Models;

namespace WebPortal.Controllers
{
    public class HomeController : Controller
    {
        private booking_dbEntities db = new booking_dbEntities();
        public ActionResult Index()
        {


            if (Session["UserID"] != null)
            {
                booking_dbEntities db = new booking_dbEntities();
                User user = (WebPortal.Models.User)Session["Currentuser"];
                var userbookings = db.Appointments.Where(x => x.UserId == user.UserId).ToList();
                ViewBag.confirmed = userbookings.Where(x => x.AppointmentStatu.Code == "CON" && x.isDeleted == false).Count();
                ViewBag.cancelled = userbookings.Where(x => x.AppointmentStatu.Code == "CAN" && x.isDeleted == false).Count();
                ViewBag.pending = userbookings.Where(x => x.AppointmentStatu.Code == "PND" && x.isDeleted == false).Count();
                ViewBag.deleted = userbookings.Where(x => x.isDeleted == true).Count();
                ViewBag.latest = userbookings.OrderByDescending(x => x.CreatedDate).ToList();
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

                ViewBag.confirmed = db.Appointments.Where(x => x.AppointmentStatu.Code == "CON" && x.isDeleted == false).Count();
                ViewBag.cancelled = db.Appointments.Where(x => x.AppointmentStatu.Code == "CAN" && x.isDeleted == false).Count();
                ViewBag.pending = db.Appointments.Where(x => x.AppointmentStatu.Code == "PND" && x.isDeleted == false).Count();
                ViewBag.deleted = db.Appointments.Where(x => x.isDeleted == true).Count();
                ViewBag.latest = db.Appointments.Where(x => x.isDeleted == false).OrderByDescending(x => x.CreatedDate).ToList();
                return View();
            }
            else
            {
                return Redirect("User/Login");
            }


        }
        public ActionResult ConfirmedBookings()
        {
            User user = (WebPortal.Models.User)Session["Currentuser"];
            if (user.UserRole.Code == "ADM")
            {
                ViewBag.confirmed = db.Appointments.Where(x => x.AppointmentStatu.Code == "CON" && x.isDeleted == false).ToList();
            }
            else
            {
                ViewBag.confirmed = db.Appointments.Where(x => x.AppointmentStatu.Code == "CON" && x.isDeleted == false && x.UserId == user.UserId).ToList();
            }
            return View();
        }
        public ActionResult CancelledBookings()
        {
            User user = (WebPortal.Models.User)Session["Currentuser"];
            if (user.UserRole.Code == "ADM")
            {
                ViewBag.cancelled = db.Appointments.Where(x => x.AppointmentStatu.Code == "CAN" && x.isDeleted == false).ToList();
            }
            else
            {
                ViewBag.cancelled = db.Appointments.Where(x => x.AppointmentStatu.Code == "CAN" && x.isDeleted == false && x.UserId == user.UserId).ToList();
            }
            return View();
        }
        public ActionResult PendingBookings()
        {
            User user = (WebPortal.Models.User)Session["Currentuser"];
            if (user.UserRole.Code == "ADM")
            {
                ViewBag.pending = db.Appointments.Where(x => x.AppointmentStatu.Code == "PND" && x.isDeleted == false).ToList();
            }
            else
            {
                ViewBag.pending = db.Appointments.Where(x => x.AppointmentStatu.Code == "PND" && x.isDeleted == false && x.UserId == user.UserId).ToList();
            }
            return View();
        }
        public ActionResult DeletedBookings()
        {
            User user = (WebPortal.Models.User)Session["Currentuser"];
            if (user.UserRole.Code == "ADM")
            {
                ViewBag.deleted = db.Appointments.Where(x => x.isDeleted == true).ToList();
            }
            else
            {
                ViewBag.deleted = db.Appointments.Where(x => x.isDeleted == true && x.UserId == user.UserId).ToList();
            }
            return View();
        }
    }

}