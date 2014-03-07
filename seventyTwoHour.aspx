<%@ Page Language="VB" AutoEventWireup="false" CodeFile="seventyTwoHour.aspx.vb" Inherits="seventyTwoHour" %>
<!DOCTYPE>
<html lang="en">
    <head id="headSeventyTwoHour" runat="server">
        <meta charset="utf-8" />
        <title>72 Hour</title>

        <!--The viewport meta tag is used to improve the presentation and behavior of the samples 
            on iOS devices-->
        <meta name="viewport" content="initial-scale=1, maximum-scale=1,user-scalable=no"/>

        <!-- Leaflet Stylesheets -->
        <link href="css/leaflet.css" rel="stylesheet" type="text/css" />
        <link href="css/demo.css" rel="stylesheet" type="text/css" />
        <link href="css/leaflet.ie.css" rel="stylesheet" type="text/css" />

        <link rel="stylesheet" href="http://heyman.github.io/leaflet-usermarker/src/leaflet.usermarker.css" />

        <!-- Site Styles -->
        <link href="css/styles.css" rel="stylesheet" type="text/css" />

        <!-- jQuery Styles -->
        <link href="css/custom-theme/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" type="text/css" />
        <link href="css/greybox2.css" rel="stylesheet" type="text/css" />

        <!--[if lt IE 9]>
            <script src="http://html5shiv.googlecode.com/svn/trunk/html5.js">

            </script>
        <![endif]-->
    </head>


    <body>
        <form id="frmMap" runat="server">
            <asp:ScriptManager ID="scriptManager"
                               runat="server">
            </asp:ScriptManager>

            <!-- Sidebar Begin -->
            <aside id="sideBar">
                <!-- Sidebar Top Begin -->
                <div id="sideBarTop">
                
                </div>
                <!-- Sidebar Top End -->

                <!-- Sidebar Top Fill Begin -->
                <asp:Panel ID="sideBarTopFill"
                           runat="server"
                           Enabled="false">
                </asp:Panel>
                <!-- Sidebar Top Fill End -->

                <!-- Sidebar Title Begin -->
                <div id="sideBarTitle">
                    <p>NSO Data Entry</p>
                    <!-- Sidebar Content Begin -->
                    <div id="sideBarContent">
                        
                            <h3>Property Info</h3>
                            <!-- Map Selection Form Begin -->
                            <div id="frmMapSelection">
                                <asp:UpdatePanel ID="updateAddressInfo" runat ="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lblPin" runat="server" Text="PIN:" EnableViewState="false">
                                
                                        </asp:Label>

                                        <br />
                                        <asp:TextBox ID="txtPin" runat="server" Enabled="false">
                                
                                        </asp:TextBox>

                                        <br />

                                        <asp:Label ID="lblAddress" runat="server" Text="Address:" EnableViewState="false">
                                
                                        </asp:Label>
                                        <br />
                                        <asp:TextBox ID="txtAddress" runat="server" Enabled="false">
                                
                                        </asp:TextBox>

                                        <br />
                                        <asp:Label ID="lblOwnersName" runat="server" Text="Owner's Name" EnableViewState="false">
                                
                                        </asp:Label>
                                        <br />

                                        <asp:TextBox ID="txtPrimaryName" runat="server" Enabled="false">
                                
                                         </asp:TextBox>
                                         <br />

                                        <asp:Label ID="lblOwnersAddress" runat="server" Text="Owner's Address" EnableViewState="false">
                                
                                        </asp:Label>

                                        <br />

                                        <asp:TextBox ID="txtPrimaryAddress" runat="server" Enabled="false">
                                
                                        </asp:TextBox>

                                        <br />

                                        <asp:Label ID="lblLegalDescription" runat="server" Text="Legal Description" EnableViewState="false">
                                
                                        </asp:Label>
                                        <br />
                                        <asp:TextBox ID="txtLegalDescription" runat="server" Enabled="false">
                                
                                        </asp:TextBox>

                                        <br />

                                        <asp:Button ID="btnStartCase" runat="server" Text="Start Case" Enabled="false" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <!-- Map Selection Form End -->
                        

                        
                            <h3>Case Entry</h3> 
                            <div id="frmCaseEntry">
                                <asp:UpdatePanel ID="updateStartCase"
                                                 runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="btnCreateCase"
                                                        runat="server"
                                                        Enabled="false"
                                                        Text="Create Case" />
                                        <br />
                                        <asp:Label ID="lblCaseCreated"
                                                    runat="server"
                                                    CssClass="successMessage">
                                        </asp:Label>

                                        <asp:TextBox ID="txtCaseNumber"
                                                     runat="server"
                                                     Visible="false">
                                        </asp:TextBox>

                                        
                                        <asp:TextBox ID="txtCaseStatus"
                                                     runat="server"
                                                     AutoPostBack="true"
                                                     Text="A"
                                                     Visible="false">
                                        </asp:TextBox>
                                        
                                        <asp:TextBox ID="txtAddressCode"
                                                     runat="server"
                                                     Visible="false">
                                        </asp:TextBox>
                                        <br />
                                         <asp:Label ID="lblLotType" Text="Lot Type:" runat="server" EnableViewState="false">
                                        </asp:Label>

                                        <br />
                                         <asp:DropDownList ID="ddlLotType" AppendDataBoundItems="true"
                                                           runat="server"
                                                           AutoPostBack="true"
                                                           Enabled="false">
                                             <asp:ListItem Selected="True" Value="O">Occupied</asp:ListItem>
                                             <asp:ListItem Value="V">Vacant Lot</asp:ListItem>
                                             <asp:ListItem Value="S">Vacant Structure</asp:ListItem>
                                        </asp:DropDownList>

                                        <br />
                                        
                                        <asp:Label ID="lblVacantLotDescription"
                                                   runat="server"
                                                   Text="Vacant Lot Desc.:"
                                                   EnableViewState="false">
                                        </asp:Label>
                                        <br />

                                        <asp:TextBox ID="txtVacantLotDescription"
                                                     runat="server"
                                                     TextMode="MultiLine"
                                                     ToolTip="Enter Vacant Lot Description"
                                                     placeholder="Lot Desc."
                                                     CssClass="multiLine"
                                                     MaxLength="200">
                                        </asp:TextBox>

                                        <br />

                                        <asp:Label ID="lblMctLot"
                                                   runat="server"
                                                   Text="MCT Lot:"
                                                   EnableViewState="false">
                                        </asp:Label>
                                        <br />
                                        <asp:DropDownList ID="ddlMctLot"
                                                          runat="server"
                                                          Enabled="false">
                                            <asp:ListItem>N</asp:ListItem>
                                            <asp:ListItem>Y</asp:ListItem>
                                        </asp:DropDownList>

                                        <br />

                                        <asp:Label ID="lblSingleViolation"
                                                   runat="server"
                                                   Text="Single Violation:"
                                                   EnableViewState="false">
                                        </asp:Label>

                                        <br />
                                        <asp:TextBox ID="txtSingleViolation"
                                                     runat="server"
                                                     TextMode="SingleLine"
                                                     Text="72H">
                                        </asp:TextBox>

                                        <br />

                                        <asp:Label ID="lblContactTypeMade" runat="server" Text="Contact Type Made:" EnableViewState="false">
                                        
                                        </asp:Label>

                                        <br />

                                        <asp:DropDownList ID="ddlContactTypeMade" runat="server" AppendDataBoundItems="true" AutoPostBack="true" Enabled="false">
                                            <asp:ListItem Selected="True" Value="0">None</asp:ListItem>
                                            <asp:ListItem Value="1">Telephone</asp:ListItem>
                                            <asp:ListItem Value="2">Face to Face</asp:ListItem>
                                            <asp:ListItem Value="3">Door Hangar</asp:ListItem>
                                            <asp:ListItem Value="4">Email/Text</asp:ListItem>
                                            <asp:ListItem Value="5">Other</asp:ListItem>
                                            <asp:ListItem Value="6">Mail</asp:ListItem>
                                        </asp:DropDownList>


                                        <br />

                                        <asp:Label ID="lblInspectionComments" Text="Insp. Comments:" runat="server" EnableViewState="false">
                                        </asp:Label>

                                        <br />

                                        <asp:TextBox ID="txtInspectionComments"
                                                     runat="server"
                                                     TextMode="MultiLine"
                                                     ToolTip="Enter Inspection Comments"
                                                     placeholder="Insp. Comments"
                                                     CssClass="multiLine">
                                        </asp:TextBox>

                                        <br />

                                        <asp:Label ID="lblLetterComments" Text="Letter Comments:" runat="server" EnableViewState="false">
                                        </asp:Label>

                                        <br />

                                        <asp:TextBox ID="txtLetterComments"
                                                     runat="server"
                                                     TextMode="MultiLine"
                                                     placeholder="Letter Comments"
                                                     ToolTip="Enter Letter Comments"
                                                     CssClass="multiLine">
                                        </asp:TextBox>

                                        <br />

                                        <asp:Label ID="lblComplaintDate" runat="server" Text="Complaint Date:" EnableViewState="false">
                                        </asp:Label>

                                        <br />

                                        <asp:TextBox ID="txtComplaintDate"
                                                     runat="server"
                                                     TextMode="SingleLine"
                                                     ToolTip="Enter Complaint Date"
                                                     placeholder="Complaint Date">
                                        </asp:TextBox>
                                        <br />

                                     
                                        <asp:Label ID="lblRecheckDate"
                                                   runat="server"
                                                   Text="Recheck Date:"
                                                   EnableViewState="false">
                                        </asp:Label>
                                        <br />
                                        <asp:TextBox ID="txtRecheckDate"
                                                     runat="server"
                                                     TextMode="SingleLine"
                                                     placeholder="Recheck Date">
                                        </asp:TextBox>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                               
                            </div>                       
                        


                            <h3>Navigation/Help</h3>
                            <div id="frmNavHelp">
                               
                                <asp:HyperLink ID="lnkNuisance" 
                                               runat="server"
                                               NavigateUrl="~/nuisance.aspx">Nuisance
                                </asp:HyperLink>
            
                                <br />

                                <asp:HyperLink ID="lnkSeventyTwoHour"
                                               runat="server"
                                               NavigateUrl="~/seventyTwoHour.aspx">72 Hour
                                </asp:HyperLink>

                                <br />

                                <asp:HyperLink ID="lnkHousing"
                                               runat="server"
                                               NavigateUrl="~/housing.aspx">Housing
                                </asp:HyperLink>

                                <br />

                                <asp:HyperLink ID="lnkSecure"
                                               runat="server"
                                               NavigateUrl="~/secure.aspx">Secure
                                </asp:HyperLink>

                                <br />

                                <asp:HyperLink ID="lnkUhh"
                                               runat="server"
                                               NavigateUrl="~/uhh.aspx">UHH
                                </asp:HyperLink>

                                <br />

                                <asp:HyperLink ID="lnkWeeds"
                                               runat="server"
                                               NavigateUrl="~/weeds.aspx">Weeds
                                </asp:HyperLink>


                                <br />

                                <asp:HyperLink ID="lnkHelp"
                                               runat="server"
                                               NavigateUrl="~/help.aspx">Help
                                </asp:HyperLink>

                            </div>
                        

                    </div>
                    <!-- Sidebar Content End -->
                </div>
                <!-- Sidebar Title End -->

                <!-- Sidebar Bottom Fill Begin -->
                <asp:Panel ID="sideBarBottomFill"
                           runat="server"
                           Enabled="false">
                </asp:Panel>
                <!-- Sidebar Bottom Fill End -->

                <!-- Sidebar Bottom Begin -->
                <div id="sideBarBottom">
                
                </div>
                <!-- Sidebar Bottom End -->
            </aside>
            <!-- Sidebar End -->

            <!-- Display Map Section Begin -->
            <section id="mapSection">
        
            </section>
            <!-- Display Map Section End -->




            <!-- Display Footer Title Begin -->
            <div id="footerTitle">
                <p>Ask For Assistance</p>
                <!-- Display Footer Content Begin -->
                <footer>
                    <p>
                        Contact <a href="mailto:dsergent@decaturil.gov">Chris Sergent</a>
                        if you need assistance or need to report an issue.
                    </p>
                </footer>
                <!-- Display Footer Content End -->
            </div>
            <!-- Display Footer Title End -->


        </form>

        <!-- Script Post Load Begin -->

        <!-- Leaflet JS Library: http://leafletjs.com/ -->
        <script type="text/javascript" src="js/leaflet.js">
    
        </script>

        <script src="js/leaflet-src.js" type="text/javascript">
    
        </script>

        <!-- Esri Leaflet plug-in: https://github.com/Esri/esri-leaflet -->
        <script type="text/javascript" src="js/esri-leaflet.min.js">
    
        </script>
        


        <script src="js/demo.js" type="text/javascript">
    
        </script>

        <!-- Leaflet Marker: http://heyman.github.io/leaflet-usermarker/src/leaflet.usermarker.js -->
        <script src="js/leaflet.usermarker.js" type="text/javascript">
    
        </script>
    
        <!-- jQuery Library -->
        <script src="js/jquery-1.9.1.js" type="text/javascript"></script>
        <script src="js/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
        <script src="js/jquery.greybox2.js" type="text/javascript"></script>

        <!-- Enables jquery mouse events to work as touch events on mobile devices: https://github.com/copernicus365/jquery-ui-touch-punch/blob/master/jquery.ui.touch-punch.js -->
        <script src="js/jquery.ui.touch-punch.js" type="text/javascript"></script>

        <!-- Page Initialization -->
        <script src="js/reactor.js" type="text/javascript"></script>

        <!-- Script Post Load End -->
    </body>
</html>
