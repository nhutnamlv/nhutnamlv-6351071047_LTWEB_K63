<<<<<<< HEAD
﻿using _6351071047_LTWEB_K63.Models;
using System;
=======
﻿using System;
>>>>>>> 4cfd6914c80ba29ef1d64b8d07152bf623b3943b
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _6351071047_LTWEB_K63.Controllers
{
<<<<<<< HEAD
    
    public class NguoidungController : Controller
    {
        // GET: Nguoidung
        QLBANSACHEntities1 db = new QLBANSACHEntities1();
=======
    public class NguoidungController : Controller
    {
        // GET: Nguoidung
>>>>>>> 4cfd6914c80ba29ef1d64b8d07152bf623b3943b
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Dangky()
        {
            return View();
        }
<<<<<<< HEAD
        [HttpPost]
        public ActionResult Dangky(FormCollection collection, KHACHHANG kh)
        {
            var hoten = collection["HotenKH"];
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            var matkhaunhaplai = collection["Matkhaunhaplai"];
            var diachi = collection["Diachi"];
            var email = collection["Email"];
            var dienthoai = collection["Dienthoai"];
            var ngaysinh = String.Format("{0:MM/dd/yyyy}", collection["Ngaysinh"]);

            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Họ tên khách hàng không được để trống";
            }
            else if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi2"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "Phải nhập mật khẩu";
            }
            else if (String.IsNullOrEmpty(matkhaunhaplai))
            {
                ViewData["Loi4"] = "Phải nhập lại mật khẩu";
            }
            if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi5"] = "Email không được để trống";
            }
            if (String.IsNullOrEmpty(dienthoai))
            {
                ViewData["Error6"] = "Phải nhập số điện thoại";
            }

            // Kiểm tra trùng lặp tài khoản và email
            var existingAccount = db.KHACHHANGs.FirstOrDefault(k => k.Taikhoan == tendn);
            if (existingAccount != null)
            {
                ViewData["Loi2"] = "Tên đăng nhập đã tồn tại";
                return this.Dangky();
            }
            var existingEmail = db.KHACHHANGs.FirstOrDefault(k => k.Email == email);
            if (existingEmail != null)
            {
                ViewData["Loi5"] = "Email đã tồn tại";
                return this.Dangky();
            }

            DateTime parsedDate;
            if (!DateTime.TryParse(ngaysinh, out parsedDate))
            {
                ViewData["Error7"] = "Ngày sinh không hợp lệ";
                return this.Dangky();
            }

            kh.HoTen = hoten;
            kh.Taikhoan = tendn;
            kh.Matkhau = matkhau; // Mã hóa mật khẩu trước khi lưu (nếu cần)
            kh.Email = email;
            kh.DiachiKH = diachi;
            kh.DienthoaiKH = dienthoai;
            kh.Ngaysinh = parsedDate;

            db.KHACHHANGs.Add(kh);
            db.SaveChanges();
            return RedirectToAction("Dangky");
        }

        [HttpGet]
        public ActionResult Dangnhap()
        {
            return View();
        }
        public ActionResult Dangnhap(FormCollection collection)
        {
            // Gán các giá trị người dùng nhập liệu cho các biến
            var tendn = collection["TenDN"];
            var matkhau = collection["MatKhau"];

            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                // Gán giá trị cho đối tượng được tạo mới (kh)
                KHACHHANG kh = db.KHACHHANGs.SingleOrDefault(n => n.Taikhoan == tendn && n.Matkhau == matkhau);
                if (kh != null)
                {
                    ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                    Session["Taikhoan"] = kh;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }
            return View();
        }

=======
>>>>>>> 4cfd6914c80ba29ef1d64b8d07152bf623b3943b
    }
}