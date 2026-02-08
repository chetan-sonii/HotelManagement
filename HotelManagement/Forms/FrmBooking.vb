Imports ReaLTaiizor.Forms
Imports ReaLTaiizor.Manager
Imports ReaLTaiizor.Enum.Poison
Imports MySql.Data.MySqlClient

Public Class FrmBooking
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

    Private Sub FrmBooking_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FillRoomCombo()
        FillGuestCombo()
        LoadBookingHistory()
        UpdateStatistics()
        ConfigureDataGridView()

        ' Set default dates
        dtCheckIn.Value = DateTime.Now
        dtCheckOut.Value = DateTime.Now.AddDays(1)
    End Sub

    ' ==========================================
    ' CONFIGURE DATAGRIDVIEW
    ' ==========================================
    Private Sub ConfigureDataGridView()
        Try
            If dgvBookings.Columns.Count > 0 Then
                dgvBookings.Columns(0).HeaderText = "Booking ID"
                dgvBookings.Columns(1).HeaderText = "Room No"
                dgvBookings.Columns(2).HeaderText = "Guest ID"
                dgvBookings.Columns(3).HeaderText = "Check-In"
                dgvBookings.Columns(4).HeaderText = "Check-Out"
                dgvBookings.Columns(5).HeaderText = "Fees (₹)"

                If dgvBookings.Columns.Count > 6 Then
                    dgvBookings.Columns(6).HeaderText = "Status"
                End If

                dgvBookings.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            End If
        Catch ex As Exception
            ' Silently handle configuration errors
        End Try
    End Sub

    ' ==========================================
    ' FILL ROOM DROPDOWN (ONLY AVAILABLE)
    ' ==========================================
    Private Sub FillRoomCombo()
        Try
            Dim query As String = "SELECT room_no, brand, model FROM tbl_rooms WHERE available='Yes' ORDER BY room_no"
            Dim dt As DataTable = DatabaseConnection.RunQuery(query)

            cbRoom.Items.Clear()
            cbRoom.DisplayMember = "room_no"
            cbRoom.ValueMember = "room_no"

            For Each row As DataRow In dt.Rows
                cbRoom.Items.Add(row("room_no").ToString())
            Next

            If cbRoom.Items.Count = 0 Then
                MsgBox("No rooms are curBookly available for booking.", MsgBoxStyle.Information, "No Availability")
            End If
        Catch ex As Exception
            MsgBox("Error loading rooms: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    ' ==========================================
    ' FILL GUEST DROPDOWN
    ' ==========================================
    Private Sub FillGuestCombo()
        Try
            Dim query As String = "SELECT cust_id, cust_name FROM tbl_guests ORDER BY cust_name"
            Dim dt As DataTable = DatabaseConnection.RunQuery(query)

            cbCust.Items.Clear()

            For Each row As DataRow In dt.Rows
                cbCust.Items.Add(row("cust_id").ToString() & " - " & row("cust_name").ToString())
            Next
        Catch ex As Exception
            MsgBox("Error loading guests: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    ' ==========================================
    ' ROOM SELECTED - SHOW ROOM INFO
    ' ==========================================
    Private Sub cbRoom_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbRoom.SelectedIndexChanged
        If cbRoom.SelectedIndex = -1 OrElse cbRoom.SelectedItem Is Nothing Then
            lblRoomInfo.Text = "Select room first"
            Return
        End If

        Try
            Dim roomNo As String = cbRoom.SelectedItem.ToString()
            Dim query As String = "SELECT brand, model, price FROM tbl_rooms WHERE room_no='" & roomNo.Replace("'", "''") & "'"
            Dim dt As DataTable = DatabaseConnection.RunQuery(query)

            If dt.Rows.Count > 0 Then
                Dim roomType As String = dt.Rows(0)("brand").ToString()
                Dim bedType As String = dt.Rows(0)("model").ToString()
                Dim price As String = dt.Rows(0)("price").ToString()

                lblRoomInfo.Text = roomType & " | " & bedType & " | ₹" & price & "/day"
            Else
                lblRoomInfo.Text = "Room info not available"
            End If
        Catch ex As Exception
            lblRoomInfo.Text = "Error loading room info"
        End Try
    End Sub

    ' ==========================================
    ' GUEST SELECTED - SHOW GUEST INFO
    ' ==========================================
    Private Sub cbCust_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbCust.SelectedIndexChanged
        If cbCust.SelectedIndex = -1 OrElse cbCust.SelectedItem Is Nothing Then
            lblGuestName.Text = "Select a guest first"
            lblGuestPhone.Text = "Phone"
            Return
        End If

        Try
            ' Extract ID from "ID - Name" format
            Dim selectedText As String = cbCust.SelectedItem.ToString()
            Dim custId As String = selectedText.Split("-"c)(0).Trim()

            Dim query As String = "SELECT cust_name, phone FROM tbl_guests WHERE cust_id=" & custId
            Dim dt As DataTable = DatabaseConnection.RunQuery(query)

            If dt.Rows.Count > 0 Then
                lblGuestName.Text = dt.Rows(0)("cust_name").ToString()
                lblGuestPhone.Text = "📞 " & dt.Rows(0)("phone").ToString()
            Else
                lblGuestName.Text = "Guest not found"
                lblGuestPhone.Text = "Phone"
            End If
        Catch ex As Exception
            lblGuestName.Text = "Error loading guest info"
            lblGuestPhone.Text = "Phone"
        End Try
    End Sub

    ' ==========================================
    ' DATE CHANGED - UPDATE DURATION
    ' ==========================================
    Private Sub dtCheckIn_ValueChanged(sender As Object, e As EventArgs) Handles dtCheckIn.ValueChanged, dtCheckOut.ValueChanged
        UpdateDuration()
    End Sub

    Private Sub UpdateDuration()
        Try
            Dim checkIn As Date = dtCheckIn.Value.Date
            Dim checkOut As Date = dtCheckOut.Value.Date
            Dim duration As TimeSpan = checkOut - checkIn
            Dim days As Integer = duration.Days

            If days < 0 Then
                lblDays.Text = "Invalid dates!"
                lblDays.UseCustomForeColor = True
                lblDays.ForeColor = Color.Red
            ElseIf days = 0 Then
                lblDays.Text = "Same day booking"
                lblDays.UseCustomForeColor = False
            Else
                lblDays.Text = days.ToString() & " day" & If(days > 1, "s", "")
                lblDays.UseCustomForeColor = False
            End If
        Catch ex As Exception
            lblDays.Text = "0 days"
        End Try
    End Sub

    ' ==========================================
    ' CALCULATE TOTAL FEE
    ' ==========================================
    Private Sub btnCalculate_Click(sender As Object, e As EventArgs) Handles btnCalculate.Click
        ' Validation
        If cbRoom.SelectedIndex = -1 Then
            MsgBox("Please select a room.", MsgBoxStyle.Exclamation, "Room Required")
            cbRoom.Focus()
            Return
        End If

        Try
            ' Get room price
            Dim roomNo As String = cbRoom.SelectedItem.ToString()
            Dim query As String = "SELECT price FROM tbl_rooms WHERE room_no='" & roomNo.Replace("'", "''") & "'"
            Dim dt As DataTable = DatabaseConnection.RunQuery(query)
            Dim pricePerDay As Decimal = 0

            If dt.Rows.Count > 0 Then
                pricePerDay = Convert.ToDecimal(dt.Rows(0)("price"))
            Else
                MsgBox("Unable to retrieve room price.", MsgBoxStyle.Exclamation)
                Return
            End If

            ' Calculate duration
            Dim checkIn As Date = dtCheckIn.Value.Date
            Dim checkOut As Date = dtCheckOut.Value.Date
            Dim duration As TimeSpan = checkOut - checkIn
            Dim totalDays As Integer = duration.Days

            ' Validate dates
            If totalDays <= 0 Then
                MsgBox("Check-out date must be after check-in date.", MsgBoxStyle.Exclamation, "Invalid Dates")
                dtCheckOut.Focus()
                Return
            End If

            ' Calculate and display total
            Dim totalFee As Decimal = totalDays * pricePerDay
            lblFee.Text = "₹ " & totalFee.ToString("N2")

            MsgBox("Calculation complete!" & vbCrLf & vbCrLf &
                   "Duration: " & totalDays & " day(s)" & vbCrLf &
                   "Rate: ₹" & pricePerDay.ToString() & "/day" & vbCrLf &
                   "Total: ₹" & totalFee.ToString("N2"),
                   MsgBoxStyle.Information, "Fee Calculated")

        Catch ex As Exception
            MsgBox("Error calculating fee: " & ex.Message, MsgBoxStyle.Critical, "Calculation Error")
        End Try
    End Sub

    ' ==========================================
    ' CONFIRM BOOKING
    ' ==========================================
    Private Sub btnBook_Click(sender As Object, e As EventArgs) Handles btnBook.Click
        ' Validation
        If cbRoom.SelectedIndex = -1 Then
            MsgBox("Please select a room.", MsgBoxStyle.Exclamation, "Room Required")
            cbRoom.Focus()
            Return
        End If

        If cbCust.SelectedIndex = -1 Then
            MsgBox("Please select a guest.", MsgBoxStyle.Exclamation, "Guest Required")
            cbCust.Focus()
            Return
        End If

        ' Validate dates
        Dim checkIn As Date = dtCheckIn.Value.Date
        Dim checkOut As Date = dtCheckOut.Value.Date
        Dim duration As TimeSpan = checkOut - checkIn
        Dim totalDays As Integer = duration.Days

        If totalDays <= 0 Then
            MsgBox("Check-out date must be after check-in date.", MsgBoxStyle.Exclamation, "Invalid Dates")
            Return
        End If

        ' Get fee
        Dim feeString As String = lblFee.Text.Replace("₹ ", "").Replace(",", "").Trim()
        Dim fee As Decimal = 0

        If Not Decimal.TryParse(feeString, fee) OrElse fee <= 0 Then
            MsgBox("Please calculate the fee first by clicking 'Calculate Total Fee'.", MsgBoxStyle.Exclamation, "Fee Required")
            btnCalculate.Focus()
            Return
        End If

        Try
            ' Extract IDs
            Dim roomNo As String = cbRoom.SelectedItem.ToString()
            Dim selectedText As String = cbCust.SelectedItem.ToString()
            Dim custId As String = selectedText.Split("-"c)(0).Trim()

            ' Confirm booking
            Dim result = MsgBox("Confirm booking details:" & vbCrLf & vbCrLf &
                               "Room: " & roomNo & vbCrLf &
                               "Guest: " & lblGuestName.Text & vbCrLf &
                               "Check-In: " & checkIn.ToString("dd-MMM-yyyy") & vbCrLf &
                               "Check-Out: " & checkOut.ToString("dd-MMM-yyyy") & vbCrLf &
                               "Duration: " & totalDays & " day(s)" & vbCrLf &
                               "Total Fee: ₹" & fee.ToString("N2") & vbCrLf & vbCrLf &
                               "Proceed with booking?",
                               MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm Booking")

            If result = MsgBoxResult.No Then Return

            ' Insert booking
            Dim dateIn As String = checkIn.ToString("yyyy-MM-dd")
            Dim dateOut As String = checkOut.ToString("yyyy-MM-dd")

            Dim queryInsert As String = "INSERT INTO tbl_bookings (room_no, cust_id, check_in, check_out, fees, status) " &
                                       "VALUES ('" & roomNo.Replace("'", "''") & "', " & custId & ", '" & dateIn & "', '" & dateOut & "', " & fee & ", 'Pending')"
            DatabaseConnection.ExecuteQuery(queryInsert)

            ' Update room status (mark as unavailable)
            ' Note: This happens immediately, but status will be 'Pending' until approved in FrmRequests
            Dim queryUpdate As String = "UPDATE tbl_rooms SET available='No' WHERE room_no='" & roomNo.Replace("'", "''") & "'"
            DatabaseConnection.ExecuteQuery(queryUpdate)

            MsgBox("✓ Booking created successfully!" & vbCrLf & vbCrLf &
                   "Status: Pending Approval" & vbCrLf &
                   "The booking will be activated once approved by management.",
                   MsgBoxStyle.Information, "Success")

            ' Refresh and clear
            LoadBookingHistory()
            FillRoomCombo()
            ClearFields()

        Catch ex As Exception
            MsgBox("Error creating booking: " & ex.Message, MsgBoxStyle.Critical, "Booking Error")
        End Try
    End Sub

    ' ==========================================
    ' LOAD BOOKING HISTORY
    ' ==========================================
    Private Sub LoadBookingHistory()
        Try
            Dim query As String = "SELECT * FROM tbl_bookings ORDER BY Book_id DESC"
            Dim dt As DataTable = DatabaseConnection.RunQuery(query)
            dgvBookings.DataSource = dt

            ConfigureDataGridView()
            UpdateStatistics()
        Catch ex As Exception
            MsgBox("Error loading booking history: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    ' ==========================================
    ' UPDATE STATISTICS
    ' ==========================================
    Private Sub UpdateStatistics()
        Try
            Dim query As String = "SELECT COUNT(*) FROM tbl_bookings"
            Dim dt As DataTable = DatabaseConnection.RunQuery(query)
            Dim total As Integer = 0

            If dt.Rows.Count > 0 Then
                total = Convert.ToInt32(dt.Rows(0)(0))
            End If

            PoisonTile1.Text = "Total Bookings: " & total.ToString()
        Catch ex As Exception
            PoisonTile1.Text = "Total Bookings: 0"
        End Try
    End Sub

    ' ==========================================
    ' SEARCH FUNCTIONALITY
    ' ==========================================
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Try
            If String.IsNullOrWhiteSpace(txtSearch.Text) Then
                LoadBookingHistory()
                Return
            End If

            Dim searchText As String = txtSearch.Text.Trim().Replace("'", "''")
            Dim query As String = "SELECT * FROM tbl_bookings WHERE " &
                                 "Book_id LIKE '%" & searchText & "%' OR " &
                                 "room_no LIKE '%" & searchText & "%' OR " &
                                 "cust_id LIKE '%" & searchText & "%' OR " &
                                 "status LIKE '%" & searchText & "%' " &
                                 "ORDER BY Book_id DESC"

            Dim dt As DataTable = DatabaseConnection.RunQuery(query)
            dgvBookings.DataSource = dt
            ConfigureDataGridView()
        Catch ex As Exception
            ' Silently handle search errors
        End Try
    End Sub

    ' ==========================================
    ' CLEAR FIELDS
    ' ==========================================
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearFields()
    End Sub

    Private Sub ClearFields()
        cbRoom.SelectedIndex = -1
        cbCust.SelectedIndex = -1
        lblGuestName.Text = "Select a guest first"
        lblGuestPhone.Text = "Phone"
        lblRoomInfo.Text = "Select room first"
        lblFee.Text = "₹ 0.00"
        lblDays.Text = "0 days"
        lblDays.UseCustomForeColor = False
        dtCheckIn.Value = DateTime.Now
        dtCheckOut.Value = DateTime.Now.AddDays(1)
        cbRoom.Focus()
    End Sub

    ' ==========================================
    ' REFRESH BUTTON
    ' ==========================================
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadBookingHistory()
        FillRoomCombo()
        FillGuestCombo()
        txtSearch.Text = ""
        MsgBox("✓ Data refreshed successfully!", MsgBoxStyle.Information, "Refreshed")
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