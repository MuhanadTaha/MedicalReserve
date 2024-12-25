using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace medical_reservation
{
    public partial class AdminAppointments : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // التأكد من أن المستخدم مسجل الدخول وأنه دكتور
                if (Session["Role"] != null && Session["Role"].ToString() == "doctor" && Session["Email"] != null)
                {
                    string userEmail = Session["Email"].ToString(); // أخذ الإيميل من السيشن
                    int doctorID = GetDoctorIDByEmail(userEmail); // الحصول على DoctorID بناءً على الإيميل
                    LoadAppointments(doctorID); // تحميل الحجوزات الخاصة بالدكتور
                }
                else
                {
                    // إذا لم يكن هناك Role أو إذا كان المستخدم ليس دكتور
                    Response.Redirect("Login.aspx"); // توجيه المستخدم لتسجيل الدخول إذا لم يكن دكتوراً
                }
            }
        }

        // دالة لاسترجاع DoctorID بناءً على الإيميل
        private int GetDoctorIDByEmail(string email)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["MedResDBConnectionString"].ConnectionString;
            int doctorID = 0;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
                    SELECT d.DoctorID
                    FROM Doctors d
                    JOIN Users u ON d.User_ID = u.ID
                    WHERE u.email = @Email";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);

                conn.Open();
                object result = cmd.ExecuteScalar();
                conn.Close();

                if (result != DBNull.Value && result != null)
                {
                    doctorID = Convert.ToInt32(result);
                }
            }

            return doctorID;
        }

        // تحميل الحجوزات المرتبطة بالدكتور
        private void LoadAppointments(int doctorID)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["MedResDBConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                // تحديث الاستعلام لربط بيانات الحجز مع مواعيد الأطباء
                string query = @"
                    SELECT b.BookingID, b.CustomerName, b.CustomerEmail, b.CustomerPhone, 
                        a.StartTime AS AppointmentDate, a.StartTime, a.EndTime 
                    FROM Bookings b
                    LEFT JOIN Appointments a ON b.AppointmentID = a.AppointmentID
                    WHERE a.DoctorID = @DoctorID"; // تحديد الدكتور باستخدام DoctorID من السيشن

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@DoctorID", doctorID);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable();
                adapter.Fill(dt);

                gvAppointments.DataSource = dt;
                gvAppointments.DataBind();
            }
        }

        // حذف الحجز
        protected void gvAppointments_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int bookingID = Convert.ToInt32(gvAppointments.DataKeys[e.RowIndex].Value);

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
            if (Session["Role"] != null && Session["Role"].ToString() == "doctor" && Session["Email"] != null)
            {
                string userEmail = Session["Email"].ToString();
                int doctorID = GetDoctorIDByEmail(userEmail);
                LoadAppointments(doctorID);
            }
        }
    }
}
