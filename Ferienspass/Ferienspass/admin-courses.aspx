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
    </script>

    <div class="container">
        <br />
        <div class="row">
            <h1>Kurse</h1>
            <p>
                 
            </p>
        </div>
        <div class="gvcourses">
            <asp:GridView ID="gvCourses" runat="server" CssClass="table" AutoGenerateColumns="False" AllowPaging="True" PageSize="20" DataKeyNames="courseID"
                ShowHeaderWhenEmpty="true" OnRowEditing="gvCourses_RowEditing" OnPageIndexChanging="gvCourses_PageIndexChanging" OnSorting="gvCourses_Sorting" 
                OnRowCommand="gvCourses_RowCommand" OnRowDeleting="gvCourses_RowDeleting">
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
                            <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("price") + "€" %>'></asp:Label>
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
                            <asp:LinkButton ID="btnShowParticipants" runat="server" ForeColor="Black"><i class="fas fa-"</asp:LinkButton>
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
                                    <label for="txtZIP">Zip:</label>
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
                            <label for="txtPrice">Ansprechperson:</label>
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
</asp:Content>
