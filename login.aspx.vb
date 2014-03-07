
Partial Class login
    Inherits System.Web.UI.Page

    ' Page uses Accessing SQL Server with Explicit Credentials - http://msdn.microsoft.com/en-us/library/aa984237



    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Setting global application variables
        Session("userName") = ""
        Session("password") = ""
        Session("userName") = txtUserName.Text.ToUpper 'Variable used in connection string for the user name
        Session("password") = txtPassword.Text 'Variable used in connection string for the password
        'Connection string to connect to database
        Session("connectionString") = "Provider=IBMDADB2.IBMDBCL1" & ";Data Source=TESTGEN" & ";Persist Security Info=False" & ";User ID=" & Session("userName") & ";Password=" & Session("password") & " "

        SetFocus(txtUserName)

        'Sets a variable for selected parcels that have missing address codes 
        Session("addressCode") = ""
        Session("streetCode") = ""
        Session("webPage") = ""
        Session("PinNum") = ""

    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As System.EventArgs) Handles btnLogin.Click
        Dim conn As Data.OleDb.OleDbConnection


        conn = New Data.OleDb.OleDbConnection(Session("connectionString"))

        Try
            'Attempt to open the connection
            conn.Open()

            'Close the connection
            conn.Close()

            'Redirect if connection was successful
            Response.Redirect("codeType.aspx")

        Catch ex As Exception
            If ex.Message.Contains("24") Then
                lblErrorMessage.Text = "Username and/or password invalid. Please try again."
                Session("userName") = ""
                'Sets focus to txtUserName and selects all text
                With txtUserName
                    .Focus()
                    .Attributes.Add("onfocusin", "select();")
                    .Text = ""
                End With
            Else
                lblErrorMessage.Text = "There appears to be a problem. Please try again later or contact MIS."
            End If
        End Try

    End Sub

End Class
