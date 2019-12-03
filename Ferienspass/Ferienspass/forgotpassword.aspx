<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="forgotpassword.aspx.cs" Inherits="Ferienspass.forgotpassword" Theme="default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Passwort vergessen</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <div class="pwforgotform shadow p-4 mb-4 bg-white">
            <div class="row">
                <div class="col-12"><h3 style="text-align:center;">Passwort vergessen</h3></div>
            </div>
            <br />
                <asp:Literal ID="litEmailFailed" runat="server"></asp:Literal>
            <div class="row">
                <div class="col">
                    <div class="form-group">
                        <asp:TextBox ID="txtEmail" runat="server" placeholder="E-mail" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <div class="form-group">
                        <asp:Button ID="btnGetNewPw" runat="server" Text="Neues Passwort anfordern" OnClick="btnGetNewPw_Click" CssClass="btn btn-secondary w-100" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
