<%@ Page Title="" Language="C#" MasterPageFile="~/user-master.Master" AutoEventWireup="true" CodeBehind="user-settings.aspx.cs" Inherits="Ferienspass.User_Settings" %>

<%-- Programmer: Kollross Marcel
    Date: 26.11.2019
    Verified by: N/A--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<br />
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
        <asp:Literal ID="litGenderError" runat="server"></asp:Literal>
        <asp:GridView ID="gvKids" runat="server" AutoGenerateColumns="false" DataKeyNames="kidId" ShowHeaderWhenEmpty="true" CssClass="table table-striped" OnRowCancelingEdit="gvKids_RowCancelingEdit" OnRowEditing="gvKids_RowEditing" OnRowCommand="gvKids_RowCommand" OnRowUpdating="gvKids_RowUpdating" OnRowDeleting="gvKids_RowDeleting" OnRowDataBound="gvKids_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="Vorname">
                    <ItemTemplate>
                        <asp:Label ID="lblGivenname" runat="server" Text='<%# Eval("givenname") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtGivennameChild" runat="server" placeholder="Vorname" Text='<%# Bind("givenname") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Nachname">
                    <ItemTemplate>
                        <asp:Label ID="lblSurname" runat="server" Text='<%# Eval("surname") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtSurnameChild" runat="server" placeholder="Nachname" Text='<%# Bind("surname") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Geschlecht">
                    <ItemTemplate>
                        <asp:Label ID="lblGender" runat="server" Text='<%# Eval("gendername") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlGender" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Geburtstag">
                    <ItemTemplate>
                        <asp:Label ID="lblBirthday" runat="server" Text='<%# Eval("birthday", "{0:dd/MM/yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtBirthday" runat="server" TextMode="Date" Text='<%# Bind("birthday", "{0:yyyy-MM-dd}") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:LinkButton ID="btnAddChild" runat="server" CommandName="Add" ForeColor="Black" ><i class="fa fa-plus-square"></i></asp:LinkButton>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEditChild" runat="server" CommandName="Edit" ForeColor="Black"><i class="fas fa-pen"></i></asp:LinkButton>
                        <asp:LinkButton ID="btnDeleteChild" runat="server" CommandName="Delete" ForeColor="Black"><i class="fa fa-trash"></i></asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="btnUpdateChild" runat="server" CommandName="Update" ForeColor="Black"><i class="fa fa-check"></i></asp:LinkButton>
                        <asp:LinkButton ID="btnCancelChild" runat="server" CommandName="Cancel" ForeColor="Black"><i class="fa fa-times"></i></asp:LinkButton>
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView> 
    </div>
</asp:Content>
