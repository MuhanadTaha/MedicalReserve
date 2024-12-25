<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="medical_reservation._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Link to custom CSS for additional styles -->
    <link rel="stylesheet" href="<%= ResolveUrl("~/Content/css_style.css") %>" />
    <!-- Add Bootstrap CDN for styling -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" />

    <div class="container mt-5">
        <!-- City Selection Dropdown -->
        <div class="row mb-4">
            <div class="col-md-4 offset-md-4">
                <label for="ddlCities" class="form-label">اختر المدينة</label>
                <asp:DropDownList ID="ddlCities" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCities_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Text="اختر المدينة" Value="" />
                </asp:DropDownList>
            </div>
        </div>

        <h2 class="text-center mb-4 text-success" style="font-weight: bold; text-shadow: 2px 2px 4px rgba(0,0,0,0.2);">مواعيد الدكاترة المتوفرة</h2>
        <hr />

        <div class="row">
            <!-- Loop through doctors and appointments -->
            <asp:Repeater ID="rptDoctors" runat="server" OnItemDataBound="rptDoctors_ItemDataBound">
                <ItemTemplate>
                    <div class="col-md-4 mb-4">
                        <!-- Card for doctor -->
                        <div class="card shadow-lg border-light rounded" style="transition: transform 0.3s ease-in-out;">
                            <div class="card-body">
                                <h5 class="card-title text-success text-center"><%# Eval("fName") %> <%# Eval("lName") %></h5>
                                <p class="card-text text-muted"><strong>التخصص:</strong> <%# Eval("Specialty") %></p>

                                <!-- Display Available Appointments -->
                                <h6 class="mt-3 font-weight-bold text-primary">المواعيد المتوفرة:</h6>
                                <asp:Repeater ID="rptAppointments" runat="server">
                                    <ItemTemplate>
                                        <!-- Set fixed height and width for images with object-fit for cropping -->
                                        <img runat="server" src='<%# Eval("image") == DBNull.Value ? "~/images/maleDoctor.jpg" : Eval("image") %>' alt="Doctor Image"
                                            class="card-img-top doctor-image img-fluid rounded-circle" style="height: 200px; object-fit: cover;" />
                                        <div class="mb-3 d-flex justify-content-between align-items-center">
                                            <span style="font-size: 1.1em; color:gray" class="p-3">
                                                <%# Eval("StartTime", "{0:yyyy-MM-dd HH:mm}") %> 
                                                <br />
                                                <%# Eval("EndTime", "{0:yyyy-MM-dd HH:mm}") %>
                                            </span>
                                            <asp:Button ID="btnBook" runat="server" Text="حجز الآن"
                                                CssClass="btn btn-success btn-sm rounded-pill px-4 py-2" 
                                                CommandArgument='<%# Eval("AppointmentID") %>' OnClick="btnBook_Click" />
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>

                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

        </div>
    </div>

    <!-- Bootstrap JS and dependencies -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>

    <style>
        /* Custom styles for doctor cards */
        .card:hover {
            transform: scale(1.05);
            cursor: pointer;
        }

        /* Image styling for doctor image */
        .doctor-image {
            border-radius: 50%;
            object-fit: cover;
            width: 100%;
            height: 200px;
        }

        .card-title {
            font-size: 1.5rem;
            font-weight: bold;
        }

        .card-text {
            font-size: 1rem;
            color: #6c757d;
        }

        /* Add some custom padding to the buttons */
        .btn-success {
            font-size: 1rem;
            padding: 10px 20px;
            width: auto;
        }
    </style>

</asp:Content>
