using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanSach.Models;

namespace WebSiteBanSach.Controllers
{
    public class LayoutController : Controller
    {
        dbQLBansachDataContext data = new dbQLBansachDataContext();
        // GET: Layout
        private List<SACH> Laysachmoi(int count)
        {
            return data.SACHes.OrderByDescending(a => a.Ngaycapnhat).Take(count).ToList();
        }
        public ActionResult Tieuthuyet()
        
          
        {
            return View(data.SACHes.ToList());

        }
    
        public ActionResult Index()
        {




            var sachmoi = Laysachmoi(13);
            return View(sachmoi);


        }
        public ActionResult Details (int id)
        {
            var sach = from s in data.SACHes
                       where s.Masach == id
                       select s;
            return View(sach.Single());
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
                KHACHHANG kh = data.KHACHHANGs.SingleOrDefault(n => n.Taikhoan == tendn && n.Matkhau == matkhau);
                if (kh != null)
                {
                    ViewBag.Thongbao = "Đăng nhập thành công";
                    Session["Taikhoan"] = kh;


                    return RedirectToAction("Index", "Layout", "Taikhoan");
                }
                else

                    ViewBag.thongbao = "Tên đăng nhập hoặc mật khẩu không chính xác";

            }
            return View();
        }
    }
}