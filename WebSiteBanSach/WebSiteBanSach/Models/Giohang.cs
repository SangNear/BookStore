using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanSach.Models;
namespace WebSiteBanSach.Models
{
    public class Giohang
    {
        dbQLBansachDataContext data = new dbQLBansachDataContext();

        public int iMasach { set; get; }

        public string sTensach { set; get; }

        public string sAnhbia { set; get; }

        public Double dDongia { set; get; }

        public int iSoluong { set; get; }

        public Double dThanhtien
        {
            get { return iSoluong * dDongia; }
        }

        public  Giohang (int Masach)
        {
            iMasach = Masach;
            SACH sach = data.SACHes.Single(n => n.Masach == iMasach);
            sTensach = sach.Tensach;
            sAnhbia = sach.Anhbia;
            dDongia = double.Parse(sach.Giaban.ToString());
            iSoluong = 1;
        }

    }
}