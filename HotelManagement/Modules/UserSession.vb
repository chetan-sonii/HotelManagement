Module UserSession
    ' If 0, it means Admin is logged in.
    ' If > 0, it is the Guest ID.
    Public CurBookUserID As Integer = 0

    ' To show their name on the dashboard
    Public CurBookUserName As String = ""
End Module