using System;
using System.Data.SqlClient;
using System.Configuration;

namespace medical_reservation
{
    public partial class AddDoctor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // Handle Save Button Click
        protected void btnSaveDoctor_Click(object sender, EventArgs e)
        {
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string specialty = txtSpecialty.Text.Trim();
            string city = txtCity.Text.Trim();

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(specialty))
            {
                lblMessage.Text = "Please fill all the fields.";
                lblMessage.Visible = true;
                return;
            }

            string query = "INSERT INTO Doctors (FirstName, LastName, Specialty, City) VALUES (@FirstName, @LastName, @Specialty,@City)";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MedResDBConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@Specialty", specialty);
                cmd.Parameters.AddWithValue("@City", city);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            Response.Redirect("DoctorsList.aspx");
        }

        // Handle Cancel Button Click
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("DoctorsList.aspx");
        }
    }
}
