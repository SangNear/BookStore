using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanSach.Models;
namespace WebSiteBanSach.Controllers
{
    public class NguoiDungController : Controller
    {
        dbQLBansachDataContext data = new dbQLBansachDataContext();
        // GET: NguoiDung
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Sach()
        {
            return View(data.SACHes.ToList());
        }
    }
}