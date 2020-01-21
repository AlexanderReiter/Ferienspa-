<%@ Page Title="" Language="C#" MasterPageFile="~/admin-master.Master" AutoEventWireup="true" CodeBehind="admin-napeople.aspx.cs" Inherits="Ferienspass.WebForm2" %>

<%-- Programmer: Andreas Mair
    Date: 07.01.2020
    Verified by: Elias Werth--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <h1>Not allowed People:</h1>
        </div>
            <br />
        <asp:Literal ID="litEmailStatus" runat="server"></asp:Literal>
        <div class="row">
            <div class="col-12">
                <asp:GridView ID="gvNAPeople" runat="server" CssClass="table table-hover" GridLines="None" ShowHeaderWhenEmpty="true"  
                    AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="email" PageSize="20">
                    <Columns>
                        <asp:BoundField DataField="surname" HeaderText="Nachname" />
                        <asp:BoundField DataField="givenname" HeaderText="Vorname" />
                        <asp:BoundField DataField="email" HeaderText="E-Mail" SortExpression="email" />
                        <asp:BoundField DataField="zipcode" HeaderText="PLZ" />
                        <asp:BoundField DataField="city" HeaderText="Ort" />
                        <asp:BoundField DataField="streetname" HeaderText="Straße" />
                        <asp:BoundField DataField="housenumber" HeaderText="Nr." />
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnSendMail" runat="server" OnClick="btnSendMail_Click" ForeColor="Black"><i class="fas fa-envelope" style='font-size:24px;'></i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
