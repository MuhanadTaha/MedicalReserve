<%@ Page Title="Manage Appointments" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminAppointments.aspx.cs" Inherits="medical_reservation.AdminAppointments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2>إدارة المواعيد</h2>
        <hr />
        <asp:GridView ID="gvAppointments" runat="server" AutoGenerateColumns="False"
            OnRowDeleting="gvAppointments_RowDeleting"
            DataKeyNames="BookingID" CssClass="table table-bordered">
            <Columns>
                <asp:BoundField DataField="BookingID" HeaderText="رقم الحجز" SortExpression="BookingID" />
                <asp:BoundField DataField="AppointmentDate" HeaderText="تاريخ الموعد" SortExpression="AppointmentDate" />
                <asp:BoundField DataField="BookingDate" HeaderText="وقت الحجز" SortExpression="BookingDate" />
                <asp:BoundField DataField="CustomerName" HeaderText="اسم المريض" SortExpression="CustomerName" />
                <asp:BoundField DataField="CustomerEmail" HeaderText="ايميل المريض" SortExpression="CustomerEmail" />
                <asp:BoundField DataField="CustomerPhone" HeaderText="هاتف لمريض" SortExpression="CustomerPhone" />
               
                <asp:CommandField ShowDeleteButton="True" EditText="تعديل" DeleteText="حذف" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
