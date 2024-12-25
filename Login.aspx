<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Login.aspx.cs" Inherits="medical_reservation.Login"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" style="margin: 20px 0px">
        <h1>تسجيل الدخول</h1>
        <hr />
        <div class="form-group">
            <label for="exampleInputEmail1">الإيميل</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="أدخل الإيميل" ></asp:TextBox>
            <small id="emailHelp" class="form-text text-muted">لا تقم بمشاركة الإيميل مع أحد</small>
        </div>
        <br /> 
        <div class="form-group">
            <label for="exampleInputPassword1">الرقم السرّي</label>
            <asp:TextBox ID="txtPassword" runat="server" type="password" CssClass="form-control" placeholder="أدخل الرقم السري"></asp:TextBox>
        </div>

        <br />

        <asp:Button ID="Button1" runat="server" Text="تسجيل الدخول" CssClass="btn btn-success" OnClick="Button1_Click" />
    </div>
</asp:Content>
