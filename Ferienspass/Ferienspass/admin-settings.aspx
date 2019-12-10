<%@ Page Title="" Language="C#" MasterPageFile="~/admin-master.Master" AutoEventWireup="true" CodeBehind="admin-settings.aspx.cs" Inherits="Ferienspass.admin_settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <asp:GridView ID="gvNeighbourcities" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" CssClass="table table-striped">
            <Columns>
                <asp:TemplateField HeaderText="PLZ">
                    <ItemTemplate>
                        <asp:Label ID="lblZipCode" runat="server" Text='<%# Eval("zipcode") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtZipCode" runat="server" Text='<%# Bind("zipcode") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Gemeinde">
                    <ItemTemplate>
                        <asp:Label ID="lblGemeinde" runat="server" Text='<%# Eval("city") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtGemeinde" runat="server" Text='<%# Bind("city") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:ImageButton ID="btnAddCity" runat="server" CommandName="Add" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:ImageButton ID="btnEditCity" runat="server" CommandName="Edit" />
                        <asp:ImageButton ID="btnDeleteCity" runat="server" CommandName="Delete" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:ImageButton ID="btnUpdateCity" runat="server" CommandName="Update" />
                        <asp:ImageButton ID="btnCancelCity" runat="server" CommandName="Cancel" />
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView> 
    </div>
</asp:Content>
