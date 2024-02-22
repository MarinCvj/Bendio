﻿using System;
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
        public string code;
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
    public class Calendar
    {
        public int id;
        public string date;
        public string time;
        public int bandID;
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
                    code = ds.Tables[0].Rows[0]["BandCode"].ToString(),
                    owner = ds.Tables[0].Rows[0]["BandOwner"].ToString()
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
        public Calendar Get_calendar_data()
        {
            Band bandInfo = Get_band_data();
            string cs = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            using (cnn = new SqlConnection(cs))
            {                
                cnn.Open();

                SqlCommand sqlCmd1 = new SqlCommand();
                sqlCmd1.CommandText = "dbo.GetCalendarByBandID";
                sqlCmd1.CommandType = CommandType.StoredProcedure;
                sqlCmd1.Connection = cnn;
                SqlParameter param1 = new SqlParameter("@BandID", bandInfo.id);
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
                Calendar CalendarInfo = new Calendar
                {
                    id = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]),
                    date = ds.Tables[0].Rows[0]["date"].ToString(),
                    time = ds.Tables[0].Rows[0]["time"].ToString(),
                    bandID = Convert.ToInt32(ds.Tables[0].Rows[0]["Band_ID"])
                };

                return CalendarInfo;
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
            Calendar calendarInfo = Get_calendar_data();

            if (bandInfo == null || userInfo.bandID.Equals(null))
            {
                join_make_band.Attributes["style"] = "display: flex";
                return;
            }

            string cs = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            cnn = new SqlConnection(cs);
            cnn.Open();

            band_default_settings.Attributes["style"] = "display: flex";
            calendar.Attributes["style"] = "display: none";
            new_rehersal_container.Attributes["style"] = "display: none";

            /*Putting the default setting into the band.*/
            band_name.Text = bandInfo.name;
            band_members.Text = bandInfo.members.ToString();
            band_description.Text = bandInfo.description;
            band_code.Text = bandInfo.code;

            /*Getting the username of the owner of a band with email field*/
            string get_username_by_mail = "SELECT username FROM [Bendio].[dbo].[User] WHERE email = '" + bandInfo.owner + "';";
            SqlCommand sqlCmd = new SqlCommand(get_username_by_mail, cnn);

            using (SqlDataReader sqlReader = sqlCmd.ExecuteReader())
            {
                while (sqlReader.Read())
                {
                    band_owner.Text = sqlReader.GetString(0);
                }
            }

            if (Request.Cookies["email"].Value != bandInfo.owner)
            {
                change_settings_btn.Visible = false;
                delete_band_btn.Visible = false;
            }else
            {
                rule.Visible = false;
            }

            if (!IsPostBack)
            {
                change_name.Text = bandInfo.name;
                change_members.Text = bandInfo.members.ToString();
                change_description.Text = bandInfo.description;
            }

            /*
            rehersal_band_name.Text = bandInfo.name;
            DateTime date = DateTime.Parse(calendarInfo.date);
            string formattedDate = date.ToString("yyyy-MM-dd");
            rehersal_date.Text = formattedDate;
            rehersal_time.Text = calendarInfo.time;
            */

            SqlCommand sqlCmd1 = new SqlCommand();
            sqlCmd1.CommandText = "SELECT date, time FROM Calendar WHERE Band_ID = " + bandInfo.id + "";
            sqlCmd1.CommandType = CommandType.Text;
            sqlCmd1.Connection = cnn;

            SqlDataAdapter da = new SqlDataAdapter(sqlCmd1);
            DataSet ds = new DataSet();
            da.Fill(ds);
            rehersal.DataSource = ds.Tables[0];
            rehersal.DataBind();
        }

        protected void Change_settings(object sender, EventArgs e)
        {
            /*Window for changing settings for a band*/
            /*Hiding the default settings window and opening the change settings window.*/
            change_settings.Attributes["style"] = "display: flex";
            band_default_settings.Attributes["style"] = "display: none";
        }

        protected void Save_settings(object sender, EventArgs e)
        {
            /*Saving the new settings into database.*/
            Band bandInfo = Get_band_data();

            string cs = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            cnn = new SqlConnection(cs);
            cnn.Open();

            /*Updating the database with new data for a band*/
            string new_band_settings = "UPDATE dbo.Band SET BandName = '"+ change_name.Text + "', BandMembers = "+ change_members.Text + ", BandDescription = '"+ change_description.Text + "' WHERE ID = "+ bandInfo.id +";";
            SqlCommand sqlCmd = new SqlCommand(new_band_settings, cnn);
            sqlCmd.ExecuteNonQuery();

            /*Hiding the change settings window and opening the default settings window.*/
            change_settings.Attributes["style"] = "display: none";
            band_default_settings.Attributes["style"] = "display: flex";

            bandInfo = Get_band_data();

            /*Desplaying the new settings.*/
            band_name.Text = bandInfo.name;
            band_members.Text = bandInfo.members.ToString();
            band_description.Text = bandInfo.description;
            band_code.Text = bandInfo.code;
        }

        protected void Cancel(object sender, EventArgs e)
        {
            /*Cancel button if the user doesn't want to change the settings*/
            Band bandInfo = Get_band_data();

            change_name.Text = bandInfo.name;
            change_members.Text = bandInfo.members.ToString();
            change_description.Text = bandInfo.description;

            /*Hiding the change settings window and opening the default settings window.*/
            change_settings.Attributes["style"] = "display: none";
            band_default_settings.Attributes["style"] = "display: flex";
        }

        protected void New_band(object sender, EventArgs e)
        {
            /*Button to create a new band.*/
            Response.Redirect("~/NewBand.aspx");
        }

        protected void Join_band(object sender, EventArgs e)
        {
            /*Darken the container window and opening the enter band code window.*/
            enter_band_code.Attributes["style"] = "display: flex";
            container.Attributes["style"] = "opacity: 0.5";
        }

        protected void Enter_band_code(object sender, EventArgs e)
        {
            Band bandInfo = Get_band_data();

            string band_code = code.Text;

            string cs = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            cnn = new SqlConnection(cs);
            cnn.Open();

            var cookie_email = Request.Cookies["email"].Value;

            /*Getting the band id with the band code so it can be put to users BandID field*/
            string get_bandID = "SELECT ID FROM [Bendio].[dbo].[Band] WHERE BandCode='" + band_code + "';";
            SqlCommand sqlCmd = new SqlCommand(get_bandID, cnn);

            var id = 0;
            using (SqlDataReader sqlReader = sqlCmd.ExecuteReader())
            {
                while (sqlReader.Read())
                {
                    id = sqlReader.GetInt32(0);
                }
            }

            /*Checking if the bad code exists*/
            string check_bandCode = "SELECT ID FROM Band WHERE BandCode='"+ band_code + "';";
            SqlCommand sqlCmd1 = new SqlCommand(check_bandCode, cnn);

            using (SqlDataReader sqlReader = sqlCmd1.ExecuteReader())
            {
                if (sqlReader.Read() == false)
                {
                    invalid_band_code.Visible = true;
                    return;
                }
            }

            /*Hiding the enter band code and join make band windows because the user got into the band.*/
            enter_band_code.Attributes["style"] = "display: none";
            join_make_band.Attributes["style"] = "display: none";
            container.Attributes["style"] = "animation: appear-container 0.5s";

            /*Giving band id to user and getting him into a band.*/
            string set_bandid_on_user = "UPDATE [Bendio].[dbo].[User] SET Band_ID = " + id + " WHERE email = '" + cookie_email + "';";
            SqlCommand sqlCmd2 = new SqlCommand(set_bandid_on_user, cnn);
            sqlCmd2.ExecuteNonQuery();

            cnn.Close();
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void Close(object sender, EventArgs e)
        {
            /*If the user doesn't want to put the code and wants to close the window*/
            /*Hiding the code enter window and appearing the container window.*/
            enter_band_code.Attributes["style"] = "display: none";
            container.Attributes["style"] = "animation: appear-container 0.5s";
        }

        protected void Delete_band(object sender, EventArgs e)
        {
            /*If the user wants to delete the band*/
            Band bandInfo = Get_band_data();

            string cs = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            cnn = new SqlConnection(cs);
            cnn.Open();

            /*Set band id null to all users who are in a band(have that band id)*/
            string set_bandid_null = "UPDATE [Bendio].[dbo].[User] SET Band_ID = NULL WHERE Band_ID = "+ bandInfo.id + ";";
            SqlCommand sqlCmd = new SqlCommand(set_bandid_null, cnn);
            sqlCmd.ExecuteNonQuery();

            /*Delete the band from database*/
            string delete_band = "DELETE FROM dbo.Band WHERE ID="+ bandInfo.id + ";";
            SqlCommand sqlCmd1 = new SqlCommand(delete_band, cnn);
            sqlCmd1.ExecuteNonQuery();

            /*The user has 0 bands so display the join make band window*/
            band_default_settings.Attributes["style"] = "display: none";
            join_make_band.Attributes["style"] = "display: flex";

            cnn.Close();
        }

        protected void Calendar_btn(object sender, EventArgs e)
        {
            band_default_settings.Attributes["style"] = "display: none";
            calendar.Attributes["style"] = "display: flex";
        }

        protected void New_rehersal(object sender, EventArgs e)
        {
            band_default_settings.Attributes["style"] = "display: none";
            new_rehersal_container.Attributes["style"] = "display: flex";
            calendar.Attributes["style"] = "display: none";
        }

        protected void Add_rehersal(object sender, EventArgs e)
        {
            string date_new_rehersal = new_rehersal_date.Text;
            string time_new_rehersal = new_rehersal_time.Text;

            Band bandInfo = Get_band_data();

            string cs = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            cnn = new SqlConnection(cs);
            cnn.Open();

            /*Putting a new rehersal data into a table*/
            string new_rehersal_input = "INSERT INTO Calendar VALUES ('" + date_new_rehersal + "', '" + time_new_rehersal + "', "+ bandInfo.id +");";
            SqlCommand sqlCmd = new SqlCommand(new_rehersal_input, cnn);
            sqlCmd.ExecuteNonQuery();

            band_default_settings.Attributes["style"] = "display: none";
            new_rehersal_container.Attributes["style"] = "display: none";
            calendar.Attributes["style"] = "display: flex";

            cnn.Close();
        }
    }
}