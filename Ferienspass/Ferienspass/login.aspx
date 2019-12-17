<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Ferienspass.login" Theme="default"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login page</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css"/>
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.0/css/all.css" integrity="sha384-lZN37f5QGtY3VHgisS14W3ExzMWZxybE1SJSEsQp9S+oqd12jhcu+A56Ebc1zFSJ" crossorigin="anonymous">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
</head>
<body>
    <form id="form1" runat="server">
        <div class ="container">
            <div class="loginform shadow p-4 mb-4 bg-white">
                <div class="row">
                    <div class="col-12"><h1 style="text-align:center;">Login</h1></div>
                </div>
                <br />
                    <asp:Literal ID="litLoginFailed" runat="server"></asp:Literal>
                    <asp:Literal ID="litLoginInfo" runat="server"></asp:Literal>
                <div class="row">
                    <div class="col">
                        <div class="form-group">
                            <div class="input-group mb-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text" style="width:40px;"><i style="font-size:16px;" class="fas fa-user"> </i></span>
                                </div>
                                <asp:TextBox ID="txtEmailaddress" runat="server" placeholder="E-Mail (example@mail.com)" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <div class="form-group">
                            <div class="input-group mb-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text" style="width:40px;"><i style="font-size:16px;" class="fas fa-key"></i></span>
                                </div>
                                <asp:TextBox ID="txtPassword" runat="server" placeholder="Passwort" TextMode="Password" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <div class="form-group">
                            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" CssClass="btn btn-secondary w-100"/>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <div class="form-group">
                            <asp:Button ID="btnRegister" runat="server" Text="Registrieren" OnClick="btnRegister_Click" CssClass="btn btn-outline-secondary w-100" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <asp:LinkButton ID="btnPasswortVergessen" runat="server" Text="Passwort vergessen" OnClick="btnPasswortVergessen_Click"/>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
