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
    public partial class SiteMaster : MasterPage
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MedResDBConnectionString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Authantication();
                Authorization();
            }


        }


        protected void btnOut_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            Response.Redirect("~/Login.aspx");
        }


        protected void Authantication()
        {
            string str = "select email from users where Email= '" + Session["Email"] + "' ";
            SqlCommand cmd = new SqlCommand(str, con);
            con.Open();
            string Result = Convert.ToString(cmd.ExecuteScalar());

            if (string.IsNullOrWhiteSpace(Result))
            {
                // ddlAdmin.Visible = false;

               // btnProduct.Visible = false;
               // btnCategory.Visible = false;


            }
            else
            {
                btnOut.Visible = true;
                btnLogin.Visible = false;
                btnRegister.Visible = false;
                //btnOrder.Visible = true;
            }
            con.Close();
        }

        protected void Authorization()
        {
           

            string str = "select authorized from users where Email= '" + Session["Email"] + "' ";
            SqlCommand cmd = new SqlCommand(str, con);
            con.Open();
            string Result = Convert.ToString(cmd.ExecuteScalar());

            if (Result != "admin")
            {
                
                btnDoctors.Visible = false;
                btnAddAdmin.Visible = false;
            }
            if (Result != "doctor")
            {
                btnAdminAppointment.Visible = false;
                //btnPatitionAppointment.Visible = false;
                btnDashboard.Visible = false;
          
            }
            
            if (Result != "customer")
            {
                btnPatitionAppointment.Visible=false;
            }
           
            
            con.Close();
        }
    }
}