<%@ Page Language="VB" AutoEventWireup="false" CodeFile="login.aspx.vb" Inherits="login" %>

<!DOCTYPE html>
<html lang="en">
    <head runat="server">
        <meta charset="utf-8" />
        <title>NSO Field Operations</title>

        <link rel="Stylesheet" media="screen" href="css/styles.css"/>

        <!--[if lt IE 9]>
            <script src="http://html5shiv.googlecode.com/svn/trunk/html5.js">

            </script>
        <![endif]-->
    </head>

    
    <body>
        <form id="frmLogin" runat="server" defaultbutton="btnLogin">
            <!-- Script Manager Begin -->
            <asp:ScriptManager ID="loginScriptManager"
                               runat="server"
                               EnablePartialRendering="true">
            </asp:ScriptManager>
            <!-- Script Manager End -->


            <!-- Login Title Begin -->
            <div id="loginTitle">
                <p>NSO Field Operations</p>
            </div>
            <!-- Login Title End -->

            
            <!-- Login Panel Begin -->
            <div id="loginPanel">
                <br />
                <span class="requiredNotification">*Denotes Required Field</span>
                <br /><br />
                <asp:Label runat="server"
                           ID="lblUserName"
                           Text="User Name:"
                           EnableViewState="false">
                </asp:Label>

                <asp:TextBox runat="server"
                             ID="txtUserName"
                             TextMode="SingleLine"
                             placeholder="User Name" 
                             required="required"
                             ToolTip="Enter User Name" />

                <br /><br />
                <asp:Label ID="lblPassword"
                           runat="server"
                           Text="Password:"
                           EnableViewState="false">
                </asp:Label>

                <asp:TextBox runat="server"
                             ID="txtPassword"
                             TextMode="Password"
                             placeholder="Password"
                             required="required"
                             Tooltip="Enter Password" />


                <br /><br />
                
                <asp:Button ID="btnLogin"
                            runat="server"
                            Text="Login" />
            </div>
            <!-- Login Panel End -->

            <br />
            <asp:Label runat="server"
                       ID="lblErrorMessage"
                       Text="">
            </asp:Label>
        </form>
    </body>
</html>

