using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace medical_reservation
{
    public partial class BookAppointment : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // تأكد من وجود AppointmentID في الـ QueryString
                if (Request.QueryString["AppointmentID"] != null)
                {
                    int appointmentID = int.Parse(Request.QueryString["AppointmentID"]);
                    hfAppointmentID.Value = appointmentID.ToString();  // تعيين القيمة في الـ HiddenField
                }
                else
                {
                    // في حال عدم وجود AppointmentID، إعادة التوجيه إلى صفحة الخطأ
                    Response.Redirect("ErrorPage.aspx");
                }
            }
        }

        // عند الضغط على زر الحجز
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // الحصول على القيم المدخلة من المستخدم
            int appointmentID = int.Parse(hfAppointmentID.Value);
            string customerName = txtCustomerName.Text;
            string customerEmail = txtCustomerEmail.Text;
            string customerPhone = txtCustomerPhone.Text;

            // الاتصال بقاعدة البيانات لإدخال الحجز
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["MedResDBConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "INSERT INTO Bookings (AppointmentID, CustomerName, CustomerEmail, CustomerPhone) " +
                               "VALUES (@AppointmentID, @CustomerName, @CustomerEmail, @CustomerPhone)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@AppointmentID", appointmentID);
                cmd.Parameters.AddWithValue("@CustomerName", customerName);
                cmd.Parameters.AddWithValue("@CustomerEmail", customerEmail);
                cmd.Parameters.AddWithValue("@CustomerPhone", customerPhone);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            // حفظ البيانات في الـ Session
            Session["CustomerName"] = customerName;
            Session["CustomerEmail"] = customerEmail;
            Session["CustomerPhone"] = customerPhone;
            Session["AppointmentID"] = appointmentID;

            // إعادة التوجيه إلى صفحة التأكيد
            Response.Redirect("Confirmation.aspx");
        }
    }
}
