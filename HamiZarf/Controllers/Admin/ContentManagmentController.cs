using HamiZarf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HamiZarf.Controllers
{
    //[Authorize(Roles = "admin")]
    public class ContentManagmentController : Controller
    {
        HamiZarfDB db = new HamiZarfDB();
        public ActionResult ContentManagmentContolPanel()
        {
            return View();
        }
       public ActionResult EditContentConfirm(Content c,HttpPostedFileBase img)
        {
            int id = (int)Session["ContentId"];
            var find = db.Contents.Find(id);
            byte[] b = { };
            if (find.Id!=null)
            {
              
                if (img == null)
                {
                    b = System.IO.File.ReadAllBytes(Server.MapPath("~/Template/img/ProductNotHaveImage/d.jpg"));
                }
                else
                {
                    b = b = new byte[img.ContentLength];
                    img.InputStream.Read(b, 0, b.Length);
                }
                find.Subject = c.Subject;
                find.Txt = c.Txt;
                find.MoreTxt = c.MoreTxt;
           
               
                find.Pic = b;
                db.SaveChanges();
            }
            Session.Remove("ContentId");
            return RedirectToAction("ShowAllContentManagment");
        }
        public ActionResult EditContent(int id)
        {
           
            var find = db.Contents.Find(id);
            if (find.Id!=null)
            {
                ViewBag.ShowContentData = db.Contents.Where(c => c.Id == find.Id).ToList();
                Session["ContentId"] = find.Id;
            }
           
            return View();
        }
        public ActionResult DeleteContent(int id)
        {
            var find = db.Contents.Find(id);
            if (find.Id==null)
            {
                TempData["msgdanger"] = "مقاله مورد نظر در سیستم وجود تدارد";
                return RedirectToAction("ShowAllContentManagment");
            }
            else
            {
                if (find.Id!=null)
                {
                    db.Contents.Remove(db.Contents.Find(id));
                    db.SaveChanges();
                    TempData["msgsuccess"] = "مقاله مورد نظر با موفقیت حذف شد";
                    return RedirectToAction("ShowAllContentManagment");
                }
            }
            return RedirectToAction("ShowAllContentManagment");
        }
        public ActionResult ShowAllContentManagment()
        {
            return View(db.Contents.ToList());
        }
        public ActionResult InsertContentConfirm(Content c,HttpPostedFileBase img)
        {
            byte[] b= { };
            if (img.ContentLength==null)
            {
                b = System.IO.File.ReadAllBytes(Server.MapPath("~/Template/img/ProductNotHaveImage/d.jpg"));
            }
            else
            {
                b = b = new byte[img.ContentLength];
                img.InputStream.Read(b, 0, b.Length);
            }
           
            db.Contents.Add(new Models.Content { Subject = c.Subject,
                Txt = c.Txt, MoreTxt = HttpUtility.HtmlDecode(c.MoreTxt), DateOfShow = DateTime.Now, Pic = b });
            db.SaveChanges();
            TempData["msgsucess"] = "مقاله با موفقیت ذخیره شد";
            return RedirectToAction("ContentManagmentContolPanel");
        }

        public void SendRequestImageAndBackImageUrl(HttpPostedFileBase Upload, string CKEditorFuncNUM)
        {
            string[] ExStr = { "image/jpg", "image/jpeg", "image/png" };

            if (!ExStr.Contains(Upload.ContentType))
            {
                string sc1 = @"<script>window.parent.CKEDITOR.tools.callFuncation(" + CKEditorFuncNUM + ",\"" + "فرمت فایل اشتباه است" + "\");<script>";
                Response.End();

                return;
            }
            if (Upload.ContentLength> 1048576)
            {
                string sc1 = @"<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNUM + ",\"" + "حجم فایل باید زیر 1 مگابایت باشد " + "\");</script>";
                Response.Write(sc1);

                Response.End();
                return;
            }
            string FileName = Upload.FileName;
            string Address = Server.MapPath("~/Uploads/") + FileName;

            Upload.SaveAs(Address);

            string Url = "http://shop.cheshmebazar.ir/Uploads/" + FileName;

            string sc = @"<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNUM + ",\"" + Url + "\");</script>";
            Response.Write(sc);

            Response.End();
        }

    }
}