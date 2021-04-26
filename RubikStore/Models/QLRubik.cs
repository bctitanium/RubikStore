using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RubikStore.Models
{
    public class QLRubik
    {
        public int Id { get; set; }

        [Display(Name = "Tên: ")]
        [Required(ErrorMessage = "Bạn phải nhập tên của Rubik")]
        public string Ten { get; set; }

        [Display(Name = "Loại: ")]
        [Required(ErrorMessage = "Bạn phải nhập loại của Rubik")]
        public string Loai { get; set; }

        [Display(Name = "Mô tả: ")]
        [Required(ErrorMessage = "Bạn phải nhập mô tả của Rubik")]
        public string MoTa { get; set; }

        [Display(Name = "Hãng: ")]
        [Required(ErrorMessage = "Bạn phải nhập hãng của Rubik")]
        public string Hang { get; set; }

        [Display(Name = "Giá: ")]
        [Required(ErrorMessage = "Bạn phải nhập giá của Rubik")]
        public int Gia { get; set; }

        [Display(Name = "Hình: ")]
        [Required(ErrorMessage = "Bạn phải dẫn hình cho Rubik")]
        public string PicCover { get; set; }

        [Display(Name = "Còn lại: ")]
        [Required(ErrorMessage = "Bạn phải nhập số lượng của Rubik")]
        public int SoLgTon { get; set; }

        [Display(Name = "Ngày cập nhật: ")]
        [Required(ErrorMessage = "Bạn phải để ngày cập nhật cho Rubik")]
        public DateTime NgayCapNhat { get; set; }
    }

    class RubikList
    {
        private DBConnection db;

        public RubikList()
        {
            db = new DBConnection();
        }

        public List<QLRubik> getRubik(string Id)
        {
            string sql = "";

            if (string.IsNullOrEmpty(Id))
            {
                sql = "Select * from Rubik";
            }
            else
            {
                sql = "Select * from Rubik where Id=" + Id;
            }

            List<QLRubik> strList = new List<QLRubik>();
            SqlConnection con = db.GetConnection();
            SqlDataAdapter cmd = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            //open connection
            con.Open();
            cmd.Fill(dt);
            //close connection
            cmd.Dispose();
            con.Close();

            QLRubik strR;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strR = new QLRubik();

                strR.Id = Convert.ToInt32(dt.Rows[i]["Id"].ToString());
                strR.Ten = dt.Rows[i]["Ten"].ToString();
                strR.Loai = dt.Rows[i]["Loai"].ToString();
                strR.MoTa = dt.Rows[i]["MoTa"].ToString();
                strR.Hang = dt.Rows[i]["Hang"].ToString();
                strR.Gia = Convert.ToInt32(dt.Rows[i]["Gia"].ToString());
                strR.PicCover = dt.Rows[i]["PicCover"].ToString();
                strR.SoLgTon = Convert.ToInt32(dt.Rows[i]["SoLgTon"].ToString());
                strR.NgayCapNhat = Convert.ToDateTime(dt.Rows[i]["NgayCapNhat"].ToString());

                strList.Add(strR);
            }

            return strList;
        }
        
        public void addRubik(QLRubik strR)
        {
            string sql = "Insert into Rubik(Ten, Loai, MoTa, Hang, Gia, PicCover, SoLgTon, NgayCapNhat) " +
                "values(N'" + strR.Ten + "', N'" + strR.Loai + "', N'" + strR.MoTa + "', N'" + strR.Hang + "', N'" + strR.Gia + "', N'" + strR.PicCover + "', N'" + strR.SoLgTon + "', N'" + strR.NgayCapNhat + "')";

            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);

            //open connection
            con.Open();
            cmd.ExecuteNonQuery();
            //close connection
            cmd.Dispose();
            con.Close();
        }
        
        public void editRubik(QLRubik strR)
        {
            string sql = "Update Rubik set Ten = N'" + strR.Ten + "', Loai = N'" + strR.Loai + "', MoTa = N'" + strR.MoTa + "', Hang = N'" + strR.Hang + "', Gia = N'" + strR.Gia + "', PicCover = N'" + strR.PicCover + "', SolgTon = N'" + strR.SoLgTon + "', NgayCapNhat = N'" + strR.NgayCapNhat + "' " +
                "where Id = " + strR.Id;

            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            //open connection
            con.Open();
            cmd.ExecuteNonQuery();
            //close connection
            cmd.Dispose();
            con.Close();
        }
        
        public void delBook(QLRubik strR)
        {
            string sql = "Delete from Rubik where Id = " + strR.Id;

            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            //open connection
            con.Open();
            cmd.ExecuteNonQuery();
            //close connection
            cmd.Dispose();
            con.Close();
        }
    }
}