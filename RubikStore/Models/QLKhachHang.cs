using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RubikStore.Models
{
    public class QLKhachHang
    {
        public int MaKH { get; set; }

        [Display(Name = "Họ và Tên: ")]
        [Required(ErrorMessage = "Phải nhập họ tên!")]
        public string HoTen { get; set; }

        [Display(Name = "Tài khoản: ")]
        [Required(ErrorMessage = "Phải nhập tên tài khoản!")]
        public string TaiKhoan { get; set; }

        [Display(Name = "Mật khẩu: ")]
        [Required(ErrorMessage = "Phải nhập mật khẩu!")]
        public string MatKhau { get; set; }

        [Display(Name = "Email: ")]
        [Required(ErrorMessage = "Phải nhập email!")]
        public string Email { get; set; }

        [Display(Name = "Địa chỉ: ")]
        [Required(ErrorMessage = "Phải nhập địa chỉ!")]
        public string DiaChiKH { get; set; }

        [Display(Name = "Điện thoại: ")]
        [Required(ErrorMessage = "Phải nhập điện thoại!")]
        public string DienThoaiKH { get; set; }

        [Display(Name = "Ngày sinh: ")]
        [Required(ErrorMessage = "Phải nhập ngày sinh!")]
        public DateTime NgaySinh { get; set; }
    }
}