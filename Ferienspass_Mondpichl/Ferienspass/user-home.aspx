<%@ Page Title="" Language="C#" MasterPageFile="~/user-master.Master" AutoEventWireup="true" CodeBehind="user-home.aspx.cs" Inherits="Ferienspass.user_home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <div class="row">
            <div class="col-12">
                <p style="text-align: center">  
                    <br />
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial Black" Font-Size="Larger"  
                        ForeColor="#0066FF">Ferienspass Terminübersicht</asp:Label>
                    <br />
                    <br />
                </p>
            </div>
        </div>
        <div class="row">
            <div class="col-5">
                <p style="text-align: left">  
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial Black" Font-Size="Small"  
                        ForeColor="#0066FF">Nächste Termine:</asp:Label><br /> 
                </p>
                <asp:GridView ID="gvNextDates" runat="server" class="table table-bordered table-sm" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="date" HeaderText="Datum" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="givenname" HeaderText="Kind" />
                        <asp:BoundField DataField="coursename" HeaderText="Kurs" />
                        <asp:BoundField DataField="description" HeaderText="Beschreibung" />
                        <asp:BoundField DataField="zipcode" HeaderText="PLZ" />
                        <asp:BoundField DataField="city" HeaderText="Ort" />
                        <asp:BoundField DataField="streetname" HeaderText="Straße" />
                        <asp:BoundField DataField="housenumber" HeaderText="Nr." />
                    </Columns>
                </asp:GridView>
                <br />
                <p style="text-align: left">
                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Arial Black" Font-Size="Small"  
                        ForeColor="#0066FF">Ausgewähltes Datum:</asp:Label><br /> 
                </p>
                <asp:GridView ID="gvSelectedDate" runat="server" class="table table-bordered table-sm" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="date" HeaderText="Datum" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="givenname" HeaderText="Kind" />
                        <asp:BoundField DataField="coursename" HeaderText="Kurs" />
                        <asp:BoundField DataField="description" HeaderText="Beschreibung" />
                        <asp:BoundField DataField="zipcode" HeaderText="PLZ" />
                        <asp:BoundField DataField="city" HeaderText="Ort" />
                        <asp:BoundField DataField="streetname" HeaderText="Straße" />
                        <asp:BoundField DataField="housenumber" HeaderText="Nr." />
                    </Columns>
                </asp:GridView>
            </div>
            <div class="col-1"></div>
            <div class="col"> 
                <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="Black"  
                    BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"  
                    ForeColor="#663399" ShowGridLines="True" OnDayRender="Calendar1_DayRender" OnSelectionChanged="Calendar1_SelectionChanged">  
                    <SelectedDayStyle BackColor="#8581CE" />  
                    <SelectorStyle BackColor="Cyan" />  
                    <TodayDayStyle BackColor="Blue" ForeColor="White" Font-Bold="true" Font-Underline="true" />  
                    <DayStyle ForeColor="Blue" />
                    <OtherMonthDayStyle ForeColor="Black" BackColor="Gray"/>  
                    <NextPrevStyle Font-Size="9pt" ForeColor="White" />  
                    <DayHeaderStyle BackColor="LightBlue" Font-Bold="True" Height="1px" />  
                    <TitleStyle BackColor="#0066FF" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC" />  
                </asp:Calendar>  
                <br />  
                <b></b> 
            </div>
        </div>  
    </div>
</asp:Content>
