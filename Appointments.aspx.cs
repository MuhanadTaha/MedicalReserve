using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace medical_reservation
{
    public partial class Appointments : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDoctors();
            }
        }

        private void BindDoctors()
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["MedResDBConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT DoctorID, FirstName, LastName, Specialty FROM Doctors";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptDoctors.DataSource = dt;
                rptDoctors.DataBind();
            }
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
                    string query = "SELECT AppointmentID, AppointmentDate FROM Appointments WHERE DoctorID = @DoctorID";
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
            Response.Redirect($"BookAppointment.aspx?AppointmentID={appointmentID}");
        }
    }
}
