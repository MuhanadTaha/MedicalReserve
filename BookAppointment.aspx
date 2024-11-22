<%@ Page Title="Book Appointment" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="BookAppointment.aspx.cs" Inherits="medical_reservation.BookAppointment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2>Book Appointment</h2>
        <hr />
        
        <!-- الفورم الخاص بالحجز -->
        <div class="form-group">
            <label for="CustomerName">Name</label>
            <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control" placeholder="Enter your name"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="CustomerEmail">Email</label>
            <asp:TextBox ID="txtCustomerEmail" runat="server" CssClass="form-control" placeholder="Enter your email"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="CustomerPhone">Phone</label>
            <asp:TextBox ID="txtCustomerPhone" runat="server" CssClass="form-control" placeholder="Enter your phone number"></asp:TextBox>
        </div>

        <!-- HiddenField لحفظ AppointmentID -->
        <asp:HiddenField ID="hfAppointmentID" runat="server" />

        <br />
        <!-- زر لتأكيد الحجز -->
        <asp:Button ID="btnSubmit" runat="server" Text="Confirm Booking" CssClass="btn btn-success" OnClick="btnSubmit_Click" />
    </div>
</asp:Content>
