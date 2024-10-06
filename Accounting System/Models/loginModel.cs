using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Accounting_System.Models
{
    public class loginModel
    {
            public DataTable UserLogin(string name, string passw)
            {
                DataTable dt = new DataTable();

                string strConString = @"Data Source=DESKTOP-60G623S;Initial Catalog=Accountingsystem;Integrated Security=True";

                using (SqlConnection con = new SqlConnection(strConString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Select * from Admin  where UserName='" + name + "' and Password ='" + passw + "'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                return dt;
            }

        }
    
}