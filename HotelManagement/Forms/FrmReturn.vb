Imports ReaLTaiizor.Forms
Imports ReaLTaiizor.Manager
Imports ReaLTaiizor.Enum.Poison
Imports MySql.Data.MySqlClient

Public Class FrmReturn
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

    Private Sub FrmReturn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConfigureForm()
        ConfigureDataGridView()
        LoadBookedRooms()
        ClearFields()
    End Sub

    ' ==========================================
    ' FORM CONFIGURATION
    ' ==========================================
    Private Sub ConfigureForm()
        ' Form properties for better appearance
        Me.Movable = True
        Me.Resizable = False

        ' Disable text fields for display only
        txtRoomId.ReadOnly = True
        txtCustName.ReadOnly = True
        txtDueDate.ReadOnly = True
        txtFine.ReadOnly = True

        ' Set button styles
        btnCalculate.Highlight = True
        btnReturn.Highlight = True
    End Sub

    ' ==========================================
    ' DATAGRIDVIEW CONFIGURATION
    ' ==========================================
    Private Sub ConfigureDataGridView()
        With dgvBooked
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .RowHeadersVisible = False
            .BorderStyle = BorderStyle.None

            ' Alternating row colors for better readability
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 250, 255)
        End With
    End Sub

    ' ==========================================
    ' 1. LOAD ONLY BookED RoomS
    ' ==========================================
    Private Sub LoadBookedRooms()
        Try
            Dim query As String = "SELECT r.Book_id AS 'Book ID', " &
                                  "r.room_no AS 'Room Registration', " &
                                  "c.cust_name AS 'Guest Name', " &
                                  "DATE_FORMAT(r.check_out, '%Y-%m-%d') AS 'Due Date' " &
                                  "FROM tbl_bookings r " &
                                  "JOIN tbl_guests c ON r.cust_id = c.cust_id " &
                                  "JOIN tbl_rooms Room ON r.room_no = Room.room_no " &
                                  "WHERE Room.available = 'No' " &
                                  "ORDER BY r.check_out ASC"

            Dim dt As DataTable = DatabaseConnection.RunQuery(query)
            dgvBooked.DataSource = dt

            ' Hide Book ID column if exists
            If dgvBooked.Columns.Contains("Book ID") Then
                dgvBooked.Columns("Book ID").Visible = False
            End If

            ' Update status label
            lblStatus.Text = "Total Booked Rooms: " & dt.Rows.Count.ToString()

        Catch ex As Exception
            MsgBox("Error loading Booked Rooms: " & ex.Message, MsgBoxStyle.Critical, "Database Error")
        End Try
    End Sub

    ' ==========================================
    ' 2. GRID CLICK (Select a Room)
    ' ==========================================
    Private Sub dgvBooked_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvBooked.CellClick
        If e.RowIndex >= 0 Then
            Try
                Dim row As DataGridViewRow = dgvBooked.Rows(e.RowIndex)

                ' Get the actual column names from the DataGridView
                Dim RoomRegColumn As String = If(dgvBooked.Columns.Contains("Room Registration"), "Room Registration", "room_no")
                Dim custNameColumn As String = If(dgvBooked.Columns.Contains("Guest Name"), "Guest Name", "cust_name")
                Dim dueDateColumn As String = If(dgvBooked.Columns.Contains("Due Date"), "Due Date", "check_out")

                txtRoomId.Text = row.Cells(RoomRegColumn).Value.ToString()
                txtCustName.Text = row.Cells(custNameColumn).Value.ToString()

                ' Format date
                Dim dateValue = row.Cells(dueDateColumn).Value
                If dateValue IsNot Nothing AndAlso Not IsDBNull(dateValue) Then
                    Dim d As DateTime = Convert.ToDateTime(dateValue)
                    txtDueDate.Text = d.ToString("yyyy-MM-dd")

                    ' Auto-calculate fine when row is selected
                    CalculateFineAmount()
                End If

            Catch ex As Exception
                MsgBox("Error selecting Room: " & ex.Message, MsgBoxStyle.Exclamation, "Selection Error")
            End Try
        End If
    End Sub

    ' ==========================================
    ' 3. CALCULATE FINE (Enhanced Logic)
    ' ==========================================
    Private Sub btnCalculate_Click(sender As Object, e As EventArgs) Handles btnCalculate.Click
        CalculateFineAmount()
    End Sub

    Private Sub CalculateFineAmount()
        If String.IsNullOrEmpty(txtDueDate.Text) Then
            MsgBox("Please select a Booked Room first.", MsgBoxStyle.Information, "No Selection")
            Return
        End If

        Try
            Dim dueDate As DateTime = DateTime.Parse(txtDueDate.Text)
            Dim today As DateTime = DateTime.Now.Date ' Use Date only for accurate day calculation

            If today > dueDate Then
                Dim delayDays As Integer = (today - dueDate).Days
                Dim finePerDay As Integer = 500 ' Fine amount per day
                Dim totalFine As Integer = delayDays * finePerDay

                txtFine.Text = totalFine.ToString("N0") ' Format with thousand separators
                txtFine.ForeColor = Color.Red

                lblFineStatus.Text = $"Late by {delayDays} day(s) @ ₹{finePerDay}/day"
                lblFineStatus.ForeColor = Color.Red
                lblFineStatus.Visible = True

            Else
                txtFine.Text = "0"
                txtFine.ForeColor = Color.Green

                Dim daysRemaining As Integer = (dueDate - today).Days
                If daysRemaining = 0 Then
                    lblFineStatus.Text = "Due today - No delay"
                Else
                    lblFineStatus.Text = $"On time ({daysRemaining} day(s) remaining)"
                End If
                lblFineStatus.ForeColor = Color.Green
                lblFineStatus.Visible = True
            End If

        Catch ex As Exception
            MsgBox("Date calculation error: " & ex.Message, MsgBoxStyle.Exclamation, "Error")
        End Try
    End Sub

    ' ==========================================
    ' 4. CONFIRM RETURN
    ' ==========================================
    Private Sub btnReturn_Click(sender As Object, e As EventArgs) Handles btnReturn.Click
        If String.IsNullOrEmpty(txtRoomId.Text) Then
            MsgBox("Please select a Room to return.", MsgBoxStyle.Information, "No Selection")
            Return
        End If

        ' Confirmation dialog
        Dim result As MsgBoxResult = MsgBox($"Confirm return of Room {txtRoomId.Text}?" & vbCrLf &
                                           $"Guest: {txtCustName.Text}" & vbCrLf &
                                           $"Fine Amount: ₹{txtFine.Text}",
                                           MsgBoxStyle.YesNo + MsgBoxStyle.Question,
                                           "Confirm Return")

        If result = MsgBoxResult.No Then Return

        Try
            ' Update Room status to available
            Dim queryUpdate As String = $"UPDATE tbl_rooms SET available='Yes' WHERE room_no='{txtRoomId.Text}'"
            DatabaseConnection.ExecuteQuery(queryUpdate)

            ' Optional: Update Booking record with actual return date and fine
            Dim queryUpdateBooking As String = $"UPDATE tbl_bookings SET " &
                                             $"actual_check_out = NOW(), " &
                                             $"fine_amount = {Val(txtFine.Text.Replace(",", ""))} " &
                                             $"WHERE room_no='{txtRoomId.Text}' AND actual_check_out IS NULL"
            DatabaseConnection.ExecuteQuery(queryUpdateBooking)

            MsgBox($"Room {txtRoomId.Text} returned successfully!" & vbCrLf &
                   "The vehicle is now available for Book.",
                   MsgBoxStyle.Information,
                   "Success")

            ' Refresh and clear
            LoadBookedRooms()
            ClearFields()

        Catch ex As Exception
            MsgBox("Error processing return: " & ex.Message, MsgBoxStyle.Critical, "Database Error")
        End Try
    End Sub

    ' ==========================================
    ' 5. REFRESH BUTTON
    ' ==========================================
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadBookedRooms()
        ClearFields()
        MsgBox("List refreshed successfully!", MsgBoxStyle.Information, "Refresh")
    End Sub

    ' ==========================================
    ' 6. BACK BUTTON
    ' ==========================================
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Dim result As MsgBoxResult = MsgBox("Are you sure you want to close this window?",
                                           MsgBoxStyle.YesNo + MsgBoxStyle.Question,
                                           "Confirm Close")
        If result = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    ' ==========================================
    ' CLEAR FIELDS
    ' ==========================================
    Private Sub ClearFields()
        txtRoomId.Text = ""
        txtCustName.Text = ""
        txtDueDate.Text = ""
        txtFine.Text = "0"
        txtFine.ForeColor = Color.Black
        lblFineStatus.Text = ""
        lblFineStatus.Visible = False

        ' Clear selection in grid
        If dgvBooked.Rows.Count > 0 Then
            dgvBooked.ClearSelection()
        End If
    End Sub

    ' ==========================================
    ' SEARCH FUNCTIONALITY
    ' ==========================================
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If dgvBooked.DataSource IsNot Nothing Then
            Try
                Dim dv As DataView = CType(dgvBooked.DataSource, DataTable).DefaultView

                If String.IsNullOrWhiteSpace(txtSearch.Text) Then
                    dv.RowFilter = ""
                Else
                    ' Search in multiple columns
                    dv.RowFilter = $"[Room Registration] LIKE '%{txtSearch.Text}%' OR " &
                                  $"[Guest Name] LIKE '%{txtSearch.Text}%'"
                End If

                lblStatus.Text = "Showing " & dv.Count & " of " & CType(dgvBooked.DataSource, DataTable).Rows.Count & " Rooms"

            Catch ex As Exception
                ' Ignore filter errors
            End Try
        End If
    End Sub

End Class
