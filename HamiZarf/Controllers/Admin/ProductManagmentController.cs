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
    //[Authorize(Roles = "admin")]
    public class ProductManagmentController : Controller
    {
        HamiZarfDB db = new HamiZarfDB();
        public ActionResult DeleteProduct(int id)
        {
            var find = db.Products.Find(id);
            if (find.Id == null)
            {
                TempData["msgdanger"] = "محصول مورد نظر موجود نیست ";
            }
            else
            {

                db.Sp_DeleteProduct(find.Id);
                db.SaveChanges();
                TempData["msgsucess"] = "محصول مورد نظر با موفقیت حذف شد";
            }

            return RedirectToAction("ShowAllProducts");

        }
       
        public ActionResult EditProductPage()
        {
            ViewBag.showParentCategory = db.ParentCategories.ToList();
            if (Session["id"] != null)
            {
                int idd = (int)Session["id"];
                var find = db.Products.Find(idd);
                ViewBag.s = db.Products.Where(x => x.Id == find.Id).ToList();
                Session["idEdit"] = find.Id;
            }
            Session.Remove("id");
            return View();
        }
        public ActionResult EditProduct(int id)
        {

            var find = db.Products.Find(id);
            Session["id"] = find.Id;
            return RedirectToAction("EditProductPage");
        }
        public ActionResult ShowAllProducts()
        {
            ViewBag.showproduct = db.Sp_SelectAllProdductOK().OrderByDescending(x => x.CreateDate).ToList();
            ViewBag.showParentCategory = db.ParentCategories.ToList();
            ViewBag.showChildCategory = db.ChildCategories.ToList();

            return View();
        }
        public ActionResult ShowChildCategoryByJson(int id)
        {
            var find = db.ParentCategories.Find(id);
            var d = db.ChildCategories.ToList().Select(x => new { name = x.Name, p_id = x.P_Cat_Id, id = x.Id });
            var s = d.Where(x => x.p_id == find.Id);


            return Json(s.ToList());
        }
        public ActionResult InsertProduct()
        {
            ViewBag.showParentCategory = db.ParentCategories.ToList();

            return View();
        }
        public ActionResult InsertProductConfirm(Product p, int p_id, int c_id, HttpPostedFileBase img, 
            string IsAvailable,string SpecialProduct,string FullSaleProduct, string NewProduct)
        {
            byte[] b = { };
         
            if (img==null)
            {
                 b =
                    System.IO.File.ReadAllBytes(Server.MapPath("~/Template/img/ProductNotHaveImage/d.jpg"));                 
          
            }
           else
            {
                if (img != null)
                {

                    try
                    {
                        b = new byte[img.ContentLength];
                        img.InputStream.Read(b, 0, b.Length);
                    }
                    catch (BadImageFormatException ex)
                    {

                    }



                }
            }
          var d=  db.Products.Add(new Product
            {
                Name = p.Name,
                Txt = p.Txt,
                Unit = p.Unit,
                IsAvailable = p.IsAvailable,
                P_Cat_Id = p_id,
                C_Cat_Id = c_id,
                pic = b,
                FullSaleProduct=p.FullSaleProduct,
                SpecialProduct=p.SpecialProduct,
                NewProduct=p.NewProduct,
                CreateDate = DateTime.Now
            });
            db.SaveChanges();



            var find = db.Products.Find(d.Id);


            if (IsAvailable != null)
            {
              
                d.IsAvailable = true;
                db.SaveChanges();
            }
            else if (IsAvailable == null)
            {
            
                d.IsAvailable = false;
                db.SaveChanges();
            }

            if (NewProduct!=null)
            {
               
                find.NewProduct = true;
                db.SaveChanges();
            }
            if (SpecialProduct != null)
            {
                find.SpecialProduct = true;
                db.SaveChanges();
            }
            if (FullSaleProduct != null)
            {
                find.FullSaleProduct = true;
                db.SaveChanges();
            }




            return RedirectToAction("insertproduct");
        }
        public ActionResult EditProductConfirm(Product p, int p_id, int c_id, HttpPostedFileBase img, string IsAvailable
            , string SpecialProduct, string FullSaleProduct, string NewProduct)
        {

            var editid = (int)Session["idEdit"];
            var find = db.Products.Find(editid);
            byte[] b = { };
            if (img==null)
            {
               
                b = find.pic;
            }
            else
            {
           
                
                    b = new byte[img.ContentLength];
                    img.InputStream.Read(b, 0, b.Length);
                
            }

                   

           

            find.Name = p.Name;
            find.Txt = p.Txt;
            find.Unit = p.Unit;
            find.IsAvailable = p.IsAvailable;
            find.P_Cat_Id = p_id;
            find.C_Cat_Id = c_id;
            find.pic = b;
           
            db.SaveChanges();
            var finda = db.Products.Find(find.Id);
            if (IsAvailable != null)
            {
                var findd = db.Products.Find(find.Id);
                findd.IsAvailable = true;
                db.SaveChanges();
            }
            if (IsAvailable == null)
            {
             
                finda.IsAvailable = false;
                db.SaveChanges();
            }

            //-------------------------

            if (NewProduct != null)
            {

                finda.NewProduct = true;
                db.SaveChanges();
            }else
            {
                finda.NewProduct = false;
                db.SaveChanges();
            }
            if (SpecialProduct != null)
            {
                finda.SpecialProduct = true;
                db.SaveChanges();
            }else
            {
                finda.SpecialProduct = false;
                db.SaveChanges();
            }
            if (FullSaleProduct != null)
            {
                finda.FullSaleProduct = true;
                db.SaveChanges();
            }else
            {
                finda.FullSaleProduct =false;
                db.SaveChanges();
            }
            Session.Remove("idEdit");
            return RedirectToAction("insertproduct");
        }
    }
}
