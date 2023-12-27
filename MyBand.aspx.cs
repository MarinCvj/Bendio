using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
        public Band Get_band_data()
        {
            string cs = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            using (cnn = new SqlConnection(cs)) {
                cnn.Open();

                var cookie_email = Request.Cookies["email"].Value;

                /*Creating an SqlCmd1 Command that has a type of a stord procedure with a parameter email*/
                SqlCommand sqlCmd1 = new SqlCommand();
                sqlCmd1.CommandText = "dbo.getBandInfo_by_email";
                sqlCmd1.CommandType = CommandType.StoredProcedure;
                sqlCmd1.Connection = cnn;
                SqlParameter param1 = new SqlParameter("@email", cookie_email);
                sqlCmd1.Parameters.Add(param1);

                /*Creating a data set that can hold multiple tables at once*/
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd1);
                DataSet ds = new DataSet();
                da.Fill(ds);

                /*Creating an object bandInfo of a class Band that contains all data of 1 band*/
                Band bandInfo = new Band();
                bandInfo.id = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]);
                bandInfo.name = ds.Tables[0].Rows[0]["BandName"].ToString();
                bandInfo.members = Convert.ToInt32(ds.Tables[0].Rows[0]["BandMembers"]);
                bandInfo.description = ds.Tables[0].Rows[0]["BandDescription"].ToString();
                bandInfo.code = Convert.ToInt32(ds.Tables[0].Rows[0]["BandCode"]);

                return bandInfo;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Band bandInfo = Get_band_data();

            string cs = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            cnn = new SqlConnection(cs);
            cnn.Open();            

            band_name.Text = bandInfo.name;
            band_members.Text = bandInfo.members.ToString();
            band_description.Text = bandInfo.description;
            band_code.Text = bandInfo.code.ToString();

            if (!IsPostBack)
            {
                change_name.Text = bandInfo.name;
                change_members.Text = bandInfo.members.ToString();
                change_description.Text = bandInfo.description;
            }
        }

        protected void Change_settings(object sender, EventArgs e)
        {
            change_settings.Attributes["style"] = "display: flex";
            band_default_settings.Attributes["style"] = "display: none";
        }

        protected void Save_settings(object sender, EventArgs e)
        {
            Band bandInfo = Get_band_data();

            string cs = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            cnn = new SqlConnection(cs);
            cnn.Open();

            /*Updating the database with new data for a band*/
            string new_band_settings = "UPDATE dbo.Band SET BandName = '"+ change_name.Text + "', BandMembers = "+ change_members.Text + ", BandDescription = '"+ change_description.Text + "' WHERE ID = "+ bandInfo.id +";";
            SqlCommand sqlCmd = new SqlCommand(new_band_settings, cnn);
            sqlCmd.ExecuteNonQuery();

            change_settings.Attributes["style"] = "display: none";
            band_default_settings.Attributes["style"] = "display: flex";

            bandInfo = Get_band_data();

            band_name.Text = bandInfo.name;
            band_members.Text = bandInfo.members.ToString();
            band_description.Text = bandInfo.description;
            band_code.Text = bandInfo.code.ToString();
        }
    }
}
