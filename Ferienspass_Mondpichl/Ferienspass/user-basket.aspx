﻿<%@ Page Title="" Language="C#" MasterPageFile="~/user-master.Master" AutoEventWireup="true" CodeBehind="user-basket.aspx.cs" Inherits="Ferienspass.user_basket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <br />
        <h1>Warenkorb</h1>
        <br />
        <asp:GridView ID="gvBasket" runat="server" DataKeyNames="id, kidId, courseId" AutoGenerateColumns="false" CssClass="table table-hover" GridLines="None" ShowHeaderWhenEmpty="true" OnRowCommand="gvBasket_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="Teilnehmer">
                    <ItemTemplate>
                        <asp:Label ID="lblParticipant" runat="server" Text='<%# Eval("givenname") + " " + Eval("surname") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
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
                        <asp:LinkButton ID="btnShowParticipants" runat="server" ToolTip="Kurs anzeigen" ForeColor="Black" CommandName="ShowDetails" CommandArgument='<%# Eval("courseID") %>'><i class="fas fa-list-ul" style='font-size:24px;'></i></asp:LinkButton>
                        <asp:LinkButton ID="btnRemoveFromBasket" runat="server" ToolTip="Kurs entfernen" ForeColor="Black" CommandName="Remove" CommandArgument='<%# Eval("courseID") + "," + Eval("kidId") %>'><i class="fas fa-trash" style='font-size:24px;'></i></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
        <div class="row">
            <div class="col">
                <table class="table table-hover">
                    <tbody>
                        <tr>
                            <td style="width: 60%;"></td>
                            <td>
                                <h5>Zwischensumme</h5>
                            </td>
                            <td class="text-right">
                                <h5><strong>
                                    <asp:Label ID="lblSubtotal" runat="server" Text="0"></asp:Label>€</strong></h5>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <h5>Rabatt</h5>
                            </td>
                            <td class="text-right">
                                <h5><strong>-
                                    <asp:Label ID="lblDiscount" runat="server" Text="0"></asp:Label>€</strong></h5>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <h4>Summe</h4>
                            </td>
                            <td class="text-right">
                                <h4><strong>
                                    <asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label>€</strong></h4>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td>
                                <asp:HiddenField ID="hiddenFieldTotal" runat="server" />
                                <asp:HiddenField ID="hiddenFieldUser" runat="server" />
                                <div id="paypal-button"></div>
                                
                                <script src="https://www.paypalobjects.com/api/checkout.js"></script>
                                <script>
                                    var price = document.getElementById('<%= hiddenFieldTotal.ClientID%>').value;
                                    var user = document.getElementById('<%= hiddenFieldUser.ClientID%>').value;
                                    paypal.Button.render({
                                        // Configure environment
                                        env: 'sandbox',
                                        client: {
                                            sandbox: 'Aevhv9xB89aOJHVhSLT_WvcszERaoejFhazTiunNABotJKQ7imwieAJEadnx-ltO_EVjm7xKCqTF5fqy',
                                            production: 'demo_production_client_id'
                                        },
                                        // Customize button (optional)
                                        locale: 'de_AT',
                                        style: {
                                            size: 'large',
                                            color: 'gold',
                                            shape: 'rect',
                                        },

                                        // Enable Pay Now checkout flow (optional)
                                        commit: true,

                                        // Set up a payment
                                        payment: function (data, actions) {
                                            return actions.payment.create({
                                                transactions: [{
                                                    amount: {
                                                        total: price,
                                                        currency: 'EUR'
                                                    }
                                                }]
                                            });
                                        },
                                        // Execute the payment
                                        onAuthorize: function (data, actions) {
                                            return actions.payment.execute().then(function () {
                                                // Show a confirmation message to the buyer
                                                window.alert('Thank you for your purchase!');
                                                InsertChild();
                                            });
                                        }
                                    }, '#paypal-button');

                                    function InsertChild() {
                                        $.ajax({
                                            type: 'POST',
                                            url: 'user-basket.aspx/Checkout',
                                            data: "{sendData: '" + user + "'}",
                                            contentType: "application/json; charset=utf-8",
                                            dataType: "json",
                                            success: function (msg) {
                                                //alert(msg.d);
                                                location.replace("user-home.aspx");
                                            }
                                        });
                                        //alert("finish");
                                    }
                                </script>
                            </td>
                        </tr>
                    </tbody>
                </table>
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
                                <asp:TextBox ID="txtCourseName" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
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
                                        <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" Enabled="false" TextMode="Time" format="HH:mm"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label for="txtTo">Bis:</label>
                                        <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" Enabled="false" TextMode="Time" format="HH:mm"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label for="txtMinParticipants">Min:</label>
                                        <asp:TextBox ID="txtMinParticipants" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
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
                                        <asp:TextBox ID="txtZIP" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-8">
                                    <div class="form-group">
                                        <label for="txtCity">Ort:</label>
                                        <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-8">
                                    <div class="form-group">
                                        <label for="txtStreet">Straße:</label>
                                        <asp:TextBox ID="txtStreet" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
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
                                <asp:Calendar ID="calendar" runat="server" Enabled="false"></asp:Calendar>
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
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
