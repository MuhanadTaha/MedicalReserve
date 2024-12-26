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
            // التحقق إذا كان المستخدم مسؤول (admin)
            if (!IsPostBack)
            {
                if (Session["Role"] != null && Session["Role"].ToString() == "admin")
                {
                    LoadDoctors();  // تحميل الأطباء فقط إذا كان المستخدم مسؤول
                }
                else
                {
                    // إعادة التوجيه إلى صفحة الخطأ إذا لم يكن المستخدم مسؤول
                    Response.Redirect("UnauthorizedAccess.aspx");
                }
            }
        }

        // دالة تحميل الأطباء من قاعدة البيانات
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
                adapter.Fill(dt);  // ملء DataTable بالبيانات

                gvDoctors.DataSource = dt;  // تعيين البيانات لمصدر البيانات في GridView
                gvDoctors.DataBind();  // ربط البيانات مع GridView
            }
        }

        // دالة تنفيذ عملية الحذف عندما يقوم المستخدم بحذف طبيب
        protected void gvDoctors_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int doctorID = Convert.ToInt32(gvDoctors.DataKeys[e.RowIndex].Value);

            // تحقق من دور المستخدم قبل السماح بالحذف
            if (Session["Role"] != null && Session["Role"].ToString() == "admin")
            {
                string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["MedResDBConnectionString"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    // استعلام لحذف جميع الحجوزات المرتبطة بالمواعيد قبل حذف الموعد نفسه
                    string deleteBookingsQuery = "DELETE FROM Bookings WHERE AppointmentID IN (SELECT AppointmentID FROM Appointments WHERE DoctorID = @DoctorID)";
                    SqlCommand deleteBookingsCmd = new SqlCommand(deleteBookingsQuery, conn);
                    deleteBookingsCmd.Parameters.AddWithValue("@DoctorID", doctorID);

                    // استعلام لحذف جميع المواعيد المرتبطة بالطبيب
                    string deleteAppointmentsQuery = "DELETE FROM Appointments WHERE DoctorID = @DoctorID";
                    SqlCommand deleteAppointmentsCmd = new SqlCommand(deleteAppointmentsQuery, conn);
                    deleteAppointmentsCmd.Parameters.AddWithValue("@DoctorID", doctorID);

                    // استعلام لحذف الطبيب من جدول Doctors
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
                // إذا لم يكن المستخدم مسؤول، إعادة التوجيه إلى صفحة الخطأ
                Response.Redirect("UnauthorizedAccess.aspx");
            }
        }
    }
}
