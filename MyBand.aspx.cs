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
    public class Band
    {
        public int id;
        public string name;
        public int members;
        public string description;
        public int code;
    }
    public partial class MyBand : System.Web.UI.Page
    {
        SqlConnection cnn;
        protected void Page_Load(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            cnn = new SqlConnection(cs);
            cnn.Open();

            var cookie_email = Request.Cookies["email"].Value;

            string get_user_bandID = "SELECT Band_ID FROM [User] WHERE email = '" + cookie_email + "';";
            SqlCommand sqlCmd1 = new SqlCommand(get_user_bandID, cnn);
            SqlDataReader bandID = sqlCmd1.ExecuteReader();
            bandID.Read();
            string user_band_id = bandID["Band_ID"].ToString();
            bandID.Close();

            string get_band_info = "SELECT * FROM Band WHERE ID = " + user_band_id + ";";
            SqlCommand sqlCmd2 = new SqlCommand(get_band_info, cnn);
            SqlDataReader bandInfo = sqlCmd2.ExecuteReader();

            Band band = new Band();
            if (bandInfo.Read())
            {
                band.id = Convert.ToInt32(bandInfo["ID"]);
                band.name = bandInfo["BandName"].ToString();
                band.members = Convert.ToInt32(bandInfo["BandMembers"]);
                band.description = bandInfo["BandDescription"].ToString();
                band.code = Convert.ToInt32(bandInfo["BandCode"]);
            }
            bandInfo.Close();

            test.Text = band.description;
        }
    }
}