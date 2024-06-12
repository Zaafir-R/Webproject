using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
    }
}
