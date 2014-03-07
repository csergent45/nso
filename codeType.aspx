<%@ Page Language="VB" AutoEventWireup="false" CodeFile="codeType.aspx.vb" Inherits="codeType" %>
<!DOCTYPE html>
<html lang=en>
<head id="headRequestType" runat="server">
    <meta charset="utf-8" />
    <title>Request Type</title>

    <link rel="Stylesheet" media="screen" href="css/styles.css"/>


    <!--[if lt IE 9]>
        <script src="http://html5shiv.googlecode.com/svn/trunk/html5.js">

        </script>
    <![endif]-->
</head>
<body>
    <form id="frmCodeType" runat="server">
        <div id="codeTypeTitle">
            <p>Select Case Type</p>
        </div>


        <div id="caseType">
            <asp:HyperLink ID="lnkNuisance" 
                           runat="server"
                           EnableViewState="False"
                           NavigateUrl="~/nuisance.aspx">Nuisance</asp:HyperLink>
            
            <br />

            <asp:HyperLink ID="lnkSeventyTwoHour"
                           runat="server"
                           EnableViewState="False"
                           NavigateUrl="~/seventyTwoHour.aspx">72 Hour</asp:HyperLink>

            <br />

            <asp:HyperLink ID="lnkHousing"
                           runat="server"
                           EnableViewState="False"
                           NavigateUrl="~/housing.aspx">Housing</asp:HyperLink>

            <br />

            <asp:HyperLink ID="lnkSecure"
                           runat="server"
                           EnableViewState="False"
                           NavigateUrl="~/secure.aspx">Secure</asp:HyperLink>

            <br />

            <asp:HyperLink ID="lnkUhh"
                           runat="server"
                           EnableViewState="False"
                           NavigateUrl="~/uhh.aspx">UHH</asp:HyperLink>

            <br />

            <asp:HyperLink ID="lnkWeeds"
                           runat="server"
                           EnableViewState="False"
                           NavigateUrl="~/weeds.aspx">Weeds</asp:HyperLink>


            <br />

            <asp:HyperLink ID="lnkHelp"
                           runat="server"
                           EnableViewState="False"
                           NavigateUrl="~/help.aspx">Help       </asp:HyperLink>


        </div>


    </form>
</body>
</html>
