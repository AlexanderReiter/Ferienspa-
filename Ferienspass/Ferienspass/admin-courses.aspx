<%@ Page Title="" Language="C#" MasterPageFile="~/admin-master.Master" AutoEventWireup="true" CodeBehind="admin-courses.aspx.cs" Inherits="Ferienspass.admin_courses" Theme="default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <script type="text/javascript">
        function Delete() {
            if (confirm("Sind Sie sicher, dass der Kurs gelöscht werden soll?\n")) {
                return true;
            }
            return false;
         }

        function ShowUserWhoGotMail() {
            document.getElementById('<%=panUserWhoGotMail.ClientID%>').style.visibility = "visible";
            document.getElementById('<%=panBlockBackgroundJavascript.ClientID%>').style.visibility = "visible";
         }
    </script>
    
    <div class="container">
        <br />
        <div class="row">
            <h1>Kurse</h1>
            <br />
            <div class="search-container input-group mb-3">
                <asp:TextBox ID="txtSearchbar" runat="server" placeholder="Suchen nach Kursname oder Organisation" class="form-control"></asp:TextBox>
                <div class="input-group-append">
                    <asp:Button ID="btnSearchCourse" runat="server" Text="Suche" OnClick="btnSearchCourse_Click" class="btn btn-secondary"/>
                </div>
            </div>
        </div>
        <asp:Literal ID="litEmail" runat="server"></asp:Literal>
        <div class="gvcourses">
            <asp:GridView ID="gvCourses" runat="server" CssClass="table table-hover" GridLines="None" AutoGenerateColumns="False" PageSize="20" DataKeyNames="courseID"
                ShowHeaderWhenEmpty="true" OnRowEditing="gvCourses_RowEditing" OnPageIndexChanging="gvCourses_PageIndexChanging" OnSorting="gvCourses_Sorting" 
                AllowSorting="true" AllowPaging="True" OnRowCommand="gvCourses_RowCommand" OnRowDeleting="gvCourses_RowDeleting">
                <Columns>
                    <asp:TemplateField HeaderText="Kursname" SortExpression="coursename">
                        <ItemTemplate>
                            <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("coursename") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Datum" SortExpression="date">
                        <ItemTemplate>
                            <asp:Label ID="lblDate" runat="server" Text='<%# Eval("date", "{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Organisation" SortExpression="organisationname">
                        <ItemTemplate>
                            <asp:Label ID="lblOrganisation" runat="server" Text='<%# Eval("organisationname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Teilnehmer">
                        <ItemTemplate>
                            <asp:Label ID="lblParticipants" runat="server" Text='<%# Eval("cntparticipants") + "/" + Eval("maxparticipants") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Preis">
                        <ItemTemplate>
                            <asp:Label ID="lblPrice" runat="server" Text='<%# "€ " + Eval("price") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:LinkButton ID="btnNewCourse" runat="server" OnClick="btnNewCourse_Click" ForeColor="Black"><i class='fas fa-plus-square' style='font-size:24px;'></i></asp:LinkButton>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="btnUpdate" runat="server" CommandName="Edit" ForeColor="Black"><i class='fas fa-pen' style='font-size:24px;'></i></asp:LinkButton>
                            <asp:LinkButton ID="btnMail" runat="server" CommandName="Mail" CommandArgument='<%# Eval("courseID") %>' ForeColor="Black"><i class="fas fa-envelope" style='font-size:24px;'></i></asp:LinkButton>
                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="return Delete()" ForeColor="Black"><i class='fas fa-trash' style='font-size:24px'></i></asp:LinkButton>
                            <asp:LinkButton ID="btnShowParticipants" runat="server" ForeColor="Black" CommandName="Participants" CommandArgument='<%# Eval("courseID") %>'><i class="fas fa-search" style='font-size:24px;'></i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
     .<asp:Panel ID="panBlockBackground" runat="server" CssClass="panBlockBackground sticky" Visible="false"></asp:Panel>
    <asp:Panel ID="panBlockBackgroundJavascript" runat="server" CssClass="panBlockBackground sticky" style="visibility:hidden"></asp:Panel>
    <asp:Panel ID="panCourse" runat="server" Visible="false">
        <div class="container">
            <div class="addCourseForm shadow p-4 mb-4 bg-white">
                <h1><asp:Literal ID="litPanHeadline" runat="server"></asp:Literal></h1>
                <asp:Literal ID="litAlert" runat="server"></asp:Literal>
                <div class="row">
                    <div class="col">
                        <div class="form-group">
                            <label for="txtCourseName">Name:</label>
                            <asp:TextBox ID="txtCourseName" CssClass="form-control"  runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <div class="form-group">
                            <label for="txtDescription">Beschreibung:</label>
                            <textarea class="form-control" rows="5" id="txtDesciption" runat="server" name="text"></textarea>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-8">
                        <div class="row">
                            <div class="col-3">
                                <div class="form-group">
                                    <label for="txtFrom">Von:</label>
                                    <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" TextMode="Time" format="HH:mm" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="form-group">
                                    <label for="txtTo">Bis:</label>
                                    <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" TextMode="Time" format="HH:mm" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="form-group">
                                    <label for="txtMinParticipants">Min:</label>
                                    <asp:TextBox ID="txtMinParticipants" runat="server" CssClass="form-control" TextMode="Number" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="form-group">
                                    <label for="txtMaxParticipants">Max:</label>
                                    <asp:TextBox ID="txtMaxParticipants" runat="server" CssClass="form-control" TextMode="Number" ></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-4">
                                <div class="form-group">
                                    <label for="txtZIP">PLZ:</label>
                                    <asp:TextBox ID="txtZIP" runat="server" CssClass="form-control"  ></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-8">
                                <div class="form-group">
                                    <label for="txtCity">Ort:</label>
                                    <asp:TextBox ID="txtCity" runat="server" CssClass="form-control"  ></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-8">
                                <div class="form-group">
                                    <label for="txtStreet">Straße:</label>
                                    <asp:TextBox ID="txtStreet" runat="server" CssClass="form-control"  ></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label for="txtNr">Nr:</label>
                                    <asp:TextBox ID="txtNr" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="form-group">
                            <br />
                            <asp:Calendar ID="calendar" runat="server" ></asp:Calendar>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-4">
                        <div class="form-group">
                            <label for="txtManagerName">Ansprechperson:</label>
                            <asp:TextBox ID="txtManagerName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="form-group">
                            <label for="txtContactMail">E-Mail:</label>
                            <asp:TextBox ID="txtContactMail" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="form-group">
                            <label for="txtPrice">Preis:</label>
                            <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" OnTextChanged="txtPrice_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <div class="form-group">
                            <label for="ddlOrganisation">Organisation</label>
                            <asp:DropDownList ID="ddlOrganisation" runat="server" CssClass="form-control" Width="100%"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-6">
                        <asp:Button ID="btnCancel" runat="server" Text="Abbrechen" CssClass="btn btn-outline-secondary btn-lg" OnClick="btnCancel_Click" />
                    </div>
                    <div class="col-6">
                        <asp:Button ID="btnAdd" runat="server" Text="Hinzufügen" CssClass="btn btn-secondary btn-lg float-right" Visible="true" OnClick="btnAdd_Click" />
                        <asp:Button ID="btnSave" runat="server" Text="Speichern" CssClass="btn btn-secondary btn-lg float-right" Visible="false" OnClick="btnSave_Click" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="panSendMail" runat="server" Visible="false">
        <div class="container">
            <div class="addCourseForm shadow p-4 mb-4 bg-white">
                <div class="row">
                    <div class="col">
                        <h1>E-Mail</h1>
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <div class="form-group">
                            <label for="txtSubject">Betreff:</label>
                            <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <div class="form-group">
                            <label for="txtSubject">Betreff:</label>
                            <textarea class="form-control" rows="20" id="txtContent" runat="server" name="text"></textarea>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-6">
                        <asp:Button ID="btnCancelSendMail" runat="server" Text="Abbrechen" CssClass="btn btn-outline-secondary btn-lg" OnClick="btnCancelSendMail_Click" />
                    </div>
                    <div class="col-6">
                        <asp:Button ID="btnSend" runat="server" Text="Senden" CssClass="btn btn-secondary btn-lg float-right" OnClick="btnSend_Click" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="panParticipants" runat="server" Visible="false">
        <div class="container">
            <div class="addCourseForm shadow p-4 mb-4 bg-white">
                <asp:GridView ID="gvParticipants" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" DataKeyNames="kidID" CssClass="table table-hover" GridLines="None" OnRowCommand="gvParticipants_RowCommand" OnRowDeleting="gvParticipants_RowDeleting">
                    <Columns>
                        <asp:TemplateField HeaderText="Vorname">
                            <ItemTemplate>
                                <asp:Label ID="lblGivenname" runat="server" Text='<%# Eval("givenname") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nachname">
                            <ItemTemplate>
                                <asp:Label ID="lblSurname" runat="server" Text='<%# Eval("surname") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Geschlecht">
                            <ItemTemplate>
                                <asp:Label ID="lblGender" runat="server" Text='<%# Eval("gendername") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Geburtstag">
                            <ItemTemplate>
                                <asp:Label ID="lblBirthday" runat="server" Text='<%# Eval("birthday", "{0:dd/MM/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btnShowUser" runat="server" ForeColor="Black" CommandName="User" CommandArgument='<%# Eval("kidID") %>'><i class="fas fa-user" style="font-size: 24px"></i></asp:LinkButton>
                                <asp:LinkButton ID="btnDeleteParticipant" runat="server" ForeColor="Black" CommandName="Delete"><i class="fas fa-trash" style="font-size: 24px"></i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:Button ID="btnClose" runat="server" Text="Schließen" CssClass="btn btn-secondary btn-lg" OnClick="btnClose_Click" />
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="panNoParticipants" runat="server" Visible="false">
        <div class="container">
            <div class="addCourseForm shadow p-4 mb-4 bg-white">
                <asp:Label ID="lblNoParticipants" runat="server" Text="Keine Teilnehmer in diesem Kurs."></asp:Label>
                <br />
                <asp:Button ID="btnNoParticipantsClose" runat="server" Text="Schließen" CssClass="btn btn-secondary btn-lg" OnClick="btnNoParticipantsClose_Click" />
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="panUser" runat="server" Visible="false">
        <div class="container">
            <div class="addCourseForm shadow p-4 mb-4 bg-white">
                <div class="row">
                    <div class="col-12">
                        <div class="form-group">
                            <asp:TextBox ID="txtEmail" placeholder="Email" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-6">
                        <div class="form-group">
                            <asp:TextBox ID="txtGivenname" placeholder="Vorname" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-6">
                        <div class="form-group">
                            <asp:TextBox ID="txtSurname" placeholder="Nachname" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-3">
                        <div class="form-group">
                            <asp:TextBox ID="txtUserZIP" placeholder="PLZ" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-9">
                        <div class="form-group">
                            <asp:TextBox ID="txtUserCity" placeholder="Ort" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-10">
                        <div class="form-group">
                            <asp:TextBox ID="txtUserStreet" placeholder="Straße" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-2">
                        <div class="form-group">
                            <asp:TextBox ID="txtUserHousenumber" placeholder="Hausnr." runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <asp:Button ID="btnBackToParticipants" runat="server" Text="Zurück" CssClass="btn btn-secondary btn-lg" OnClick="btnBackToParticipants_Click" />
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="panUserWhoGotMail" runat="server" style="visibility:hidden">
        <div class="container">
            <div class="addCourseForm shadow p-4 mb-4 bg-white">
                <asp:GridView ID="gvUserWhoGotMail" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" CssClass="table table-hover" GridLines="None">
                    <Columns>
                        <asp:TemplateField HeaderText="Email">
                            <ItemTemplate>
                                <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("email") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Vorname">
                            <ItemTemplate>
                                <asp:Label ID="lblUserGivenname" runat="server" Text='<%# Eval("givenname") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nachname">
                            <ItemTemplate>
                                <asp:Label ID="lblUserSurname" runat="server" Text='<%# Eval("surname") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:Button ID="btnCloseUser" runat="server" Text="Schließen" CssClass="btn btn-secondary btn-lg" />
            </div>
        </div>
    </asp:Panel>
</asp:Content>
