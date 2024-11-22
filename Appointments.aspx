<%@ Page Title="Doctor Appointments" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Appointments.aspx.cs" Inherits="medical_reservation.Appointments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div class="container mt-5">
        <h2 class="text-center mb-4" style="color: #28a745;">Available Doctor Appointments</h2>
        <hr />

        <div class="row">
            <!-- Loop through doctors and appointments -->
            <asp:Repeater ID="rptDoctors" runat="server" OnItemDataBound="rptDoctors_ItemDataBound">
                <ItemTemplate>
                    <div class="col-md-4 mb-4">
                        <!-- Card for doctor -->
                        <div class="card shadow-sm border-light rounded">
                            <div class="card-body">
                                <h5 class="card-title text-success"><%# Eval("FirstName") %> <%# Eval("LastName") %></h5>
                                <p class="card-text"><strong>Specialty:</strong> <%# Eval("Specialty") %></p>

                                <!-- Display Available Appointments -->
                                <h6 class="mt-3" style="font-weight: bold;">Available Appointments:</h6>
                                <asp:Repeater ID="rptAppointments" runat="server">
                                    <ItemTemplate>
                                        <div class="mb-3 d-flex justify-content-between align-items-center">
                                            <span style="font-size: 1.1em;"><%# Eval("AppointmentDate", "{0:yyyy-MM-dd HH:mm}") %></span>
                                            <asp:Button ID="btnBook" runat="server" Text="Book Now" 
                                                        CssClass="btn btn-success btn-sm" CommandArgument='<%# Eval("AppointmentID") %>' 
                                                        OnClick="btnBook_Click" />
                                        </div>
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <div><strong>Available Slots:</strong></div>
                                    </HeaderTemplate>
                                </asp:Repeater>

                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
