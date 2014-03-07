<%@ Page Language="VB" AutoEventWireup="false" CodeFile="help.aspx.vb" Inherits="help" %>
<!DOCTYPE>
<html lang="en">
    <head runat="server">
        <meta charset="utf-8" />
        <title>NSO Help</title>

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
            <asp:ScriptManager ID="scriptManagerHelp"
                               runat="server">
            </asp:ScriptManager>

            <section id="sectionHelp">
                <p>Help will be displayed on this page as needed.</p>
            </section>
            
        </form>
    </body>
</html>
