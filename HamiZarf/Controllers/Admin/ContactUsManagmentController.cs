using HamiZarf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HamiZarf.Controllers
{
    [Authorize(Roles = "admin")]
    public class ContactUsManagmentController : Controller
    {
        HamiZarfDB db = new HamiZarfDB();
        public ActionResult DeleteContactUs(int id)
        {
            var find = db.ContactUs.Find(id);
            if (find.Id!=null)
            {
                db.ContactUs.Remove(db.ContactUs.Find(find.Id));
               

            }
            db.SaveChanges();
            TempData["msgsuccess"] = "با موفقیت حذف شد";
            return RedirectToAction("ManageContactUs");
        }
        public ActionResult ManageContactUs()
        {
            ViewBag.ShowAllContacts = db.ContactUs.ToList();
            return View();
        }
    }
}