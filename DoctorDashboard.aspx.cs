using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
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
                Response.Redirect("Login.aspx"); // إذا لم يكن الطبيب مسجلاً دخوله أو إذا لم يكن دوره "doctor"
            }
            else
            {
                // جلب بيانات الطبيب من قاعدة البيانات
                if (!IsPostBack)
                {
                    LoadDoctorData();
                }
            }
        }

        private void LoadDoctorData()
        {
            // استخدام Session["Email"] للحصول على الـ UserID
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
                    // إذا كانت بيانات الطبيب موجودة، عرضها في النصوص
                    txtSpecialty.Value = reader["Specialty"].ToString();
                    txtCity.Value = reader["City"].ToString();
                    txtAddress.Value = reader["Address"].ToString();
                    // تحميل الصورة (إذا كانت موجودة)
                    if (reader["image"] != DBNull.Value)
                    {
                        // يمكن عرض الصورة إذا كانت موجودة في الـ image
                    }
                }
                else
                {
                    // إذا لم تكن بيانات الطبيب موجودة، نسمح له بإدخال البيانات الجديدة
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

            // نبدأ بالتحقق إذا كانت بيانات الطبيب موجودة في الجدول
            string checkQuery = "SELECT DoctorID FROM Doctors WHERE user_id = (SELECT ID FROM Users WHERE email = @Email)";
            SqlCommand checkCmd = new SqlCommand(checkQuery, con);
            checkCmd.Parameters.AddWithValue("@Email", email);

            try
            {
                con.Open(); // فتح الاتصال مرة واحدة

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
