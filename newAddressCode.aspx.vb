
Partial Class newAddressCode
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Address code to be used for selected parcel
        txtAddressCode.Text = Session("addressCode")

        If Not Page.IsPostBack Then
            'Populates Streets
            Dim connStreets As New Data.OleDb.OleDbConnection
            Dim commStreets As New Data.OleDb.OleDbCommand
            Dim readerStreets As Data.OleDb.OleDbDataReader

            connStreets = New Data.OleDb.OleDbConnection(Session("connectionString"))
            commStreets = New Data.OleDb.OleDbCommand("SELECT STREET_DESC FROM GENINFO.STREET_LOG", connStreets)


            Try
                connStreets.Open()
                readerStreets = commStreets.ExecuteReader()
                ddlStreets.DataSource = readerStreets
                ddlStreets.DataValueField = "STREET_DESC"
                ddlStreets.DataTextField = "STREET_DESC"
                ddlStreets.DataBind()
                readerStreets.Close()
            Finally
                connStreets.Close()
            End Try

        End If
    End Sub

    Protected Sub btnEnterAddressCode_Click(sender As Object, e As System.EventArgs) Handles btnEnterAddressCode.Click
        Dim connStreets As New Data.OleDb.OleDbConnection
        Dim commStreets As New Data.OleDb.OleDbCommand

        connStreets = New Data.OleDb.OleDbConnection(Session("connectionString"))
        commStreets = New Data.OleDb.OleDbCommand("SELECT STREET_CODE FROM GENINFO.STREET_LOG WHERE STREET_DESC='" & ddlStreets.SelectedValue & "'", connStreets)
        connStreets.Open()
        Session("streetCode") = commStreets.ExecuteScalar().ToString()
        connStreets.Close()

        commStreets = New Data.OleDb.OleDbCommand("INSERT INTO GENINFO.ADDRESS_LOG(ADDRESS_CODE, STREET_NUM, STREET_CODE," +
                                                    "CITY_CODE, CITY_LIMITS, STATE_CODE, COUNTRY_CODE, ZIPCODE," +
                                                    "CODE_DATE, DB2_APP, ANNEX_NUM, PIN_NUM)" +
                                                    "VALUES('" & Session("addressCode") & "'," + _
                                                    "'0'," + _
                                                    "'" & Session("streetCode") & "'," + _
                                                    "'1'," + _
                                                    "'1'," + _
                                                    "'16'," + _
                                                    "'1'," + _
                                                    "'6252'," + _
                                                    "'" & Now().ToString("MM/dd/yyyy") & "'," + _
                                                    "'Y'," + _
                                                    "'QQ'," + _
                                                    "'" & Session("PinNum") & "')", connStreets)
        connStreets.Open()
        commStreets.ExecuteNonQuery()
        connStreets.Close()



                                                    





        Session("PinNum") = ""
        Session("addressCode") = ""
        Session("streetCode") = ""

        txtWebPage.Text = Session("webPage")
        Response.Redirect(txtWebPage.Text)


    End Sub
End Class
