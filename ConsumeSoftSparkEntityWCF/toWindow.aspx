<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="toWindow.aspx.cs" Inherits="ConsumeSoftSparkEntityWCF.toWindow" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<%--<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
</head>
<body>





    <form id="form1" runat="server">
        <div class="container">

            <br />
            <div class="form-group row">
                <div class="col-md-1 col-sm-1">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-default" OnClick="btnSave_Click" />
                </div>
                <div class="col-md-1 col-sm-1">
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClientClick="closeWindow()" CssClass="btn btn-danger" />
                </div>
            </div>

            <hr />

            <div class="form-group row">
                <div class="col-md-1 col-sm-1">
                    <asp:Label ID="lblTo" runat="server" Text="To" class="col-form-label"></asp:Label>
                </div>
                <div class="col-md-11 col-sm-11">
                    <asp:TextBox ID="txtTo" runat="server" class="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group row">
                <div class="col-md-11 col-sm-11">
                    <div id="ContactPanell">

                    </div>
                </div>
                <asp:HiddenField ID="hdf"  runat="server" />
            </div>

            <div class="form-group row">
                <div class="col-md-5 col-sm-5">
                    <asp:DataList ID="DataList1" runat="server">
                        <ItemTemplate>
                            <div class="form-group row">
                                <div class="col-md-9 col-sm-9">
                                    <asp:Label ID="lblName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FullName") %>'></asp:Label>
                                    <br />
                                    <asp:Label ID="lblEmail" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Email") %>'></asp:Label>
                                </div>
                                <div class="col-md-3 col-sm-3">
                                    <button type="button" id="btnAddEmail" onclick="AddContact('<%# DataBinder.Eval(Container.DataItem, "FullName") %>    ', '<%# DataBinder.Eval(Container.DataItem, "Email") %>    ', '<%# DataBinder.Eval(Container.DataItem, "RelatedContactGuid") %> ') ">
                                        <span class="glyphicon glyphicon-plus"></span>
                                    </button>
                                    <br />
                                    <input type="button" id="btnAddEmails" onclick="AddContact()" <img src="images/plus.png" width="60" height="50" /> />
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:DataList>

                </div>

                <div class="col-md-7 col-sm-7">
                    <div class="form-group row">
                        <div class="col-md-12 col-sm-12">
                            Name:
                            <label id="lblName"></label>
                            <br />
                            Email:
                            <label id="lblEmail"></label>
                            <br />
                            User Guid :
                            <label id="lblUserGuid"></label>
                        </div>
                    </div>
                </div>
            </div>
        </div>


    </form>

    <script src="Scripts/jquery-1.9.1.js"></script>
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
                if (contact[i] == email) {
                    alert('You have already placed a bet on this match');
                    return;
                }
            }
            accountId++

            var AC2 = 
                            '<span  id=' + accountId + ' class="badge badge-pill badge-secondary">' + fullname +
                            '<button type="button" class="close" aria-label="Close" onclick="RemoveContact(' + accountId + ')>' +
                                '<span aria-hidden="true">&times;</span>' +
                            '</button>' +
                            '</span>' +


            $(AC2).appendTo('div#ContactPanell');

            contact.push(email);

            hdf_store = document.getElementById("hdf");
            hdf_store.value = contact.join(";");

            //$("<%= hdf.ClientID %>").val(contact.join(";"));
            

            //Display selected value
            $("#lblName").html(fullname);
            $("#lblEmail").html(email);
            $("#lblUserGuid").html(contactGuid);
        }

        function RemoveContact(accountId) {
            $('div#' + accountId).remove();
        }
                //# sourceURL=JideScript.js
    </script>
