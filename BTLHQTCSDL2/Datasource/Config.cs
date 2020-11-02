using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTLHQTCSDL2.Datasource
{
    public class Config
    {
        public static string CHUOIKETNOI_Main = "Data Source=HOME-PC;Initial Catalog=QuanLyKyTucXaDaiHocThuyLoi;User ID=sa;Password=thiemtinhte;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static string CHUOIKETNOI_Config = "Data Source=HOME-PC;Initial Catalog=QuanLyKyTucXaDaiHocThuyLoi; Integated Security = True ";
        public static SqlConnection Conn = null; 
        
    }
}
