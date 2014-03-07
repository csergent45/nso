
Partial Class help
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session("userName") = "" Or Session("password") = "" Then
            Response.Redirect("login.aspx")
        End If
    End Sub

End Class
