using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HamiZarf.Models;

namespace HamiZarf.Controllers.Admin
{
    public class ManageContentPagesController : Controller
    {
        // GET: ManageContentPages
        /*modoriate safahate darbare ma tamas ba ma safheye moshahehe on dar home mibashad*/
        HamiZarfDB db = new HamiZarfDB();
        public ActionResult EditPagesConfirm(AllPage alp)
        {
            int id = (int)Session["id"];
            var find = db.AllPages.Find(id);
            if (db.AllPages.Where(x=>x.Id==find.Id)!=null)
            {
                find.StrOfAllPage = HttpUtility.HtmlDecode(alp.StrOfAllPage);
              
            }
            db.SaveChanges();
            Session.Remove("id");
            return RedirectToAction("SelectPageType");
        }
        public ActionResult Editpages(int id)
        {
            Session["id"] = id;
            var find = db.AllPages.Find(id);
            ViewBag.show = db.AllPages.Where(x => x.Id == find.Id).ToList();
            return View();
        }
        public ActionResult SelectPageType()
        {
            return View();
        }
    }
}