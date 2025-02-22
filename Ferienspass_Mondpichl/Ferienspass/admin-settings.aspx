﻿<%@ Page Title="" Language="C#" MasterPageFile="~/admin-master.Master" AutoEventWireup="true" CodeBehind="admin-settings.aspx.cs" Inherits="Ferienspass.admin_settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function DeleteCity() {
            if (confirm("Es könnten bereits Personen unter dieser Gemeinde registriert sein! Soll die Gemeinde trotzdem entfernt werden?")) {
                return true;
            }
            return false;
        }
    </script>


    <div class="container">
        <br />
        <h3>Unsere Nachbargemeinden</h3>
        <asp:Literal ID="litAlertNeighbourcities" runat="server"></asp:Literal>
        <asp:GridView ID="gvNeighbourcities" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" CssClass="table table-hover" GridLines="None" Width="30%"
            OnRowEditing="gvNeighbourcities_RowEditing" OnRowCancelingEdit="gvNeighbourcities_RowCancelingEdit" OnRowDeleting="gvNeighbourcities_RowDeleting"
            OnRowCommand="gvNeighbourcities_RowCommand" OnRowUpdating="gvNeighbourcities_RowUpdating">
            <Columns>
                <asp:TemplateField HeaderText="PLZ">
                    <ItemTemplate>
                        <asp:Label ID="lblZipCode" runat="server" Text='<%# Eval("zipcode") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtZipCode" runat="server" Text='<%# Bind("zipcode") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Gemeinde">
                    <ItemTemplate>
                        <asp:Label ID="lblGemeinde" runat="server" Text='<%# Eval("city") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtGemeinde" runat="server" Text='<%# Bind("city") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:LinkButton ID="btnAddCity" runat="server" CommandName="Add" ToolTip="Partnergemeinde hinzufügen" ForeColor="Black" CssClass="float-right"><i class="fa fa-plus-square"></i></asp:LinkButton>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="float-right">
                            <asp:LinkButton ID="btnEditCity" runat="server" CommandName="Edit" ToolTip="Partnergemeinde bearbeiten" ForeColor="Black"><i class="fas fa-pen"></i></asp:LinkButton>
                            <asp:LinkButton ID="btnDeleteCity" runat="server" CommandName="Delete" ToolTip="Partnergemeinde entfernen" OnClientClick="return Delete()" ForeColor="Black"><i class="fa fa-trash"></i></asp:LinkButton>
                        </div>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <div class="float-right">
                            <asp:LinkButton ID="btnUpdateCity" runat="server" CommandName="Update" ToolTip="Änderungen speichern" ForeColor="Black"><i class="fa fa-check"></i></asp:LinkButton>
                            <asp:LinkButton ID="btnCancelCity" runat="server" CommandName="Cancel" ToolTip="Abbrechen" ForeColor="Black"><i class="fa fa-times"></i></asp:LinkButton>
                        </div>
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView> 
        <br />

        <h3>Email-Einstellungen</h3>
        Host und Port:
        <div class="row">
            <div class="col-4">
                <div class="form-group">
                    <asp:TextBox ID="txtHost" placeholder="Host" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
            </div>
            <div class="col-4">
                <div class="form-group">
                    <asp:TextBox ID="txtPort" placeholder="Port" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
            </div>
        </div>

        Email und Passwort:
        <div class="row">
            <div class="col-4">
                <div class="form-group">
                    <asp:TextBox ID="txtEmail" placeholder="Email" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
            </div>
            <div class="col-4">
                <div class="form-group">
                    <asp:TextBox ID="txtPassword" placeholder="Passwort" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
            </div>
        </div>

        Gültigkeitsdauer des PW-Forgotten-Links [in Tagen]:
        <div class="row">
            <div class="col-2">
                <div class="form-group">
                    <asp:TextBox ID="txtResetDauer" placeholder="Reset-Passwort-Dauer" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
            </div>

            <div class="col-10">
                <div class="form-group">
                    <asp:Panel ID="pnlChangeSettings" runat="server">
                        <asp:Button ID="btnChangeSettings" Text="Ändern" runat="server" CssClass="btn btn-secondary float-right" OnClick="btnChangeSettings_Click" />
                    </asp:Panel>
                </div>
            </div>
            <div class="col-10">
                <div class="form-group">
                    <asp:Panel ID="pnlCancelSettings" runat="server" Visible="false">
                        <asp:Button ID="btnCancelAdress" runat="server" Text="Abbrechen" CssClass="btn btn-secondary float-right" OnClick="btnCancelSettings_Click"/>
                    </asp:Panel>
                    <asp:Panel ID="pnlSaveSettings" runat="server" Visible="false">
                        <asp:Button ID="btnSaveSettings" Text="Speichern" runat="server" CssClass="btn btn-secondary float-right" OnClick="btnSaveSettings_Click" />
                    </asp:Panel>
                </div>
            </div>
        </div>

        <h3>Sonstige Einstellungen</h3>
        <asp:Literal ID="litAlertOtherSettings" runat="server"></asp:Literal>
        Anmeldezeitraum:
        <div class="row">
            <div class="col-4">
                <div class="form-group">
                    Von: <asp:TextBox ID="txtStartRegistrationSpan" placeholder="Startdatum" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
            </div>
            <div class="col-4">
                <div class="form-group">
                    Bis: <asp:TextBox ID="txtStopRegistrationSpan" placeholder="Enddatum" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
            </div>
        </div>
        Rabatt (bei Anmeldung von mind. 2 Kindern):
        <div class="row">
            <div class="col-2">
                <div class="form-group">
                    <asp:TextBox ID="txtDiscount" placeholder="Rabatt" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
            </div>
        </div>
        Gültigkeitsdauer des Warenkorbes in Stunden:
        <div class="row">
            <div class="col-2">
                <div class="form-group">
                    <asp:TextBox ID="txtBasketExpiryTime" placeholder="Warenkorbdauer" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
            </div>
            <div class="col-10">
                <div class="form-group">
                    <asp:Panel ID="pnlChangeOtherSettings" runat="server">
                        <asp:Button ID="btnChangeOtherSettings" Text="Ändern" runat="server" CssClass="btn btn-secondary float-right" OnClick="btnChangeOtherSettings_Click" />
                    </asp:Panel>
                </div>
            </div>
            <div class="col-10">
                <div class="form-group">
                    <asp:Panel ID="pnlCancelOtherSettings" runat="server" Visible="false">
                        <asp:Button ID="btnCancelOtherSettings" runat="server" Text="Abbrechen" CssClass="btn btn-secondary float-right" OnClick="btnCancelOtherSettings_Click"/>
                    </asp:Panel>
                    <asp:Panel ID="pnlSaveOtherSettings" runat="server" Visible="false">
                        <asp:Button ID="btnSaveOtherSettings" Text="Speichern" runat="server" CssClass="btn btn-secondary float-right" OnClick="btnSaveOtherSettings_Click" />
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
