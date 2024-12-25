<%@ Page Title="Add New Doctor" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddDoctor.aspx.cs" Inherits="medical_reservation.AddDoctor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div dir="rtl">
    <h2>إضافة طبيب جديد</h2>

    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Visible="false"></asp:Label>
    
    <div class="form-group">
        <label for="txtFirstName">الاسم الأول</label>
        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
    </div>

    <br />

    <div class="form-group">
        <label for="txtLastName">الاسم الأخير</label>
        <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
    
    <br />

    <div class="form-group">
        <label for="txtSpecialty">التخصص</label>
        <asp:TextBox ID="txtSpecialty" runat="server" CssClass="form-control"></asp:TextBox>
    </div>

    <br />

    <div class="form-group">
        <label for="txtCity">المدينة</label>
        <asp:TextBox ID="txtCity" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
    <br />


    <asp:Button ID="btnSaveDoctor" runat="server" Text="حفظ" OnClick="btnSaveDoctor_Click" CssClass="btn btn-primary" />
    <asp:Button ID="btnCancel" runat="server" Text="إغلاف" OnClick="btnCancel_Click" CssClass="btn btn-secondary" />
</div>
</asp:Content>