</body>
</html>--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="App_Themes/mydefault/default.css" rel="stylesheet" type="text/css" />

    <script src="loadmask/jquery-latest.pack.js" type="text/javascript"></script>
    <script src="loadmask/jquery.loadmask.js" type="text/javascript"></script>
    <link href="loadmask/jquery.loadmask.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnSend").bind("click", function () {
                $("#HomePanel2").mask("Sending Message...");
            });
        });
    </script>

    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .auto-style2 {
            height: 26px;
        }

        .iround {
            background-color: #F6F6F6;
            padding: 2px 5px 0px 2px;
            text-align: left;
            color: #000000;
            font-weight: normal;
            vertical-align: middle;
            border: solid 1px #ccc;
            height: 23px;
            font-family: Tahoma, Arial, Verdana, Sans-Serif;
            border-radius: 3px;
        }

            .iround:hover {
                background-color: #EAEAEA;
                color: #ff0000;
                cursor: pointer;
            }

        .scroll {
            height: 120px;
            min-height: 120px;
            max-height: 120px;
            overflow: auto;
            margin-bottom: 5px;
        }

        .defaultGrid {
            border: 1px solid #999999;
            font-family: Arial, Helvetica, sans-serif;
            background-color: #FEFEFE;
            font-size: 11px;
            color: #000000;
        }

            .defaultGrid td {
                border: 1px solid #999999;
                padding: 2px;
            }

            .defaultGrid th {
                border: 1px solid #999999;
                padding: 2px;
                color: #000000;
                font-weight: bold;
                background-color: #EFE7CD;
            }

        .selected_row {
            color: #000FFF;
            background-color: #f9f9f9;
        }

        .style3 {
        }

        .style4 {
            height: 26px;
        }
    </style>
</head>

<script type="text/javascript">
    function UploadFileNow() {

        var value = $("#MyUploader").val();

        if (value != '') {

            $("#form1").submit();

        }

    };
