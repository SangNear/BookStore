using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanSach.Models;

using System.Web.UI;

using System.IO;
namespace WebSiteBanSach.Controllers
{
    public class AdminController : Controller
    {
        dbQLBansachDataContext data = new dbQLBansachDataContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Sach()
        {
            return View(data.SACHes.ToList());

        }
        [HttpGet]
        public ActionResult Dangnhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangnhap(FormCollection collection)
        {
            var tendn = collection["Email"];
            var matkhau = collection["Password"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Chưa nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Chưa nhập mật khẩu";
            }
            else
            {
                Admin ad = data.Admins.SingleOrDefault(n => n.UserAdmin == tendn && n.PassAdmin == matkhau);
                if (ad != null)
                {
                    Session["Admin"] = ad;


                    return RedirectToAction("Sach", "Admin");
                }
                else

                    ViewBag.thongbao = "Tên đăng nhập hoặc mật khẩu không chính xác";

            }
            return View();
        }
        [HttpGet]
        public ActionResult Xoasp(int id)
        {
            SACH sach = data.SACHes.SingleOrDefault(n => n.Masach == id);
            ViewBag.Masach = sach.Masach;
            if (sach == null)
            {
                Response.StatusCode = 404;
                ViewBag.Thongbao = "Đã xóa";
                return null;
            }
            return View(sach);
        }
        [HttpPost, ActionName("Xoasp")]
        public ActionResult Xacnhanxoa(int id)
        {
            SACH sach = data.SACHes.SingleOrDefault(n => n.Masach == id);
            ViewBag.Masach = sach.Masach;
            if (sach == null)
            {
                Response.StatusCode = 404;
                ViewBag.Thongbao = "Đã xóa";
                return null;
            }
            data.SACHes.DeleteOnSubmit(sach);
            data.SubmitChanges();
            return RedirectToAction("Sach");
        }

        [HttpGet]
        public ActionResult Suasp(int id)
        {
            SACH sach = data.SACHes.SingleOrDefault(n => n.Masach == id);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(sach);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Suasp(SACH sach, HttpPostedFileBase fileUpload)
        {
            ViewBag.MaCD = new SelectList(data.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe", sach.MaCD);
            if (fileUpload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();

            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);

                    var path = Path.Combine(Server.MapPath("~/image"), fileName);

                    if (System.IO.File.Exists(path))
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    else
                    {

                        fileUpload.SaveAs(path);
                    }
                    sach.Anhbia = fileName;
                    UpdateModel(sach);
                    data.SubmitChanges();
                }
                return RedirectToAction("Sach");
            }
        }

        [HttpGet]
        public ActionResult Themmoisp()
        {
            ViewBag.MaCD = new SelectList(data.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe");

            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Themmoisp(SACH sach, HttpPostedFileBase fileupload)
        {
            ViewBag.MaCD = new SelectList(data.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe");
            ViewBag.MaNXB = new SelectList(data.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            if (fileupload == null)
            {
              ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileupload.FileName);

                    var path = Path.Combine(Server.MapPath("~/image"), fileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        fileupload.SaveAs(path);
                    }
                    sach.Anhbia = fileName;
                    data.SACHes.InsertOnSubmit(sach);
                    data.SubmitChanges();
                }
                return RedirectToAction("Sach");
            }
        }
    }
    
}