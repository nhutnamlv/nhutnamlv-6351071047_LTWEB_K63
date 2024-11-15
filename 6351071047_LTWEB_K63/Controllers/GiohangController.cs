using _6351071047_LTWEB_K63.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _6351071047_LTWEB_K63.Controllers
{
    public class GiohangController : Controller
    {
        QLBANSACHEntities1 data = new QLBANSACHEntities1();
        // GET: Giohang
        public ActionResult Index()
        {
            return View();
        }
        public List<Giohang> Laygiohang()
        {
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang == null)
            {
                // Nếu giỏ hàng chưa tồn tại thì khởi tạo listGiohang
                lstGiohang = new List<Giohang>();
                Session["Giohang"] = lstGiohang;
            }
            return lstGiohang;
        }
        // Thêm hàng vào giỏ
        public ActionResult ThemGiohang(int iMasach, string strURL)
        {
            // Lấy ra Session giỏ hàng
            List<Giohang> lstGiohang = Laygiohang();

            // Kiểm tra sách này tồn tại trong Session["Giohang"] chưa
            Giohang sanpham = lstGiohang.Find(n => n.iMasach == iMasach);
            if (sanpham == null)
            {
                // Nếu chưa có, thêm sản phẩm mới vào giỏ
                sanpham = new Giohang(iMasach);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                // Nếu đã có, tăng số lượng
                sanpham.iSoluong++;
                return Redirect(strURL);
            }
        }

        // Tính tổng số lượng
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                iTongSoLuong = lstGiohang.Sum(n => n.iSoluong);
            }
            return iTongSoLuong;
        }

        // Tính tổng tiền
        private double TongTien()
        {
            double iTongTien = 0;
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                iTongTien = lstGiohang.Sum(n => n.dThanhtien);
            }
            return iTongTien;
        }


        public ActionResult GioHang()
        {
            List<Giohang> lstGioHang = Laygiohang();
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }


        public ActionResult SuaGioHang(int iMasach, int soLuongMoi)
        {
            List<Giohang> lstGioHang = Laygiohang();

            // Tìm sản phẩm trong giỏ hàng
            Giohang sanpham = lstGioHang.Find(n => n.iMasach == iMasach);

            if (sanpham != null)
            {
                // Cập nhật số lượng sản phẩm
                sanpham.iSoluong = soLuongMoi;



                // Lưu lại giỏ hàng vào session
                Session["GioHang"] = lstGioHang;
            }

            // Sau khi sửa, chuyển hướng lại giỏ hàng
            return RedirectToAction("GioHang");
        }

        public ActionResult ChiTietGioHang(int iMasach)
        {
            // Tìm sản phẩm trong giỏ hàng
            List<Giohang> lstGioHang = Laygiohang();
            Giohang sanpham = lstGioHang.Find(n => n.iMasach == iMasach);

            if (sanpham != null)
            {
                return View(sanpham);  // Trả về view chi tiết của sản phẩm
            }

            // Nếu sản phẩm không có trong giỏ hàng, chuyển về giỏ hàng
            return RedirectToAction("GioHang");
        }

        public ActionResult XoaGioHang(int iMasach)
        {
            // Lấy giỏ hàng từ session
            List<Giohang> lstGioHang = Laygiohang();

            // Tìm sản phẩm trong giỏ hàng
            Giohang sanpham = lstGioHang.Find(n => n.iMasach == iMasach);

            if (sanpham != null)
            {
                lstGioHang.Remove(sanpham);
                Session["GioHang"] = lstGioHang;
            }

            // Sau khi xóa, quay lại trang giỏ hàng
            return RedirectToAction("GioHang");
        }

        public ActionResult XoaTatCaGioHang()
        {
            // Xóa giỏ hàng khỏi session
            Session["GioHang"] = null;

            // Quay lại trang giỏ hàng hoặc trang chính
            return RedirectToAction("Index", "Home");
        }


        public ActionResult GioHangpartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }

        [HttpGet]
        public ActionResult Dathang()
        {
            if (Session["User"] == null || Session["User"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            List<Giohang> lstGiohang = Laygiohang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();

            return View(lstGiohang);
        }

        public ActionResult Dathang(FormCollection collection)
        {
            DONDATHANG ddh = new DONDATHANG();
            KHACHHANG kh = (KHACHHANG)Session["User"];
            List<Giohang> gh = Laygiohang();
            ddh.MaKH = kh.MaKH;
            ddh.Ngaydat = DateTime.Now;
            var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["Ngaygiao"]);
            ddh.Ngaygiao = DateTime.Parse(ngaygiao);
            ddh.Tinhtranggiaohang = false;
            ddh.Dathanhtoan = false;
            data.DONDATHANGs.Add(ddh);
            data.SaveChanges();

            foreach (Giohang g in gh)
            {
                CHITIETDONTHANG ctdh = new CHITIETDONTHANG();
                ctdh.MaDonHang = ddh.MaDonHang;
                ctdh.Masach = g.iMasach;
                ctdh.Soluong = g.iSoluong;
                ctdh.Dongia = (decimal)g.dDongia;
                data.CHITIETDONTHANGs.Add(ctdh);

            }
            data.SaveChanges();
            Session["Giohang"] = null;
            return RedirectToAction("Xacnhandonhang", "GioHang");
        }

        public ActionResult Xacnhandonhang()
        {
            return View();
        }
    }
}