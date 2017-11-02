<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="jquery.loadmask.css" rel="stylesheet" />
     
		<script type="text/javascript" src ="jquery-latest.pack.js"></script>
		<script type='text/javascript' src= "jquery.loadmask.js"></script>
    <style>
			body{font-size:11px;font-family:tahoma;}
			#content { padding:5px;width:466px;
            z-index: 1;
            left: 332px;
            top: 184px;
            position: absolute;
            height: 139px;
        }
			#buttons { padding-left:40px; }
		#process {
            z-index: 1;
            left: 286px;
            top: 72px;
            position: absolute;
            height: 27px;
            width: 67px;
        }
       
		</style>
</head>

<body>
    <form id="form1" runat="server">
        <asp:scriptmanager runat="server"></asp:scriptmanager>
         <asp:UpdatePanel runat="server">
    <ContentTemplate>
        &nbsp;<div id="content">
			<div style="z-index: 1; left: 8px; top: 37px; position: absolute; height: 22px; width: 172px; right: 186px; font-size: medium">Quotation Number  <asp:TextBox ID="TextBox1" runat="server" style="z-index: 1; left: 174px; top: -5px; position: absolute; width: 162px; height: 22px"></asp:TextBox>  </div>
		
            
           
        <!-- Content -->
        	<div id="buttons">
            <asp:Button ID="process" runat="server" Text="Send" />
		<%--	<input type="button" value="Process" />--%>
			&nbsp;</div>
		</div>
    </ContentTemplate>
    <Triggers>
   <%--      <asp:AsyncPostBackTrigger ControlID="process" /> --%>
        <asp:PostBackTrigger ControlID="process" />
        
    </Triggers>
            
          
		</asp:UpdatePanel>
	
		<script>
		    $(document).ready(function () {
		        $("#process").bind("click", function () {
		            $("#content").mask("Loading...");
		        });

		        
		    });
		</script>

    </form>
</body>
</html>
