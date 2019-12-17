<%@ Page Title="" Language="C#" MasterPageFile="~/admin-master.Master" AutoEventWireup="true" CodeBehind="admin-settings.aspx.cs" Inherits="Ferienspass.admin_settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function Delete() {
            if (confirm("Sind Sie sicher, dass der Datensatz gelöscht werden soll?\n")) {
                return true;
            }
            return false;
        }
    </script>



    <div class="container">
        Unsere Nachbargemeinden:
        <asp:GridView ID="gvNeighbourcities" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" CssClass="table table-striped" 
            OnRowEditing="gvNeighbourcities_RowEditing" OnRowCancelingEdit="gvNeighbourcities_RowCancelingEdit" OnRowDeleting="gvNeighbourcities_RowDeleting"
            OnRowCommand="gvNeighbourcities_RowCommand" OnRowUpdating="gvNeighbourcities_RowUpdating">
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
                        <asp:LinkButton ID="btnAddCity" runat="server" CommandName="Add" ForeColor="Black" ><i class="fa fa-plus-square"></i></asp:LinkButton>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEditCity" runat="server" CommandName="Edit" ForeColor="Black"><i class="fa fa-pen"></i></asp:LinkButton>
                        <asp:LinkButton ID="btnDeleteCity" runat="server" CommandName="Delete" OnClientClick="return Delete()" ForeColor="Black"><i class="fa fa-trash"></i></asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="btnUpdateCity" runat="server" CommandName="Update" ForeColor="Black"><i class="fa fa-check"></i></asp:LinkButton>
                        <asp:LinkButton ID="btnCancelCity" runat="server" CommandName="Cancel" ForeColor="Black"><i class="fa fa-times"></i></asp:LinkButton>
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView> 
    </div>
</asp:Content>
