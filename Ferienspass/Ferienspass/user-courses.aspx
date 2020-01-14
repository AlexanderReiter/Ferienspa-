<%@ Page Title="" Language="C#" MasterPageFile="~/user-master.Master" AutoEventWireup="true" CodeBehind="user-courses.aspx.cs" Inherits="Ferienspass.user_courses" Theme="default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <br />
        <h1>Kurse</h1>
        <br />
        <div class="gvCourses">
            <asp:GridView ID="gvUserCourses" runat="server" CssClass="table" AutoGenerateColumns="False" AllowPaging="True" PageSize="20" DataKeyNames="courseID"
                ShowHeaderWhenEmpty="true" OnRowCommand="gvUserCourses_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Kursname">
                        <ItemTemplate>
                            <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("coursename") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Datum">
                        <ItemTemplate>
                            <asp:Label ID="lblDate" runat="server" Text='<%# Eval("date", "{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Organisation">
                        <ItemTemplate>
                            <asp:Label ID="lblOrganisation" runat="server" Text='<%# Eval("organisationname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Teilnehmer">
                        <ItemTemplate>
                            <asp:Label ID="lblTeilnehmer" runat="server" Text='<%# Eval("cntparticipants") + "/" + Eval("maxparticipants") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Preis">
                        <ItemTemplate>
                            <asp:Label ID="lblPrice" runat="server" Text='<%# "€ " + Eval("price") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="btnShowParticipants" runat="server" ForeColor="Black" CommandName="ShowDetails" CommandArgument='<%# Eval("courseID") %>'><i class="fas fa-list-ul" style='font-size:24px;'></i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <asp:Panel ID="panBlockBackground" runat="server" CssClass="panBlockBackground sticky" Visible="false"></asp:Panel>

    <asp:Panel ID="panCourse" runat="server" Visible="false">
        <div class="container">
            <div class="addCourseForm shadow p-4 mb-4 bg-white">
                <h1>Kurs</h1>
                <div class="row">
                    <div class="col">
                        <div class="form-group">
                            <label for="txtCourseName">Name:</label>
                            <asp:TextBox ID="txtCourseName" CssClass="form-control"  runat="server" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <div class="form-group">
                            <label for="txtDescription">Beschreibung:</label>
                            <textarea class="form-control" rows="5" id="txtDesciption" runat="server" name="text" disabled="disabled"></textarea>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-8">
                        <div class="row">
                            <div class="col-3">
                                <div class="form-group">
                                    <label for="txtFrom">Von:</label>
                                    <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" Enabled="false" TextMode="Time" format="HH:mm" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="form-group">
                                    <label for="txtTo">Bis:</label>
                                    <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" Enabled="false" TextMode="Time" format="HH:mm" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="form-group">
                                    <label for="txtMinParticipants">Min:</label>
                                    <asp:TextBox ID="txtMinParticipants" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="form-group">
                                    <label for="txtMaxParticipants">Max:</label>
                                    <asp:TextBox ID="txtMaxParticipants" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-4">
                                <div class="form-group">
                                    <label for="txtZIP">PLZ:</label>
                                    <asp:TextBox ID="txtZIP" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-8">
                                <div class="form-group">
                                    <label for="txtCity">Ort:</label>
                                    <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" Enabled="false"  ></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-8">
                                <div class="form-group">
                                    <label for="txtStreet">Straße:</label>
                                    <asp:TextBox ID="txtStreet" runat="server" CssClass="form-control" Enabled="false"  ></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label for="txtNr">Nr:</label>
                                    <asp:TextBox ID="txtNr" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="form-group">
                            <br />
                            <asp:Calendar ID="calendar" runat="server" Enabled="false" ></asp:Calendar>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-4">
                        <div class="form-group">
                            <label for="txtManagerName">Ansprechperson:</label>
                            <asp:TextBox ID="txtManagerName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="form-group">
                            <label for="txtContactMail">E-Mail:</label>
                            <asp:TextBox ID="txtContactMail" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="form-group">
                            <label for="txtPrice">Preis:</label>
                            <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <div class="form-group">
                            <label for="txtOrganisation">Organisation</label>
                            <asp:TextBox ID="txtOrganisation" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-6">
                        <asp:Button ID="btnCancel" runat="server" Text="Abbrechen" CssClass="btn btn-outline-secondary btn-lg" OnClick="btnCancel_Click" />
                    </div>
                    <div class="col-6">
                        <asp:Button ID="btnRegister" runat="server" Text="Anmelden" CssClass="btn btn-secondary btn-lg float-right" Visible="true" OnClick="btnRegister_Click" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="panSelectKids" runat="server" Visible="false">
        <div class="container">
            <div class="addCourseForm shadow p-4 mb-4 bg-white">
                <asp:GridView ID="gvKids" runat="server" CssClass="table" AutoGenerateColumns="False" DataKeyNames="kidId">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblKid" runat="server" Text='<%# Eval("givenname") + " " + Eval("surname") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
