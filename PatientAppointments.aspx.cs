using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace medical_reservation
{
    public partial class PatientAppointments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // التأكد من أن المستخدم مسجل دخوله
                if (Session["Role"] != null && Session["Role"].ToString() == "customer" && Session["Email"] != null)
                {
                    string userEmail = Session["Email"].ToString(); // أخذ الإيميل من السيشن
                    LoadCustomerAppointments(userEmail); // تحميل الحجوزات الخاصة بالعميل
                }
                else
                {
                    // إذا لم يكن هناك Role أو إذا كان المستخدم ليس عميل
                    Response.Redirect("Login.aspx"); // توجيه المستخدم لتسجيل الدخول
                }
            }
        }

        // تحميل الحجوزات الخاصة بالعميل بناءً على الإيميل
        private void LoadCustomerAppointments(string email)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["MedResDBConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
                            SELECT b.BookingID, a.StartTime AS AppointmentDate, 
                                   u.fName + ' ' + u.lName AS DoctorName,  -- ربط اسم الطبيب بشكل صحيح
                                   b.CustomerName, b.CustomerEmail, b.CustomerPhone
                            FROM Bookings b
                            JOIN Appointments a ON b.AppointmentID = a.AppointmentID
                            JOIN Doctors d ON a.DoctorID = d.DoctorID
                            JOIN Users u ON d.User_ID = u.ID  -- ربط جدول Users مع جدول Doctors عبر User_ID
                            WHERE b.CustomerEmail = @CustomerEmail";


                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CustomerEmail", email);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable();
                adapter.Fill(dt);

                gvCustomerAppointments.DataSource = dt;
                gvCustomerAppointments.DataBind();
            }
        }

        // إلغاء الحجز
        protected void gvCustomerAppointments_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int bookingID = Convert.ToInt32(gvCustomerAppointments.DataKeys[e.RowIndex].Value);

            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["MedResDBConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "DELETE FROM Bookings WHERE BookingID = @BookingID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@BookingID", bookingID);

                conn.Open();
                cmd.ExecuteNonQuery();  // تنفيذ عملية الحذف
                conn.Close();
            }

            // إعادة تحميل البيانات بعد الحذف
            if (Session["Role"] != null && Session["Role"].ToString() == "customer" && Session["Email"] != null)
            {
                string userEmail = Session["Email"].ToString();
                LoadCustomerAppointments(userEmail); // إعادة تحميل الحجوزات بعد إلغاء الحجز
            }
        }
    }
}