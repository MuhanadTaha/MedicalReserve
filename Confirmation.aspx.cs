using System;
using System.Web.UI;

namespace medical_reservation
{
    public partial class Confirmation : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // تأكد من أن الحجز تم بنجاح
                if (Session["CustomerName"] != null && Session["CustomerEmail"] != null && Session["CustomerPhone"] != null && Session["AppointmentID"] != null)
                {
                    // قراءة البيانات من الـ Session وعرضها
                    lblCustomerName.Text = Session["CustomerName"].ToString();
                    lblCustomerEmail.Text = Session["CustomerEmail"].ToString();
                    lblCustomerPhone.Text = Session["CustomerPhone"].ToString();
                    lblAppointmentID.Text = Session["AppointmentID"].ToString();
                }
                else
                {
                    // في حال لم تكن هناك بيانات صحيحة في الـ Session، إعادة التوجيه إلى صفحة الخطأ أو الصفحة الرئيسية
                    Response.Redirect("ErrorPage.aspx");
                }
            }
        }
    }
}
