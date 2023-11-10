using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bendio
{
    public partial class Account : System.Web.UI.Page
    {
        SqlConnection cnn;
        protected void Page_Load(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            cnn = new SqlConnection(cs);
            cnn.Open();
        }

        private void CreateCookie(string email_address)
        {
            HttpCookie c = new HttpCookie("email");
            c.Value = email_address;
            c.Expires.Add(new TimeSpan(1, 0, 0));
            Response.Cookies.Add(c);
            Response.Redirect("~/Home.aspx");
        }

        protected void Create_Account(object sender, EventArgs e)
        {
            string email_text = email_create.Text;
            string password_text = password_create.Text;

            string user = "INSERT INTO [Bendio].[dbo].[User] (email, password) VALUES ('" + email_text + "','" + password_text + "')";
            SqlCommand sqlCmd = new SqlCommand(user, cnn);
            sqlCmd.ExecuteNonQuery();

            CreateCookie(email_text);
            Response.Redirect("~/Home.aspx");
            cnn.Close();
        }

        protected void Log_in(object sender, EventArgs e)
        {
            string email_text = email_log.Text;
            string password_text = password_log.Text;

            string user_get = "SELECT * FROM [Bendio].[dbo].[User] WHERE email='" + email_text + "'" + "AND password='" + password_text + "'";
            SqlCommand sqlCmd = new SqlCommand(user_get, cnn);

            var user = 0;
            using (SqlDataReader sqlReader = sqlCmd.ExecuteReader())
            {
                while (sqlReader.Read())
                {
                    user = 1;
                    CreateCookie(email_text);
                    Response.Redirect("~/Home.aspx");
                }
            }

            if (user == 0)
            {
                wrong.Visible = true;
            }
            cnn.Close();
        }
    }
}