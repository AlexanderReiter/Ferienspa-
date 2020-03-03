<%@ Page Language="C#" MasterPageFile="~/admin-master.Master" AutoEventWireup="true" CodeBehind="admin-organisations.aspx.cs" Inherits="Ferienspass.admin_organisations" Theme="default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <br />
        <div class="row">
            <h1>Organisationen:</h1>
            <br />
        </div>
        <div class="row">
            <div class="col-12">
                <asp:Literal ID="litError" runat="server"></asp:Literal>
                <asp:GridView ID="gvOrganisations" runat="server" CssClass="table table-hover" DataKeyNames="organisationId" AutoGenerateColumns="false" AllowPaging="true" PageSize="20" ShowHeaderWhenEmpty="true" GridLines="None" OnRowEditing="gvOrganisations_RowEditing" OnRowCancelingEdit="gvOrganisations_RowCancelingEdit" OnRowUpdating="gvOrganisations_RowUpdating" OnRowCommand="gvOrganisations_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Organisationsname">
                            <ItemTemplate>
                                <asp:Label ID="lblOrganisationName" runat="server" Text='<%# Eval("organisationname") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtOrganisationName" runat="server" Text='<%# Bind("organisationname") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email">
                            <ItemTemplate>
                                <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("email") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEmail" runat="server" Text='<%# Bind("email") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:LinkButton ID="btnAddChild" runat="server" CommandName="Add" ForeColor="Black" ><i class="fa fa-plus-square"></i></asp:LinkButton>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEditChild" runat="server" CommandName="Edit" ForeColor="Black"><i class="fas fa-pen"></i></asp:LinkButton>
                                <%--<asp:LinkButton ID="btnDeleteChild" runat="server" CommandName="Delete" ForeColor="Black"><i class="fa fa-trash"></i></asp:LinkButton>--%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="btnUpdateChild" runat="server" CommandName="Update" ForeColor="Black"><i class="fa fa-check"></i></asp:LinkButton>
                                <asp:LinkButton ID="btnCancelChild" runat="server" CommandName="Cancel" ForeColor="Black"><i class="fa fa-times"></i></asp:LinkButton>
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
