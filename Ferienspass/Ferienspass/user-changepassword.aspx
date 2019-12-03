<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user-changepassword.aspx.cs" Inherits="Ferienspass.user_changepassword" Theme="default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="loginform shadow p-4 mb-4 bg-white">
                <div class="row">
                    <div class="col-12"><h1 style="text-align:center;">Passwort ändern</h1></div>
                </div>
                <br />
                <asp:Literal ID="litPasswordError" runat="server"></asp:Literal>
                <div class="row">
                    <div class="col">
                        <div class="form-group">
                            <asp:TextBox ID="txtOldPassword" runat="server" CssClass="form-control" TextMode="Password" AutoPostBack="true" placeholder="Altes Passwort"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <div class="form-group">
                            <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control" TextMode="Password" AutoPostBack="true" placeholder="Neues Passwort"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <div class="form-group">
                            <asp:TextBox ID="txtRepeatPassword" runat="server" CssClass="form-control" TextMode="Password" AutoPostBack="true" placeholder="Neues Passwort wiederholen"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <div class="form-group">
                            <asp:Button ID="btnSavePassword" runat="server" CssClass="btn btn-secondary w-100" Text="Speichern" OnClick="btnSavePassword_Click" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <div class="form-group">
                            <asp:Button ID="btnCancelPassword" runat="server" CssClass="btn btn-outline-secondary w-100" Text="Abbrechen" OnClick="btnCancelPassword_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
