<%@ Page Language="VB" AutoEventWireup="false" CodeFile="newAddressCode.aspx.vb" Inherits="newAddressCode" %>
<!DOCTYPE>
<html lang="en">
    <head id="Head1" runat="server">
        <meta charset="utf-8" />
        <title>Add New Address Code</title>

        <!--The viewport meta tag is used to improve the presentation and behavior of the samples 
            on iOS devices-->
        <meta name="viewport" content="initial-scale=1, maximum-scale=1,user-scalable=no"/>

        <!-- Site Styles -->
        <link href="css/styles.css" rel="stylesheet" type="text/css" />

        <!--[if lt IE 9]>
            <script src="http://html5shiv.googlecode.com/svn/trunk/html5.js">

            </script>
        <![endif]-->
    </head>

    <body>
        <form id="frmAddressCode" runat="server">
            <asp:ScriptManager ID="scriptAddressCode"
                               runat="server">
            </asp:ScriptManager>

            <section id="sectionAddressCode">
                <asp:UpdatePanel ID="updateAddressCode" runat="server">
                    <ContentTemplate>
                        <p>The parcel that you selected appears to have no Address Code in the address database.</p>
                        <p>Please select the street here to have an Address Code added to the address database.</p>
                        <p>Once you have selected a street and have added the Address Code to the database,<br /> you will be redirected back to the map.</p>
                        <br />
                        <asp:Label ID="lblStreetName" runat="server" EnableViewState="false" Text="Select a Street:">
                        </asp:Label>
                        
                        <br />

                        <asp:DropDownList ID="ddlStreets" runat="server" AppendDataBoundItems="true" Required="required">
                            <asp:ListItem Text="--Select a Street--" Value=""></asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="txtAddressCode" runat="server" Visible="false">
                        </asp:TextBox>

                        <br />
                        <asp:Button ID="btnEnterAddressCode" runat="server" Text="Add New Address Code" />

                        
                        <asp:TextBox ID="txtWebPage" runat="server" Visible="false"></asp:TextBox>

                    </ContentTemplate>
                </asp:UpdatePanel>
                
            </section>
            
        </form>
    </body>
</html>
