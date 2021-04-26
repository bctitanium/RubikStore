using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RubikStore.Models
{
    public class Giohang
    {
        RubikStoreDataContext data = new RubikStoreDataContext();

        public int Id { get; set; }

        [Display(Name = "Tên")]
        public string Ten { get; set; }

        [Display(Name = "Loại")]
        public string Loai { get; set; }

        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }

        [Display(Name = "Hãng")]
        public string Hang { get; set; }

        [Display(Name = "Giá")]
        public Double Gia { get; set; }

        [Display(Name = "Hình")]
        public string PicCover { get; set; }

        [Display(Name = "Số lượng")]
        public int iSoLg { get; set; }

        [Display(Name = "Thành tiền")]
        public Double dThanhtien
        {
            get { return iSoLg * Gia; }
        }

        public Giohang(int id)
        {
            Id = id;

            Rubik rubik = data.Rubiks.Single(n => n.Id == Id);
            
            Ten = rubik.Ten;
            Loai = rubik.Loai;
            MoTa = rubik.MoTa;
            Hang = rubik.Hang;
            Gia = double.Parse(rubik.Gia.ToString());
            PicCover = rubik.PicCover;
            iSoLg = 1;
        }
    }
}