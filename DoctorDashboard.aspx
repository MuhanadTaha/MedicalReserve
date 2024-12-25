<%@ Page Title="Doctor Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DoctorDashboard.aspx.cs" Inherits="medical_reservation.DoctorDashboard" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-5">
        <h2 class="text-center mb-4" style="color: #28a745;">إضافة بيانات الطبيب</h2>
        <hr />
        <div >
            <div class="form-group">
                <label for="txtSpecialty">التخصص</label>
                <input type="text" class="form-control" id="txtSpecialty" runat="server" />
            </div>

            <div class="form-group">
                <label for="txtCity">المدينة</label>
                <input type="text" class="form-control" id="txtCity" runat="server" />
            </div>

            <div class="form-group">
                <label for="txtAddress">العنوان</label>
                <input type="text" class="form-control" id="txtAddress" runat="server" />
            </div>

            <br />
            <div class="form-group">
                <label for="fileImage">الصورة</label>
                <asp:FileUpload id="fileImage" runat="server" class="form-control-file" />

            </div>
            <br />
            <button type="submit" class="btn btn-success" runat="server" onserverclick="btnSave_Click">حفظ البيانات</button>
        </div>
    </div>

</asp:Content>
