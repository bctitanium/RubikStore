using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RubikStore.Models
{
    public class Admin
    {
        [Display(Name = "Username: ")]
        [Required(ErrorMessage = "Username không được để trống!")]
        public string UserAdmin { get; set; }

        [Display(Name = "Password: ")]
        [Required(ErrorMessage = "Password không được để trống!")]
        public string PassAdmin { get; set; }

        [Display(Name = "Name: ")]
        [Required(ErrorMessage = "Tên không được để trống!")]
        public string Hoten { get; set; }
    }
}