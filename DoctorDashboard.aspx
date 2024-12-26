<%@ Page Title="Doctor Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DoctorDashboard.aspx.cs" Inherits="medical_reservation.DoctorDashboard" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-5">
        <h2 class="text-center mb-4" style="color: #28a745;">إضافة بيانات الطبيب</h2>
        <hr />
        <div>
            <!-- حقول بيانات الطبيب -->
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

        <br /><hr />
        
        <!-- حقول إضافة المواعيد -->
        <h3 class="text-center mb-4" style="color: #007bff;">إضافة مواعيد جديدة</h3>
        <div>
            <div class="form-group">
                <label for="txtStartTime">وقت البداية</label>
                <asp:TextBox ID="txtStartTime" runat="server" CssClass="form-control" TextMode="DateTimeLocal" />
            </div>

            <div class="form-group">
                <label for="txtEndTime">وقت النهاية</label>
                <asp:TextBox ID="txtEndTime" runat="server" CssClass="form-control" TextMode="DateTimeLocal" />
            </div>

            <div class="form-group">
                <label for="chkAvailable">هل الموعد متاح؟</label>
                <asp:CheckBox ID="chkAvailable" runat="server" />
            </div>

            <button type="submit" class="btn btn-primary" runat="server" onserverclick="btnAddAppointment_Click">إضافة الموعد</button>
        </div>

        <br /><hr />

        <!-- جدول المواعيد المعروضة -->
        <h3 class="text-center mb-4" style="color: #007bff;">المواعيد المتاحة</h3>
        <asp:GridView ID="gvAppointments" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
            <Columns>
                <asp:BoundField DataField="StartTime" HeaderText="وقت البداية" SortExpression="StartTime" />
                <asp:BoundField DataField="EndTime" HeaderText="وقت النهاية" SortExpression="EndTime" />
                <asp:BoundField DataField="Available" HeaderText="متاح" SortExpression="Available" />
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>
