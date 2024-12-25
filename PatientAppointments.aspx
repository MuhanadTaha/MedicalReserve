<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PatientAppointments.aspx.cs" Inherits="medical_reservation.PatientAppointments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2>حجوزاتي</h2>
        <hr />
        <asp:GridView ID="gvCustomerAppointments" runat="server" AutoGenerateColumns="False"
            OnRowDeleting="gvCustomerAppointments_RowDeleting"
            DataKeyNames="BookingID" CssClass="table table-bordered">
            <Columns>
                <asp:BoundField DataField="BookingID" HeaderText="رقم الحجز" SortExpression="BookingID" />
                <asp:BoundField DataField="AppointmentDate" HeaderText="تاريخ الموعد" SortExpression="AppointmentDate" />
                <asp:BoundField DataField="DoctorName" HeaderText="اسم الطبيب" SortExpression="DoctorName" />
                <asp:BoundField DataField="CustomerName" HeaderText="اسم المريض" SortExpression="CustomerName" />
                <asp:BoundField DataField="CustomerEmail" HeaderText="ايميل المريض" SortExpression="CustomerEmail" />
                <asp:BoundField DataField="CustomerPhone" HeaderText="هاتف المريض   " SortExpression="CustomerPhone" />
                <asp:CommandField ShowDeleteButton="True" DeleteText="إلغاء الحجز" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>