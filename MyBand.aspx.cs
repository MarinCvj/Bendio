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
        public string owner;
    }
    public class User
    {
        public int id;
        public string username;
        public string email;
        public string password;
        public int? bandID;
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

                if (ds.Tables[0].Rows.Count <= 0)
                {
                    return null;
                }

                /*Creating an object bandInfo of a class Band that contains all data of 1 band*/
                Band bandInfo = new Band
                {
                    id = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]),
                    name = ds.Tables[0].Rows[0]["BandName"].ToString(),
                    members = Convert.ToInt32(ds.Tables[0].Rows[0]["BandMembers"]),
                    description = ds.Tables[0].Rows[0]["BandDescription"].ToString(),
                    code = Convert.ToInt32(ds.Tables[0].Rows[0]["BandCode"]),
                    owner = ds.Tables[0].Rows[0]["BandOwner"].ToString(),
                };

                return bandInfo;
            }
        }
        public User Get_user_data()
        {
            string cs = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            using (cnn = new SqlConnection(cs))
            {
                cnn.Open();

                var cookie_email = Request.Cookies["email"].Value;

                /*Creating an SqlCmd1 Command that has a type of a stord procedure with a parameter email*/
                SqlCommand sqlCmd1 = new SqlCommand();
                sqlCmd1.CommandText = "dbo.getUserInfo_by_email";
                sqlCmd1.CommandType = CommandType.StoredProcedure;
                sqlCmd1.Connection = cnn;
                SqlParameter param1 = new SqlParameter("@email", cookie_email);
                sqlCmd1.Parameters.Add(param1);

                /*Creating a data set that can hold multiple tables at once*/
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd1);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count <= 0)
                {
                    return null;
                }

                /*Creating an object UserInfo of a class User that contains all data of 1 user*/
                User userInfo = new User
                {
                    id = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]),
                    username = ds.Tables[0].Rows[0]["username"].ToString(),
                    email = ds.Tables[0].Rows[0]["email"].ToString(),
                    password = ds.Tables[0].Rows[0]["password"].ToString()
                };
                if (ds.Tables[0].Rows[0]["Band_ID"] == DBNull.Value)
                    userInfo.bandID = null;
                else
                    userInfo.bandID = Convert.ToInt32(ds.Tables[0].Rows[0]["Band_ID"]);

                return userInfo;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["email"] == null)
            {
                Response.Redirect("Account.aspx");
            }

            Band bandInfo = Get_band_data();
            User userInfo = Get_user_data();

            if (bandInfo == null || userInfo.bandID.Equals(null))
            {
                join_make_band.Attributes["style"] = "display: flex";
                return;
            }

            string cs = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            cnn = new SqlConnection(cs);
            cnn.Open();

            band_default_settings.Attributes["style"] = "display: flex";

            band_name.Text = bandInfo.name;
            band_members.Text = bandInfo.members.ToString();
            band_description.Text = bandInfo.description;
            band_code.Text = bandInfo.code.ToString();
            band_owner.Text = userInfo.username;

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

        protected void Cancel(object sender, EventArgs e)
        {
            Band bandInfo = Get_band_data();

            change_name.Text = bandInfo.name;
            change_members.Text = bandInfo.members.ToString();
            change_description.Text = bandInfo.description;

            change_settings.Attributes["style"] = "display: none";
            band_default_settings.Attributes["style"] = "display: flex";
        }

        protected void New_band(object sender, EventArgs e)
        {
            Response.Redirect("~/NewBand.aspx");
        }

        protected void Join_band(object sender, EventArgs e)
        {
            enter_band_code.Attributes["style"] = "display: flex";
            container.Attributes["style"] = "opacity: 0.5";
        }

        protected void Enter_band_code(object sender, EventArgs e)
        {
            var band_code = code.Text;
            enter_band_code.Attributes["style"] = "display: none";
            code_entered.Attributes["style"] = "display: flex";

            string cs = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            cnn = new SqlConnection(cs);
            cnn.Open();

            var cookie_email = Request.Cookies["email"].Value;

            string get_bandID = "SELECT ID FROM [Bendio].[dbo].[Band] WHERE BandCode=" + band_code + ";";
            SqlCommand sqlCmd = new SqlCommand(get_bandID, cnn);

            var id = 0;
            using (SqlDataReader sqlReader = sqlCmd.ExecuteReader())
            {
                while (sqlReader.Read())
                {
                    id = sqlReader.GetInt32(0);
                }
            }

            string set_bandid_on_user = "UPDATE [Bendio].[dbo].[User] SET Band_ID = " + id + " WHERE email = '" + cookie_email + "';";
            SqlCommand sqlCmd1 = new SqlCommand(set_bandid_on_user, cnn);
            sqlCmd1.ExecuteNonQuery();

            join_make_band.Attributes["style"] = "display: none";

            cnn.Close();
        }

        protected void Close(object sender, EventArgs e)
        {
            enter_band_code.Attributes["style"] = "display: none";
            container.Attributes["style"] = "animation: appear-container 0.5s";
        }

        protected void Close_code_entered(object sender, EventArgs e)
        {
            code_entered.Attributes["style"] = "display: none";
            container.Attributes["style"] = "animation: appear-container 0.5s";
        }

        protected void Delete_band(object sender, EventArgs e)
        {
            Band bandInfo = Get_band_data();

            string cs = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            cnn = new SqlConnection(cs);
            cnn.Open();

            string set_bandid_null = "UPDATE [Bendio].[dbo].[User] SET Band_ID = NULL WHERE Band_ID = "+ bandInfo.id + ";";
            SqlCommand sqlCmd = new SqlCommand(set_bandid_null, cnn);
            sqlCmd.ExecuteNonQuery();

            string delete_band = "DELETE FROM dbo.Band WHERE ID="+ bandInfo.id + ";";
            SqlCommand sqlCmd1 = new SqlCommand(delete_band, cnn);
            sqlCmd1.ExecuteNonQuery();

            band_default_settings.Attributes["style"] = "display: none";
            join_make_band.Attributes["style"] = "display: flex";

            cnn.Close();
        }
    }
}
