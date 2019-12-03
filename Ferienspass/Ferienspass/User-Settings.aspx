<%@ Page Title="" Language="C#" MasterPageFile="~/user-master.Master" AutoEventWireup="true" CodeBehind="user-settings.aspx.cs" Inherits="Ferienspass.User_Settings" %>

<%-- Programmer: Kollross Marcel
    Date: 26.11.2019
    Verified by: N/A--%>

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
                    <EditItemTemplate>
                        <asp:TextBox ID="txtGivennameChild" runat="server" Text='<%# Bind("givenname") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Nachname">
                    <ItemTemplate>
                        <asp:Label ID="lblSurname" runat="server" Text='<%# Eval("surname") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtSurnameChild" runat="server" Text='<%# Bind("surname") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Geschlecht">
                    <ItemTemplate>
                        <asp:Label ID="lblGender" runat="server" Text='<%# Eval("gender") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtGender" runat="server" Text='<%# Bind("gender") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Geburtstag">
                    <ItemTemplate>
                        <asp:Label ID="lblBirthday" runat="server" Text='<%# Eval("birthday", "{0:dd/MM/yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtBirthday" runat="server" Text='<%# Bind("birthday", "{0:dd/MM/yyyy}") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:ImageButton ID="btnAddChild" runat="server" CommandName="Add" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:ImageButton ID="btnEditChild" runat="server" CommandName="Edit" />
                        <asp:ImageButton ID="btnDeleteChild" runat="server" CommandName="Delete" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:ImageButton ID="btnUpdateChild" runat="server" CommandName="Update" />
                        <asp:ImageButton ID="btnCancelChild" runat="server" CommandName="Cancel" />
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView> 
    </div>
</asp:Content>
