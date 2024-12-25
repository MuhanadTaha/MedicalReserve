using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace medical_reservation
{
    public partial class DoctorsList : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Role"] != null && Session["Role"].ToString() == "admin")
                {
                    LoadDoctors();
                }
            }
        }

        private void LoadDoctors()
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["MedResDBConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                // استعلام محدث لدمج جدول Doctors مع جدول Users للحصول على بيانات الأطباء
                string query = @"
                    SELECT d.DoctorID, u.fName, u.lName, d.Specialty, d.City, d.Address
                    FROM Doctors d
                    INNER JOIN Users u ON d.User_ID = u.ID";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                System.Data.DataTable dt = new System.Data.DataTable();
                adapter.Fill(dt);
                gvDoctors.DataSource = dt;
                gvDoctors.DataBind();
            }
        }

        protected void gvDoctors_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int doctorID = Convert.ToInt32(gvDoctors.DataKeys[e.RowIndex].Value);

            // تحقق من الدور قبل السماح بالحذف
            if (Session["Role"] != null && Session["Role"].ToString() == "admin")
            {
                string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["MedResDBConnectionString"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    // حذف جميع الحجوزات المرتبطة بالمواعيد قبل حذف الموعد نفسه
                    string deleteBookingsQuery = "DELETE FROM Bookings WHERE AppointmentID IN (SELECT AppointmentID FROM Appointments WHERE DoctorID = @DoctorID)";
                    SqlCommand deleteBookingsCmd = new SqlCommand(deleteBookingsQuery, conn);
                    deleteBookingsCmd.Parameters.AddWithValue("@DoctorID", doctorID);

                    // حذف جميع المواعيد المرتبطة بالطبيب
                    string deleteAppointmentsQuery = "DELETE FROM Appointments WHERE DoctorID = @DoctorID";
                    SqlCommand deleteAppointmentsCmd = new SqlCommand(deleteAppointmentsQuery, conn);
                    deleteAppointmentsCmd.Parameters.AddWithValue("@DoctorID", doctorID);

                    // حذف الطبيب من جدول Doctors
                    string deleteDoctorQuery = "DELETE FROM Doctors WHERE DoctorID = @DoctorID";
                    SqlCommand deleteDoctorCmd = new SqlCommand(deleteDoctorQuery, conn);
                    deleteDoctorCmd.Parameters.AddWithValue("@DoctorID", doctorID);

                    conn.Open();

                    // تنفيذ الحذف أولًا من جدول الحجوزات
                    deleteBookingsCmd.ExecuteNonQuery();

                    // ثم الحذف من جدول المواعيد
                    deleteAppointmentsCmd.ExecuteNonQuery();

                    // أخيرًا الحذف من جدول الأطباء
                    deleteDoctorCmd.ExecuteNonQuery();

                    conn.Close();
                }

                // إعادة تحميل الأطباء بعد الحذف
                LoadDoctors();
            }
            else
            {
                // يمكن إضافة رسالة خطأ أو توجيه إلى صفحة أخرى في حال لم يكن المستخدم أدمن.
                Response.Redirect("UnauthorizedAccess.aspx");
            }
        }



    }
}
