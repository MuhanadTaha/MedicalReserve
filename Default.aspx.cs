using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace medical_reservation
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCities(); // تحميل المدن عند أول تحميل للصفحة
                BindDoctors(""); // عرض الدكاترة من دون فلترة المدينة في البداية
            }
        }

        // تحميل المدن في DropDownList
        private void BindCities()
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["MedResDBConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT DISTINCT City FROM Doctors";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlCities.DataSource = dt;
                ddlCities.DataTextField = "City";
                ddlCities.DataValueField = "City";
                ddlCities.DataBind();
            }
        }

        // ربط الدكاترة بالمدينة المختارة
        private void BindDoctors(string city)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["MedResDBConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
                    SELECT u.fName, u.lName, u.email, u.mobile, d.Specialty, d.DoctorID, d.image
                    FROM [dbo].[Users] u
                    JOIN [dbo].[Doctors] d ON u.ID = d.User_ID
                    WHERE u.authorized = 'doctor'";

                if (!string.IsNullOrEmpty(city))
                {
                    query += " AND d.City = @City"; // إضافة شرط المدينة في الاستعلام
                }

                SqlCommand cmd = new SqlCommand(query, conn);
                if (!string.IsNullOrEmpty(city))
                {
                    cmd.Parameters.AddWithValue("@City", city);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptDoctors.DataSource = dt;
                rptDoctors.DataBind();
            }
        }

        // حدث عند اختيار مدينة من DropDownList
        protected void ddlCities_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCity = ddlCities.SelectedValue;
            BindDoctors(selectedCity); // تحميل الدكاترة بناءً على المدينة المختارة
        }

        protected void rptDoctors_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // الحصول على DoctorID من العنصر المرتبط
                int doctorID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "DoctorID"));

                // ربط مواعيد الطبيب في rptAppointments داخل هذا العنصر
                Repeater rptAppointments = (Repeater)e.Item.FindControl("rptAppointments");

                string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["MedResDBConnectionString"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = @"
                    SELECT a.AppointmentID, a.StartTime, a.EndTime, d.image
                    FROM Appointments a
                    JOIN Doctors d ON a.DoctorID = d.DoctorID
                    WHERE a.DoctorID = @DoctorID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@DoctorID", doctorID);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    rptAppointments.DataSource = dt;
                    rptAppointments.DataBind();
                }
            }
        }

        protected void btnBook_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int appointmentID = int.Parse(btn.CommandArgument);

            // التحقق من أن المستخدم مسجل الدخول باستخدام الجلسة
            if (Session["UserID"] != null)
            {
                int userID = Convert.ToInt32(Session["UserID"]);
                string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["MedResDBConnectionString"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    // استعلام لإدراج حجز جديد
                    string query = @"
                INSERT INTO [dbo].[Bookings] (AppointmentID, CustomerName, CustomerEmail, CustomerPhone, BookingDate, AppointmentDate)
                SELECT @AppointmentID, u.fName + ' ' + u.lName, u.email, u.mobile, GETDATE(), a.StartTime
                FROM [dbo].[Users] u
                JOIN [dbo].[Appointments] a ON a.AppointmentID = @AppointmentID
                WHERE u.ID = @UserID;
            ";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@AppointmentID", appointmentID);
                    cmd.Parameters.AddWithValue("@UserID", userID);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    // عرض رسالة تأكيد باستخدام SweetAlert
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", "Swal.fire('تم الحجز بنجاح!', 'تم حجز الموعد بنجاح. سيتم الاتصال بك قريبًا.', 'success');", true);
                }
            }
            else
            {
                // إذا لم يكن المستخدم مسجل دخول، عرض رسالة تنبيه
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", "Swal.fire('خطأ', 'يرجى تسجيل الدخول أولاً.', 'error');", true);
            }
        }

    }
}
