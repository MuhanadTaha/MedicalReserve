using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace medical_reservation
{
    public partial class Login : System.Web.UI.Page
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MedResDBConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            SqlCommand objcmd = null;

            con.Open();
            string stmt = "select * from users where email = '" + txtEmail.Text + "'  and password = '" + txtPassword.Text + "' ";
            objcmd = new SqlCommand(stmt, con);

            SqlDataReader reader = objcmd.ExecuteReader();

            if (reader.Read())
            {
                Session["Email"] = txtEmail.Text;
                Response.Redirect("Default.aspx");
            }
            else
            {
                Response.Write("<script>alert('Invalid Password or Email ...');</script>");

            }
        }

    }

}