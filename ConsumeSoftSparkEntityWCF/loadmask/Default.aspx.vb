Imports System.Data
Imports System.Data.SqlClient
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub process_Click(sender As Object, e As EventArgs) Handles process.Click
        Dim sq As New SqlConnection("data source=dLaw;initial catalog=oExpert;integrated security=True")
        Try

            If sq.State = ConnectionState.Closed Then
                sq.Open()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub
End Class
