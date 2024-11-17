
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Register.aspx.cs" Inherits="medical_reservation.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5 mb-5">
        <div class="row justify-content-center">
            <div class="col-md-8 col-lg-6">
                <div class="card shadow-lg">
                    <div class="card-header text-center bg-primary text-white">
                        <h2>Sign Up</h2>
                    </div>
                    <div class="card-body">
                        
                            <div class="form-group row">
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" placeholder="First Name"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFirstName"
                                        ErrorMessage="First Name is required." ToolTip="First Name required." CssClass="text-danger" />
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" placeholder="Last Name"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLastName"
                                        ErrorMessage="Last Name is required." ToolTip="Last Name required." CssClass="text-danger" />
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtDOB" runat="server" type="date" CssClass="form-control" placeholder="Date of Birth"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDOB"
                                        ErrorMessage="DOB is required." ToolTip="DOB required." CssClass="text-danger" />
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtMobile" runat="server" type="tel" CssClass="form-control" placeholder="Mobile Number"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtMobile"
                                        ErrorMessage="Mobile is required." ToolTip="Mobile is required." CssClass="text-danger" />
                                    <asp:RegularExpressionValidator ID="RegExp1" runat="server"
                                        ErrorMessage="Mobile number must be 10 digits."
                                        ControlToValidate="txtMobile" CssClass="text-danger"
                                        ValidationExpression="^\d{10}$" />
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtEmail" runat="server" type="email" CssClass="form-control" placeholder="Email"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEmail"
                                        ErrorMessage="Email is required." ToolTip="Email is required." CssClass="text-danger" />
                                </div>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Select Gender" Value="" />
                                        <asp:ListItem Text="Male" Value="Male" />
                                        <asp:ListItem Text="Female" Value="Female" />
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtPassword" runat="server" type="password" CssClass="form-control" placeholder="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPassword"
                                        ErrorMessage="Password is required." ToolTip="Password required." CssClass="text-danger" />
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtCPassword" runat="server" type="password" CssClass="form-control" placeholder="Confirm Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtCPassword"
                                        ErrorMessage="Confirm Password is required." ToolTip="Confirm Password required." CssClass="text-danger" />
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtCPassword"
                                        ControlToCompare="txtPassword" Operator="Equal" ErrorMessage="Passwords must match."
                                        CssClass="text-danger" Display="Dynamic" />
                                </div>
                            </div>

                            <div class="form-group text-center">
                                <asp:Button ID="btnSubmit" runat="server" Text="Register" CssClass="btn btn-success btn-block" OnClick="btnSubmit_Click" />
                            </div>
                 
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
