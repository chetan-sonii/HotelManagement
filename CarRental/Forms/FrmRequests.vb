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
        _styleManager.Style = ColorStyle.Blue
        _styleManager.Theme = ThemeStyle.Light
        Me.StyleManager = _styleManager
    End Sub

    Private Sub FrmRequests_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadRentRequests()
        LoadReturnRequests()
        UpdateStatistics()
        ConfigureDataGridViews()
    End Sub

    ' ==========================================
    ' CONFIGURE DATAGRIDS
    ' ==========================================
    Private Sub ConfigureDataGridViews()
        Try
            ' Configure Rent Requests Grid
            If dgvRentRequests.Columns.Count > 0 Then
                dgvRentRequests.Columns(0).HeaderText = "Booking ID"
                dgvRentRequests.Columns(1).HeaderText = "Room No"
                dgvRentRequests.Columns(2).HeaderText = "Guest ID"

                If dgvRentRequests.Columns.Count > 3 Then
                    dgvRentRequests.Columns(3).HeaderText = "Check-In"
                End If
                If dgvRentRequests.Columns.Count > 4 Then
                    dgvRentRequests.Columns(4).HeaderText = "Check-Out"
                End If
                If dgvRentRequests.Columns.Count > 5 Then
                    dgvRentRequests.Columns(5).HeaderText = "Guest Name"
                End If
                If dgvRentRequests.Columns.Count > 6 Then
                    dgvRentRequests.Columns(6).HeaderText = "Status"
                End If

                dgvRentRequests.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
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
    ' LOAD RENT/BOOKING REQUESTS
    ' ==========================================
    Private Sub LoadRentRequests()
        Try
            ' Enhanced query with guest information
            Dim query As String = "SELECT b.rent_id, b.room_no, b.cust_id, b.check_in, b.check_out, " &
                                 "g.cust_name, b.status " &
                                 "FROM tbl_bookings b " &
                                 "LEFT JOIN tbl_guests g ON b.cust_id = g.cust_id " &
                                 "WHERE b.status='Pending' " &
                                 "ORDER BY b.rent_id DESC"

            Dim dt As DataTable = DatabaseConnection.RunQuery(query)
            dgvRentRequests.DataSource = dt

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
            Dim query As String = "SELECT b.rent_id, b.room_no, b.cust_id, b.check_in, b.check_out, " &
                                 "g.cust_name, b.status " &
                                 "FROM tbl_bookings b " &
                                 "LEFT JOIN tbl_guests g ON b.cust_id = g.cust_id " &
                                 "WHERE b.status='ReturnPending' " &
                                 "ORDER BY b.rent_id DESC"

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
            Dim rentQuery As String = "SELECT COUNT(*) FROM tbl_bookings WHERE status='Pending'"
            Dim rentDt As DataTable = DatabaseConnection.RunQuery(rentQuery)
            Dim pendingRent As Integer = 0
            If rentDt.Rows.Count > 0 Then
                pendingRent = Convert.ToInt32(rentDt.Rows(0)(0))
            End If

            ' Count pending returns
            Dim returnQuery As String = "SELECT COUNT(*) FROM tbl_bookings WHERE status='ReturnPending'"
            Dim returnDt As DataTable = DatabaseConnection.RunQuery(returnQuery)
            Dim pendingReturn As Integer = 0
            If returnDt.Rows.Count > 0 Then
                pendingReturn = Convert.ToInt32(returnDt.Rows(0)(0))
            End If

            ' Update tiles
            PoisonTile1.Text = "Pending Bookings: " & pendingRent.ToString()
            PoisonTile2.Text = "Pending Returns: " & pendingReturn.ToString()
        Catch ex As Exception
            PoisonTile1.Text = "Pending Bookings: 0"
            PoisonTile2.Text = "Pending Returns: 0"
        End Try
    End Sub

    ' ==========================================
    ' APPROVE BOOKING REQUEST
    ' ==========================================
    Private Sub btnApproveRent_Click(sender As Object, e As EventArgs) Handles btnApproveRent.Click
        ' Validate selection
        If dgvRentRequests.SelectedRows.Count = 0 Then
            MsgBox("Please select a booking request to approve.", MsgBoxStyle.Exclamation, "No Selection")
            Return
        End If

        ' Safety check for empty row
        If dgvRentRequests.SelectedRows(0).IsNewRow Then
            MsgBox("Invalid selection. Please select a valid booking record.", MsgBoxStyle.Exclamation, "Invalid Selection")
            Return
        End If

        Try
            Dim row As DataGridViewRow = dgvRentRequests.SelectedRows(0)

            ' Validate cell data
            If row.Cells("rent_id").Value Is Nothing OrElse IsDBNull(row.Cells("rent_id").Value) Then
                MsgBox("Error: Selected row has no Booking ID.", MsgBoxStyle.Critical, "Invalid Data")
                Return
            End If

            Dim rentId As String = row.Cells("rent_id").Value.ToString()
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
                DatabaseConnection.ExecuteQuery("UPDATE tbl_bookings SET status='Active' WHERE rent_id=" & rentId)

                ' Mark room as unavailable
                DatabaseConnection.ExecuteQuery("UPDATE tbl_rooms SET available='No' WHERE room_no='" & roomNo.Replace("'", "''") & "'")

                MsgBox("✓ Booking approved successfully!" & vbCrLf & "Room " & roomNo & " is now occupied.", MsgBoxStyle.Information, "Success")

                LoadRentRequests()
            End If

        Catch ex As Exception
            MsgBox("Error approving booking: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ' ==========================================
    ' REJECT BOOKING REQUEST
    ' ==========================================
    Private Sub btnRejectRent_Click(sender As Object, e As EventArgs) Handles btnRejectRent.Click
        If dgvRentRequests.SelectedRows.Count = 0 Then
            MsgBox("Please select a booking request to reject.", MsgBoxStyle.Exclamation, "No Selection")
            Return
        End If

        If dgvRentRequests.SelectedRows(0).IsNewRow Then Return

        Try
            Dim row As DataGridViewRow = dgvRentRequests.SelectedRows(0)

            If row.Cells("rent_id").Value Is Nothing Then Return

            Dim rentId As String = row.Cells("rent_id").Value.ToString()
            Dim roomNo As String = row.Cells("room_no").Value.ToString()

            Dim result = MsgBox("Are you sure you want to reject this booking?" & vbCrLf &
                               "Room: " & roomNo & vbCrLf & vbCrLf &
                               "This action will cancel the booking request.",
                               MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm Rejection")

            If result = MsgBoxResult.Yes Then
                ' Update booking status to Rejected
                DatabaseConnection.ExecuteQuery("UPDATE tbl_bookings SET status='Rejected' WHERE rent_id=" & rentId)

                MsgBox("✓ Booking rejected successfully.", MsgBoxStyle.Information, "Success")

                LoadRentRequests()
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
            If row.Cells("rent_id").Value Is Nothing Then Return

            Dim rentId As String = row.Cells("rent_id").Value.ToString()
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
                DatabaseConnection.ExecuteQuery("UPDATE tbl_bookings SET status='Returned' WHERE rent_id=" & rentId)

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

            If row.Cells("rent_id").Value Is Nothing Then Return

            Dim rentId As String = row.Cells("rent_id").Value.ToString()

            ' Query full booking details
            Dim query As String = "SELECT b.*, g.cust_name, g.phone, g.email, r.brand, r.model, r.price " &
                                 "FROM tbl_bookings b " &
                                 "LEFT JOIN tbl_guests g ON b.cust_id = g.cust_id " &
                                 "LEFT JOIN tbl_rooms r ON b.room_no = r.room_no " &
                                 "WHERE b.rent_id=" & rentId

            Dim dt As DataTable = DatabaseConnection.RunQuery(query)

            If dt.Rows.Count > 0 Then
                Dim detailRow As DataRow = dt.Rows(0)

                Dim details As String = "📋 BOOKING DETAILS" & vbCrLf & vbCrLf &
                                       "Booking ID: " & detailRow("rent_id").ToString() & vbCrLf &
                                       "Room Number: " & detailRow("room_no").ToString() & vbCrLf &
                                       "Room Type: " & detailRow("brand").ToString() & vbCrLf &
                                       "Bed Type: " & detailRow("model").ToString() & vbCrLf & vbCrLf &
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
    Private Sub btnRefreshRent_Click(sender As Object, e As EventArgs) Handles btnRefreshRent.Click
        LoadRentRequests()
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