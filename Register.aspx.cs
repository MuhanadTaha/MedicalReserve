using System;
using System.Data.SqlClient;
using System.Configuration;

namespace medical_reservation
{
    public partial class Register : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MedResDBConnectionString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Role"] == null || Session["Role"].ToString() != "admin")
                {
                    rbAdmin.Visible = false;
                }
            }
             

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            con.Open();
            cmd.Connection = con;

            // التحقق من وجود البريد الإلكتروني في قاعدة البيانات
            cmd.CommandText = "SELECT * FROM users WHERE email = @Email";
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                Response.Write("<script>alert('Email Already Exists');</script>");
                con.Close();
            }
            else
            {
                con.Close();
                con.Open();

                // تحديد دور المستخدم بناءً على اختياره
                string authorizedRole = "customer"; // القيمة الافتراضية
                if (rbDoctor.Checked)
                {
                    authorizedRole = "doctor";
                }
                else if (rbAdmin.Checked)
                {
                    authorizedRole = "admin";
                }

                // إدخال البيانات في قاعدة البيانات باستخدام المعاملات
                cmd = new SqlCommand("INSERT INTO users (fName, lName, mobile, dob, email, password, cPassword, gender, authorized) VALUES (@FirstName, @LastName, @Mobile, @DOB, @Email, @Password, @CPassword, @Gender, @Authorized)", con);

                cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);
                cmd.Parameters.AddWithValue("@DOB", txtDOB.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                cmd.Parameters.AddWithValue("@CPassword", txtCPassword.Text);
                cmd.Parameters.AddWithValue("@Gender", ddlGender.SelectedValue);
                cmd.Parameters.AddWithValue("@Authorized", authorizedRole);

                cmd.ExecuteNonQuery();

                // إعادة توجيه المستخدم إلى صفحة تسجيل الدخول
                Response.Redirect("LogIn.aspx");
                con.Close();
            }
            con.Close();
        }
    }
}
