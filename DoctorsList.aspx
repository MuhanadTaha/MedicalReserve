<%@ Page Title="Doctors List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DoctorsList.aspx.cs" Inherits="medical_reservation.DoctorsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2>قائمة الأطباء</h2>
        <hr />

        <!-- عرض الأطباء فقط للأدمن -->
        <% if (Session["Role"] != null && Session["Role"].ToString() == "admin") { %>
            <asp:GridView ID="gvDoctors" runat="server" AutoGenerateColumns="False"
                OnRowDeleting="gvDoctors_RowDeleting" CssClass="table table-bordered" 
                DataKeyNames="DoctorID">
                <Columns>
                    <asp:BoundField DataField="DoctorID" HeaderText="رقم الطبيب" SortExpression="DoctorID" />
                    <asp:BoundField DataField="fName" HeaderText="الاسم الأول" SortExpression="fName" />
                    <asp:BoundField DataField="lName" HeaderText="الاسم الأخير" SortExpression="lName" />
                    <asp:BoundField DataField="Specialty" HeaderText="التخصص" SortExpression="Specialty" />
                    <asp:BoundField DataField="City" HeaderText="المدينة" SortExpression="City" />
                    <asp:BoundField DataField="Address" HeaderText="العنوان" SortExpression="Address" />
                    <asp:CommandField ShowDeleteButton="True" DeleteText="حذف" />
                </Columns>
            </asp:GridView>
        <% } else { %>
            <p>لا تملك صلاحية للوصول إلى هذه الصفحة.</p>
        <% } %>
    </div>
</asp:Content>
