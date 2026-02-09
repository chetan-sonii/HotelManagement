Imports ReaLTaiizor.Forms
Imports ReaLTaiizor.Manager
Imports ReaLTaiizor.Enum.Poison
Imports MySql.Data.MySqlClient

Public Class FrmRooms
    Inherits PoisonForm

    ' Manager defined in code for safety
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

    Private Sub FrmRooms_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadRoomList()
        UpdateStatistics()
        ConfigureDataGridView()
    End Sub

    ' ==========================================
    ' ENHANCED DATAGRIDVIEW CONFIGURATION
    ' ==========================================
    Private Sub ConfigureDataGridView()
        Try
            ' Set column headers if data exists
            If dgvRooms.Columns.Count > 0 Then
                dgvRooms.Columns(0).HeaderText = "Room No. Number"
                dgvRooms.Columns(1).HeaderText = "Room_Type"
                dgvRooms.Columns(2).HeaderText = "bed_type"
                dgvRooms.Columns(3).HeaderText = "Price/Day (₹)"
                dgvRooms.Columns(4).HeaderText = "Available"

                ' Auto-size columns
                dgvRooms.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

                ' Set minimum column widths
                For Each col As DataGridViewColumn In dgvRooms.Columns
                    col.MinimumWidth = 100
                Next
            End If
        Catch ex As Exception
            ' Silently handle if columns aren't ready yet
        End Try
    End Sub

    ' ==========================================
    ' LOAD DATA (Refreshes the Grid)
    ' ==========================================
    Private Sub LoadRoomList()
        Try
            Dim query As String = "SELECT * FROM tbl_rooms"
            Dim dt As DataTable = DatabaseConnection.RunQuery(query)
            dgvRooms.DataSource = dt

            ' Configure columns after loading data
            ConfigureDataGridView()
            UpdateStatistics()
        Catch ex As Exception
            MsgBox("Error loading Room list: " & ex.Message, MsgBoxStyle.Critical, "Database Error")
        End Try
    End Sub

    ' ==========================================
    ' UPDATE STATISTICS
    ' ==========================================
    Private Sub UpdateStatistics()
        Try
            ' Count total Rooms
            Dim totalQuery As String = "SELECT COUNT(*) FROM tbl_rooms"
            Dim totalDt As DataTable = DatabaseConnection.RunQuery(totalQuery)
            Dim totalRooms As Integer = 0
            If totalDt.Rows.Count > 0 Then
                totalRooms = Convert.ToInt32(totalDt.Rows(0)(0))
            End If

            ' Count available Rooms
            Dim availQuery As String = "SELECT COUNT(*) FROM tbl_rooms WHERE available='Yes'"
            Dim availDt As DataTable = DatabaseConnection.RunQuery(availQuery)
            Dim availRooms As Integer = 0
            If availDt.Rows.Count > 0 Then
                availRooms = Convert.ToInt32(availDt.Rows(0)(0))
            End If

            ' Update tiles
            PoisonTile1.Text = "Total Rooms: " & totalRooms.ToString()
            PoisonTile2.Text = "Available: " & availRooms.ToString()

        Catch ex As Exception
            PoisonTile1.Text = "Total Rooms: 0"
            PoisonTile2.Text = "Available: 0"
        End Try
    End Sub

    ' ==========================================
    ' ENHANCED VALIDATION
    ' ==========================================
    Private Function ValidateInputs() As Boolean
        ' Check for empty fields
        If String.IsNullOrWhiteSpace(txtRegNo.Text) Then
            MsgBox("Please enter a Registration Number.", MsgBoxStyle.Exclamation, "Validation Error")
            txtRegNo.Focus()
            Return False
        End If

        If String.IsNullOrWhiteSpace(txtRoom_Type.Text) Then
            MsgBox("Please enter a Room_Type.", MsgBoxStyle.Exclamation, "Validation Error")
            txtRoom_Type.Focus()
            Return False
        End If

        If String.IsNullOrWhiteSpace(txtmodel.Text) Then
            MsgBox("Please enter a bed_type.", MsgBoxStyle.Exclamation, "Validation Error")
            txtmodel.Focus()
            Return False
        End If

        If String.IsNullOrWhiteSpace(txtPrice.Text) Then
            MsgBox("Please enter a Price.", MsgBoxStyle.Exclamation, "Validation Error")
            txtPrice.Focus()
            Return False
        End If

        ' Validate price is numeric
        Dim price As Decimal
        If Not Decimal.TryParse(txtPrice.Text, price) Then
            MsgBox("Please enter a valid numeric price.", MsgBoxStyle.Exclamation, "Validation Error")
            txtPrice.Focus()
            Return False
        End If

        If price <= 0 Then
            MsgBox("Price must be greater than zero.", MsgBoxStyle.Exclamation, "Validation Error")
            txtPrice.Focus()
            Return False
        End If

        ' Check availability selection
        If cbAvailable.SelectedIndex = -1 Then
            MsgBox("Please select Availability Status.", MsgBoxStyle.Exclamation, "Validation Error")
            cbAvailable.Focus()
            Return False
        End If

        Return True
    End Function

    ' ==========================================
    ' ADD BUTTON (Insert Logic)
    ' ==========================================
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        ' Validation
        If Not ValidateInputs() Then
            Return
        End If

        Try
            ' Check if registration number already exists
            Dim checkQuery As String = "SELECT COUNT(*) FROM tbl_rooms WHERE room_no='" & txtRegNo.Text.Trim() & "'"
            Dim checkDt As DataTable = DatabaseConnection.RunQuery(checkQuery)

            If checkDt.Rows.Count > 0 AndAlso Convert.ToInt32(checkDt.Rows(0)(0)) > 0 Then
                MsgBox("A Room with this registration number already exists!", MsgBoxStyle.Exclamation, "Duplicate Entry")
                txtRegNo.Focus()
                Return
            End If

            ' Sanitize inputs to prevent SQL injection (basic version - consider using parameters)
            Dim regNo As String = txtRegNo.Text.Trim().Replace("'", "''")
            Dim Room_Type As String = txtRoom_Type.Text.Trim().Replace("'", "''")
            Dim bed_type As String = txtmodel.Text.Trim().Replace("'", "''")
            Dim price As Decimal = Val(txtPrice.Text.Trim())
            Dim available As String = cbAvailable.SelectedItem.ToString()

            ' SQL Insert
            Dim query As String = "INSERT INTO tbl_rooms VALUES ('" & regNo & "', '" & Room_Type & "', '" & bed_type & "', " & price & ", '" & available & "')"
            DatabaseConnection.ExecuteQuery(query)

            MsgBox("✓ Room added successfully!", MsgBoxStyle.Information, "Success")
            LoadRoomList()
            ClearFields()
        Catch ex As Exception
            MsgBox("Error adding Room: " & ex.Message, MsgBoxStyle.Critical, "Database Error")
        End Try
    End Sub

    ' ==========================================
    ' EDIT BUTTON (Update Logic)
    ' ==========================================
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If String.IsNullOrWhiteSpace(txtRegNo.Text) Then
            MsgBox("Please select a Room from the list to update.", MsgBoxStyle.Exclamation, "No Selection")
            Return
        End If

        If Not ValidateInputs() Then
            Return
        End If

        Try
            ' Sanitize inputs
            Dim regNo As String = txtRegNo.Text.Trim().Replace("'", "''")
            Dim Room_Type As String = txtRoom_Type.Text.Trim().Replace("'", "''")
            Dim bed_type As String = txtmodel.Text.Trim().Replace("'", "''")
            Dim price As Decimal = Val(txtPrice.Text.Trim())
            Dim available As String = cbAvailable.SelectedItem.ToString()

            Dim query As String = "UPDATE tbl_rooms SET Room_Type='" & Room_Type & "', bed_type='" & bed_type & "', price=" & price & ", available='" & available & "' WHERE room_no='" & regNo & "'"
            DatabaseConnection.ExecuteQuery(query)

            MsgBox("✓ Room updated successfully!", MsgBoxStyle.Information, "Success")
            LoadRoomList()
            ClearFields()
        Catch ex As Exception
            MsgBox("Error updating Room: " & ex.Message, MsgBoxStyle.Critical, "Database Error")
        End Try
    End Sub

    ' ==========================================
    ' DELETE BUTTON (Remove Logic)
    ' ==========================================
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If String.IsNullOrWhiteSpace(txtRegNo.Text) Then
            MsgBox("Please select a Room from the list to delete.", MsgBoxStyle.Exclamation, "No Selection")
            Return
        End If

        Try
            Dim result = MsgBox("Are you sure you want to delete Room: " & txtRegNo.Text & "?" & vbCrLf & vbCrLf & "This action cannot be undone.", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm Deletion")

            If result = MsgBoxResult.Yes Then
                Dim regNo As String = txtRegNo.Text.Trim().Replace("'", "''")
                Dim query As String = "DELETE FROM tbl_rooms WHERE room_no='" & regNo & "'"
                DatabaseConnection.ExecuteQuery(query)

                MsgBox("✓ Room deleted successfully!", MsgBoxStyle.Information, "Success")
                LoadRoomList()
                ClearFields()
            End If
        Catch ex As Exception
            MsgBox("Error deleting Room: " & ex.Message, MsgBoxStyle.Critical, "Database Error")
        End Try
    End Sub

    ' ==========================================
    ' SEARCH FUNCTIONALITY
    ' ==========================================
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Try
            If String.IsNullOrWhiteSpace(txtSearch.Text) Then
                LoadRoomList()
                Return
            End If

            Dim searchText As String = txtSearch.Text.Trim().Replace("'", "''")
            Dim query As String = "SELECT * FROM tbl_rooms WHERE " &
                                 "room_no LIKE '%" & searchText & "%' OR " &
                                 "Room_Type LIKE '%" & searchText & "%' OR " &
                                 "bed_type LIKE '%" & searchText & "%' OR " &
                                 "available LIKE '%" & searchText & "%'"

            Dim dt As DataTable = DatabaseConnection.RunQuery(query)
            dgvRooms.DataSource = dt
            ConfigureDataGridView()
        Catch ex As Exception
            ' Silently handle search errors
        End Try
    End Sub

    ' ==========================================
    ' GRID CLICK (Fill Textboxes)
    ' ==========================================
    Private Sub dgvRooms_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvRooms.CellClick
        If e.RowIndex >= 0 Then
            Try
                Dim row As DataGridViewRow = dgvRooms.Rows(e.RowIndex)

                ' Populate fields from the clicked row
                txtRegNo.Text = row.Cells(0).Value.ToString()
                txtRoom_Type.Text = row.Cells(1).Value.ToString()
                txtmodel.Text = row.Cells(2).Value.ToString()
                txtPrice.Text = row.Cells(3).Value.ToString()

                ' Set combo box selection
                Dim availStatus As String = row.Cells(4).Value.ToString()
                If availStatus = "Yes" Then
                    cbAvailable.SelectedIndex = 0
                ElseIf availStatus = "No" Then
                    cbAvailable.SelectedIndex = 1
                End If
            Catch ex As Exception
                MsgBox("Error loading Room details: " & ex.Message, MsgBoxStyle.Exclamation)
            End Try
        End If
    End Sub

    ' ==========================================
    ' CLEAR BUTTON
    ' ==========================================
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearFields()
    End Sub

    ' ==========================================
    ' REFRESH BUTTON
    ' ==========================================
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadRoomList()
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

    ' ==========================================
    ' HELPER: CLEAR FIELDS
    ' ==========================================
    Private Sub ClearFields()
        txtRegNo.Text = ""
        txtRoom_Type.Text = ""
        txtmodel.Text = ""
        txtPrice.Text = ""
        cbAvailable.SelectedIndex = -1
        txtRegNo.Focus()
    End Sub

    ' ==========================================
    ' NUMERIC INPUT VALIDATION FOR PRICE
    ' ==========================================
    Private Sub txtPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPrice.KeyPress
        ' Allow only numbers, decimal point, and control keys
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "."c Then
            e.Handled = True
        End If

        ' Allow only one decimal point
        If e.KeyChar = "."c AndAlso txtPrice.Text.IndexOf("."c) > -1 Then
            e.Handled = True
        End If
    End Sub

End Class
