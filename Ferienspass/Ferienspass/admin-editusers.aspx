<%@ Page Language="C#"  MasterPageFile="~/admin-master.Master" AutoEventWireup="true" CodeBehind="admin-editusers.aspx.cs" Inherits="Ferienspass.edit_users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script type="text/javascript">

    function Delete() {
        if (confirm("Wollen Sie diesen User löschen?\n"))
        {

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
                    <asp:Button ID="btnSearchUser" runat="server" Text="Suche" OnClick="btnSearchUser_Click" class="btn btn-secondary"/>
                </div>
            </div>

        </div>

            <div class="gvuser">
                <asp:GridView ID="gvUser" runat="server" CssClass="table table-hover" GridLines="None" AutoGenerateColumns="False" DataKeyNames="email" AllowPaging="True" PageSize="20" AllowSorting="True" 
                    OnSorting="gvUser_Sorting"  OnPageIndexChanging="gvUser_PageIndexChanging" ShowHeaderWhenEmpty="True" OnRowDeleting="gvUser_RowDeleting" >
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
                                <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="return Delete()" ForeColor="Black"><i class='fas fa-trash' style='font-size:24px'></i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>

            </div>        
        
    </div>
    
</asp:Content>



