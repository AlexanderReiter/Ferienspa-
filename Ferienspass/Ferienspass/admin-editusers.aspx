<%@ Page Language="C#" MasterPageFile="~/admin-master.Master" AutoEventWireup="true" CodeBehind="admin-editusers.aspx.cs" Theme="default" Inherits="Ferienspass.edit_users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">

        function Delete() {
            if (confirm("Wollen Sie diesen User löschen?\n")) {

                return true;
            }

            return false;
        }

    </script>

    <div class="container">
        <br />
        <div class="row">
            <h1>User</h1>
            <br />
            <div class="search-container input-group mb-3">
                <asp:TextBox ID="txtSearchbar" runat="server" placeholder="Suchen nach Email, Vorname oder Nachname" class="form-control"></asp:TextBox>
                <div class="input-group-append">
                    <asp:Button ID="btnSearchUser" runat="server" Text="Suche" OnClick="btnSearchUser_Click" class="btn btn-secondary" />
                </div>
            </div>

        </div>

        <div class="gvuser">
            <asp:GridView ID="gvUser" runat="server" CssClass="table table-hover" GridLines="None" AutoGenerateColumns="False" DataKeyNames="email" AllowPaging="True" AllowSorting="True"
                OnSorting="gvUser_Sorting" OnPageIndexChanging="gvUser_PageIndexChanging" ShowHeaderWhenEmpty="True" OnRowDeleting="gvUser_RowDeleting" OnRowEditing="gvUser_RowEditing" OnRowDataBound="gvUser_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="E-Mail" SortExpression="email">
                        <ItemTemplate>
                            <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("email") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="User-Status" SortExpression="userstatus">
                        <ItemTemplate>
                            <asp:Label ID="lblUserStatus" runat="server" Text='<%# Eval("userstatus") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Vorname" SortExpression="givenname">
                        <ItemTemplate>
                            <asp:Label ID="lblGivenname" runat="server" Text='<%# Eval("givenname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nachname" SortExpression="surname">
                        <ItemTemplate>
                            <asp:Label ID="lblSurname" runat="server" Text='<%# Eval("surname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PLZ">
                        <ItemTemplate>
                            <asp:Label ID="lblZIP" runat="server" Text='<%# Eval("zipcode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ort">
                        <ItemTemplate>
                            <asp:Label ID="lblCity" runat="server" Text='<%# Eval("city") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Straße">
                        <ItemTemplate>
                            <asp:Label ID="lblStreet" runat="server" Text='<%# Eval("streetname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nr.">
                        <ItemTemplate>
                            <asp:Label ID="lblNr" runat="server" Text='<%# Eval("housenumber") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fehlgeschlagene Logins">
                        <ItemTemplate>
                            <asp:Label ID="lblFailedLogins" runat="server" Text='<%# Eval("failedlogins") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="User gesperrt" SortExpression="blocked">
                        <ItemTemplate>
                            <asp:Label ID="lblBlockedUser" runat="server" Text='<%# Eval("blocked") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnUpdate" runat="server" CommandName="Edit" ForeColor="Black"><i class='fas fa-pen' style='font-size:24px;'></i></asp:LinkButton>
                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="return Delete()" ForeColor="Black"><i class='fas fa-trash' style='font-size:24px'></i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>


    <asp:Panel ID="panBlockBackground" runat="server" CssClass="panBlockBackground sticky" Visible="false"></asp:Panel>
    <asp:Panel ID="panBlockBackgroundJavascript" runat="server" CssClass="panBlockBackground sticky" Style="visibility: hidden"></asp:Panel>
    <asp:Panel ID="panUser" runat="server" Visible="false">
        <div class="container">
            <div class="addCourseForm shadow p-4 mb-4 bg-white">
                <h1>User bearbeiten</h1>
                <asp:Literal ID="litAlert" runat="server"></asp:Literal>

                <div class="row">
                    <div class="col">
                        <div class="form-group">
                            <label for="txtEmail">Email:</label><asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col">
                        <div class="form-group">
                            <label for="txtGivenname">Vorname:</label><asp:TextBox ID="txtGivenname" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col">
                        <div class="form-group">
                            <label for="txtSurname">Nachname:</label><asp:TextBox ID="txtSurname" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="row row-cols-3">
                    <div class="col">
                        <div class="form-group">
                            <label for="txtUserstatus">Userstatus:</label>
                            <asp:TextBox ID="txtUserstatus" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col">
                        <div class="form-group">
                            <label for="txt">User gesperrt:</label>
                            <asp:TextBox ID="txtBlocked" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col">
                        <div class="form-group">
                            <label for="txt">Fehlgeschlagene Logins:</label>
                            <asp:TextBox ID="txtFailedLogins" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-4">
                        <div class="form-group">
                            <label for="txtZIP">PLZ:</label>
                            <asp:TextBox ID="txtZIP" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="form-group">
                            <label for="txtCity">Ort:</label>
                            <asp:TextBox ID="txtCity" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-4">
                        <div class="form-group">
                            <label for="txtStreet">Straße:</label>
                            <asp:TextBox ID="txtStreet" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="form-group">
                            <label for="txtNr">Nr:</label>
                            <asp:TextBox ID="txtNr" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-5">
                        <asp:Button ID="btnCancel" runat="server" Text="Abbrechen" CssClass="btn btn-outline-secondary btn-lg" OnClick="btnCancel_Click" />
                    </div>
                    <div class="col-5">
                        <asp:Button ID="btnSave" runat="server" Text="Speichern" CssClass="btn btn-secondary btn-lg float-right" Visible="false" OnClick="btnSave_Click" />
                    </div>
                </div>

            </div>
        </div>
    </asp:Panel>
</asp:Content>



