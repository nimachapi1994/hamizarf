using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using IdentitySample.Models;
using HamiZarf.Models;

namespace HamiZarf.Controllers
{
    public class HomeController : Controller
    {
        ApplicationRoleManager rolemngr
        {
            get
            {
                return HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
        }
        HamiZarfDB db = new HamiZarfDB();
        public ActionResult ContactUsConfirm(ContactU cu)
        {
            db.Sp_InsertContactUs(cu.Name, cu.Email, cu.Txt, cu.subject);
            db.SaveChanges();
            TempData["msgoksendtoadmin"] = "پیام شما با موفقیت ارسال شد";
            return RedirectToAction("ContactUs");
        }
        public ActionResult showproduct(int id)
        {
            var find = db.Products.Find(id);


            ViewBag.show = db.Products.Where(x => x.Id == find.Id).ToList();

            return View();
        }
        public ActionResult ContactUs()
        {
            return View();
        }
        public ActionResult HelpForBuy()
        {
            ViewBag.show = db.AllPages.Where(x => x.Id == 3).ToList();
            return View();
        }
        public ActionResult BackWarranty()
        {
            ViewBag.show = db.AllPages.Where(x => x.Id == 5).ToList();
            return View();
        }
        public ActionResult SaleSupport()
        {
            ViewBag.show = db.AllPages.Where(x => x.Id == 2).ToList();
            return View();
        }
        public ActionResult SendOrders()
        {
            ViewBag.show = db.AllPages.Where(x => x.Id == 4).ToList();
            return View();
        }
        public ActionResult SearchProductByChildCategory(int id)
        {

            var find = db.ChildCategories.Find(id);
            if (db.Products.Where(x => x.C_Cat_Id == find.Id) != null)
            {

                ViewBag.ShowProductSide = db.Products.Where(x => x.C_Cat_Id == find.Id).ToList().OrderByDescending(x => x.CreateDate).Take(3);


                ViewBag.ShowAllProductsByChildCategory = db.Products.Where(x => x.C_Cat_Id == find.Id).ToList().
                    OrderByDescending(X => X.CreateDate);
            }




            return View();
        }
        public ActionResult SearchProductByParentCategory(int id)
        {

            var find = db.ParentCategories.Find(id);
            if (db.Products.Where(x => x.P_Cat_Id == find.Id) != null)
            {

                ViewBag.ShowProductSide = db.Products.Where(x => x.P_Cat_Id == find.Id).ToList().OrderByDescending(x => x.CreateDate).Take(3);


                ViewBag.ShowAllProductsByCategory = db.Products.ToList().Where(x => x.P_Cat_Id == find.Id).
                    OrderByDescending(X => X.CreateDate);
            }






            return View();
        }
        public ActionResult SearchProductBySearchBox(string search, string category)
        {



            if (search == string.Empty && !string.IsNullOrEmpty(category))
            {
                ViewBag.ShowAllProductsBySearchBox = db.Products.Where(x => x.ParentCategory.Name.Contains(category)).ToList().OrderByDescending(c => c.CreateDate);
                ViewBag.ShowProductSide = db.Products.Where(x => x.ParentCategory.Name.Contains(category)).ToList().OrderByDescending(c => c.CreateDate).Take(3);



            }


            else
            {
                if (search != string.Empty && !string.IsNullOrEmpty(category))
                {
                    if (db.Products.Where(x => x.Name.Contains(search) && x.ParentCategory.Name.Contains(category)) != null)
                    {
                        ViewBag.ShowAllProductsBySearchBox = db.Products.Where(x => x.Name.Contains(search) && x.ParentCategory.Name.Contains(category)).ToList().OrderByDescending(c => c.CreateDate);
                        ViewBag.ShowProductSide = db.Products.Where(x => x.Name.Contains(search) && x.ParentCategory.Name.Contains(category)).ToList().OrderByDescending(c => c.CreateDate).Take(3);
                    }
                    else
                    {
                        if (db.Products.Where(x => x.Name.Contains(search)) != null && db.Products.Where(x => !x.ParentCategory.Name.Contains(category)) == null)
                        {
                            ViewBag.ShowAllProductsBySearchBox = db.Products.Where(x => x.Name.Contains(search)).ToList().OrderByDescending(c => c.CreateDate);
                            ViewBag.ShowProductSide = db.Products.Where(x => x.Name.Contains(search)).ToList().OrderByDescending(c => c.CreateDate).Take(3);
                        }
                    }

                }

            }



            return View();
        }
        public ActionResult ShowFullSaleProduct(int pagenumber = 0, int pagesize = 2)
        {
            ViewBag.count = db.Products.Count();
            ViewBag.pagenumber = pagenumber;
            ViewBag.pagesize = pagesize;
            ViewBag.ShowFullSaleProduct = db.Sp_sp_sp_GetAllrproductBySelectOfsetfullsale(pagenumber, pagesize).ToList();
            return View();
        }
        public ActionResult ShowNewProduct(int pagenumber = 0, int pagesize = 2)
        {
            ViewBag.count = db.Products.Count();
            ViewBag.pagenumber = pagenumber;
            ViewBag.pagesize = pagesize;
            ViewBag.ShowNewProduct = db.Sp_sp_sp_GetAllrproductBySelectOfsetnew(pagenumber, pagesize).ToList();
            return View();
        }
        public ActionResult ShowSpecialProduct(int pagenumber = 0, int pagesize = 2)
        {
            ViewBag.count = db.Products.Count();
            ViewBag.pagesize = pagesize;
            ViewBag.pagenumber = pagenumber;
            ViewBag.ShowSpecialProduct = db.Sp_sp_sp_GetAllrproductBySelectOfsetSpecial(pagenumber, pagesize).ToList();
            return View();
        }
        public ActionResult ShowContent(int id)
        {
            var find = db.Contents.Find(id);
            if (find.Id != null)
            {
                ViewBag.ShowContent = db.Contents.Where(x => x.Id == find.Id).ToArray();
            }
            ViewBag.ShowProductSide = db.Products.OrderByDescending(x => x.CreateDate).Take(3).ToList();
            return View();
        }
        public ActionResult ShowAllContents()
        {
            ViewBag.showBlog = db.Contents.ToList().OrderByDescending(x => x.DateOfShow);
            return View();
        }
        public ActionResult about()
        {
            ViewBag.show = db.AllPages.Where(x => x.Id == 1).ToList();
            return View();
        }
        public ActionResult Index()
        {

            ViewBag.showContent = db.sp_sp_showcontent().Take(5).OrderByDescending(x => x.DateOfShow).ToList();

            ViewBag.ShowAllSpecialProducts1 = db.sp_sp_showProdcuts().Where(x => x.SpecialProduct == true).OrderByDescending(X => X.CreateDate).Take(2);
            ViewBag.ShowAllNewProducts1 = db.sp_sp_showProdcuts().Where(x => x.NewProduct == true).OrderByDescending(X => X.CreateDate).Take(2);
            ViewBag.ShowAllFullSaleProducts1 = db.sp_sp_showProdcuts().Where(x => x.FullSaleProduct == true).OrderByDescending(X => X.CreateDate).Take(2);
            //.......................................................................





            //ViewBag.showprorductseletedaschildcategory = db.ParentCategories.ToList();

            //List<string> lst = new List<string>()
            //{
            //    "admin","customer"
            //};
            //lst.ForEach(x =>
            //{
            //    if (rolemngr.RoleExists(x)==false)
            //    {
            //        IdentityRole role = new IdentityRole(x);
            //        rolemngr.Create(role);
            //    }
            //});
            return View();
        }
        public ActionResult ShowChildCategoryByAjax(int id)
        {

            var s = db.ChildCategories.ToList().Select(x =>
            new { name = x.Name, parentid = x.P_Cat_Id, id = x.Id }).Where(x => x.parentid == id).ToList();



            return Json(s);
        }
    }
}