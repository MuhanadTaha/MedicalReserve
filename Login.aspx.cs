using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;

namespace medical_reservation
{
    public partial class Login : Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MedResDBConnectionString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            // يمكنك إضافة بعض منطق هنا للتحقق إذا كان المستخدم قد سجل الدخول مسبقًا باستخدام الـ Session
            if (Session["Email"] != null)
            {
                Response.Redirect("Default.aspx"); // إعادة توجيه المستخدم إذا كان قد سجل الدخول بالفعل
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            SqlCommand objcmd = null;

            try
            {
                con.Open();
                string stmt = "SELECT id, email, password, authorized FROM users WHERE email = @Email AND password = @Password";
                objcmd = new SqlCommand(stmt, con);

                // استخدام المعاملات لمنع هجمات SQL Injection
                objcmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                objcmd.Parameters.AddWithValue("@Password", txtPassword.Text);

                SqlDataReader reader = objcmd.ExecuteReader();

                if (reader.Read())
                {
                    // تخزين بيانات المستخدم في الـ session
                    Session["Email"] = txtEmail.Text;
                    Session["UserID"] = reader["id"];
                    Session["Role"] = reader["authorized"]; // هنا نقوم بتخزين الدور (مثلاً: doctor أو patient)

                    // التوجيه إلى الصفحة الرئيسية بناءً على الدور
                    if (reader["authorized"].ToString() == "doctor")
                    {
                        Response.Redirect("DoctorDashboard.aspx");
                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
                    }
                }
                else
                {
                    Response.Write("<script>alert('Invalid Password or Email ...');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
            finally
            {
                con.Close();
            }
        }
    }
}
