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
    public partial class New_band : System.Web.UI.Page
    {
        SqlConnection cnn;
        protected void Page_Load(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            cnn = new SqlConnection(cs);
            cnn.Open();

            if (Request.Cookies["email"] == null)
            {
                Response.Redirect("Account.aspx");
            }
        }

        protected void Submit(object sender, EventArgs e)
        {
            string name_of_band = band_name.Text;
            int num_of_members = int.Parse(members_num.Text);
            string description_text = description.Text;
            string band_code = GenerateRandomCode();

            var cookie_email = Request.Cookies["email"].Value;

            if (num_of_members != 0)
            {
                string band_info = "INSERT INTO dbo.Band (BandName, BandMembers, BandDescription, BandCode, BandOwner)" +
                " VALUES ('" + name_of_band + "'," + num_of_members + ",'" + description_text + "','" + band_code + "','" + cookie_email + "');";
                SqlCommand sqlCmd = new SqlCommand(band_info, cnn);
                sqlCmd.ExecuteNonQuery();

                string get_BandID = "SELECT ID FROM dbo.Band WHERE BandName = '" + name_of_band + "'";
                SqlCommand sqlCmd1 = new SqlCommand(get_BandID, cnn);
                SqlDataReader num = sqlCmd1.ExecuteReader();
                num.Read();
                int ID = num.GetInt32(0);
                num.Close();

                string put_BandID = "UPDATE [User] SET Band_ID = " + ID + " WHERE email = '" + cookie_email + "'";
                SqlCommand sqlCmd2 = new SqlCommand(put_BandID, cnn);
                sqlCmd2.ExecuteNonQuery();

                Response.Redirect("~/MyBand.aspx");
            }
            else
            {
                zero_members.Visible = true;
            }
            cnn.Close();
        }

        private string GenerateRandomCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            char[] codeArray = new char[6];

            for (int i = 0; i < 6; i++)
            {
                codeArray[i] = chars[random.Next(chars.Length)];
            }

            return new string(codeArray);
        }
    }
}