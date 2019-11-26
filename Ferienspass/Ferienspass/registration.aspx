<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="registration.aspx.cs" Inherits="Ferienspass.registration" Theme="default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="registration-container border border-secondary">
            <asp:literal ID="litAlert" runat="server"></asp:literal>
            <div class="row">
                <div class="col">
                    <h3>Ferienspaß-Konto erstellen</h3>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <div class="form-group">
                        <asp:TextBox ID="txtEMail" runat="server" placeholder="E-Mail" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <div class="form-group">
                        <asp:TextBox ID="txtPassword" runat="server" placeholder="Passwort" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-6">
                    <div class="form-group">
                        <asp:TextBox ID="txtFirstname" runat="server" placeholder="Vorname" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-6">
                    <div class="form-group">
                        <asp:TextBox ID="txtLastname" runat="server" placeholder="Nachname" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-3">
                    <div class="form-group">
                        <asp:TextBox ID="txtPLZ" runat="server" placeholder="PLZ" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-9">
                    <div class="form-group">
                        <asp:TextBox ID="txtCity" runat="server" placeholder="Ort" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-10">
                    <div class="form-group">
                        <asp:TextBox ID="txtStreet" runat="server" placeholder="Straße" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-2">
                    <div class="form-group">
                        <asp:TextBox ID="txtNumber" runat="server" placeholder="Nr." CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-6">
                    <asp:Button ID="btnCancel" runat="server" Text="Stattdessen anmelden" CssClass="btn btn-outline-secondary btn-lg" />
                </div>
                <div class="col-6">
                    <asp:Button ID="btnRegister" runat="server" Text="Registrieren" CssClass="btn btn-secondary btn-lg float-right" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
