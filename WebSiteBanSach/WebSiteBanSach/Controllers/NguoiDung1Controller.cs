using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanSach.Models;
namespace WebSiteBanSach.Controllers
{
    public class NguoiDung1Controller : Controller
    {
        dbQLBansachDataContext data = new dbQLBansachDataContext();
        // GET: NguoiDung1
        public ActionResult Index()
        {
            return View();
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
    }
}