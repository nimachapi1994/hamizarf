using HamiZarf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HamiZarf.Controllers
{
    //[Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {
        HamiZarfDB db = new HamiZarfDB();
        /* managment for category page*/

        public ActionResult ManagmentCategoryView(int id = -1, int idd = -1)
        {
            ViewBag.dd = id;
            ViewBag.r = idd;
            ViewBag.d = db.ParentCategories.ToList();
            return View();
        }
        /* Action*/
        public ActionResult GetParentCategoryAndAddToDb(string name)
        {
            db.Sp_InsertParentCategory(name);
            db.SaveChanges();
            return RedirectToAction("ManagmentCategoryView","Category" );
        }
        public ActionResult GetChildCategoryAndAddToDb(List<string> name, int id)
        {
            foreach (var item in name)
            {
                db.Sp_InsertChildCategory(item, id);

            }

            db.SaveChanges();
            return RedirectToAction("ManagmentCategoryView", "Category");
        }
        public ActionResult ShowCategoryByJsonId(int id)
        {
            var find = db.ParentCategories.Find(id);
            var s = db.ChildCategories.ToList().Select(x =>
            new { name = x.Name, parentid = x.P_Cat_Id });
            var d = s.Where(x => x.parentid == find.Id);//replace id ,,,find.id
     

            return Json(d.ToList());
        }
        public ActionResult UpdateParentCategory(ChildCategory p)
        {
            var found = db.ParentCategories.Find(p.Id);
            if (found.Id != null)
            {
                db.Sp_UpdateParentCategory(found.Id, p.Name);
            }
            db.SaveChanges();
            TempData["msg"] = "اطلاعات با موفقیت آپدیت شد";


            
            return RedirectToAction("ManagmentCategoryView", "Category", new { id = -1 });
        }
        public ActionResult DeleteChildCategory(ChildCategory ch)
        {

            
            db.Products.RemoveRange(db.Products.Where(x => x.C_Cat_Id == ch.Id));
            db.ChildCategories.Remove(db.ChildCategories.Find(ch.Id));
            db.SaveChanges();
            TempData["msg"] = "اطلاعات با موفقیت آپدیت شد";
            return RedirectToAction("ManagmentCategoryView", "Category", new { id = -1 });
        }
        public ActionResult UpdateChildCategory(ChildCategory ch)
        {
            var d = db.ChildCategories.Find(ch.Id);
            d.Name = ch.Name;
            db.SaveChanges();


            TempData["msg"] = "اطلاعات با موفقیت آپدیت شد";
            return RedirectToAction("ManagmentCategoryView", "Category", new { id = -1 });
        }
        public ActionResult DeleteParentCategory(ParentCategory cp)
        {
            db.Products.RemoveRange(db.Products.Where(x => x.P_Cat_Id == cp.Id));
            db.ChildCategories.RemoveRange(db.ChildCategories.Where(x => x.P_Cat_Id == cp.Id));
            db.ParentCategories.Remove(db.ParentCategories.Find(cp.Id));
            
            db.SaveChanges();
            TempData["msg"] = "دسته اصلی با موفقیت حذف شد";
            return RedirectToAction("ManagmentCategoryView", "Category", new { id = -1 });
        }

    }
}