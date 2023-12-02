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
        protected void Page_Load(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            cnn = new SqlConnection(cs);
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

            /*Creting an object bandInfo of a class Band that contains all data of 1 band*/
            Band bandInfo = new Band();
            bandInfo.id = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]);
            bandInfo.name = ds.Tables[0].Rows[0]["BandName"].ToString();
            bandInfo.members = Convert.ToInt32(ds.Tables[0].Rows[0]["BandMembers"]);
            bandInfo.description = ds.Tables[0].Rows[0]["BandDescription"].ToString();
            bandInfo.code = Convert.ToInt32(ds.Tables[0].Rows[0]["BandCode"]);

            test.Text = bandInfo.description;
        }
    }
}