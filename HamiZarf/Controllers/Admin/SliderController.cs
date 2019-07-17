using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HamiZarf.Controllers.Admin
{
    public class SliderController : Controller
    {
        HamiZarf.Models.HamiZarfDB db = new Models.HamiZarfDB();
        public ActionResult Slider()
        {
           
            return View();
        }
        public ActionResult slidermanagment()
        {
            ViewBag.showallslider = db.Sliders.ToList().OrderByDescending(x=>x.caption);
            return View();
        }
      public ActionResult InsertSliderConfirm(HttpPostedFileBase img, string caption)
        {
            byte[] b = new byte[img.ContentLength];
            img.InputStream.Read(b, 0, b.Length);
            db.Sliders.Add(new Models.Slider { img = b ,caption=caption});
            db.SaveChanges();
            return RedirectToAction("slider");
        }
        public ActionResult EditSlider(int id) 
        {
            var find = db.Sliders.Find(id);
            ViewBag.ShowEdit = db.Sliders.Where(x => x.Id == find.Id).ToList();
            Session["id"] = id;
            return View();
        }
        public ActionResult EditSliderConfirm(HttpPostedFileBase img,string caption)
        {
            byte[] b= { };
            int id = (int)Session["id"];
            var find = db.Sliders.Find(id);
            if (img==null)
            {
                b = find.img;
            }
            else
            {
                b = new byte[img.ContentLength];
                img.InputStream.Read(b, 0, b.Length);
            }

            find.caption = caption;
            find.img = b;
            db.SaveChanges();
            return RedirectToAction("slider");
        }
        public ActionResult DeleteSlider(int id)
        {
            db.Sliders.Remove(db.Sliders.Find(id));
            db.SaveChanges();
            return RedirectToAction("slidermanagment");
        }
    }
}