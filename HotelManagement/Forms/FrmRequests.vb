Imports ReaLTaiizor.Forms
Imports ReaLTaiizor.Manager
Imports ReaLTaiizor.Enum.Poison
Imports MySql.Data.MySqlClient

Public Class FrmRequests
    Inherits PoisonForm

    Private _styleManager As PoisonStyleManager

    Public Sub New()
        InitializeComponent()

        ' Initialize style manager
        _styleManager = New PoisonStyleManager()
        _styleManager.Owner = Me
        _styleManager.Style = ColorStyle.Red
        _styleManager.Theme = ThemeStyle.Light
        Me.StyleManager = _styleManager
    End Sub

    Private Sub FrmRequests_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadBookRequests()
        LoadReturnRequests()
        UpdateStatistics()
        ConfigureDataGridViews()
    End Sub

    ' ==========================================
    ' CONFIGURE DATAGRIDS
    ' ==========================================
    Private Sub ConfigureDataGridViews()
        Try
            ' Configure Book Requests Grid
            If dgvBookRequests.Columns.Count > 0 Then
                dgvBookRequests.Columns(0).HeaderText = "Booking ID"
                dgvBookRequests.Columns(1).HeaderText = "Room No"
                dgvBookRequests.Columns(2).HeaderText = "Guest ID"

                If dgvBookRequests.Columns.Count > 3 Then
                    dgvBookRequests.Columns(3).HeaderText = "Check-In"
                End If
                If dgvBookRequests.Columns.Count > 4 Then
                    dgvBookRequests.Columns(4).HeaderText = "Check-Out"
                End If
                If dgvBookRequests.Columns.Count > 5 Then
                    dgvBookRequests.Columns(5).HeaderText = "Guest Name"
                End If
                If dgvBookRequests.Columns.Count > 6 Then
                    dgvBookRequests.Columns(6).HeaderText = "Status"
                End If

                dgvBookRequests.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            End If

            ' Configure Return Requests Grid
            If dgvReturnRequests.Columns.Count > 0 Then
                dgvReturnRequests.Columns(0).HeaderText = "Booking ID"
                dgvReturnRequests.Columns(1).HeaderText = "Room No"
                dgvReturnRequests.Columns(2).HeaderText = "Guest ID"

                If dgvReturnRequests.Columns.Count > 3 Then
                    dgvReturnRequests.Columns(3).HeaderText = "Check-In"
                End If
                If dgvReturnRequests.Columns.Count > 4 Then
                    dgvReturnRequests.Columns(4).HeaderText = "Check-Out"
                End If
                If dgvReturnRequests.Columns.Count > 5 Then
                    dgvReturnRequests.Columns(5).HeaderText = "Guest Name"
                End If
                If dgvReturnRequests.Columns.Count > 6 Then
                    dgvReturnRequests.Columns(6).HeaderText = "Status"
                End If

                dgvReturnRequests.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            End If
        Catch ex As Exception
            ' Silently handle configuration errors
        End Try
    End Sub

    ' ==========================================
    ' LOAD Book/BOOKING REQUESTS
    ' ==========================================
    Private Sub LoadBookRequests()
        Try
            ' Enhanced query with guest information
            Dim query As String = "SELECT b.Book_id, b.room_no, b.cust_id, b.check_in, b.check_out, " &
                                 "g.cust_name, b.status " &
                                 "FROM tbl_bookings b " &
                                 "LEFT JOIN tbl_guests g ON b.cust_id = g.cust_id " &
                                 "WHERE b.status='Pending' " &
                                 "ORDER BY b.Book_id DESC"

            Dim dt As DataTable = DatabaseConnection.RunQuery(query)
            dgvBookRequests.DataSource = dt

            ConfigureDataGridViews()
            UpdateStatistics()
        Catch ex As Exception
            MsgBox("Error loading booking requests: " & ex.Message, MsgBoxStyle.Critical, "Database Error")
        End Try
    End Sub

    ' ==========================================
    ' LOAD RETURN REQUESTS
    ' ==========================================
    Private Sub LoadReturnRequests()
        Try
            ' Enhanced query with guest information
            Dim query As String = "SELECT b.Book_id, b.room_no, b.cust_id, b.check_in, b.check_out, " &
                                 "g.cust_name, b.status " &
                                 "FROM tbl_bookings b " &
                                 "LEFT JOIN tbl_guests g ON b.cust_id = g.cust_id " &
                                 "WHERE b.status='ReturnPending' " &
                                 "ORDER BY b.Book_id DESC"

            Dim dt As DataTable = DatabaseConnection.RunQuery(query)
            dgvReturnRequests.DataSource = dt

            ConfigureDataGridViews()
            UpdateStatistics()
        Catch ex As Exception
            MsgBox("Error loading return requests: " & ex.Message, MsgBoxStyle.Critical, "Database Error")
        End Try
    End Sub

    ' ==========================================
    ' UPDATE STATISTICS
    ' ==========================================
    Private Sub UpdateStatistics()
        Try
            ' Count pending bookings
            Dim BookQuery As String = "SELECT COUNT(*) FROM tbl_bookings WHERE status='Pending'"
            Dim BookDt As DataTable = DatabaseConnection.RunQuery(BookQuery)
            Dim pendingBook As Integer = 0
            If BookDt.Rows.Count > 0 Then
                pendingBook = Convert.ToInt32(BookDt.Rows(0)(0))
            End If

            ' Count pending returns
            Dim returnQuery As String = "SELECT COUNT(*) FROM tbl_bookings WHERE status='ReturnPending'"
            Dim returnDt As DataTable = DatabaseConnection.RunQuery(returnQuery)
            Dim pendingReturn As Integer = 0
            If returnDt.Rows.Count > 0 Then
                pendingReturn = Convert.ToInt32(returnDt.Rows(0)(0))
            End If

            ' Update tiles
            PoisonTile1.Text = "Pending Bookings: " & pendingBook.ToString()
            PoisonTile2.Text = "Pending Returns: " & pendingReturn.ToString()
        Catch ex As Exception
            PoisonTile1.Text = "Pending Bookings: 0"
            PoisonTile2.Text = "Pending Returns: 0"
        End Try
    End Sub

    ' ==========================================
    ' APPROVE BOOKING REQUEST
    ' ==========================================
    Private Sub btnApproveBook_Click(sender As Object, e As EventArgs) Handles btnApproveBook.Click
        ' Validate selection
        If dgvBookRequests.SelectedRows.Count = 0 Then
            MsgBox("Please select a booking request to approve.", MsgBoxStyle.Exclamation, "No Selection")
            Return
        End If

        ' Safety check for empty row
        If dgvBookRequests.SelectedRows(0).IsNewRow Then
            MsgBox("Invalid selection. Please select a valid booking record.", MsgBoxStyle.Exclamation, "Invalid Selection")
            Return
        End If

        Try
            Dim row As DataGridViewRow = dgvBookRequests.SelectedRows(0)

            ' Validate cell data
            If row.Cells("Book_id").Value Is Nothing OrElse IsDBNull(row.Cells("Book_id").Value) Then
                MsgBox("Error: Selected row has no Booking ID.", MsgBoxStyle.Critical, "Invalid Data")
                Return
            End If

            Dim BookId As String = row.Cells("Book_id").Value.ToString()
            Dim roomNo As String = row.Cells("room_no").Value.ToString()
            Dim guestName As String = ""

            ' Get guest name if available
            If row.Cells.Count > 5 AndAlso row.Cells(5).Value IsNot Nothing Then
                guestName = row.Cells(5).Value.ToString()
            End If

            ' Confirm action
            Dim result = MsgBox("Approve booking for:" & vbCrLf & vbCrLf &
                               "Room: " & roomNo & vbCrLf &
                               "Guest: " & guestName & vbCrLf & vbCrLf &
                               "This will mark the room as occupied.",
                               MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm Approval")

            If result = MsgBoxResult.Yes Then
                ' Update booking status to Active
                DatabaseConnection.ExecuteQuery("UPDATE tbl_bookings SET status='Active' WHERE Book_id=" & BookId)

                ' Mark room as unavailable
                DatabaseConnection.ExecuteQuery("UPDATE tbl_rooms SET available='No' WHERE room_no='" & roomNo.Replace("'", "''") & "'")

                MsgBox("✓ Booking approved successfully!" & vbCrLf & "Room " & roomNo & " is now occupied.", MsgBoxStyle.Information, "Success")

                LoadBookRequests()
            End If

        Catch ex As Exception
            MsgBox("Error approving booking: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ' ==========================================
    ' REJECT BOOKING REQUEST
    ' ==========================================
    Private Sub btnRejectBook_Click(sender As Object, e As EventArgs) Handles btnRejectBook.Click
        If dgvBookRequests.SelectedRows.Count = 0 Then
            MsgBox("Please select a booking request to reject.", MsgBoxStyle.Exclamation, "No Selection")
            Return
        End If

        If dgvBookRequests.SelectedRows(0).IsNewRow Then Return

        Try
            Dim row As DataGridViewRow = dgvBookRequests.SelectedRows(0)

            If row.Cells("Book_id").Value Is Nothing Then Return

            Dim BookId As String = row.Cells("Book_id").Value.ToString()
            Dim roomNo As String = row.Cells("room_no").Value.ToString()

            Dim result = MsgBox("Are you sure you want to reject this booking?" & vbCrLf &
                               "Room: " & roomNo & vbCrLf & vbCrLf &
                               "This action will cancel the booking request.",
                               MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm Rejection")

            If result = MsgBoxResult.Yes Then
                ' Update booking status to Rejected
                DatabaseConnection.ExecuteQuery("UPDATE tbl_bookings SET status='Rejected' WHERE Book_id=" & BookId)

                MsgBox("✓ Booking rejected successfully.", MsgBoxStyle.Information, "Success")

                LoadBookRequests()
            End If

        Catch ex As Exception
            MsgBox("Error rejecting booking: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ' ==========================================
    ' CONFIRM RETURN
    ' ==========================================
    Private Sub btnConfirmReturn_Click(sender As Object, e As EventArgs) Handles btnConfirmReturn.Click
        If dgvReturnRequests.SelectedRows.Count = 0 Then
            MsgBox("Please select a return request to confirm.", MsgBoxStyle.Exclamation, "No Selection")
            Return
        End If

        If dgvReturnRequests.SelectedRows(0).IsNewRow Then Return

        Try
            Dim row As DataGridViewRow = dgvReturnRequests.SelectedRows(0)

            ' Validate data
            If row.Cells("Book_id").Value Is Nothing Then Return

            Dim BookId As String = row.Cells("Book_id").Value.ToString()
            Dim roomNo As String = row.Cells("room_no").Value.ToString()
            Dim guestName As String = ""

            If row.Cells.Count > 5 AndAlso row.Cells(5).Value IsNot Nothing Then
                guestName = row.Cells(5).Value.ToString()
            End If

            ' Confirm action
            Dim result = MsgBox("Confirm return for:" & vbCrLf & vbCrLf &
                               "Room: " & roomNo & vbCrLf &
                               "Guest: " & guestName & vbCrLf & vbCrLf &
                               "This will mark the room as available again.",
                               MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm Return")

            If result = MsgBoxResult.Yes Then
                ' Update booking status to Returned
                DatabaseConnection.ExecuteQuery("UPDATE tbl_bookings SET status='Returned' WHERE Book_id=" & BookId)

                ' Mark room as available
                DatabaseConnection.ExecuteQuery("UPDATE tbl_rooms SET available='Yes' WHERE room_no='" & roomNo.Replace("'", "''") & "'")

                MsgBox("✓ Return confirmed successfully!" & vbCrLf & "Room " & roomNo & " is now available.", MsgBoxStyle.Information, "Success")

                LoadReturnRequests()
            End If

        Catch ex As Exception
            MsgBox("Error confirming return: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ' ==========================================
    ' VIEW BOOKING DETAILS
    ' ==========================================
    Private Sub btnViewDetails_Click(sender As Object, e As EventArgs) Handles btnViewDetails.Click
        If dgvReturnRequests.SelectedRows.Count = 0 Then
            MsgBox("Please select a return request to view details.", MsgBoxStyle.Exclamation, "No Selection")
            Return
        End If

        If dgvReturnRequests.SelectedRows(0).IsNewRow Then Return

        Try
            Dim row As DataGridViewRow = dgvReturnRequests.SelectedRows(0)

            If row.Cells("Book_id").Value Is Nothing Then Return

            Dim BookId As String = row.Cells("Book_id").Value.ToString()

            ' Query full booking details
            Dim query As String = "SELECT b.*, g.cust_name, g.phone, g.email, r.Room_Type, r.bed_type, r.price " &
                                 "FROM tbl_bookings b " &
                                 "LEFT JOIN tbl_guests g ON b.cust_id = g.cust_id " &
                                 "LEFT JOIN tbl_rooms r ON b.room_no = r.room_no " &
                                 "WHERE b.Book_id=" & BookId

            Dim dt As DataTable = DatabaseConnection.RunQuery(query)

            If dt.Rows.Count > 0 Then
                Dim detailRow As DataRow = dt.Rows(0)

                Dim details As String = "📋 BOOKING DETAILS" & vbCrLf & vbCrLf &
                                       "Booking ID: " & detailRow("Book_id").ToString() & vbCrLf &
                                       "Room Number: " & detailRow("room_no").ToString() & vbCrLf &
                                       "Room Type: " & detailRow("Room_Type").ToString() & vbCrLf &
                                       "Bed Type: " & detailRow("bed_type").ToString() & vbCrLf & vbCrLf &
                                       "Guest Name: " & detailRow("cust_name").ToString() & vbCrLf &
                                       "Phone: " & detailRow("phone").ToString() & vbCrLf &
                                       "Email: " & detailRow("email").ToString() & vbCrLf & vbCrLf &
                                       "Check-In: " & detailRow("check_in").ToString() & vbCrLf &
                                       "Check-Out: " & detailRow("check_out").ToString() & vbCrLf &
                                       "Fees: ₹" & detailRow("fees").ToString() & vbCrLf &
                                       "Status: " & detailRow("status").ToString()

                MsgBox(details, MsgBoxStyle.Information, "Booking Details")
            End If

        Catch ex As Exception
            MsgBox("Error retrieving booking details: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ' ==========================================
    ' REFRESH BUTTONS
    ' ==========================================
    Private Sub btnRefreshBook_Click(sender As Object, e As EventArgs) Handles btnRefreshBook.Click
        LoadBookRequests()
        MsgBox("✓ Booking requests refreshed!", MsgBoxStyle.Information, "Refreshed")
    End Sub

    Private Sub btnRefreshReturn_Click(sender As Object, e As EventArgs) Handles btnRefreshReturn.Click
        LoadReturnRequests()
        MsgBox("✓ Return requests refreshed!", MsgBoxStyle.Information, "Refreshed")
    End Sub

    ' ==========================================
    ' BACK BUTTON
    ' ==========================================
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Dim result = MsgBox("Are you sure you want to close this window?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm Exit")
        If result = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

End Class