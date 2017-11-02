<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="ConsumeSoftSparkEntityWCF.index"  Async="true"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <title></title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <style>
        .right {
            float: right;
        }
    </style>

</head>
<body class="container">
    <form id="form1" runat="server">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
          <br />
        <%--
        <asp:Label ID="lblmsg" runat="server" Text="" Visible="false" class="alert alert-success"></asp:Label>
        <br />--%>

        <!--Build New user Interface-->
        <div class="container">
            <br />
            <div class="form-horizontal">
                <div class="form-group row">
                    <div class="col-md-1 col-sm-1">

                        <button type="submit" id="Button1" class="btn btn-warning" runat="server">Send <span class="glyphicon glyphicon-send"></span></button>

                        <%--<asp:Button ID="btnSend" runat="server" Text="Send" CssClass="btn btn-default " /> <span class="glyphicon glyphicon-send"></span>--%>
                    </div>

                    <div class="col-md-11 col-sm-11">
                        <div class="form-group row">
                            <div class="col-md-1 col-sm-1">
                                <asp:Label ID="lblFrom" runat="server" Text="From" class="col-form-label"></asp:Label>
                            </div>
                            <div class="col-md-10 col-sm-10">
                                <asp:Label ID="lblFromName" runat="server" Text="Bantale Babajide"></asp:Label>
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-md-1 col-sm-1">
                                <button type="button" class="btn btn-default" id="btnTo" onclick="toWindow()">To</button>
                                <%--<asp:Button ID="btnTo" runat="server" Text="To" CssClass="btn btn-default"  OnClientClick="toWindow()" UseSubmitBehavior="false" />--%>
                            </div>
                            <div class=" col-md-11 col-sm-11">
                                <asp:TextBox ID="txtTo" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-md-1 col-sm-1">
                                <button type="button" class="btn btn-default" id="btnCC" onclick="toWindow()">CC</button>
                                <%--<asp:Button ID="btnCC" runat="server" Text="CC" CssClass="btn btn-default" UseSubmitBehavior="false" />--%>
                            </div>
                            <div class=" col-md-11 col-sm-11">
                                <asp:TextBox ID="txtCC" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-md-1 col-sm-1">
                                <asp:Label ID="lblSubject" runat="server" Text="Subject"></asp:Label>
                            </div>
                            <div class=" col-md-11 col-sm-11">
                                <asp:TextBox ID="txtSubject" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>


                </div>

                <div class="form-group row">
                    <div class="col-md-12 col-sm-12">
                        <asp:TextBox ID="txtMsgBox" runat="server" TextMode="MultiLine" Rows="7" class="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>

        <!--End of New User Interface-->
        
        <%--<div class="row">
            <div class="col-md-8">
                    <div class="col-md-6 col-sm-6">
                        <asp:TextBox ID="txtQuote" runat="server" class="form-control" Visible="false" placeholder="QuoteNumber"></asp:TextBox>
                    </div>
                    <div class="col-md-6 col-sm-6">
                        <asp:TextBox ID="txtGuid" runat="server" class="form-control" Visible="false" placeholder="RecordGuid" ></asp:TextBox>
                    </div>
            </div>
        </div>--%>
        
        <br />
            
        <%--<div runat="server" id="successMessage" visible="false">
            <div class="row">
                <div class="col-md-8">
                    <div>
                        <div class="alert alert-success" role="alert" style="text-align:center;">Your email has been successfully sent.</div>
                    </div>
                    <br />
                    <div>
                        <asp:Button ID="btnExit" runat="server" Text="Close" CssClass="btn btn-danger" OnClientClick="closeWindow()" />
                    </div>
                </div>
            </div>
        </div>--%>

        <%--<div id="emailDiv" runat="server">
            <div class="row">
                <div class="col-md-8">
                    <asp:TextBox ID="txtSubject" class="form-control" runat="server" placeholder="Subject of Mail" required></asp:TextBox>
                    <br />
                    <asp:TextBox ID="txtMsgBox" class="form-control" runat="server" placeholder="Your message" TextMode="MultiLine" Rows="20" required></asp:TextBox>

                    <br />
                    <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-danger" OnClientClick="closeWindow()" />
                    <asp:Button ID="btnSendEmail" runat="server" Text="Send Email" CssClass="btn btn-success right" OnClick="btnSendEmail_Click" />
                </div>
                <div class="col-md-4">
                    <div id="ContactPanel">
                    </div>
                </div>

               
            </div>
        </div>--%>


        <ajaxToolkit:HtmlEditorExtender ID="htmlEditor" runat="server" EnableSanitization="true" TargetControlID="txtMsgBox">
            <Toolbar>
                <ajaxToolkit:Bold />
                <ajaxToolkit:Underline />
                <ajaxToolkit:Italic />
                <ajaxToolkit:HorizontalSeparator />
                <ajaxToolkit:JustifyLeft />
                <ajaxToolkit:JustifyCenter />
                <ajaxToolkit:JustifyRight />
                <ajaxToolkit:JustifyFull />
                <ajaxToolkit:HorizontalSeparator />
                <ajaxToolkit:InsertOrderedList />
                <ajaxToolkit:InsertUnorderedList />
                <ajaxToolkit:HorizontalSeparator />
                <ajaxToolkit:Indent />
                <ajaxToolkit:Outdent />
                <ajaxToolkit:FontSizeSelector />
                <ajaxToolkit:FontNameSelector />
            </Toolbar>

        </ajaxToolkit:HtmlEditorExtender>


        
        <asp:DataList ID="DataList1" runat="server">
            <ItemTemplate>
                <table>
                    <tr >
                        <td ><%# DataBinder.Eval(Container.DataItem, "FullName") %> <%# DataBinder.Eval(Container.DataItem, "Email") %> 
                        <input type="button" value="Add" onclick="AddContact('<%# DataBinder.Eval(Container.DataItem, "FullName") %>    ', '<%# DataBinder.Eval(Container.DataItem, "Email") %>    ', '<%# DataBinder.Eval(Container.DataItem, "RelatedContactGuid") %> ') " /></td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:DataList>
    </form>

    <script src="Scripts/jquery-1.9.1.min.js"></script>
    <script src="Scripts/bootstrap.js"></script>
    <script>
        function closeWindow() {
            window.close();
        }

        var accountId = 0;
        var contact = [];
        function AddContact(fullname, email, contactGuid) {
            
            var contactGuidfield = contactGuid.split('-')[0];
            
            for (var i = 0; i < contact.length; i++) {
                if (contact[i] == contactGuidfield) {
                    alert('You have already placed a bet on this match');
                    return;
                }
            }
            accountId++
            var AC = '<div id=' + accountId + '>' +
                    '<table>' +
                        '<tr>' +
                            '<td>' + fullname + '</td>' +
                            '<td>' + email + '</td>' +
                            '<td> <input type="button" value="Rem" onclick="RemoveContact(' + accountId + ')" /></td>' +
                        '</tr>' +
                    '</table>' +
                '</div>';
            $(AC).appendTo('div#ContactPanel');

            contact.push(contactGuidfield);
        }

        function RemoveContact(accountId) {
            $('div#' + accountId).remove();
        }

        function toWindow() {
            var url = "http://" +window.location.host + "/toWindow.aspx";
            var left = ((window).innerWidth / 2) - (900 / 2),
            top = ((window).innerHeight / 2) - (600 / 2);
            window.open(url, "toWindow", "scrollbars=no,resizable=no, width=900, height=600, top=" + top + ", left=" + left);
        }
        //# sourceURL=JideScript.js
    </script>
</body>
</html>
