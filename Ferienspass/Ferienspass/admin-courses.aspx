<%@ Page Title="" Language="C#" MasterPageFile="~/admin-master.Master" AutoEventWireup="true" CodeBehind="admin-courses.aspx.cs" Inherits="Ferienspass.admin_courses" Theme="default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <h1>Kurse</h1>
        </div>
        <div class="gvcourses">
            <asp:GridView ID="gvCourses" runat="server" CssClass="table" AutoGenerateColumns="False" AllowPaging="True" PageSize="20" DataKeyNames="coursID"
                ShowHeaderWhenEmpty="true" OnRowEditing="gvCourses_RowEditing" OnRowCancelingEdit="gvCourses_RowCancelingEdit" OnRowUpdating="gvCourses_RowUpdating"
                OnPageIndexChanging="gvCourses_PageIndexChanging" OnSorting="gvCourses_Sorting">
                <Columns>
                    <asp:TemplateField HeaderText="Kursname">
                        <ItemTemplate>
                            <asp:Label ID="lblCoursName" runat="server" Text='<%# Eval("coursname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Organisation">
                        <ItemTemplate>
                            <asp:Label ID="lblOrganisation" runat="server" Text='<%# Eval("organisationname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Teilnehmer">
                        <ItemTemplate>
                            <asp:Label ID="lblTeilnehmer" runat="server" Text='<%# Eval("cntparticipants") + "/" + Eval("maxparticipants") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Teilnehmer">
                        <HeaderTemplate>
                            <asp:LinkButton ID="btnNewCours" runat="server" OnClick="btnNewCours_Click" ForeColor="Black"><i class='fas fa-plus-square' style='font-size:24px;'></i></asp:LinkButton>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="btnUpdate" runat="server" CommandName="Edit" ForeColor="Black"><i class='fa fa-pen' style='font-size:24px;'></i></i></asp:LinkButton>
                            <asp:LinkButton ID="btnMail" runat="server" OnClick="btnMail_Click"><i class="fas fa-envelope"></i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <asp:Panel ID="panBlockBackground" runat="server" CssClass="panBlockBackground" Visible="false"></asp:Panel>
    <asp:Panel ID="panNewCours" runat="server" Enabled="false">
        <div class="container">
            <div class="addCoursForm ">
                
            </div>
        </div>
    </asp:Panel>
</asp:Content>
