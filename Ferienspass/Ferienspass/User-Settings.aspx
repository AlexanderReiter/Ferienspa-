<%@ Page Title="" Language="C#" MasterPageFile="~/site-master.Master" AutoEventWireup="true" CodeBehind="User-Settings.aspx.cs" Inherits="Ferienspass.User_Settings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <asp:GridView ID="gvDataParent" runat="server" AllowPaging="true" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
            <Columns>
                <asp:TemplateField HeaderText="Nachname">
                    <ItemTemplate>
                        <asp:Label ID="lblLastnameParent" runat="server" Text='<%# Eval("surname") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Vorname">
                    <ItemTemplate>
                        <asp:Label ID="lblFirstnameParent" runat="server" Text='<%# Eval("givenname") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Adresse">
                    <ItemTemplate>
                        <asp:Label ID="lblAddressParent" runat="server" Text='<%# Eval("address") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Email">
                    <ItemTemplate>
                        <asp:Label ID="lblEmailParent" runat="server" Text='<%# Eval("email") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Passwort">
                    <ItemTemplate>
                        <asp:Label ID="lblPassword" runat="server" Text='<%# Eval("password") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView> 
    </div>
</asp:Content>
