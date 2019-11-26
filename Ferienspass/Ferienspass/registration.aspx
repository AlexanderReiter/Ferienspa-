<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="registration.aspx.cs" Inherits="Ferienspass.registration" Theme="default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css">
</head>
<body>
    <form id="form1" runat="server">
        <div class="registration-container">
            <div class="row">
                <div class="col">
                    <h1>Registrierung</h1>
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
                    <asp:TextBox ID="txtPassword" runat="server" placeholder="Passwort" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
