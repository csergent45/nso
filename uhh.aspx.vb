
Partial Class housingUhh
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session("userName") = "" Or Session("password") = "" Then
            Response.Redirect("login.aspx")
        End If

        ' running JavaScript from code - http://forums.asp.net/t/1382342.aspx
        btnStartCase.Attributes.Add("onclick", "showAddCase();")


        'This sets the page redirect value to nothing for use in adding address codes
        Session("webPage") = ""
    End Sub

    Protected Sub btnStartCase_Click(sender As Object, e As System.EventArgs) Handles btnStartCase.Click
        Dim conn As Data.OleDb.OleDbConnection
        Dim commAddrCode As Data.OleDb.OleDbCommand

        Dim commCount As Data.OleDb.OleDbCommand
        Dim reader As Data.OleDb.OleDbDataReader

        conn = New Data.OleDb.OleDbConnection(Session("connectionString"))

        commCount = New Data.OleDb.OleDbCommand("SELECT COUNT(ADDRESS_CODE) AS MySelection FROM GENINFO.ADDRESS_LOG WHERE PIN_NUM = '" & txtPin.Text & "'", conn)



        conn.Open()
        reader = commCount.ExecuteReader()
        reader.Read()

        If reader("MySelection") > 0 Then

            commAddrCode = New Data.OleDb.OleDbCommand("SELECT ADDRESS_CODE FROM GENINFO.ADDRESS_LOG WHERE PIN_NUM = '" & txtPin.Text & "'", conn)
            txtAddressCode.Text = commAddrCode.ExecuteScalar().ToString()

            conn.Close()
            'Enable the entry form with default values
            txtComplaintDate.Text = Date.Today
            ddlLotType.Enabled = True
            ddlMctLot.Enabled = True
            ddlContactTypeMade.Enabled = True
            txtInspectionComments.Enabled = True
            txtLetterComments.Enabled = True
            txtRecheckDate.Enabled = True
            txtComplaintDate.Enabled = True
            btnCreateCase.Enabled = True

            txtRecheckDate.Text = DateAdd(DateInterval.Weekday, 5, Date.Today)
        Else
            commAddrCode = New Data.OleDb.OleDbCommand("SELECT MAX(ADDRESS_CODE + 1) FROM GENINFO.ADDRESS_LOG WHERE ADDRESS_CODE < '99998'", conn)
            conn.Close()
            conn.Open()
            Session("addressCode") = commAddrCode.ExecuteScalar().ToString()

            conn.Close()

            Session("PinNum") = txtPin.Text

            Session("webPage") = "uhh.aspx"

            Response.Redirect("newAddressCode.aspx")
        End If

        Dim comm As Data.OleDb.OleDbCommand
        lstViolations.Items.Clear()

        conn = New Data.OleDb.OleDbConnection(Session("connectionString"))
        comm = New Data.OleDb.OleDbCommand("SELECT VIOLATION_CODE, VIOLATION_TITLE FROM TESTGEN.CODE_ENF.VIOLATION_LOG WHERE (VIOLATION_STATUS = 'A') AND (VISIT_CODE='H/5' OR VISIT_CODE='UHH') ORDER BY VIOLATION_TITLE", conn)

        Try
            conn.Open()
            reader = comm.ExecuteReader()

            With lstViolations
                .DataSource = reader
                .DataTextField = "VIOLATION_TITLE"
                .DataValueField = "VIOLATION_CODE"
                .DataBind()

            End With
            reader.Close()
        Finally
            conn.Close()
        End Try

        'Setting the font size for the listbox here because the CSS is not working for fontSize
        lstViolations.Font.Size = FontUnit.Point(9)

        lstViolations.Enabled = True

    End Sub

    Protected Sub btnCreateCase_Click(sender As Object, e As System.EventArgs) Handles btnCreateCase.Click
        Dim connCreateCase As Data.OleDb.OleDbConnection
        Dim commCreateCase As Data.OleDb.OleDbCommand

        connCreateCase = New Data.OleDb.OleDbConnection(Session("connectionString"))

        'Date complaint was initiated
        Dim complaintDate As Date

        'Date required to recheck property
        Dim recheckDate As Date


        Dim myYear As String

        myYear = "YEAR"

        commCreateCase = New Data.OleDb.OleDbCommand("SELECT RTRIM(CHAR(""" & myYear & """(CURRENT DATE))) || CASE WHEN MAX(RIGHT(CASE_NUMBER,5)) IS NULL THEN '00001' ELSE RIGHT('00000' || RTRIM(CHAR(MAX(INTEGER(RIGHT(CASE_NUMBER,5))) + 1)),5) END FROM CODE_ENF.CE_CASE_LOG WHERE LEFT(CASE_NUMBER,4) =""" & myYear & """(CURRENT DATE)", connCreateCase)

        connCreateCase.Open()

        txtCaseNumber.Text = commCreateCase.ExecuteScalar().ToString()

        connCreateCase.Close()

        complaintDate = Date.Parse(txtComplaintDate.Text)




        'Basic Information
        commCreateCase = New Data.OleDb.OleDbCommand( _
            "INSERT INTO CODE_ENF.CE_CASE_LOG(CASE_NUMBER, USERID, DATE_ENTERED, CASE_STATUS, ADDRID)" + _
            "VALUES('" & txtCaseNumber.Text & "'," + _
                    "'" & Session("userName") & "'," + _
                    "'" & txtComplaintDate.Text & "'," + _
                    "'" & txtCaseStatus.Text & "'," + _
                    "'" & txtAddressCode.Text & "')", connCreateCase)
        connCreateCase.Open()
        commCreateCase.ExecuteNonQuery()

        connCreateCase.Close()

        'Get Inspector Code
        commCreateCase = New Data.OleDb.OleDbCommand("SELECT INSPECTOR_CODE FROM TESTGEN.EUD.INSPECTOR_LOG WHERE MIS_USER_ID='" & Session("userName") & "'", connCreateCase)
        connCreateCase.Open()


        Dim inspectorCode As String
        inspectorCode = commCreateCase.ExecuteScalar().ToString()


        connCreateCase.Close()

        'Violations
        Dim commList As Data.OleDb.OleDbCommand


        connCreateCase.Open()

        For i As Integer = 0 To lstViolations.Items.Count - 1

            If lstViolations.Items(i).Selected = True Then
                commList = New Data.OleDb.OleDbCommand("INSERT INTO CODE_ENF.CE_ACTIVE_VIOL(CASE_NUMBER, VIOLATION_CODE, VIOLATION_DATE, INSPECTOR_CODE)" + _
                                                            "VALUES('" & txtCaseNumber.Text & "'," + _
                                                                    "'" & lstViolations.Items(i).Value & "'," + _
                                                                    "'" & txtComplaintDate.Text & "'," + _
                                                                    "'" & inspectorCode & "')", connCreateCase)
                commList.ExecuteNonQuery()

            End If
        Next



        connCreateCase.Close()

        'Inspection Comments
        If txtInspectionComments.Text <> String.Empty Then
            commCreateCase = New Data.OleDb.OleDbCommand( _
                "INSERT INTO CODE_ENF.INSPECT_COMMENT(CASE_NUMBER, INSPECTION_DATE, COMMENT_DESC, INSPECTOR_CODE, CONTACT_TYPE)" + _
                "VALUES('" & txtCaseNumber.Text & "'," + _
                        "'" & txtComplaintDate.Text & "'," + _
                        "'" & txtInspectionComments.Text & "'," + _
                        "'" & inspectorCode & "'," + _
                        "'" & ddlContactTypeMade.SelectedValue & "')", connCreateCase)
            connCreateCase.Open()
            commCreateCase.ExecuteNonQuery()
            connCreateCase.Close()
        End If

        'Letter Comments
        If txtLetterComments.Text <> String.Empty Then
            commCreateCase = New Data.OleDb.OleDbCommand( _
                "INSERT INTO CODE_ENF.LETTER_COMMENT(CASE_NUMBER, INSPECTION_DATE, COMMENT_DESC, INSPECTOR_CODE)" + _
                "VALUES('" & txtCaseNumber.Text & "'," + _
                        "'" & txtComplaintDate.Text & "'," + _
                        "'" & txtLetterComments.Text & "'," + _
                        "'" & inspectorCode & "')", connCreateCase)
            connCreateCase.Open()
            commCreateCase.ExecuteNonQuery()
            connCreateCase.Close()
        End If


        'Inspection Results
        commCreateCase = New Data.OleDb.OleDbCommand( _
                "INSERT INTO CODE_ENF.CE_INSPECT_RESULT(CASE_NUMBER, VISIT_CODE, INSPECTION_DATE, VIOLATION_CODE, INSPECTOR_CODE, RESULT_CODE, DATE_POSTED)" + _
                "VALUES('" & txtCaseNumber.Text & "'," + _
                        "'UHH'," + _
                        "'" & txtComplaintDate.Text & "'," + _
                        "'UHH'," + _
                        "'" & inspectorCode & "'," + _
                        "'FAI'," + _
                        "'" & txtComplaintDate.Text & "')", connCreateCase)
        connCreateCase.Open()
        commCreateCase.ExecuteNonQuery()
        connCreateCase.Close()

        'Schedule Inspection
        commCreateCase = New Data.OleDb.OleDbCommand( _
                "INSERT INTO CODE_ENF.CE_INSPECT_SCHEDUL(CASE_NUMBER, INSPECTOR_CODE, VISIT_CODE, DATE_POSTED)" + _
                "VALUES('" & txtCaseNumber.Text & "'," + _
                        "'" & inspectorCode & "'," + _
                        "'600'," + _
                        "'" & txtComplaintDate.Text & "')", connCreateCase)
        connCreateCase.Open()
        commCreateCase.ExecuteNonQuery()
        connCreateCase.Close()



        'Vacant Lot Description


        'Reset form
        lblCaseCreated.Text = "Case " & txtCaseNumber.Text & " Created!" & "<br/>"
        btnCreateCase.Enabled = False
        txtCaseNumber.Text = ""
        txtComplaintDate.Text = ""
        txtInspectionComments.Text = ""
        txtLetterComments.Text = ""
        txtRecheckDate.Text = ""

        txtRecheckDate.Enabled = False
        txtComplaintDate.Enabled = False
        ddlContactTypeMade.Enabled = False
        ddlContactTypeMade.SelectedValue = "0"
        ddlLotType.Enabled = False
        ddlLotType.SelectedValue = "O"
        ddlMctLot.Enabled = False
        ddlMctLot.SelectedValue = "N"
        txtInspectionComments.Enabled = False
        txtLetterComments.Enabled = False

    End Sub

    Protected Sub txtInspectionComments_TextChanged(sender As Object, e As System.EventArgs) Handles txtInspectionComments.TextChanged
        txtInspectionComments.Text = txtInspectionComments.Text.Replace("'", "''")
    End Sub


    Protected Sub txtLetterComments_TextChanged(sender As Object, e As System.EventArgs) Handles txtLetterComments.TextChanged
        txtLetterComments.Text = txtLetterComments.Text.Replace("'", "''")
    End Sub

    
End Class