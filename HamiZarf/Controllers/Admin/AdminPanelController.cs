using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HamiZarf.Controllers.Admin
{
    public class AdminPanelController : Controller
    {
        [Authorize(Roles ="admin")]
        public ActionResult ControlPanel()
        {
            return View();
        }
    }
}