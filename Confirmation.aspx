<%@ Page Title="Booking Confirmation" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Confirmation.aspx.cs" Inherits="medical_reservation.Confirmation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2>Your Appointment Has Been Booked Successfully!</h2>
        <hr />
        
        <h3>Booking Details:</h3>
        <div class="form-group">
            <label for="CustomerName">Name:</label>
            <asp:Label ID="lblCustomerName" runat="server" Text=""></asp:Label>
        </div>

        <div class="form-group">
            <label for="CustomerEmail">Email:</label>
            <asp:Label ID="lblCustomerEmail" runat="server" Text=""></asp:Label>
        </div>

        <div class="form-group">
            <label for="CustomerPhone">Phone:</label>
            <asp:Label ID="lblCustomerPhone" runat="server" Text=""></asp:Label>
        </div>

        <div class="form-group">
            <label for="AppointmentID">Appointment ID:</label>
            <asp:Label ID="lblAppointmentID" runat="server" Text=""></asp:Label>
        </div>

        <div>
            <a href="Default.aspx" class="btn btn-primary">Back to Homepage</a>
        </div>
    </div>
</asp:Content>
