<%@ Page Title="" Language="C#" MasterPageFile="~/user-master.Master" AutoEventWireup="true" CodeBehind="user-settings.aspx.cs" Inherits="Ferienspass.User_Settings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-10">
                <div class="form-group">
                    <asp:TextBox ID="txtEmail" placeholder="Email" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
            </div>
<%--            <div class="col-4">
                <div class="form-group">
                    <asp:Panel ID="pnlChangeEmail" runat="server">
                        <asp:Button ID="btnChangeEmail" Text="Ändern" runat="server" CssClass="btn btn-secondary float-left" OnClick="btnChangeEmail_Click" />
                    </asp:Panel>
                    <asp:Panel ID="pnlSaveCancelEmail" runat="server" Visible="false">
                        <asp:Button ID="btnSaveEmail" Text="Speichern" runat="server" CssClass="btn btn-secondary float-left" OnClick="btnSaveEmail_Click" />
                        &nbsp;
                        <asp:Button ID="btnCancelEmail" Text="Abbrechen" runat="server" CssClass="btn btn-secondary" OnClick="btnCancelEmail_Click" />
                    </asp:Panel>
                </div>
            </div>--%>
            <div class="col-2">
                <div class="form-group">
                    <asp:Button ID="btnChangePassword" Text="Passwort ändern" runat="server" CssClass="btn btn-secondary float-right" OnClick="btnChangePassword_Click" />
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-5">
                <div class="form-group">
                    <asp:TextBox ID="txtGivenname" placeholder="Vorname" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
            </div>
            <div class="col-5">
                <div class="form-group">
                    <asp:TextBox ID="txtSurname" placeholder="Nachname" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
            </div>
            <div class="col-2">
                <div class="form-group">
                    <asp:Panel ID="pnlChangeName" runat="server">
                        <asp:Button ID="btnChangeName" Text="Ändern" runat="server" CssClass="btn btn-secondary float-right" OnClick="btnChangeName_Click" />
                    </asp:Panel>
                    <asp:Panel ID="pnlSaveName" runat="server" Visible="false">
                        <asp:Button ID="btnSaveName" Text="Speichern" runat="server" CssClass="btn btn-secondary float-right" OnClick="btnSaveName_Click" />
                    </asp:Panel>
                </div>
            </div>
        </div>
        <asp:Panel ID="pnlCancelName" runat="server" Visible="false">
            <div class="row">
                <div class="col-10">
                </div>
                <div class="col-2">
                    <asp:Button ID="btnCancelName" Text="Abbrechen" runat="server" CssClass="btn btn-secondary float-right" OnClick="btnCancelName_Click" />
                </div>
            </div>
        </asp:Panel>
        <br />
        <div class="row">
            <div class="col-3">
                <div class="form-group">
                    <asp:TextBox ID="txtZIP" placeholder="PLZ" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
            </div>
            <div class="col-7">
                <div class="form-group">
                    <asp:TextBox ID="txtCity" placeholder="Ort" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
            </div>
            <div class="col-2">
                <div class="form-group">
                    <asp:Panel ID="pnlChangeAdress" runat="server">
                        <asp:Button ID="btnChangeAdress" Text="Ändern" runat="server" CssClass="btn btn-secondary float-right" OnClick="btnChangeAdress_Click" />
                    </asp:Panel>
                    <asp:Panel ID="pnlSaveAdress" runat="server" Visible="false">
                        <asp:Button ID="btnSaveAdress" Text="Speichern" runat="server" CssClass="btn btn-secondary float-right" OnClick="btnSaveAdress_Click" />
                    </asp:Panel>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-8">
                <div class="form-group">
                    <asp:TextBox ID="txtStreet" placeholder="Straße" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
            </div>
            <div class="col-2">
                <div class="form-group">
                    <asp:TextBox ID="txtNr" placeholder="Hausnr." runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
            </div>
            <div class="col-2">
                <div class="form-group">
                    <asp:Panel ID="pnlCancelAdress" runat="server" Visible="false">
                        <asp:Button ID="btnCancelAdress" runat="server" Text="Abbrechen" CssClass="btn btn-secondary float-right" OnClick="btnCancelAdress_Click" />
                    </asp:Panel>
                </div>
            </div>
        </div>
        <br />
        <asp:GridView ID="gvKids" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" CssClass="table table-striped">
            <Columns>
                <asp:TemplateField HeaderText="Vorname">
                    <ItemTemplate>
                        <asp:Label ID="lblGivenname" runat="server" Text='<%# Eval("givenname") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Nachname">
                    <ItemTemplate>
                        <asp:Label ID="lblSurname" runat="server" Text='<%# Eval("surname") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Geschlecht">
                    <ItemTemplate>
                        <asp:Label ID="lblGender" runat="server" Text='<%# Eval("gender") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Geburtstag">
                    <ItemTemplate>
                        <asp:Label ID="lblBirthday" runat="server" Text='<%# Eval("birthday", "{0:dd/MM/yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView> 
    </div>
</asp:Content>
