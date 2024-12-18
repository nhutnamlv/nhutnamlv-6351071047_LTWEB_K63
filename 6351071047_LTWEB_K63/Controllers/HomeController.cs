﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _6351071047_LTWEB_K63.Models;
<<<<<<< HEAD
using PagedList;
using PagedList.Mvc;
=======
>>>>>>> 4cfd6914c80ba29ef1d64b8d07152bf623b3943b

namespace _6351071047_LTWEB_K63.Controllers
{
    public class HomeController : Controller
    {
<<<<<<< HEAD
        private QLBANSACHEntities1 data = new QLBANSACHEntities1();
=======
        private QLBANSACHEntities data = new QLBANSACHEntities();
>>>>>>> 4cfd6914c80ba29ef1d64b8d07152bf623b3943b

        private List<SACH> Laysachmoi(int count)
        {
            // Sử dụng "SACHes" thay vì "SACH"
            return data.SACHes.OrderByDescending(s => s.Ngaycapnhat).Take(count).ToList();
        }
<<<<<<< HEAD
        public ActionResult Index(int ? page)
        {
            int pageSize = 5;
            int  pageNum=(page ?? 1);
            // Lấy 5 quyển sách mới nhất
            var sachmoi = Laysachmoi(4);
            return View(sachmoi.ToPagedList(pageNum,pageSize));
=======
        public ActionResult Index()
        {
            // Lấy 5 quyển sách mới nhất
            var sachmoi = Laysachmoi(4);
            return View(sachmoi);
>>>>>>> 4cfd6914c80ba29ef1d64b8d07152bf623b3943b

        }
        public ActionResult Chude()
        {
            var chude = from cd in data.CHUDEs select cd;
            return PartialView(chude);
        }
        public ActionResult Nhaxuatban()
        {
            var nhaxuatban = from cd in data.NHAXUATBANs select cd; // Lấy danh sách nhà xuất bản từ cơ sở dữ liệu
            return PartialView(nhaxuatban); // Trả về Partial View
        }
        public ActionResult SPTheoChude(int id)
        {
            var sach = from s in data.SACHes
                       where s.MaCD == id // Giả sử MaCD là ID của chủ đề
                       select s;
            return View(sach.ToList()); // Chuyển danh sách sách đến View
        }
        public ActionResult SPTheoNXB(int id)
        {
            var sach = from s in data.SACHes
                       where s.MaNXB == id // Mã nhà xuất bản
                       select s;

            return View(sach.ToList()); // Chuyển danh sách sách đến View
        }
        public ActionResult Details(int id)
        {
            var sach = data.SACHes.FirstOrDefault(s => s.Masach == id); // Lấy thông tin sản phẩm theo ID
            if (sach == null)
            {
                return HttpNotFound(); // Nếu không tìm thấy sản phẩm
            }
            return View(sach); // Trả về view chi tiết sản phẩm
        }






    }
}