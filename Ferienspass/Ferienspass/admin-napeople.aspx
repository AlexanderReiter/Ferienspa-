<%@ Page Title="" Language="C#" MasterPageFile="~/admin-master.Master" AutoEventWireup="true" CodeBehind="admin-napeople.aspx.cs" Inherits="Ferienspass.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="shadow p-4 mb-4 bg-white">       
            <h3>Not allowed People:</h3>
            <asp:GridView CssClass="table-striped table-dark" ID="gvNAPeople" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="email" PageSize="20">
                <Columns>
                    <asp:BoundField DataField="surname" HeaderText="Nachname" />
                    <asp:BoundField DataField="givenname" HeaderText="Vorname" />
                    <asp:BoundField DataField="email" HeaderText="E-Mail" SortExpression="email" />
                    <asp:BoundField DataField="zipcode" HeaderText="PLZ" />
                    <asp:BoundField DataField="city" HeaderText="Ort" />
                    <asp:BoundField DataField="streetname" HeaderText="Straße" />
                    <asp:BoundField DataField="housenumber" HeaderText="Nr." />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
