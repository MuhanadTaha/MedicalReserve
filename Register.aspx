<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Register.aspx.cs" Inherits="medical_reservation.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5 mb-5">
        <div class="row justify-content-center">
            <div class="col-md-8 col-lg-6">
                <div class="card shadow-lg">
                    <div class="card-header text-center bg-primary text-white">
                        <h2>تسجيل حساب جديد</h2>
                    </div>
                    <div class="card-body">

                        <!-- First and Last Name -->
                        <div class="form-group row">
                            <div class="col-md-6">
                                <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" placeholder="الاسم الأول"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFirstName"
                                    ErrorMessage="الاسم الأول مطلوب" ToolTip="First Name required." CssClass="text-danger" />
                            </div>
                            <div class="col-md-6">
                                <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" placeholder="الاسم الأخير"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLastName"
                                    ErrorMessage="الاسم الأخير مطلوب" ToolTip="Last Name required." CssClass="text-danger" />
                            </div>
                        </div>

                        <!-- Date of Birth and Mobile Number -->
                        <div class="form-group row">
                            <div class="col-md-6">
                                <asp:TextBox ID="txtDOB" runat="server" type="date" CssClass="form-control" placeholder="تاريخ الميلاد"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDOB"
                                    ErrorMessage="تاريخ الميلاد مطلوب" ToolTip="DOB required." CssClass="text-danger" />
                            </div>
                            <div class="col-md-6">
                                <asp:TextBox ID="txtMobile" runat="server" type="number" CssClass="form-control" placeholder="رقم الهاتف"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtMobile"
                                    ErrorMessage="رقم الهاتف مطلوب" ToolTip="Mobile is required." CssClass="text-danger" />
                                <asp:RegularExpressionValidator ID="RegExp1" runat="server"
                                    ErrorMessage="Mobile number must be 10 digits."
                                    ControlToValidate="txtMobile" CssClass="text-danger"
                                    ValidationExpression="^\d{10}$" />
                            </div>
                        </div>

                        <!-- Email and Gender -->
                        <div class="form-group row">
                            <div class="col-md-6">
                                <asp:TextBox ID="txtEmail" runat="server" type="email" CssClass="form-control" placeholder="البريد الإلكتروني"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEmail"
                                    ErrorMessage="البريد الإلكتروني مطلوب" ToolTip="Email is required." CssClass="text-danger" />
                            </div>
                            <div class="col-md-6">
                             
                                <asp:DropDownList ID="ddlGender" CssClass="form-control" runat="server">
                                    <asp:ListItem>ذكر</asp:ListItem>
                                    <asp:ListItem>أنثى</asp:ListItem>

                                </asp:DropDownList>
                            </div>
                        </div>

                        <!-- Password and Confirm Password -->
                        <div class="form-group row">
                            <div class="col-md-6">
                                <asp:TextBox ID="txtPassword" runat="server" type="password" CssClass="form-control" placeholder="الرقم السري"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPassword"
                                    ErrorMessage="الرقم السري مطلوب" ToolTip="Password required." CssClass="text-danger" />
                            </div>
                            <div class="col-md-6">
                                <asp:TextBox ID="txtCPassword" runat="server" type="password" CssClass="form-control" placeholder="تأكيد الرقم السري"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtCPassword"
                                    ErrorMessage="تأكيد الرقم السري مطلوب" ToolTip="Confirm Password required." CssClass="text-danger" />
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtCPassword"
                                    ControlToCompare="txtPassword" Operator="Equal" ErrorMessage="الرقم السري يجب أن يكون متطابق"
                                    CssClass="text-danger" Display="Dynamic" />
                            </div>
                        </div>

                        <!-- Role Selection -->

                        <div class="form-group row">
                            <div class="col-md-12">
                                <label class="form-check-label">اختار الدور</label>
                                <div class="form-check form-check-inline">
                                    <asp:RadioButton ID="rbDoctor" runat="server" GroupName="Role" Text="طبيب" />
                                </div>
                                <div class="form-check form-check-inline">
                                    <asp:RadioButton ID="rbAdmin" runat="server" GroupName="Role" Text="مسؤول" />
                                </div>
                            </div>
                        </div>


                        <br />

                        <!-- Submit Button -->
                        <div class="form-group text-center">
                            <asp:Button ID="btnSubmit" runat="server" Text="إنشاء حساب" CssClass="btn btn-success btn-block" OnClick="btnSubmit_Click" />
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
