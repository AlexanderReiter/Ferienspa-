<%@ Page Language="C#"  MasterPageFile="~/admin-master.Master" AutoEventWireup="true" CodeBehind="edit-users.aspx.cs" Inherits="Ferienspass.edit_users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="container">
        <div class="row">
            <h1>User</h1>
        </div>

            <div class="gvuser">
                <asp:GridView ID="gvUser" runat="server" CssClass="table" AutoGenerateColumns="false" DataKeyNames="email" AllowPaging="true" PageSize="20" >
                    <Columns>
                    <asp:TemplateField HeaderText="E-Mail">
                        <ItemTemplate>
                            <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("email") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="User-Status">
                        <ItemTemplate>
                            <asp:Label ID="lblUserStatus" runat="server" Text='<%# Eval("userstatus") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Vorname">
                        <ItemTemplate>
                            <asp:Label ID="lblVorname" runat="server" Text='<%# Eval("givenname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nachname">
                        <ItemTemplate>
                            <asp:Label ID="lblNachname" runat="server" Text='<%# Eval("surname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PLZ">
                        <ItemTemplate>
                            <asp:Label ID="lblPLZ" runat="server" Text='<%# Eval("zipcode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ort">
                        <ItemTemplate>
                            <asp:Label ID="lblOrt" runat="server" Text='<%# Eval("city") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Straße">
                        <ItemTemplate>
                            <asp:Label ID="lblStrasse" runat="server" Text='<%# Eval("streetname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nr.">
                        <ItemTemplate>
                            <asp:Label ID="lblNr" runat="server" Text='<%# Eval("housenumber") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                    </Columns>
                </asp:GridView>

            </div>
        
        
    </div>
    
</asp:Content>



