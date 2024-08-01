using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
namespace WebApplication4.Models
{
    public class DBACCESS
    {
        public static string ConnectionString= @"Data Source=DESKTOP-44PAKFG\MSSQLSERVER02; initial catalog=ImgUpload;integrated Security=true";
        public  SqlConnection con =new SqlConnection(ConnectionString);
        public  SqlCommand cmd = null;

        public void OpenCon()
        {
            if (con.State==ConnectionState.Closed)
            {
                con.Open();
        }
        }
        public void CloseCon()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        public void InsertUpdateDelete(string q)
        {
            OpenCon();
            cmd = new SqlCommand(q,con);
            cmd.ExecuteNonQuery();
            CloseCon();
        }
    }
}