</script>
<body style="background-color: #ffffff">

    <form id="form2" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Debug" LoadScriptsBeforeUI="false" EnablePartialRendering="true"></asp:ScriptManager>

        <%--<asp:ToolkitScriptManager runat="server" ID="ScriptManager1" ScriptMode="Release" LoadScriptsBeforeUI="false"
        EnablePartialRendering="true" CombineScripts="false" >
    </asp:ToolkitScriptManager>--%>

        <div>
            <div class="row">
                <div class="col-md-8">
                    <div class="col-md-6 col-sm-6">
                        <asp:TextBox ID="txtQuote" runat="server" class="form-control" Visible="false" placeholder="QuoteNumber"></asp:TextBox>
                    </div>
                    <div class="col-md-6 col-sm-6">
                        <asp:TextBox ID="txtGuid" runat="server" class="form-control" Visible="false" placeholder="RecordGuid"></asp:TextBox>
                    </div>
                </div>
            </div>
            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                <asp:View ID="view1" runat="server">
                    <asp:Panel runat="server" ID="Panel3" class="box">
                        <div class="box-header">
                            <h2>Send Quote to Customer</h2>
                        </div>

                        <div class="box-content" style="background-color: #ffffff">
                            <%--<asp:Label ID="lblMsgError" runat="server" Text="" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblMsgErr" runat="server" Text="" ForeColor="Red"></asp:Label>--%>
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" class="style1" style="border: 1px solid #BCB0B0">
                                            <tr>
                                                <td style="padding-left: 2px; width: 400px; height: 30px;">
                                                    <asp:Button ID="btnSend" runat="server" CssClass="btnemail ibtn" Text="Send" OnClick="btnSend_Click" />
                                                    <asp:Button ID="Button1" runat="server" Text="Cancel"
                                                        CssClass="btncancel ibtn" Style="font-weight: normal" Enabled="false" />
                                                </td>
                                                <td style="text-align: left; padding-left: 30px; padding-right: 20px;"></td>


                                                <td style="padding-right: 10px; text-align: right">
                                                    <%--<asp:Button ID="btnBack" runat="server" Text="Send Another Quote" CssClass="btnback ibtn" Style="font-weight: normal" />--%>

                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblmsg" runat="server" BackColor="#F1F1F1"
                                            BorderStyle="None" Font-Bold="True" Font-Size="13px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table class="style1">
                                            <tr>
                                                <td class="style3">From:</td>
                                                <td>
                                                    <asp:TextBox ID="txtFrom" runat="server" Width="100%" ReadOnly="True"
                                                        BackColor="White" BorderStyle="None" BorderWidth="0px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="border-bottom: 1px groove #C0C0C0; color: #28323E; height: 2px;" colspan="2"></td>
                                            </tr>
                                            <tr>
                                                <td class="style3">To:</td>
                                                <td style="width: 100%">
                                                    <table class="style1">
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="MyTxtTo" runat="server" Width="100%" ReadOnly="True"
                                                                    BorderColor="White" BorderStyle="None" BorderWidth="0px"></asp:TextBox>
                                                            </td>


                                                        </tr>
                                                    </table>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="border-bottom: 1px groove #C0C0C0; color: #28323E; height: auto;" colspan="2"></td>
                                            </tr>
                                            <tr>
                                                <td class="style3">
                                                    <asp:Button ID="btnGetContact" runat="server" Text="Cc" SkinID="Normal" OnClick="btnGetContact_Click" /></td>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <%-- <td style="width: 80px">
                                                               
                                                            </td>--%>
                                                            <td style="color: #28323E; height: auto;">

                                                                <asp:DataList ID="ddlAddressF" runat="server" RepeatLayout="Flow"
                                                                    RepeatDirection="Horizontal" Width="100%">
                                                                    <HeaderStyle VerticalAlign="Top" />
                                                                    <ItemStyle BorderStyle="None" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <div style="font-size: small" class="iround">
                                                                                        <div style="margin: 0px; padding: 1px 1px 0px 0px; float: left;">
                                                                                            <asp:Literal ID="litName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FullName") %>'></asp:Literal>
                                                                                        </div>

                                                                                        <div style="margin: 0px; padding: 1px 1px 0px 5px; float: left">
                                                                                            <asp:ImageButton ID="btnRemove" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Email") %>' ImageUrl="~/App_Themes/mydefault/Images/x-icon.png" />
                                                                                        </div>

                                                                                    </div>
                                                                                </td>
                                                                                <td>&nbsp;</td>
                                                                            </tr>
                                                                        </table>


                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <%-- <SelectedItemStyle BackColor="#FF9999" BorderColor="#CCCCCC" BorderStyle="Solid"
                                                        BorderWidth="1px" />
                                                    <SeparatorStyle BackColor="#FFFF99" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
                                                        VerticalAlign="Top" />--%>
                                                                </asp:DataList>


                                                            </td>


                                                        </tr>
                                                    </table>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="border-bottom: 1px groove #C0C0C0; color: #28323E; height: auto;" colspan="2"></td>
                                            </tr>
                                            <tr>
                                                <td class="style4">Subject:</td>
                                                <td class="auto-style2">
                                                    <asp:TextBox ID="TxtSubject" runat="server" Width="100%" BorderColor="White"
                                                        BorderStyle="None"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="border-bottom: 1px groove #C0C0C0; color: #28323E; height: auto;" colspan="2"></td>
                                            </tr>
                                            <tr>
                                                <td class="style3" colspan="2">Quote Attached:<asp:LinkButton ID="hyp" runat="server" BackColor="#F1F1F1"
                                                    Font-Size="Medium" Font-Underline="True" ForeColor="#0099CC" Width="43%" OnClick="hyp_Click"></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtBody" runat="server" TextMode="MultiLine" Rows="7" class="form-control" Width="100%"></asp:TextBox>
                                        <%--<cc1:editor id="txtBody" runat="server" borderwidth="1px" bordercolor="#E0E0E0" height="250"></cc1:editor>--%>

                                        <ajaxToolkit:HtmlEditorExtender ID="htmlEditor" runat="server" EnableSanitization="true" TargetControlID="txtBody">
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

                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>

                        </div>
                    </asp:Panel>

                </asp:View>
                <asp:View ID="View2" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:Panel runat="server" ID="Panel2" class="box">
                                <div class="box-header">
                                    <h2>Add Contacts</h2>
                                </div>

                                <div class="box-content">
                                    <table align="left" width="100%">
                                        <tr>
                                            <td style="border-bottom: 1px groove #C0C0C0; color: #28323E; font-size: 172%; height: 35px">
                                                <asp:Button ID="btnSaveContact" runat="server" CssClass="btnsave ibtn" Text="Save" OnClick="btnSaveContact_Click" />
                                                <asp:Button ID="BtnRemoveContact" runat="server" Text="Cancel"
                                                    CssClass="btncancel ibtn" Style="font-weight: normal" Enabled="false" OnClick="BtnRemoveContact_Click" /></td>
                                        </tr>
                                        <tr>
                                            <td style="padding-top: 8px; text-align: left">

                                                <table cellpadding="0" cellspacing="0" class="style1">
                                                    <tr>
                                                        <td style="vertical-align: top">
                                                            <asp:Label ID="Label1" runat="server" Text="CC:"></asp:Label>
                                                        </td>
                                                        <td style="color: #28323E; height: auto; text-align: left">
                                                            <asp:DataList ID="ddlAddress" runat="server" RepeatLayout="Flow" HorizontalAlign="Left" RepeatDirection="Horizontal" AutoPostBack="True" OnItemCommand="ddlAddress_ItemCommand1">
                                                                <HeaderStyle VerticalAlign="Top" />
                                                                <ItemStyle BorderStyle="None" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <div style="font-size: small" class="iround">
                                                                                    <div style="margin: 0px; padding: 1px 1px 0px 0px; float: left;">
                                                                                        <asp:Literal ID="litName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FullName") %>'></asp:Literal>
                                                                                    </div>

                                                                                    <div style="margin: 0px; padding: 1px 1px 0px 5px; float: left">
                                                                                        <asp:ImageButton ID="btnRemove" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Email") %> ' ImageUrl="~/App_Themes/mydefault/Images/x-icon.png" />
                                                                                    </div>

                                                                                </div>
                                                                            </td>
                                                                            <td>&nbsp;</td>
                                                                        </tr>
                                                                    </table>


                                                                    </div>
                                                                </ItemTemplate>





                                                            </asp:DataList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="border-bottom: 1px groove #C0C0C0; color: #28323E; height: 1px;"></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>

                            <asp:Panel runat="server" ID="Panel1" class="box">
                                <div class="box-header">
                                    <h2>Related Contact List</h2>
                                </div>
                                <div class="box-content">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" EnableTheming="true" CssClass="defaultGrid"
                                        DataKeyNames="RelatedContactGuid" Width="100%" OnRowCreated="GridView1_RowCreated1" OnSelectedIndexChanged="GridView1_SelectedIndexChanged1">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Width="10px" ItemStyle-Width="10px">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>FullName</HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFullName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FullName") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>Email Address</HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmail" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Email") %> '></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>Contact Guid</HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblContactGuid" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RelatedContactGuid") %> '></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <HeaderTemplate>Organization</HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccountName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AccountName")%> '></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <HeaderTemplate>Designation</HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblJobTitle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JobTitle")%> '></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField DataField="ContactID" HeaderText="ContactID" SortExpression="ContactID"
                                                Visible="false" />
                                            <asp:BoundField DataField="FullName" HeaderText="FullName" SortExpression="FullName" />
                                            <asp:BoundField DataField="Email" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left"
                                                HeaderText="EmailAddress" SortExpression="EmailAddress" />
                                            <asp:BoundField DataField="organisationName" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left"
                                                HeaderText="Organisation" SortExpression="organisationName" />
                                            <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department" />
                                            <asp:BoundField DataField="Designation" HeaderText="Designation" />--%>


                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="editImageButton" runat="server" ImageUrl="~/App_Themes/mydefault/Images/Add.png" CommandName="Select" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <SelectedRowStyle BorderStyle="None" CssClass="selected_row" />
                                        <RowStyle BackColor="#FCFCFC" />
                                        <HeaderStyle BackColor="#E9EAEF" />
                                        <AlternatingRowStyle BackColor="#F4F4F7" />
                                    </asp:GridView>

                                </div>

                            </asp:Panel>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlAddress" />
                            <asp:AsyncPostBackTrigger ControlID="GridView1" />
                            <asp:PostBackTrigger ControlID="BtnRemoveContact" />
                            <asp:PostBackTrigger ControlID="btnSaveContact" />

                        </Triggers>
                    </asp:UpdatePanel>
                </asp:View>
            </asp:MultiView>

        </div>
    </form>
</body>
</html>
