Imports ReaLTaiizor.Forms
Imports ReaLTaiizor.Manager
Imports ReaLTaiizor.Enum.Poison
Imports MySql.Data.MySqlClient

Public Class FrmGuests
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

    Private Sub FrmGuests_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadGuestList()
        UpdateStatistics()
        ConfigureDataGridView()
    End Sub

    ' ==========================================
    ' CONFIGURE DATAGRIDVIEW
    ' ==========================================
    Private Sub ConfigureDataGridView()
        Try
            If dgvGuests.Columns.Count > 0 Then
                dgvGuests.Columns(0).HeaderText = "ID"
                dgvGuests.Columns(1).HeaderText = "Guest Name"
                dgvGuests.Columns(2).HeaderText = "Address"
                dgvGuests.Columns(3).HeaderText = "Phone"
                
                ' If email column exists
                If dgvGuests.Columns.Count > 4 Then
                    dgvGuests.Columns(4).HeaderText = "Email"
                End If

                ' Auto-size columns
                dgvGuests.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                
                ' Set minimum widths
                dgvGuests.Columns(0).Width = 50
                dgvGuests.Columns(0).MinimumWidth = 50
            End If
        Catch ex As Exception
            ' Silently handle if columns aren't ready
        End Try
    End Sub

    ' ==========================================
    ' LOAD Guest LIST
    ' ==========================================
    Private Sub LoadGuestList()
        Try
            Dim query As String = "SELECT * FROM tbl_guests ORDER BY cust_id DESC"
            Dim dt As DataTable = DatabaseConnection.RunQuery(query)
            dgvGuests.DataSource = dt
            
            ConfigureDataGridView()
            UpdateStatistics()
        Catch ex As Exception
            MsgBox("Error loading Guest list: " & ex.Message, MsgBoxStyle.Critical, "Database Error")
        End Try
    End Sub

    ' ==========================================
    ' UPDATE STATISTICS
    ' ==========================================
    Private Sub UpdateStatistics()
        Try
            Dim query As String = "SELECT COUNT(*) FROM tbl_guests"
            Dim dt As DataTable = DatabaseConnection.RunQuery(query)
            Dim totalGuests As Integer = 0
            
            If dt.Rows.Count > 0 Then
                totalGuests = Convert.ToInt32(dt.Rows(0)(0))
            End If

            PoisonTile1.Text = "Total Guests: " & totalGuests.ToString()
        Catch ex As Exception
            PoisonTile1.Text = "Total Guests: 0"
        End Try
    End Sub

    ' ==========================================
    ' INPUT VALIDATION
    ' ==========================================
    Private Function ValidateInputs() As Boolean
        ' Check name
        If String.IsNullOrWhiteSpace(txtName.Text) Then
            MsgBox("Please enter Guest name.", MsgBoxStyle.Exclamation, "Validation Error")
            txtName.Focus()
            Return False
        End If

        ' Check phone
        If String.IsNullOrWhiteSpace(txtPhone.Text) Then
            MsgBox("Please enter phone number.", MsgBoxStyle.Exclamation, "Validation Error")
            txtPhone.Focus()
            Return False
        End If

        ' Validate phone number (basic check)
        If txtPhone.Text.Trim().Length < 10 Then
            MsgBox("Phone number must be at least 10 digits.", MsgBoxStyle.Exclamation, "Invalid Phone")
            txtPhone.Focus()
            Return False
        End If

        ' Validate email if provided
        If Not String.IsNullOrWhiteSpace(txtEmail.Text) Then
            If Not txtEmail.Text.Contains("@") OrElse Not txtEmail.Text.Contains(".") Then
                MsgBox("Please enter a valid email address.", MsgBoxStyle.Exclamation, "Invalid Email")
                txtEmail.Focus()
                Return False
            End If
        End If

        Return True
    End Function

    ' ==========================================
    ' ADD BUTTON
    ' ==========================================
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If Not ValidateInputs() Then
            Return
        End If

        Try
            ' Sanitize inputs
            Dim name As String = txtName.Text.Trim().Replace("'", "''")
            Dim address As String = txtAddress.Text.Trim().Replace("'", "''")
            Dim phone As String = txtPhone.Text.Trim().Replace("'", "''")
            Dim email As String = txtEmail.Text.Trim().Replace("'", "''")

            ' Check for duplicate phone number
            Dim checkQuery As String = "SELECT COUNT(*) FROM tbl_guests WHERE phone='" & phone & "'"
            Dim checkDt As DataTable = DatabaseConnection.RunQuery(checkQuery)
            
            If checkDt.Rows.Count > 0 AndAlso Convert.ToInt32(checkDt.Rows(0)(0)) > 0 Then
                MsgBox("A Guest with this phone number already exists!", MsgBoxStyle.Exclamation, "Duplicate Entry")
                txtPhone.Focus()
                Return
            End If

            ' Insert query (ID is auto-increment)
            Dim query As String = "INSERT INTO tbl_guests (cust_name, address, phone, email) VALUES ('" & name & "', '" & address & "', '" & phone & "', '" & email & "')"
            DatabaseConnection.ExecuteQuery(query)

            MsgBox("✓ Guest added successfully!", MsgBoxStyle.Information, "Success")
            LoadGuestList()
            ClearFields()
        Catch ex As Exception
            MsgBox("Error adding Guest: " & ex.Message, MsgBoxStyle.Critical, "Database Error")
        End Try
    End Sub

    ' ==========================================
    ' EDIT BUTTON
    ' ==========================================
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If String.IsNullOrWhiteSpace(txtId.Text) Then
            MsgBox("Please select a Guest from the list to update.", MsgBoxStyle.Exclamation, "No Selection")
            Return
        End If

        If Not ValidateInputs() Then
            Return
        End If

        Try
            ' Sanitize inputs
            Dim id As String = txtId.Text.Trim()
            Dim name As String = txtName.Text.Trim().Replace("'", "''")
            Dim address As String = txtAddress.Text.Trim().Replace("'", "''")
            Dim phone As String = txtPhone.Text.Trim().Replace("'", "''")
            Dim email As String = txtEmail.Text.Trim().Replace("'", "''")

            ' Check for duplicate phone (excluding current record)
            Dim checkQuery As String = "SELECT COUNT(*) FROM tbl_guests WHERE phone='" & phone & "' AND cust_id<>" & id
            Dim checkDt As DataTable = DatabaseConnection.RunQuery(checkQuery)
            
            If checkDt.Rows.Count > 0 AndAlso Convert.ToInt32(checkDt.Rows(0)(0)) > 0 Then
                MsgBox("Another Guest with this phone number already exists!", MsgBoxStyle.Exclamation, "Duplicate Entry")
                txtPhone.Focus()
                Return
            End If

            Dim query As String = "UPDATE tbl_guests SET cust_name='" & name & "', address='" & address & "', phone='" & phone & "', email='" & email & "' WHERE cust_id=" & id
            DatabaseConnection.ExecuteQuery(query)

            MsgBox("✓ Guest updated successfully!", MsgBoxStyle.Information, "Success")
            LoadGuestList()
            ClearFields()
        Catch ex As Exception
            MsgBox("Error updating Guest: " & ex.Message, MsgBoxStyle.Critical, "Database Error")
        End Try
    End Sub

    ' ==========================================
    ' DELETE BUTTON
    ' ==========================================
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If String.IsNullOrWhiteSpace(txtId.Text) Then
            MsgBox("Please select a Guest from the list to delete.", MsgBoxStyle.Exclamation, "No Selection")
            Return
        End If

        Try
            Dim result = MsgBox("Are you sure you want to delete Guest: " & txtName.Text & "?" & vbCrLf & vbCrLf & "This action cannot be undone.", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm Deletion")
            
            If result = MsgBoxResult.Yes Then
                Dim id As String = txtId.Text.Trim()
                Dim query As String = "DELETE FROM tbl_guests WHERE cust_id=" & id
                DatabaseConnection.ExecuteQuery(query)

                MsgBox("✓ Guest deleted successfully!", MsgBoxStyle.Information, "Success")
                LoadGuestList()
                ClearFields()
            End If
        Catch ex As Exception
            MsgBox("Error deleting Guest: " & ex.Message, MsgBoxStyle.Critical, "Database Error")
        End Try
    End Sub

    ' ==========================================
    ' SEARCH FUNCTIONALITY
    ' ==========================================
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Try
            If String.IsNullOrWhiteSpace(txtSearch.Text) Then
                LoadGuestList()
                Return
            End If

            Dim searchText As String = txtSearch.Text.Trim().Replace("'", "''")
            Dim query As String = "SELECT * FROM tbl_guests WHERE " &
                                 "cust_name LIKE '%" & searchText & "%' OR " &
                                 "phone LIKE '%" & searchText & "%' OR " &
                                 "address LIKE '%" & searchText & "%' OR " &
                                 "email LIKE '%" & searchText & "%' " &
                                 "ORDER BY cust_id DESC"
            
            Dim dt As DataTable = DatabaseConnection.RunQuery(query)
            dgvGuests.DataSource = dt
            ConfigureDataGridView()
        Catch ex As Exception
            ' Silently handle search errors
        End Try
    End Sub

    ' ==========================================
    ' GRID CLICK - FILL TEXTBOXES
    ' ==========================================
    Private Sub dgvGuests_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvGuests.CellClick
        If e.RowIndex >= 0 Then
            Try
                Dim row As DataGridViewRow = dgvGuests.Rows(e.RowIndex)

                ' Populate fields
                txtId.Text = row.Cells(0).Value.ToString()
                txtName.Text = row.Cells(1).Value.ToString()
                txtAddress.Text = row.Cells(2).Value.ToString()
                txtPhone.Text = row.Cells(3).Value.ToString()
                
                ' Email field (if exists)
                If row.Cells.Count > 4 AndAlso row.Cells(4).Value IsNot Nothing Then
                    txtEmail.Text = row.Cells(4).Value.ToString()
                Else
                    txtEmail.Text = ""
                End If
            Catch ex As Exception
                MsgBox("Error loading Guest details: " & ex.Message, MsgBoxStyle.Exclamation)
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
        LoadGuestList()
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
        txtId.Text = ""
        txtName.Text = ""
        txtAddress.Text = ""
        txtPhone.Text = ""
        txtEmail.Text = ""
        txtName.Focus()
    End Sub

    ' ==========================================
    ' PHONE NUMBER VALIDATION (NUMERIC ONLY)
    ' ==========================================
    Private Sub txtPhone_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPhone.KeyPress
        ' Allow only numbers, backspace, delete, and some special chars (+, -, space)
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "+"c AndAlso e.KeyChar <> "-"c AndAlso e.KeyChar <> " "c Then
            e.Handled = True
        End If
    End Sub

End Class
