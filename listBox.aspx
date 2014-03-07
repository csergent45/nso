<%@ Page Language="VB" AutoEventWireup="false" CodeFile="listBox.aspx.vb" Inherits="listBox" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
    
    </div>
    <asp:ListBox ID="ListBox1" runat="server" DataSourceID="SqlDataSource1" 
        DataTextField="VIOLATION_TITLE" DataValueField="VIOLATION_CODE" 
        SelectionMode="Multiple" Width="385px"></asp:ListBox>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="Provider=IBMDADB2.IBMDBCL1;Data Source=TESTGEN;Persist Security Info=True;Password=Welcome2u!;User ID=Db2mapr" 
        ProviderName="System.Data.OleDb" 
        
        
        SelectCommand="SELECT VIOLATION_CODE, VIOLATION_TITLE FROM TESTGEN.CODE_ENF.VIOLATION_LOG WHERE (VIOLATION_STATUS = 'A') AND (VISIT_CODE = 'H/5') ORDER BY VIOLATION_TITLE">
    </asp:SqlDataSource>
    <br />
    <br />
    <asp:DataList ID="DataList1" runat="server" DataKeyField="VIOLATION_CODE" 
        DataSourceID="SqlDataSource1" RepeatColumns="1" 
        BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" 
        CellPadding="2" ForeColor="Black" Height="10px">
        <AlternatingItemStyle BackColor="PaleGoldenrod" />
        <FooterStyle BackColor="Tan" />
        <HeaderStyle BackColor="Tan" Font-Bold="True" />
        <ItemTemplate>
             VIOLATION_CODE:
             <asp:Label ID="VIOLATION_CODELabel" runat="server" 
                Text='<%# Eval("VIOLATION_CODE") %>' />
            
            
             <br />
             VIOLATION_TITLE:
            
            
            <asp:Label ID="VIOLATION_TITLELabel" runat="server" 
                Text='<%# Eval("VIOLATION_TITLE") %>' />
           
            <br />
<br />
        </ItemTemplate>
        <SelectedItemStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
    </asp:DataList>
    
   <asp:Label ID="lblFirstName" Text="First Name:" runat="server"></asp:Label><input type="text" name="firstName" />
    
    </form>
</body>
</html>
