
Partial Class DynamicSend
    Inherits System.Web.UI.Page
    Dim iContactlist As New List(Of Contact)
    Dim iAddedContact As New List(Of Contact)

    'Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
    '    getContact()
    'End Sub
    Sub getContact()
        Me.MultiView1.ActiveViewIndex = 1

        'iAddedContact.Add(New Contact(111, "Danjuma Audu", "danjuma@need-solution.com"))
        'iAddedContact.Add(New Contact(112, "Faith William", "faith@gmail.com"))
        'iAddedContact.Add(New Contact(113, "Danyo Ade", "danyo@need-solution.com"))
        'iAddedContact.Add(New Contact(114, "Bantele Aliu", "Bantele@reliance.com"))
        'iAddedContact.Add(New Contact(115, "Koya Mudaishuru", "koya@Dynamic.com"))
        'iAddedContact.Add(New Contact(116, "Jumoke Aliu", "jummy@Jide.com"))
        'iAddedContact.Add(New Contact(117, "Danjuma Igoche", "igoche@need-solution.com"))
        'iAddedContact.Add(New Contact(118, "John Audu", "john@need-solution.com"))

        PropContactList = Nothing


        'iContactlist.Add(New Contact(111, "Danjuma Audu", "danjuma@need-solution.com", "Reliance", "Head Client Engagement", "Marketting and Sales"))
        'iContactlist.Add(New Contact(112, "Faith William", "faith@gmail.com", "AUC Nigeria", "Head Admin", "Finance and Admin"))
        'iContactlist.Add(New Contact(113, "Danyo Ade", "danyo@need-solution.com", "AUC Nigeria", "Head Marketting", "Marketting and Sales"))
        'iContactlist.Add(New Contact(114, "Bantele Aliu", "Bantele@reliance.com", "AUC Nigeria", "Head Marketting", "Marketting and Sales"))
        'iContactlist.Add(New Contact(115, "Koya Mudaishuru", "koya@Dynamic.com", "AUC Nigeria", "Head Marketting", "Marketting and Sales"))
        'iContactlist.Add(New Contact(116, "Jumoke Aliu", "jummy@Jide.com", "AUC Nigeria", "Head Marketting", "Marketting and Sales"))
        'iContactlist.Add(New Contact(117, "Danjuma Igoche", "igoche@need-solution.com", "AUC Nigeria", "Head Marketting", "Marketting and Sales"))
        'iContactlist.Add(New Contact(118, "John Audu", "john@need-solution.com", "AUC Nigeria", "Head Marketting", "Marketting and Sales"))

        Me.GridView1.DataSource = Session("entityRelatedContactList")
        Me.GridView1.DataBind()

        Me.msgBody = txtBody.Text

    End Sub
    Protected Sub GridView1_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" & e.Row.RowIndex)
            e.Row.Attributes("style") = "cursor:pointer"
            e.Row.ToolTip = "Click to select this row."
            e.Row.Attributes("onmouseover") = "this.style.color='#000';"
            'Dim imgBtn As System.Web.UI.WebControls.ImageButton = e.Row.FindControl("editImageButton")

            'ScriptManager1.Controls.Remove(imgBtn)

            'ScriptManager1.RegisterPostBackControl(imgBtn)

            ' Dim btn As WebControls.ImageButton = DirectCast(e.Row.FindControl("editImageButton"), WebControls.ImageButton)
            ' btn.OnClientClick = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex.ToString)
            'btn.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex.ToString)

        End If
    End Sub
    Protected Property PropContactList As List(Of Contact)
        Get
            Return ViewState("PropContactList")
        End Get
        Set(ByVal value As List(Of Contact))
            ViewState("PropContactList") = value
            Me.ddlAddress.DataSource = value
                Me.ddlAddress.DataBind()
        End Set
    End Property

    Protected Property msgBody As String
        Get
            Return ViewState("msgBody")
        End Get
        Set(ByVal value As String)
            ViewState("msgBody") = value
        End Set
    End Property
    Protected Sub Button62_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnRemoveContact.Click
        Me.MultiView1.ActiveViewIndex = 0 '  Dim iCon As Contact = 
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

        If Not GridView1.SelectedIndex = -1 Then
            Dim ContactID As String = GridView1.SelectedValue.ToString()
            'Dim id As Integer = GridView1.SelectedRow.Cells(1).Text
            Dim CName As String = GridView1.SelectedRow.Cells(2).Text
            Dim CEmail As String = GridView1.SelectedRow.Cells(3).Text
            Dim Org As String = GridView1.SelectedRow.Cells(4).Text
            Dim Dept As String = GridView1.SelectedRow.Cells(5).Text
            Dim Desig As String = GridView1.SelectedRow.Cells(6).Text
            Dim NewContact As New Contact(ContactID, CName, CEmail, Org, Desig, Dept)
            Dim Addedlist As New List(Of Contact)
            '   Addedlist = PropContactList
            If Not IsNothing(PropContactList) Then
                For Each iEmail As Contact In PropContactList
                    If Not iEmail.EmailAddress = CEmail Then

                        Addedlist.Add(New Contact(iEmail.ContactID, iEmail.FullName, iEmail.EmailAddress, iEmail.organisationName, iEmail.Designation, iEmail.Department))
                    End If
                Next
                'Else
                '    Addedlist.Add(New Contact(ContactID, CName, CEmail, Org, Desig, Dept))
            End If

            Addedlist.Add(New Contact(ContactID, CName, CEmail, Org, Desig, Dept))
            PropContactList = Addedlist

        End If
    End Sub

    Protected Sub ddladdress_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles ddlAddress.ItemCommand
        Try

            Dim EmailToDelete As String = e.CommandArgument
            Dim iNewList As New List(Of Contact)
            For Each iemail As Contact In PropContactList
                If iemail.EmailAddress <> EmailToDelete Then
                    iNewList.Add(New Contact(iemail.ContactID, iemail.FullName, iemail.EmailAddress, iemail.organisationName, iemail.Designation, iemail.Department))
                End If
            Next
            PropContactList = iNewList
        Catch ex As Exception
            'Me.litEr1.Text = ex.ToString
        End Try
    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGetContact.Click
        getContact()
    End Sub

    Protected Sub Button12_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveContact.Click
        Me.MultiView1.ActiveViewIndex = 0
        Me.ddlAddressF.DataSource = Me.PropContactList
        Me.ddlAddressF.DataBind()
        Me.txtBody.Text = Me.msgBody



    End Sub
End Class



<Serializable()> _
Public Class Contact
    Public Property ContactID() As Integer
    Public Property FullName() As String
    Public Property EmailAddress() As String
    Public Property organisationName() As String
    Public Property Department() As String
    Public Property Designation() As String
    
    Public Sub New()
        '   Me.RowNumber = 0
    End Sub

    Public Sub New(ByVal IntID As Integer, ByVal iName As String, ByVal iEmail As String)
        Me.ContactID = IntID
        Me.FullName = Iname
        Me.EmailAddress = iEmail
    End Sub
    Public Sub New(ByVal IntID As Integer, ByVal iName As String, ByVal iEmail As String, ByVal Company As String, ByVal Position As String, ByVal Dept As String)
        Me.ContactID = IntID
        Me.FullName = iName
        Me.EmailAddress = iEmail
        Me.organisationName = Company
        Me.Department = Dept
        Me.Designation = Position
    End Sub
End Class