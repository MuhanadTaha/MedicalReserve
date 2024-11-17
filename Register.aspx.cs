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
    public partial class Register : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MedResDBConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "select * from users where email = '" + txtEmail.Text + "' ";

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                Response.Write("<script>alert('Email Already Exist');</script>");
                con.Close();
            }
            else
            {
                con.Close();
                con.Open();
                cmd = new SqlCommand("insert into users values('" + txtFirstName.Text + "'  , '" + txtLastName.Text + "' , '" + txtMobile.Text + "' ,  '" + txtDOB.Text + "' ,  '" + txtEmail.Text + "',  '" + txtPassword.Text + "' , '" + txtCPassword.Text + "' , '" + ddlGender.Text + "' ,'customer')", con);

                cmd.ExecuteNonQuery();

                Response.Redirect("LogIn.aspx");
                con.Close();
            }
            con.Close();
        }
    }
}