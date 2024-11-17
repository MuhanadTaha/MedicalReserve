<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Login.aspx.cs" Inherits="medical_reservation.Login"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" style="margin: 20px 0px">
        <h1>Login</h1>
        <hr />
        <div class="form-group">
            <label for="exampleInputEmail1">Email address</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter Email" ></asp:TextBox>
            <small id="emailHelp" class="form-text text-muted">We'll never share your email with anyone else.</small>
        </div>
        <div class="form-group">
            <label for="exampleInputPassword1">Password</label>
            <asp:TextBox ID="txtPassword" runat="server" type="password" CssClass="form-control" placeholder="Enter Password"></asp:TextBox>
        </div>

        <br />

        <asp:Button ID="Button1" runat="server" Text="Login" CssClass="btn btn-success" OnClick="Button1_Click" />
    </div>
</asp:Content>
