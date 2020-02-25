<%@ Page Title="" Language="C#" MasterPageFile="~/user-master.Master" AutoEventWireup="true" CodeBehind="user-home.aspx.cs" Inherits="Ferienspass.user_home" %>
<%@ Register assembly="DayPilot" namespace="DayPilot.Web.Ui" tagprefix="DayPilot" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<%--    <DayPilot:DayPilotCalendar ID="DayPilotCalendar1" runat="server" />--%>

    <%--<DayPilot:DayPilotCalendar ID="DayPilotCalendar2" runat="server" ViewType="Week"  />--%>
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
                <p style="text-align: left">
                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Arial Black" Font-Size="Small"  
                        ForeColor="#0066FF">Ausgewähltes Datum:</asp:Label><br /> 
                </p>
                <asp:GridView ID="gvSelectedDate" runat="server" CssClass="table table-hover" GridLines="None" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="coursename" HeaderText="Course" />
                        <asp:BoundField DataField="description" HeaderText="Description" />
                    </Columns>
                </asp:GridView>
            </div>
            <div class="col"> 
                <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="Black"  
                    BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"  
                    ForeColor="#663399" ShowGridLines="True" OnDayRender="Calendar1_DayRender" OnSelectionChanged="Calendar1_SelectionChanged"  
                    OnVisibleMonthChanged="Calendar1_VisibleMonthChanged">  
                    <SelectedDayStyle BackColor="#8581CE" Font-Bold="True" />  
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
                <asp:Label ID="LabelAction" runat="server"></asp:Label><br />  
                <b></b> 
            </div>
        </div>  
    </div>
</asp:Content>
