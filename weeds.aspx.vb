Imports System.Data.OleDb.OleDbConnection
Imports System.Data
Imports System.Data.OleDb.OleDbException
Imports System.Data.OleDb


Partial Class weeds
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Ensures the user entered both a user ID and Password
        If Session("userName") = "" Or Session("password") = "" Then
            Response.Redirect("login.aspx")
        End If

        ' running JavaScript from code - http://forums.asp.net/t/1382342.aspx
        btnStartCase.Attributes.Add("onclick", "showAddCase();")


        'This sets the page redirect value to nothing for use in adding address codes
        Session("webPage") = ""


        Select Case Session("userName")
            Case "DRHOULT"
                ddlDistrict.SelectedItem.Text = "Danny Hoult"
                ddlDistrict.SelectedValue = "NE"
            Case "DLRAVER"
                ddlDistrict.SelectedItem.Text = "Doug Raver"
                ddlDistrict.SelectedValue = "SE"
            Case "JHIGHCOCK"
                ddlDistrict.SelectedItem.Text = "John Highcock"
                ddlDistrict.SelectedValue = "NW"
            Case "REOTTO"
                ddlDistrict.SelectedItem.Text = "Ron Otto"
                ddlDistrict.SelectedValue = "SW"
            Case "SGKRETSI"
                ddlDistrict.SelectedItem.Text = "Susan Kretsinger"
                ddlDistrict.SelectedValue = "FL"
            Case "DCSERGEN"
                ddlDistrict.SelectedItem.Text = "Chris Sergent"
                ddlDistrict.SelectedValue = "FL"
            Case "CALASKOW"
                ddlDistrict.SelectedItem.Text = " Cheryl Laskowski"
                ddlDistrict.SelectedValue = "FL"
            Case "SASTARK"
                ddlDistrict.SelectedItem.Text = "Seth Stark"
                ddlDistrict.SelectedValue = "FL"
            Case Else
                ddlDistrict.SelectedItem.Text = "--Select One--"
        End Select

    End Sub

    Protected Sub btnStartCase_Click(sender As Object, e As System.EventArgs) Handles btnStartCase.Click

        Select Case Session("userName")
            Case "DRHOULT"
                ddlDistrict.SelectedItem.Text = "Danny Hoult"
                ddlDistrict.SelectedValue = "NE"
            Case "DLRAVER"
                ddlDistrict.SelectedItem.Text = "Doug Raver"
                ddlDistrict.SelectedValue = "SE"
            Case "JHIGHCOCK"
                ddlDistrict.SelectedItem.Text = "John Highcock"
                ddlDistrict.SelectedValue = "NW"
            Case "REOTTO"
                ddlDistrict.SelectedItem.Text = "Ron Otto"
                ddlDistrict.SelectedValue = "SW"
            Case "SGKRETSI"
                ddlDistrict.SelectedItem.Text = "Susan Kretsinger"
                ddlDistrict.SelectedValue = "FL"
            Case "DCSERGEN"
                ddlDistrict.SelectedItem.Text = "Chris Sergent"
                ddlDistrict.SelectedValue = "FL"
            Case "CALASKOW"
                ddlDistrict.SelectedItem.Text = " Cheryl Laskowski"
                ddlDistrict.SelectedValue = "FL"
            Case "SASTARK"
                ddlDistrict.SelectedItem.Text = "Seth Stark"
                ddlDistrict.SelectedValue = "FL"
            Case Else
                ddlDistrict.SelectedItem.Text = "--Select One--"
        End Select


        Dim conn As Data.OleDb.OleDbConnection
        Dim commAddrCode As Data.OleDb.OleDbCommand

        Dim commCount As Data.OleDb.OleDbCommand
        Dim reader As Data.OleDb.OleDbDataReader



        conn = New Data.OleDb.OleDbConnection(Session("connectionString"))

       
        'commAddrCode = New Data.OleDb.OleDbCommand("SELECT CASE WHEN ADDRESS_CODE IS NULL THEN '99998' ELSE ADDRESS_CODE END FROM GENINFO.ADDRESS_LOG WHERE PIN_NUM = '" & txtPin.Text & "'", conn)
        commCount = New Data.OleDb.OleDbCommand("SELECT COUNT(ADDRESS_CODE) AS MySelection FROM GENINFO.ADDRESS_LOG WHERE PIN_NUM = '" & txtPin.Text & "'", conn)


     
        conn.Open()
        reader = commCount.ExecuteReader()
        reader.Read()

        If reader("MySelection") > 0 Then
            commAddrCode = New Data.OleDb.OleDbCommand("SELECT ADDRESS_CODE FROM GENINFO.ADDRESS_LOG WHERE PIN_NUM = '" & txtPin.Text & "'", conn)
            txtAddressCode.Text = commAddrCode.ExecuteScalar().ToString()

            conn.Close()

            txtComplaintDate.Text = Date.Today
            txtDateEntered.Text = Date.Today
            ddlDistrict.Enabled = True
            txtInitiatedBy.Text = "I"
            btnCreateCase.Enabled = True
            txtCaseStatus.Enabled = True
            lblCaseCreated.Text = ""
            ddlMctLot.Enabled = True
            ddlLotType.Enabled = True
            txtMowArea.Enabled = True
            txtLocSuppliment.Enabled = True
            txtInitiatedBy.Enabled = True
            txtRecheckDate.Enabled = True
            txtComplaintDate.Enabled = True

            txtRecheckDate.Text = DateAdd(DateInterval.Weekday, 5, Date.Today)

            
        Else
            commAddrCode = New Data.OleDb.OleDbCommand("SELECT MAX(ADDRESS_CODE + 1) FROM GENINFO.ADDRESS_LOG WHERE ADDRESS_CODE < '99998'", conn)
            conn.Close()
            conn.Open()
            Session("addressCode") = commAddrCode.ExecuteScalar().ToString()

            conn.Close()

            Session("PinNum") = txtPin.Text

            Session("webPage") = "weeds.aspx"

            Response.Redirect("newAddressCode.aspx")

        End If

        ddlDistrict.Focus()


    End Sub

    Protected Sub btnCreateCase_Click(sender As Object, e As System.EventArgs) Handles btnCreateCase.Click
        Dim connCreateCase As Data.OleDb.OleDbConnection
        Dim commCreateCase As Data.OleDb.OleDbCommand

        connCreateCase = New Data.OleDb.OleDbConnection(Session("connectionString"))


        Dim complaintDate As Date
        Dim recheckDate As Date
        Dim dateEntered As Date

        Dim myYear As String


        myYear = "YEAR"


        commCreateCase = New Data.OleDb.OleDbCommand("SELECT RTRIM(CHAR(""" & myYear & """(CURRENT DATE))) || CASE WHEN MAX(RIGHT(CASE_NUMBER,5)) IS NULL THEN '00001' ELSE RIGHT('00000' || RTRIM(CHAR(MAX(INTEGER(RIGHT(CASE_NUMBER,5))) + 1)),5) END FROM WEEDS.CASE_LOG WHERE LEFT(CASE_NUMBER,4) =""" & myYear & """(CURRENT DATE)", connCreateCase)

        connCreateCase.Open()


        txtCaseNumber.Text = commCreateCase.ExecuteScalar().ToString()


        connCreateCase.Close()


        complaintDate = Date.Parse(txtComplaintDate.Text)

        dateEntered = Date.Parse(txtDateEntered.Text)


        If txtRecheckDate.Text = String.Empty Then
            commCreateCase = New Data.OleDb.OleDbCommand( _
            "INSERT INTO WEEDS.CASE_LOG(CASE_NUMBER,CASE_STATUS,ADDRESS_CODE," + _
                                        "COMPLAINT_DATE,DISTRICT,INITIATED_BY," + _
                                        "DATE_ENTERED,MCT_LOT)" + _
            "VALUES('" & txtCaseNumber.Text & "'," + _
                    "'" & txtCaseStatus.Text & "'," + _
                    "'" & txtAddressCode.Text & "'," + _
                    "'" & complaintDate & "'," + _
                    "'" & ddlDistrict.SelectedValue & "'," + _
                    "'" & txtInitiatedBy.Text & "'," + _
                    "'" & dateEntered & "'," + _
                    "'" & ddlMctLot.SelectedValue & "')", connCreateCase)
            connCreateCase.Open()
            commCreateCase.ExecuteNonQuery()
        Else
            recheckDate = Date.Parse(txtRecheckDate.Text)
            commCreateCase = New Data.OleDb.OleDbCommand( _
            "INSERT INTO WEEDS.CASE_LOG(CASE_NUMBER,CASE_STATUS,ADDRESS_CODE," + _
                                        "COMPLAINT_DATE,DISTRICT,INITIATED_BY," + _
                                        "RECHECK_DATE,DATE_ENTERED,MCT_LOT)" + _
            "VALUES('" & txtCaseNumber.Text & "'," + _
                    "'" & txtCaseStatus.Text & "'," + _
                    "'" & txtAddressCode.Text & "'," + _
                    "'" & complaintDate & "'," + _
                    "'" & ddlDistrict.SelectedValue & "'," + _
                    "'" & txtInitiatedBy.Text & "'," + _
                    "'" & recheckDate & "'," + _
                    "'" & dateEntered & "'," + _
                    "'" & ddlMctLot.SelectedValue & "')", connCreateCase)
            connCreateCase.Open()
            commCreateCase.ExecuteNonQuery()

        End If


        connCreateCase.Close()

        Dim commCaseLocation As Data.OleDb.OleDbCommand


        commCaseLocation = New Data.OleDb.OleDbCommand( _
            "INSERT INTO WEEDS.CASE_LOCATION(CASE_NUMBER,LOC_SUPPLIMENT)" + _
            "VALUES('" & txtCaseNumber.Text & "'," + _
                    "'" & txtLocSuppliment.Text & "')", connCreateCase)
        connCreateCase.Open()
        commCaseLocation.ExecuteNonQuery()

        connCreateCase.Close()


        commCreateCase = New Data.OleDb.OleDbCommand("SELECT INSPECTOR_CODE FROM TESTGEN.EUD.INSPECTOR_LOG WHERE MIS_USER_ID='" & Session("userName") & "'", connCreateCase)
        connCreateCase.Open()


        Dim inspectorCode As String
        inspectorCode = commCreateCase.ExecuteScalar().ToString()


        connCreateCase.Close()

        

        commCreateCase = New Data.OleDb.OleDbCommand( _
            "INSERT INTO WEEDS.INSPECTIONS(CASE_NUMBER,INSPECTION_DATE,INSPECT_REMARKS,INSPECTOR_CODE)" + _
            "VALUES('" & txtCaseNumber.Text & "'," + _
                    "'" & txtComplaintDate.Text & "'," + _
                    "'" & txtMowArea.Text & "(" & ddlDistrict.SelectedItem.Text & ")" & "'," + _
                    "'" & inspectorCode & "')", connCreateCase)

        connCreateCase.Open()
        commCreateCase.ExecuteNonQuery()

        connCreateCase.Close()

        Dim lot As String = ddlLotType.SelectedValue
        Dim lotDate As String = txtComplaintDate.Text
        Dim addressCode As String = txtAddressCode.Text


        commCreateCase = New Data.OleDb.OleDbCommand("UPDATE TESTGEN.GENINFO.ZONING_CENSUS SET LOT='" & lot & "', LOT_DATE='" & lotDate & "' WHERE (ADDRESS_CODE='" & addressCode & "')", connCreateCase)

        Try
            connCreateCase.Open()
            commCreateCase.ExecuteNonQuery()
        Catch er As OleDbException
            Dim errorMessages As String = ""
            Dim i As Integer

            For i = 0 To er.Errors.Count - 1
                errorMessages += "Index #" & i.ToString() & ControlChars.Cr _
                    & "Message: " & er.Errors(i).Message & ControlChars.Cr _
                    & "NativeError: " & er.Errors(i).NativeError & ControlChars.Cr _
                    & "Source: " & er.Errors(i).Source & ControlChars.Cr _
                    & "SQLState: " & er.Errors(i).SQLState & ControlChars.Cr _
                    & "Error Code: " & er.ErrorCode & ControlChars.Cr _
                    & "Type: " & er.Errors(i).GetType.ToString


            Next i

            Dim log As System.Diagnostics.EventLog = New System.Diagnostics.EventLog()
            log.Source = "NSO"
            log.WriteEntry(errorMessages)
            Console.WriteLine("An exception occurred in attempting to update TEST.GENINFO.ZONING_CENSUS in Weeds. Please contact your system administrator.")

        Finally
            connCreateCase.Close()
        End Try

        lblCaseCreated.Text = "Case " & txtCaseNumber.Text & " Created!" & "<br/>"
        btnCreateCase.Enabled = False
        txtCaseStatus.Text = ""
        txtCaseNumber.Text = ""
        txtAddressCode.Text = ""
        txtComplaintDate.Text = ""
        txtRecheckDate.Text = ""
        txtDateEntered.Text = ""
        txtLocSuppliment.Text = ""
        ddlLotType.SelectedValue = "O"
        txtMowArea.Text = ""
        ddlLotType.Enabled = False
        ddlMctLot.Enabled = False
        txtMowArea.Enabled = False
        txtMowArea.Text = "Entire Lot"
        txtPin.Text = ""
        txtAddress.Text = ""
        txtPrimaryAddress.Text = ""
        txtPrimaryName.Text = ""
        txtLegalDescription.Text = ""
        ddlDistrict.Enabled = False
        txtInitiatedBy.Text = "I"
        ddlLotType.SelectedValue = "O"
        ddlDistrict.Items.Clear()
        ddlDistrict.Items.Add(New ListItem(Text:="--Select One--", value:=""))
        ddlDistrict.Items.Add(New ListItem(Text:="Danny Holt", value:="NE"))
        ddlDistrict.Items.Add(New ListItem(Text:="Doug Raver", value:="SE"))
        ddlDistrict.Items.Add(New ListItem(Text:="John Highcock", value:="NW"))
        ddlDistrict.Items.Add(New ListItem(Text:="Ron Otto", value:="SW"))
        ddlDistrict.Items.Add(New ListItem(Text:="Susan Krestinger", value:="FL"))
        txtLocSuppliment.Enabled = False
        txtInitiatedBy.Enabled = False
        txtRecheckDate.Enabled = False
        txtComplaintDate.Enabled = False


    End Sub

    Protected Sub ddlLotType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlLotType.SelectedIndexChanged
        If ddlLotType.SelectedValue = "O" Then
            txtCaseStatus.Text = "R"
            txtRecheckDate.Text = DateAdd(DateInterval.Weekday, 5, Date.Today)
        Else
            txtCaseStatus.Text = "I"
            txtRecheckDate.Text = ""
        End If

        ddlLotType.Focus()
    End Sub

    Protected Sub txtLocSuppliment_TextChanged(sender As Object, e As System.EventArgs) Handles txtLocSuppliment.TextChanged
        txtLocSuppliment.Text = txtLocSuppliment.Text.Replace("'", "''")

    End Sub

    Protected Sub txtMowArea_TextChanged(sender As Object, e As System.EventArgs) Handles txtMowArea.TextChanged
        txtMowArea.Text = txtMowArea.Text.Replace("'", "''")
    End Sub

    
End Class
