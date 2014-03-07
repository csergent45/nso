<%@ Page Language="VB" AutoEventWireup="false" CodeFile="appError.aspx.vb" Inherits="appError" %>
<!DOCTYPE>
<html lang="en">
    <head id="headAppError" runat="server">
        <meta charset="utf-8" />
        <title>Application Error</title>

        <!--The viewport meta tag is used to improve the presentation and behavior of the samples 
            on iOS devices-->
        <meta name="viewport" content="initial-scale=1, maximum-scale=1,user-scalable=no"/>

        <!--[if lt IE 9]>
            <script src="http://html5shiv.googlecode.com/svn/trunk/html5.js">

            </script>
        <![endif]-->
    </head>

    <body>
        <form id="frmHelp" runat="server">
            <asp:ScriptManager ID="scriptManagerAppError"
                               runat="server">
            </asp:ScriptManager>
            <div id="pageError">
            <h1>Application Error</h1>
            <p>This application has encountered an error. Please contact MIS if this problem continues.</p>
            <p>If you feel that you have arrived at this page in error please attempt to  
                <asp:LinkButton ID="lnkLogin" runat="server" PostBackUrl="~/login.aspx">Login</asp:LinkButton>.</p>
        </div>

        </form>
    </body>
</html>