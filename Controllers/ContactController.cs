using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPortal.Controllers

{
    public class ContactController : Controller 

    {
        public string Info(string name)
        {
            return name;
        }
         public ActionResult Aboutus()
        {
            return View();
        }
    }
}
