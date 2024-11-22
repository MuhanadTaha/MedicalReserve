<%@ Page Title="Manage Appointments" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminAppointments.aspx.cs" Inherits="medical_reservation.AdminAppointments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2>Manage Appointments</h2>
        <hr />
        <asp:GridView ID="gvAppointments" runat="server" AutoGenerateColumns="False"
           
            OnRowDeleting="gvAppointments_RowDeleting"
            DataKeyNames="BookingID" CssClass="table table-bordered">
            <Columns>
                <asp:BoundField DataField="BookingID" HeaderText="Booking ID" SortExpression="BookingID" />
                <asp:BoundField DataField="AppointmentID" HeaderText="Appointment ID" SortExpression="AppointmentID" />
                <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" SortExpression="CustomerName" />
                <asp:BoundField DataField="CustomerEmail" HeaderText="Customer Email" SortExpression="CustomerEmail" />
                <asp:BoundField DataField="CustomerPhone" HeaderText="Customer Phone" SortExpression="CustomerPhone" />
                <asp:CommandField  ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
