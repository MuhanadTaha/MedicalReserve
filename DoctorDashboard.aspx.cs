using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace medical_reservation
{
    public partial class DoctorDashboard : Page
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MedResDBConnectionString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Email"] == null || Session["Role"].ToString() != "doctor")
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    LoadDoctorData();
                    LoadAppointment(); // تحميل الموعد عند التحميل الأولي للصفحة
                }
            }
        }

        private void LoadDoctorData()
        {
            string email = Session["Email"].ToString();
            string query = "SELECT d.DoctorID, d.Specialty, d.City, d.Address, d.image " +
                           "FROM Doctors d " +
                           "JOIN Users u ON u.ID = d.user_id " +
                           "WHERE u.email = @Email";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Email", email);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtSpecialty.Value = reader["Specialty"].ToString();
                    txtCity.Value = reader["City"].ToString();
                    txtAddress.Value = reader["Address"].ToString();
                }
                else
                {
                    txtSpecialty.Value = "";
                    txtCity.Value = "";
                    txtAddress.Value = "";
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

        private void LoadAppointment()
        {
            string email = Session["Email"].ToString();
            string query = "SELECT a.StartTime, a.EndTime, a.Available " +
                           "FROM Appointments a " +
                           "JOIN Doctors d ON d.DoctorID = a.DoctorID " +
                           "JOIN Users u ON u.ID = d.user_id " +
                           "WHERE u.email = @Email";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Email", email);

            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // إذا تم العثور على موعد
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    txtStartTime.Text = row["StartTime"].ToString();
                    txtEndTime.Text = row["EndTime"].ToString();
                    chkAvailable.Checked = Convert.ToBoolean(row["Available"]);
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

        protected void btnAddAppointment_Click(object sender, EventArgs e)
        {
            // التأكد من تعبئة جميع الحقول
            if (string.IsNullOrWhiteSpace(txtStartTime.Text) ||
                string.IsNullOrWhiteSpace(txtEndTime.Text))
            {
                Response.Write("<script>alert('يرجى ملء جميع الحقول!');</script>");
                return;
            }

            string email = Session["Email"].ToString();
            DateTime startTime = DateTime.Parse(txtStartTime.Text);
            DateTime endTime = DateTime.Parse(txtEndTime.Text);
            bool isAvailable = chkAvailable.Checked;

            // الحصول على DoctorID بناءً على الـ Email
            string query = "SELECT DoctorID FROM Doctors WHERE user_id = (SELECT ID FROM Users WHERE email = @Email)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Email", email);

            try
            {
                con.Open();
                object result = cmd.ExecuteScalar();
                int doctorID = Convert.ToInt32(result);

                // التحقق مما إذا كان الموعد موجودًا مسبقًا
                string checkAppointmentQuery = "SELECT COUNT(*) FROM Appointments WHERE DoctorID = @DoctorID";
                SqlCommand checkCmd = new SqlCommand(checkAppointmentQuery, con);
                checkCmd.Parameters.AddWithValue("@DoctorID", doctorID);

                int appointmentCount = (int)checkCmd.ExecuteScalar();

                if (appointmentCount > 0)
                {
                    // إذا كان الموعد موجودًا مسبقًا، نقوم بتعديل الموعد
                    string updateQuery = "UPDATE Appointments SET StartTime = @StartTime, EndTime = @EndTime, Available = @Available WHERE DoctorID = @DoctorID";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, con);
                    updateCmd.Parameters.AddWithValue("@StartTime", startTime);
                    updateCmd.Parameters.AddWithValue("@EndTime", endTime);
                    updateCmd.Parameters.AddWithValue("@Available", isAvailable);
                    updateCmd.Parameters.AddWithValue("@DoctorID", doctorID);

                    updateCmd.ExecuteNonQuery();
                    Response.Write("<script>alert('تم تعديل الموعد بنجاح!');</script>");
                }
                else
                {
                    // إذا لم يكن الموعد موجودًا، نقوم بإضافة موعد جديد
                    string insertQuery = "INSERT INTO Appointments (DoctorID, StartTime, EndTime, Available) " +
                                         "VALUES (@DoctorID, @StartTime, @EndTime, @Available)";
                    SqlCommand insertCmd = new SqlCommand(insertQuery, con);
                    insertCmd.Parameters.AddWithValue("@DoctorID", doctorID);
                    insertCmd.Parameters.AddWithValue("@StartTime", startTime);
                    insertCmd.Parameters.AddWithValue("@EndTime", endTime);
                    insertCmd.Parameters.AddWithValue("@Available", isAvailable);

                    insertCmd.ExecuteNonQuery();
                    Response.Write("<script>alert('تم إضافة الموعد بنجاح!');</script>");
                }

                // إعادة تحميل المواعيد بعد إضافة أو تعديل الموعد
                LoadAppointment();
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // التأكد من أن الحقول مليئة بالقيم
            if (string.IsNullOrWhiteSpace(txtSpecialty.Value) ||
                string.IsNullOrWhiteSpace(txtCity.Value) ||
                string.IsNullOrWhiteSpace(txtAddress.Value))
            {
                Response.Write("<script>alert('يرجى ملء جميع الحقول!');</script>");
                return;
            }

            string email = Session["Email"].ToString();

            // التحقق من وجود بيانات الطبيب في الجدول
            string checkQuery = "SELECT DoctorID FROM Doctors WHERE user_id = (SELECT ID FROM Users WHERE email = @Email)";
            SqlCommand checkCmd = new SqlCommand(checkQuery, con);
            checkCmd.Parameters.AddWithValue("@Email", email);

            try
            {
                con.Open();

                object result = checkCmd.ExecuteScalar();

                if (result != null)
                {
                    // إذا كانت البيانات موجودة، نقوم بتحديث البيانات
                    string updateQuery = "UPDATE Doctors SET Specialty = @Specialty, City = @City, Address = @Address, image = @Image WHERE user_id = (SELECT ID FROM Users WHERE email = @Email)";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, con);
                    updateCmd.Parameters.AddWithValue("@Specialty", txtSpecialty.Value);
                    updateCmd.Parameters.AddWithValue("@City", txtCity.Value);
                    updateCmd.Parameters.AddWithValue("@Address", txtAddress.Value);
                    updateCmd.Parameters.AddWithValue("@Email", email);

                    // الحصول على الصورة القديمة من قاعدة البيانات قبل التحديث
                    string oldImagePath = "";
                    string checkImageQuery = "SELECT image FROM Doctors WHERE user_id = (SELECT ID FROM Users WHERE email = @Email)";
                    SqlCommand checkImageCmd = new SqlCommand(checkImageQuery, con);
                    checkImageCmd.Parameters.AddWithValue("@Email", email);
                    SqlDataReader reader = checkImageCmd.ExecuteReader();
                    if (reader.Read())
                    {
                        oldImagePath = reader["image"].ToString(); // حفظ المسار القديم للصورة
                    }
                    reader.Close();

                    // إذا كانت الصورة قد تم تحميلها
                    if (fileImage.HasFile)
                    {
                        string filePath = "~/images/" + fileImage.FileName;
                        fileImage.SaveAs(Server.MapPath(filePath));
                        updateCmd.Parameters.AddWithValue("@Image", filePath); // إضافة الصورة الجديدة
                    }
                    else
                    {
                        // إذا لم يتم تحميل صورة جديدة، نحتفظ بالصورة القديمة
                        updateCmd.Parameters.AddWithValue("@Image", oldImagePath); // إبقاء الصورة القديمة كما هي
                    }

                    updateCmd.ExecuteNonQuery();
                    Response.Write("<script>alert('تم تحديث البيانات بنجاح!');</script>");
                }
                else
                {
                    // إذا لم تكن البيانات موجودة، نقوم بإضافة البيانات الجديدة
                    string insertQuery = "INSERT INTO Doctors (Specialty, City, Address, user_id, image) " +
                                         "VALUES (@Specialty, @City, @Address, (SELECT ID FROM Users WHERE email = @Email), @Image)";
                    SqlCommand insertCmd = new SqlCommand(insertQuery, con);
                    insertCmd.Parameters.AddWithValue("@Specialty", txtSpecialty.Value);
                    insertCmd.Parameters.AddWithValue("@City", txtCity.Value);
                    insertCmd.Parameters.AddWithValue("@Address", txtAddress.Value);
                    insertCmd.Parameters.AddWithValue("@Email", email);

                    // إضافة الصورة إذا كانت موجودة
                    if (fileImage.HasFile)
                    {
                        string filePath = "~/images/" + fileImage.FileName;
                        fileImage.SaveAs(Server.MapPath(filePath));
                        insertCmd.Parameters.AddWithValue("@Image", filePath);
                    }
                    else
                    {
                        insertCmd.Parameters.AddWithValue("@Image", "~/images/" + "maleDoctor.jpg");
                    }

                    insertCmd.ExecuteNonQuery();
                    Response.Write("<script>alert('تم إضافة البيانات بنجاح!');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
            finally
            {
                con.Close(); // إغلاق الاتصال
            }
        }
    }
}
