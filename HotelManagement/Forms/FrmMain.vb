Imports ReaLTaiizor.Forms
Imports ReaLTaiizor.Manager
Imports ReaLTaiizor.Enum.Poison
Imports MySql.Data.MySqlClient

Public Class FrmMain
    Inherits PoisonForm

    Private _styleManager As PoisonStyleManager

    Public Sub New()
        InitializeComponent()
        _styleManager = New PoisonStyleManager()
        _styleManager.Owner = Me
        _styleManager.Style = ColorStyle.Red
        _styleManager.Theme = ThemeStyle.Light
        Me.StyleManager = _styleManager
    End Sub

    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConfigureForm()
        LoadDashboardStats()
        ShowWelcomeMessage()
    End Sub

    ' ==========================================
    ' FORM CONFIGURATION
    ' ==========================================
    Private Sub ConfigureForm()
        Me.Movable = True
        Me.Resizable = False
    End Sub

    ' ==========================================
    ' LOAD DASHBOARD STATISTICS
    ' ==========================================
    Private Sub LoadDashboardStats()
        Try
            ' Count Available Rooms
            Dim dtRooms As DataTable = DatabaseConnection.RunQuery("SELECT COUNT(*) FROM tbl_rooms WHERE available='Yes'")
            If dtRooms IsNot Nothing AndAlso dtRooms.Rows.Count > 0 Then
                If tileRooms IsNot Nothing Then
                    tileRooms.TileCount = CInt(dtRooms.Rows(0)(0))
                End If
            End If

            ' Count Total Guests
            Dim dtCust As DataTable = DatabaseConnection.RunQuery("SELECT COUNT(*) FROM tbl_guests")
            If dtCust IsNot Nothing AndAlso dtCust.Rows.Count > 0 Then
                If tileCust IsNot Nothing Then
                    tileCust.TileCount = CInt(dtCust.Rows(0)(0))
                End If
            End If

            ' Count Occupied Rooms (Rooms that are NOT available)
            Dim dtBook As DataTable = DatabaseConnection.RunQuery("SELECT COUNT(*) FROM tbl_rooms WHERE available='No'")
            If dtBook IsNot Nothing AndAlso dtBook.Rows.Count > 0 Then
                If tileBookings IsNot Nothing Then
                    tileBookings.TileCount = CInt(dtBook.Rows(0)(0))
                End If
            End If

            ' Count Pending Requests
            Dim dtRequests As DataTable = DatabaseConnection.RunQuery("SELECT COUNT(*) FROM tbl_bookings WHERE status!='Active'")
            If dtRequests IsNot Nothing AndAlso dtRequests.Rows.Count > 0 Then
                If tileRequests IsNot Nothing Then
                    tileRequests.TileCount = CInt(dtRequests.Rows(0)(0))
                End If
            End If

            ' Update last refresh time
            If lblLastUpdate IsNot Nothing Then
                lblLastUpdate.Text = "Last Updated: " & DateTime.Now.ToString("hh:mm:ss tt")
            End If

        Catch ex As Exception
            Console.WriteLine("Stats Error: " & ex.Message)
            MsgBox("Error loading dashboard statistics: " & ex.Message, MsgBoxStyle.Exclamation, "Database Error")
        End Try
    End Sub

    ' ==========================================
    ' WELCOME MESSAGE
    ' ==========================================
    Private Sub ShowWelcomeMessage()
        If lblWelcome IsNot Nothing Then
            Dim hour As Integer = DateTime.Now.Hour
            Dim greeting As String = ""

            If hour < 12 Then
                greeting = "Good Morning"
            ElseIf hour < 18 Then
                greeting = "Good Afternoon"
            Else
                greeting = "Good Evening"
            End If

            lblWelcome.Text = greeting & ", Admin! 👋"
        End If
    End Sub

    ' ==========================================
    ' TILE CLICK EVENTS
    ' ==========================================
    Private Sub tileRooms_Click(sender As Object, e As EventArgs) Handles tileRooms.Click
        Try
            Dim f As New FrmRooms()
            f.ShowDialog()
            LoadDashboardStats()
        Catch ex As Exception
            MsgBox("Error opening Rooms Management: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tileCust_Click(sender As Object, e As EventArgs) Handles tileCust.Click
        Try
            Dim f As New FrmGuests()
            f.ShowDialog()
            LoadDashboardStats()
        Catch ex As Exception
            MsgBox("Error opening Guest Management: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tileBookings_Click(sender As Object, e As EventArgs) Handles tileBookings.Click
        Try
            Dim f As New FrmBooking()
            f.ShowDialog()
            LoadDashboardStats()
        Catch ex As Exception
            MsgBox("Error opening Booking Management: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tileReturn_Click(sender As Object, e As EventArgs) Handles tileReturn.Click
        Try
            Dim f As New FrmReturn()
            f.ShowDialog()
            LoadDashboardStats()
        Catch ex As Exception
            MsgBox("Error opening Return Management: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tileRequests_Click(sender As Object, e As EventArgs) Handles tileRequests.Click
        Try
            Dim f As New FrmRequests()
            f.ShowDialog()
            LoadDashboardStats()
        Catch ex As Exception
            MsgBox("Error opening Requests: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ' ==========================================
    ' LOGOUT
    ' ==========================================
    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnlogout.Click
        Dim result As MsgBoxResult = MsgBox("Are you sure you want to logout?", 
                                           MsgBoxStyle.YesNo + MsgBoxStyle.Question, 
                                           "Confirm Logout")
        
        If result = MsgBoxResult.Yes Then
            Dim f As New FrmLogin()
            f.Show()
            Me.Close()
        End If
    End Sub

    ' ==========================================
    ' REFRESH BUTTON
    ' ==========================================
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadDashboardStats()
        MsgBox("Dashboard statistics refreshed successfully!", MsgBoxStyle.Information, "Refreshed")
    End Sub

End Class
