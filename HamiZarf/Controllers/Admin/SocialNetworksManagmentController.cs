using HamiZarf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HamiZarf.Controllers
{
    //[Authorize(Roles = "admin")]
    public class SocialNetworksManagmentController : Controller
    {
        HamiZarfDB db = new HamiZarfDB();
        public ActionResult AddSocialNetwork(SocialNetworksLink s)
        {
            var find = db.SocialNetworksLinks.Find(1);
            find.Facebook = s.Facebook;
            find.Twitter = s.Twitter;
            find.LinkedIn = s.LinkedIn;
            find.Gmail = s.Gmail;
            find.Instagram = s.Instagram;
            find.Telegram = s.Telegram;
           
            db.SaveChanges();
            TempData["msgSocialNeteworks"] = "اطلاعات با موفقیت ذخیره گردید";
            return RedirectToAction("SocialNetworksManagment");
        }
        public ActionResult SocialNetworksManagment()
        {
           
            ViewBag.s = db.SocialNetworksLinks.ToList();
            return View();
        }
    }
}