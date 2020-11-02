using BTLHQTCSDL2.Datasource;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTLHQTCSDL2
{
    public partial class Form1 : Form
    {
        //Kết nối Cơ sở dữ liệu: 
        private SqlCommand cmd = null;
        //private SqlDataReader data = null;
        private SqlDataAdapter adapter = null;
        private DataTable table = null;
        private string query = null;

        //  Init UI
        public Form1()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            Color color = System.Drawing.ColorTranslator.FromHtml("#92817a");
            this.BackColor = color;

            this.initUI();
            this.openConnection();
            this.renderData();
        }
        #region Connection manager
        void openConnection()
        {
            Config.Conn = new SqlConnection(Config.CHUOIKETNOI_Main);
            Config.Conn.Open();
        }

        void closeConnection()
        {
            if (Config.Conn != null)
            {
                Config.Conn.Close();
            }
        }
        #endregion

        #region Data Management
        void renderData()
        {
            this.hoadon_grid.DataSource = null;
            this.sinhvien_grid.DataSource = null;
            this.phong_grid.DataSource = null;
            this.khunha_grid.DataSource = null;
            this.hoadon_grid.DataSource = null;
            this.nhanvien_grid.DataSource = null;


            this.Sinhvien_renderData();
            this.Phong_renderData();
            this.Hoadon_renderData();
            this.Nhanvien_renderData();
            this.Hopdong_renderData();
            this.Khunha_renderData();
        }

        void clearData()
        {
            this.hoadon_txt_sohoadon.Text = "";
            this.hoadon_txt_maphong.Text = "";
            this.hoadon_txt_manhanvien.Text = "";
            this.hoadon_txt_chisodiencuoi.Text = "";
            this.hoadon_txt_chisodiendau.Text = "";
            this.hoadon_txt_chisonuocdau.Text = "";
            this.hoadon_txt_chisonuoccuoi.Text = "";

            this.hopdong_txt_mahopdong.Text = "";
            this.hopdong_txt_maphong.Text = "";
            this.hopdong_txt_manhanvien.Text = "";

            this.khunha_txt_makhunha.Text = "";
            this.khunha_txt_tenkhunha.Text = "";
            this.khunha_txt_mota.Text = "";

            this.phong_txt_makhunha.Text = "";
            this.phong_txt_maphong.Text = "";
            this.phong_txt_loaiphong.Text = "";
            this.phong_txt_sogiuong.Text = "";
            this.phong_txt_mota.Text = "";

            this.sinhvien_txt_masinhvien.Text = "";
            this.sinhvien_txt_maphong.Text = "";
            this.sinhvien_txt_hoten.Text = "";
            this.sinhvien_txt_malop.Text = "";
            this.sinhvien_txt_sodienthoai.Text = "";
            this.sinhvien_txt_diachi.Text = "";

            this.nhanvien_txt_manhanvien.Text = "";
            this.nhanvien_txt_tennhanvien.Text = "";
            this.nhanvien_txt_email.Text = "";
            this.nhanvien_txt_diachi.Text = "";
            this.nhanvien_txt_dienthoai.Text = "";
            this.nhanvien_txt_chucdanh.Text = "";


        }

        #endregion

        #region SINHVIEN Managerment
        private void sinhvien_btn_them_Click(object sender, EventArgs e)
        {
            if (this.Sinhvien_validate())
            {
                string maSV = this.sinhvien_txt_masinhvien.Text.ToString();
                string maLop = this.sinhvien_txt_malop.Text.ToString();
                string maPhong = this.sinhvien_txt_maphong.Text.ToString();
                string hoten = this.sinhvien_txt_hoten.Text.ToString();

                string dienthoai = this.sinhvien_txt_sodienthoai.Text.ToString();
                string ngaysinh = this.sinhvien_time_ngaysinh.Value.ToShortDateString().ToString();
                string gioitinh = (this.sinhvien_rad_nam.Checked == true) ? "Nam" : "Nữ";
                string diachi = this.sinhvien_txt_diachi.Text.ToString();

                this.query = $"INSERT INTO SINHVIEN VALUES(N'{maSV}', N'{maPhong}', N'{maLop}', N'{hoten}',N'{ngaysinh}',N'{diachi}', N'{gioitinh}',N'{dienthoai}');";
                this.cmd = new SqlCommand(this.query, Config.Conn);

                this.cmd.ExecuteNonQuery();
                //MessageBox.Show(this.sinhvien_time_ngaysinh.Value.ToShortDateString().ToString(), "Nogthing");
            }
            this.clearData();
            this.renderData();
        }
        void Sinhvien_renderData()
        {
            this.query = "SELECT * FROM VIEW_danhsachsinhvien";
            this.adapter = new SqlDataAdapter(this.query, Config.Conn);
            this.table = new DataTable();
            this.adapter.Fill(this.table);

            this.sinhvien_grid.DataSource = this.table;

        }

        private void sinhvien_grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = sinhvien_grid.Rows[e.RowIndex];

            this.sinhvien_txt_masinhvien.Text = row.Cells[0].Value.ToString();
            this.sinhvien_txt_maphong.Text = row.Cells[1].Value.ToString();
            this.sinhvien_txt_malop.Text = row.Cells[2].Value.ToString();
            this.sinhvien_txt_hoten.Text = row.Cells[3].Value.ToString();

            this.sinhvien_time_ngaysinh.Text = row.Cells[4].Value.ToString();

            this.sinhvien_txt_diachi.Text = row.Cells[5].Value.ToString();

            _ = (row.Cells[6].Value.ToString() == "Nam") ? this.sinhvien_rad_nam.Checked = true : this.sinhvien_rad_nu.Checked = true;
            this.sinhvien_txt_sodienthoai.Text = row.Cells[7].Value.ToString();
        }
        #endregion

        #region PHONG Management
        void Phong_renderData()
        {
            this.query = "SELECT * FROM PHONG";
            this.adapter = new SqlDataAdapter(this.query, Config.Conn);
            this.table = new DataTable();
            this.adapter.Fill(this.table);

            this.phong_grid.DataSource = this.table;
        }

        private void phong_btn_them_Click(object sender, EventArgs e)
        {
            if (this.Phong_validate()){
                string maPhong = this.phong_txt_maphong.Text.ToString();
                string maKhunha = this.phong_txt_makhunha.Text.ToString();
                string loaiphong = this.phong_txt_loaiphong.Text.ToString();
                string sogiuong = this.phong_txt_sogiuong.Text.ToString();
                string mota = this.phong_txt_mota.Text.ToString();


                this.query = $"INSERT INTO PHONG(maKN,maP,loaiphong,sogiuong,soluongSV,motakhac) VALUES(N'{maKhunha}',N'{maPhong}',N'{loaiphong}',{Int32.Parse(sogiuong)},0, N'{mota}');";
                this.cmd = new SqlCommand(this.query, Config.Conn);
                MessageBox.Show(this.query,"Thongbao");
                this.cmd.ExecuteNonQuery();
            }
            this.clearData();
            this.renderData();
        }

        private void phong_grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = phong_grid.Rows[e.RowIndex];

            this.phong_txt_maphong.Text = row.Cells[0].Value.ToString();
            this.phong_txt_makhunha.Text = row.Cells[1].Value.ToString();
            this.phong_txt_loaiphong.Text = row.Cells[2].Value.ToString();

            this.phong_txt_sogiuong.Text = row.Cells[3].Value.ToString();
            this.phong_txt_soluongisnhvien.Text = row.Cells[4].Value.ToString();

            this.phong_txt_mota.Text = row.Cells[5].Value.ToString();
        }
        #endregion

        #region HOADON Management
        void Hoadon_renderData()
        {
            this.query = "SELECT * FROM HOADON";
            this.adapter = new SqlDataAdapter(this.query, Config.Conn);
            this.table = new DataTable();
            this.adapter.Fill(this.table);

            this.hoadon_grid.DataSource = this.table;
        }
        private void hoadon_btn_them_Click(object sender, EventArgs e)
        {
            if (this.Hoadon_validate())
            {
                string mahoadon = this.hoadon_txt_sohoadon.Text.ToString();
                string maphong = this.hoadon_txt_maphong.Text.ToString();
                string manhanvien = this.hoadon_txt_manhanvien.Text.ToString();
                string chisodiendau = this.hoadon_txt_chisodiendau.Text.ToString();
                string chisodiencuoi = this.hoadon_txt_chisodiencuoi.Text.ToString();
                string chisonuocdau = this.hoadon_txt_chisonuocdau.Text.ToString();
                string chisonuocuoi = this.hoadon_txt_chisonuoccuoi.Text.ToString();
                string ngaydongtien = this.hoadon_time_ngaydongtien.Value.ToShortDateString().ToString();

                this.query = $"INSERT INTO HOADON(soHD,maP,maNV,ngaydongtien,chisodiendau,chisodiencuoi,dongiadien,chisonuocdau,chisonuoccuoi,dongianuoc) VALUES(N'{mahoadon}',N'{maphong}',N'{manhanvien}',N'{ngaydongtien}',{Int32.Parse(chisodiendau)},{Int32.Parse(chisodiencuoi)},4,{Int32.Parse(chisonuocdau)},{Int32.Parse(chisonuocuoi)},25);";
                this.cmd = new SqlCommand(this.query, Config.Conn);
                try
                {
                    this.cmd.ExecuteNonQuery();
                }
                catch
                {
                    //TODO
                }
            }
            this.clearData();
            this.renderData();
        }

        private void hoadon_grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = hoadon_grid.Rows[e.RowIndex];

            this.hoadon_txt_sohoadon.Text = row.Cells[0].Value.ToString();
            this.hoadon_txt_maphong.Text = row.Cells[1].Value.ToString();
            this.hoadon_txt_manhanvien.Text = row.Cells[2].Value.ToString();

            this.hoadon_time_ngaydongtien.Text = row.Cells[3].Value.ToString();

            this.hoadon_txt_chisodiendau.Text = row.Cells[4].Value.ToString();
            this.hoadon_txt_chisodiencuoi.Text = row.Cells[5].Value.ToString();

            this.hoadon_txt_chisonuocdau.Text = row.Cells[7].Value.ToString();
            this.hoadon_txt_chisonuoccuoi.Text = row.Cells[8].Value.ToString();

            this.hoadon_txt_tongtien.Text = row.Cells[10].Value.ToString() + ".000 VNĐ";
  
        }
        #endregion

        #region HOPDONG Management
        void Hopdong_renderData()
        {
            this.query = "SELECT * FROM HOPDONG";
            this.adapter = new SqlDataAdapter(this.query, Config.Conn);
            this.table = new DataTable();
            this.adapter.Fill(this.table);

            this.hopdong_grid.DataSource = this.table;
        }
        private void hopdong_btn_them_Click(object sender, EventArgs e)
        {
            if (Hopdong_validate())
            {
                string mahopdong = this.hopdong_txt_mahopdong.Text.ToString();
                string maphong = this.hopdong_txt_maphong.Text.ToString();
                string manhanvien = this.hopdong_txt_manhanvien.Text.ToString();

                string ngaybatdauthue = this.hopdong_time_ngaybatdauthue.Value.ToShortDateString().ToString();
                string ngayketthucthue = this.hopdong_time_ngayketthucthue.Value.ToShortDateString().ToString();
                string ngaylaphopdong = this.hopdong_time_ngaylaphopdong.Value.ToShortDateString().ToString();

                this.query = $"INSERT INTO HOPDONG(maHD, maP,maNV,ngaybatdauthue,ngayketthucthue ,giaphong, ngaylaphopdong) VALUES(N'{mahopdong}',N'{maphong}',N'{manhanvien}',N'{ngaybatdauthue}',N'{ngayketthucthue}',10,N'{ngaylaphopdong}');";
                this.cmd = new SqlCommand(this.query, Config.Conn);
                try
                {
                    this.cmd.ExecuteNonQuery();
                }
                catch
                {
                    //TODO
                }
            }
            this.clearData();
            this.renderData();
        }

        private void hopdong_grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = hopdong_grid.Rows[e.RowIndex];

            this.hopdong_txt_mahopdong.Text = row.Cells[0].Value.ToString();
            this.hopdong_txt_maphong.Text = row.Cells[1].Value.ToString();
            this.hopdong_txt_manhanvien.Text = row.Cells[2].Value.ToString();

            this.hopdong_time_ngaybatdauthue.Text = row.Cells[3].Value.ToString();
            this.hopdong_time_ngaybatdauthue.Text = row.Cells[4].Value.ToString();

            this.hopdong_time_ngaylaphopdong.Text = row.Cells[6].Value.ToString();

            this.hopdong_txt_tongtien.Text = row.Cells[7].Value.ToString() +".000 VNĐ";
        }
        #endregion

        #region KHUNHA Management
        void Khunha_renderData()
        {
            this.query = "SELECT * FROM KHUNHA";
            this.adapter = new SqlDataAdapter(this.query, Config.Conn);
            this.table = new DataTable();
            this.adapter.Fill(this.table);

            this.khunha_grid.DataSource = this.table;
        }
        private void khunha_btn_them_Click(object sender, EventArgs e)
        {
            if (this.Khunha_validate())
            {
                string makhunha = this.khunha_txt_makhunha.Text.ToString();
                string tenkhunha = this.khunha_txt_tenkhunha.Text.ToString();
                string motakhac = this.khunha_txt_mota.Text.ToString();

                this.query = $"INSERT INTO KHUNHA VALUES(N'{makhunha}',N'{tenkhunha}',0,0,N'{motakhac}');";
                this.cmd = new SqlCommand(this.query, Config.Conn);
                try
                {
                    this.cmd.ExecuteNonQuery();
                }
                catch
                {
                    //TODO
                }
            }
            this.clearData();
            this.renderData();
        }
        private void khunha_grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = khunha_grid.Rows[e.RowIndex];

            this.khunha_txt_makhunha.Text = row.Cells[0].Value.ToString();
            this.khunha_txt_tenkhunha.Text = row.Cells[1].Value.ToString();

            this.khunha_txt_mota.Text = row.Cells[4].Value.ToString();
            this.khunha_txt_soluongsv.Text = row.Cells[2].Value.ToString(); 
            this.khunha_txt_sophong.Text = row.Cells[3].Value.ToString(); 
        }
        #endregion

        #region NHANVIENQUANLY 
        void Nhanvien_renderData()
        {
            this.query = "SELECT * FROM NHANVIENQUANLY";
            this.adapter = new SqlDataAdapter(this.query, Config.Conn);
            this.table = new DataTable();
            this.adapter.Fill(this.table);

            this.nhanvien_grid.DataSource = this.table;
        }

        private void nhanvien_btn_them_Click(object sender, EventArgs e)
        {
            if (Nhanvienquanly_validate())
            {
                string manhanvien = this.nhanvien_txt_manhanvien.Text.ToString(); 
                string tennhanvien = this.nhanvien_txt_tennhanvien.Text.ToString();
                string email = this.nhanvien_txt_email.Text.ToString();
                string diachi = this.nhanvien_txt_diachi.Text.ToString();
                string dienthoai = this.nhanvien_txt_dienthoai.Text.ToString();
                string gioitinh = (this.nhanvien_rad_nam.Checked == true) ? "Nam" : "Nữ";
                string chucdanh = this.nhanvien_txt_chucdanh.Text.ToString();

                this.query = $"INSERT INTO NHANVIENQUANLY VALUES(N'{manhanvien}',N'{tennhanvien}',N'{email}',N'{diachi}',{dienthoai}, N'{gioitinh}',N'{chucdanh}');";
                this.cmd = new SqlCommand(this.query, Config.Conn);

                try
                {
                    this.cmd.ExecuteNonQuery();
                }
                catch
                {
                    //TODO
                }
            }
            this.clearData();
            this.renderData();
        }

        private void nhanvien_grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = nhanvien_grid.Rows[e.RowIndex];

            this.nhanvien_txt_manhanvien.Text = row.Cells[0].Value.ToString();
            this.nhanvien_txt_tennhanvien.Text = row.Cells[1].Value.ToString();
            this.nhanvien_txt_email.Text = row.Cells[2].Value.ToString();

            this.nhanvien_txt_diachi.Text = row.Cells[3].Value.ToString();
            this.nhanvien_txt_dienthoai.Text = row.Cells[4].Value.ToString();


            this.nhanvien_txt_chucdanh.Text = row.Cells[6].Value.ToString();
            if (row.Cells[5].Value.ToString() == "Nam") { this.nhanvien_rad_nam.Checked = true; this.nhanvien_rad_nu.Checked = false; } else { this.nhanvien_rad_nam.Checked = false; this.nhanvien_rad_nu.Checked = true; };
        }
        #endregion

        #region Validate input form

        //SinhVien
        bool Sinhvien_validate()
        {
            if(this.sinhvien_txt_diachi.Text.ToString() == "" || 
                this.sinhvien_txt_malop.Text.ToString() == "" || 
                this.sinhvien_txt_maphong.Text.ToString() == "" || 
                this.sinhvien_txt_masinhvien.Text.ToString() == "" || 
                this.sinhvien_txt_sodienthoai.Text.ToString() == ""|| 
                this.sinhvien_txt_hoten.Text.ToString() == "")
            {
                return false;
            }
            return true;
        } 

        //PHONG
        bool Phong_validate()
        {
            if (this.phong_txt_loaiphong.Text.ToString() == "" || this.phong_txt_makhunha.Text.ToString() == "" ||
                this.phong_txt_sogiuong.Text.ToString() == "" || this.phong_txt_maphong.Text.ToString() == "")
            {
                return false;
            }
            return true;
        }

        //HOADON 
        bool Hoadon_validate()
        {
            if(this.hoadon_txt_chisodiencuoi.Text.ToString() == "" ||
                this.hoadon_txt_chisodiendau.Text.ToString() == "" ||
                this.hoadon_txt_chisonuoccuoi.Text.ToString() == "" ||
                this.hoadon_txt_chisonuocdau.Text.ToString() == "" ||
                this.hoadon_txt_manhanvien.Text.ToString() == "" ||
                this.hoadon_txt_maphong.Text.ToString() == "" ||
                this.hoadon_txt_sohoadon.Text.ToString() == "" 
                )
            {
                return false; 
            }
            return true; 
        }

        //HOPDONG 
        bool Hopdong_validate()
        {
            if(this.hopdong_txt_mahopdong.Text.ToString() == "" ||
                this.hopdong_txt_manhanvien.Text.ToString() == "" ||
                this.hopdong_txt_maphong.Text.ToString() == "")
            {
                return false;
            }
            return true;
        }

        //NHANVIENQUANLY
        bool Nhanvienquanly_validate()
        {
            if(this.nhanvien_txt_chucdanh.Text.ToString() == "" ||
                this.nhanvien_txt_email.Text.ToString() == "" ||
                this.nhanvien_txt_tennhanvien.Text.ToString() == "" ||
                this.nhanvien_txt_dienthoai.Text.ToString() == "" ||
                this.nhanvien_txt_manhanvien.Text.ToString() == "" ||
                this.nhanvien_txt_diachi.Text.ToString() == ""
                ) {
                return false;
                    };
            return true;
        }

        //KHUNHA
        bool Khunha_validate()
        {
            if (
                this.khunha_txt_makhunha.Text.ToString() == "" ||
                this.khunha_txt_mota.Text.ToString() == "" ||
                this.khunha_txt_tenkhunha.Text.ToString() == ""
                )
            {
                return false;
            }
            return true;
        }
    #endregion

        #region Init UI
        void initUI()
        {
            this.hoadon_time_ngaydongtien.Format = DateTimePickerFormat.Custom;
            this.hoadon_time_ngaydongtien.CustomFormat = "yyyy/MM/dd";

            this.hopdong_time_ngaybatdauthue.Format = DateTimePickerFormat.Custom;
            this.hopdong_time_ngaybatdauthue.CustomFormat = "yyyy/MM/dd";
            this.hopdong_time_ngayketthucthue.Format = DateTimePickerFormat.Custom;
            this.hopdong_time_ngayketthucthue.CustomFormat = "yyyy/MM/dd";
            this.hopdong_time_ngaylaphopdong.Format = DateTimePickerFormat.Custom;
            this.hopdong_time_ngaylaphopdong.CustomFormat = "yyyy/MM/dd";

            this.sinhvien_time_ngaysinh.Format = DateTimePickerFormat.Custom;
            this.sinhvien_time_ngaysinh.CustomFormat = "yyyy/MM/dd";


        }
        #endregion
    }
}
