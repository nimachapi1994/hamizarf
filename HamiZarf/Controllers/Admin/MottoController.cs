using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HamiZarf.Controllers.Admin
{
    public class MottoController : Controller
    {
        // GET: Motto
        public ActionResult showmottopage()
        { 

            return View();
        }
        public ActionResult EditeMotto(string m)
        {
            var db = new HamiZarf.Models.HamiZarfDB();
            var found = db.Mottoes.Find(1);
            found.Motto1 = m;
            db.SaveChanges();
            TempData["ok"] = "شعار وبسایت با موفقیت ذخیره شد";
            return RedirectToAction("showmottopage", "motto");
        }
    }
}