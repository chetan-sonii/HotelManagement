Imports ReaLTaiizor.Forms
Imports ReaLTaiizor.Manager
Imports ReaLTaiizor.Enum.Poison
Imports MySql.Data.MySqlClient
Imports System.Data

Public Class FrmUserDashboard
    Inherits PoisonForm

    Private _styleManager As PoisonStyleManager

    ' FIX 1: Use Parameterless Constructor to prevent Login Form crash.
    ' We use UserSession to get the logged-in user's details.
    Public Sub New()
        InitializeComponent()

        ' Initialize style manager (GREEN for Users)
        _styleManager = New PoisonStyleManager()
        _styleManager.Owner = Me
        _styleManager.Style = ColorStyle.Green
        _styleManager.Theme = ThemeStyle.Light
        Me.StyleManager = _styleManager
    End Sub

    ' Form Load
    Private Sub FrmUserDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToScreen()

        ' FIX 2: Get details from Global Session
        ' (Ensure you created the UserSession module as discussed previously)
        lblWelcome.Text = $"Welcome, {UserSession.CurBookUserName}!"
        ' Note: If you don't have email in UserSession, you can query it or just hide the label.
        ' lblUserEmail.Text = UserSession.CurBookUserEmail 

        ConfigureDataGridViews()
        LoadAvailableRooms()
        LoadMyBookings()
    End Sub

    Private Sub ConfigureDataGridViews()
        ' Common settings for both grids to prevent UI glitches
        Dim grids() As DataGridView = {dgvAvailable, dgvMyBookings}

        For Each dgv In grids
            With dgv
                .BackgroundColor = Color.White
                .GridColor = Color.Black
                .AutoGenerateColumns = True
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                .AllowUserToAddRows = False
                .AllowUserToDeleteRows = False
                .ReadOnly = True
                .MultiSelect = False
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .RowHeadersVisible = False ' Looks cleaner
            End With
        Next
    End Sub

    ' ========================================================
    ' 1. LOAD AVAILABLE RoomS
    ' ========================================================
    Private Sub LoadAvailableRooms()
        Try
            ' We use aliases (AS ...) to make column headers look nice automatically
            Dim query As String = "SELECT room_no AS 'Room No No', Room_Type AS 'Room_Type', bed_type AS 'bed_type', " &
                                  "price AS 'Price/Day', available AS 'Status' " &
                                  "FROM tbl_rooms WHERE available = 'Yes' ORDER BY Room_Type, bed_type"

            Dim dt As DataTable = DatabaseConnection.RunQuery(query)
            dgvAvailable.DataSource = dt

            ' Update Status Label
            If dt.Rows.Count = 0 Then
                lblSelectedRoom.Text = "No Rooms available"
            Else
                lblSelectedRoom.Text = $"{dt.Rows.Count} Rooms available"
            End If

        Catch ex As Exception
            MsgBox("Error loading Rooms: " & ex.Message)
        End Try
    End Sub

    ' ========================================================
    ' 2. LOAD MY BookingS
    ' ========================================================
    Private Sub LoadMyBookings()
        Try
            ' Join tables to get Room Details + Booking Status
            Dim query As String = "SELECT r.Book_id, c.Room_Type, c.bed_type, " &
                                  "r.check_in AS 'Start Date', r.check_out AS 'Return Date', " &
                                  "r.fees AS 'Total Fees', r.status AS 'Status', r.room_no " &
                                  "FROM tbl_bookings r " &
                                  "JOIN tbl_rooms c ON r.room_no = c.room_no " &
                                  "WHERE r.cust_id = " & UserSession.CurBookUserID &
                                  " ORDER BY r.check_in DESC"

            Dim dt As DataTable = DatabaseConnection.RunQuery(query)
            dgvMyBookings.DataSource = dt

            ' Hide technical columns
            If dgvMyBookings.Columns.Contains("Book_id") Then dgvMyBookings.Columns("Book_id").Visible = False
            If dgvMyBookings.Columns.Contains("room_no") Then dgvMyBookings.Columns("room_no").Visible = False

        Catch ex As Exception
            MsgBox("Error loading history: " & ex.Message)
        End Try
    End Sub

    ' ========================================================
    ' 3. SEARCH (Real-time)
    ' ========================================================
    Private Sub txtSearchRoom_TextChanged(sender As Object, e As EventArgs) Handles txtSearchRoom.TextChanged
        Try
            Dim searchTerm As String = txtSearchRoom.Text.Trim()
            If searchTerm = "" Then
                LoadAvailableRooms()
                Return
            End If

            Dim query As String = "SELECT room_no AS 'Room No No', Room_Type AS 'Room_Type', bed_type AS 'bed_type', " &
                                  "price AS 'Price/Day', available AS 'Status' " &
                                  "FROM tbl_rooms " &
                                  "WHERE available = 'Yes' AND (Room_Type LIKE @search OR bed_type LIKE @search)"

            Dim params As New List(Of MySqlParameter)
            params.Add(New MySqlParameter("@search", "%" & searchTerm & "%"))

            Dim dt As DataTable = DatabaseConnection.RunQuery(query, params)
            dgvAvailable.DataSource = dt

        Catch ex As Exception
            ' Silent fail to not annoy user while typing
        End Try
    End Sub

    ' ========================================================
    ' 4. BOOKING LOGIC (The Request Workflow)
    ' ========================================================
    Private Sub btnBook_Click(sender As Object, e As EventArgs) Handles btnBook.Click
        If dgvAvailable.SelectedRows.Count = 0 Then
            MsgBox("Please select a Room first.")
            Return
        End If

        Try
            ' Get Room Details
            Dim row As DataGridViewRow = dgvAvailable.SelectedRows(0)
            Dim RoomReg As String = row.Cells("Room No").Value.ToString()
            Dim price As Decimal = Convert.ToDecimal(row.Cells("Price/Day").Value)
            Dim Room_Type As String = row.Cells("Room_Type").Value.ToString()
            Dim bed_type As String = row.Cells("bed_type").Value.ToString()

            ' Confirmation
            Dim ans = MsgBox($"Request to book {Room_Type} {bed_type} for ₹{price}/day?", MsgBoxStyle.YesNo + MsgBoxStyle.Question)
            If ans = MsgBoxResult.No Then Return

            ' FIX 3: INSERT AS 'PENDING'. Do NOT set Room to 'No' yet.
            ' The Admin must approve this request.
            Dim today As String = DateTime.Now.ToString("yyyy-MM-dd")
            Dim tomorrow As String = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")

            Dim query As String = "INSERT INTO tbl_bookings (room_no, cust_id, check_in, check_out, fees, status) " &
                                  "VALUES ('" & RoomReg & "', " & UserSession.CurBookUserID & ", '" & today & "', '" & tomorrow & "', " & price & ", 'Pending')"

            DatabaseConnection.ExecuteQuery(query)

            MsgBox("Booking Request Sent! Please wait for Admin approval.", MsgBoxStyle.Information)

            ' We switch tabs to show them the pending request
            LoadMyBookings()
            ' If you have a TabControl, you can select the history tab here:
            ' PoisonTabControl1.SelectedIndex = 1 

        Catch ex As Exception
            MsgBox("Booking Error: " & ex.Message)
        End Try
    End Sub

    ' ========================================================
    ' 5. RETURN LOGIC (The Request Workflow)
    ' ========================================================
    Private Sub btnReturn_Click(sender As Object, e As EventArgs) Handles btnReturn.Click
        If dgvMyBookings.SelectedRows.Count = 0 Then
            MsgBox("Select a Booking to return.")
            Return
        End If

        Try
            Dim row As DataGridViewRow = dgvMyBookings.SelectedRows(0)
            Dim status As String = row.Cells("Status").Value.ToString()
            Dim RoomReg As String = row.Cells("room_no").Value.ToString()

            ' Validation
            If status = "Returned" Then
                MsgBox("This Room is already returned.")
                Return
            End If
            If status = "Pending" Then
                MsgBox("Your booking is not active yet. You cannot return it.")
                Return
            End If
            If status = "ReturnPending" Then
                MsgBox("You have already requested a return.")
                Return
            End If

            ' Confirmation
            Dim ans = MsgBox("Request to return this Room?", MsgBoxStyle.YesNo + MsgBoxStyle.Question)
            If ans = MsgBoxResult.No Then Return

            ' FIX 4: Just update status to 'ReturnPending'. 
            ' Do NOT calculate final fees or free the Room yet. Admin does that.
            Dim query As String = "UPDATE tbl_bookings SET status='ReturnPending' WHERE room_no='" & RoomReg & "' AND status='Active'"
            DatabaseConnection.ExecuteQuery(query)

            MsgBox("Return Request Sent! Please hand over keys to Admin.", MsgBoxStyle.Information)
            LoadMyBookings()

        Catch ex As Exception
            MsgBox("Return Error: " & ex.Message)
        End Try
    End Sub

    ' Logout
    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        Dim f As New FrmLogin()
        f.Show()
        Me.Close()
    End Sub

    ' Grid Selection Effects (Optional UI Polish)
    Private Sub dgvAvailable_SelectionChanged(sender As Object, e As EventArgs) Handles dgvAvailable.SelectionChanged
        If dgvAvailable.SelectedRows.Count > 0 Then
            Dim row = dgvAvailable.SelectedRows(0)
            lblSelectedRoom.Text = row.Cells("Room_Type").Value.ToString() & " " & row.Cells("bed_type").Value.ToString()
        End If
    End Sub

End Class