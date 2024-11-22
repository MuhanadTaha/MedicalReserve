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
                LoadAppointments();
            }
        }

        private void LoadAppointments()
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["MedResDBConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT BookingID, AppointmentID, CustomerName, CustomerEmail, CustomerPhone, AppointmentDate FROM Bookings";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                System.Data.DataTable dt = new System.Data.DataTable();
                adapter.Fill(dt);
                gvAppointments.DataSource = dt;
                gvAppointments.DataBind();
            }
        }

        //protected void gvAppointments_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    gvAppointments.EditIndex = e.NewEditIndex;
        //    LoadAppointments();
        //}

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

            LoadAppointments();  // إعادة تحميل البيانات بعد الحذف
        }


        //protected void gvAppointments_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{
        //    gvAppointments.EditIndex = -1; // العودة إلى وضع العرض
        //    LoadAppointments();
        //}

        //protected void gvAppointments_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    // التأكد من أن الفهرس داخل النطاق الصحيح
        //    if (e.RowIndex >= 0 && e.RowIndex < gvAppointments.Rows.Count)
        //    {
        //        // الحصول على ID الحجز من DataKeys
        //        int bookingID = Convert.ToInt32(gvAppointments.DataKeys[e.RowIndex].Value);

        //        // محاولة الحصول على القيم المدخلة من الصف المعدل
        //        TextBox txtCustomerName = (TextBox)gvAppointments.Rows[e.RowIndex].FindControl("txtCustomerName");
        //        TextBox txtCustomerEmail = (TextBox)gvAppointments.Rows[e.RowIndex].FindControl("txtCustomerEmail");
        //        TextBox txtCustomerPhone = (TextBox)gvAppointments.Rows[e.RowIndex].FindControl("txtCustomerPhone");

        //        // التأكد من أن الـ TextBoxs ليست فارغة أو null
        //        if (txtCustomerName != null && txtCustomerEmail != null && txtCustomerPhone != null)
        //        {
        //            string customerName = txtCustomerName.Text;
        //            string customerEmail = txtCustomerEmail.Text;
        //            string customerPhone = txtCustomerPhone.Text;

        //            // الاتصال بقاعدة البيانات لتحديث الحجز
        //            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["MedResDBConnectionString"].ConnectionString;
        //            using (SqlConnection conn = new SqlConnection(connStr))
        //            {
        //                string query = "UPDATE Bookings SET CustomerName = @CustomerName, CustomerEmail = @CustomerEmail, CustomerPhone = @CustomerPhone WHERE BookingID = @BookingID";
        //                SqlCommand cmd = new SqlCommand(query, conn);
        //                cmd.Parameters.AddWithValue("@BookingID", bookingID);
        //                cmd.Parameters.AddWithValue("@CustomerName", customerName);
        //                cmd.Parameters.AddWithValue("@CustomerEmail", customerEmail);
        //                cmd.Parameters.AddWithValue("@CustomerPhone", customerPhone);

        //                conn.Open();
        //                cmd.ExecuteNonQuery();  // تنفيذ عملية التحديث
        //                conn.Close();
        //            }

        //            gvAppointments.EditIndex = -1;  // إلغاء وضع التحرير
        //            LoadAppointments();  // إعادة تحميل البيانات بعد التحديث
        //        }
        //        else
        //        {
        //            // إذا كانت هناك مشكلة في إيجاد الـ TextBox
        //            Response.Write("One or more controls not found.");
        //        }
        //    }
        //    else
        //    {
        //        Response.Write("Invalid RowIndex");
        //    }
        //}


    }
}
