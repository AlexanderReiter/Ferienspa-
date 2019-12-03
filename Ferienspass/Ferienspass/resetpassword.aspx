<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="resetpassword.aspx.cs" Inherits="Ferienspass.resetpassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Passwort zurücksetzen</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <div class="pwforgotform shadow p-4 mb-4 bg-white">
        <div class="row">
            <div class="col-12"><h3 style="text-align:center;">Passwort zurücksetzen</h3></div>
            </div>
            <br />
                <asp:Literal ID="litResetFailed" runat="server"></asp:Literal>
            <div class="row">
                <div class="col">
                    <div class="form-group">
                        <asp:TextBox ID="txtNewPw1" runat="server" placeholder="Neues Passwort" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <div class="form-group">
                        <asp:TextBox ID="txtNewPw2" runat="server" placeholder="Passwort wiederholen" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <div class="form-group">
                        <asp:Button ID="btnResetPw" runat="server" Text="Passwort zurücksetzen" OnClick="btnResetPw_Click" CssClass="btn btn-secondary w-100" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
