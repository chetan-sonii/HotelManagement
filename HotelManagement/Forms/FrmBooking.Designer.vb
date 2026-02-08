<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmBooking
    Inherits ReaLTaiizor.Forms.PoisonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.PoisonStyleManager1 = New ReaLTaiizor.Manager.PoisonStyleManager(Me.components)
        Me.dgvBookings = New ReaLTaiizor.Controls.PoisonDataGridView()
        Me.lblRoom = New ReaLTaiizor.Controls.PoisonLabel()
        Me.lblDate = New ReaLTaiizor.Controls.PoisonLabel()
        Me.PoisonLabel3 = New ReaLTaiizor.Controls.PoisonLabel()
        Me.lblFee = New ReaLTaiizor.Controls.PoisonLabel()
        Me.lblGuestName = New ReaLTaiizor.Controls.PoisonLabel()
        Me.lblReturn = New ReaLTaiizor.Controls.PoisonLabel()
        Me.cbRoom = New ReaLTaiizor.Controls.PoisonComboBox()
        Me.cbCust = New ReaLTaiizor.Controls.PoisonComboBox()
        Me.dtCheckIn = New ReaLTaiizor.Controls.PoisonDateTime()
        Me.dtCheckOut = New ReaLTaiizor.Controls.PoisonDateTime()
        Me.btnCalculate = New ReaLTaiizor.Controls.PoisonButton()
        Me.btnBook = New ReaLTaiizor.Controls.PoisonButton()
        Me.btnBack = New ReaLTaiizor.Controls.PoisonButton()
        Me.PoisonLabel1 = New ReaLTaiizor.Controls.PoisonLabel()
        Me.PoisonPanel1 = New ReaLTaiizor.Controls.PoisonPanel()
        Me.lblRoomInfo = New ReaLTaiizor.Controls.PoisonLabel()
        Me.PoisonLabel8 = New ReaLTaiizor.Controls.PoisonLabel()
        Me.lblDays = New ReaLTaiizor.Controls.PoisonLabel()
        Me.PoisonLabel6 = New ReaLTaiizor.Controls.PoisonLabel()
        Me.lblGuestPhone = New ReaLTaiizor.Controls.PoisonLabel()
        Me.PoisonLabel4 = New ReaLTaiizor.Controls.PoisonLabel()
        Me.btnClear = New ReaLTaiizor.Controls.PoisonButton()
        Me.btnRefresh = New ReaLTaiizor.Controls.PoisonButton()
        Me.PoisonPanel2 = New ReaLTaiizor.Controls.PoisonPanel()
        Me.PoisonLabel2 = New ReaLTaiizor.Controls.PoisonLabel()
        Me.PoisonTile1 = New ReaLTaiizor.Controls.PoisonTile()
        Me.txtSearch = New ReaLTaiizor.Controls.PoisonTextBox()
        Me.PoisonLabel5 = New ReaLTaiizor.Controls.PoisonLabel()
        CType(Me.PoisonStyleManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvBookings, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PoisonPanel1.SuspendLayout()
        Me.PoisonPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'PoisonStyleManager1
        '
        Me.PoisonStyleManager1.Owner = Me
        '
        'dgvBookings
        '
        Me.dgvBookings.AllowUserToAddRows = False
        Me.dgvBookings.AllowUserToDeleteRows = False
        Me.dgvBookings.AllowUserToResizeRows = False
        Me.dgvBookings.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvBookings.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvBookings.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.dgvBookings.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvBookings.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle10
        Me.dgvBookings.ColumnHeadersHeight = 35
        Me.dgvBookings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(136, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(136, Byte), Integer))
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvBookings.DefaultCellStyle = DataGridViewCellStyle11
        Me.dgvBookings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvBookings.EnableHeadersVisualStyles = False
        Me.dgvBookings.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.dgvBookings.GridColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvBookings.Location = New System.Drawing.Point(0, 0)
        Me.dgvBookings.MultiSelect = False
        Me.dgvBookings.Name = "dgvBookings"
        Me.dgvBookings.ReadOnly = True
        Me.dgvBookings.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvBookings.RowHeadersDefaultCellStyle = DataGridViewCellStyle12
        Me.dgvBookings.RowHeadersVisible = False
        Me.dgvBookings.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvBookings.RowTemplate.Height = 30
        Me.dgvBookings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvBookings.Size = New System.Drawing.Size(670, 480)
        Me.dgvBookings.Style = ReaLTaiizor.[Enum].Poison.ColorStyle.Red
        Me.dgvBookings.TabIndex = 0
        '
        'lblRoom
        '
        Me.lblRoom.AutoSize = True
        Me.lblRoom.Location = New System.Drawing.Point(15, 33)
        Me.lblRoom.Name = "lblRoom"
        Me.lblRoom.Size = New System.Drawing.Size(83, 19)
        Me.lblRoom.TabIndex = 1
        Me.lblRoom.Text = "Select Room"
        '
        'lblDate
        '
        Me.lblDate.AutoSize = True
        Me.lblDate.Location = New System.Drawing.Point(15, 298)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(90, 19)
        Me.lblDate.TabIndex = 3
        Me.lblDate.Text = "Check-In Date"
        '
        'PoisonLabel3
        '
        Me.PoisonLabel3.AutoSize = True
        Me.PoisonLabel3.Location = New System.Drawing.Point(15, 97)
        Me.PoisonLabel3.Name = "PoisonLabel3"
        Me.PoisonLabel3.Size = New System.Drawing.Size(79, 19)
        Me.PoisonLabel3.TabIndex = 3
        Me.PoisonLabel3.Text = "Select Guest"
        '
        'lblFee
        '
        Me.lblFee.AutoSize = True
        Me.lblFee.Location = New System.Drawing.Point(17, 483)
        Me.lblFee.Name = "lblFee"
        Me.lblFee.Size = New System.Drawing.Size(44, 19)
        Me.lblFee.Style = ReaLTaiizor.[Enum].Poison.ColorStyle.Red
        Me.lblFee.TabIndex = 3
        Me.lblFee.Text = "₹ 0.00"
        Me.lblFee.UseStyleColors = True
        '
        'lblGuestName
        '
        Me.lblGuestName.AutoSize = True
        Me.lblGuestName.Location = New System.Drawing.Point(15, 257)
        Me.lblGuestName.Name = "lblGuestName"
        Me.lblGuestName.Size = New System.Drawing.Size(114, 19)
        Me.lblGuestName.TabIndex = 3
        Me.lblGuestName.Text = "Select a guest first"
        '
        'lblReturn
        '
        Me.lblReturn.AutoSize = True
        Me.lblReturn.Location = New System.Drawing.Point(15, 362)
        Me.lblReturn.Name = "lblReturn"
        Me.lblReturn.Size = New System.Drawing.Size(102, 19)
        Me.lblReturn.TabIndex = 3
        Me.lblReturn.Text = "Check-Out Date"
        '
        'cbRoom
        '
        Me.cbRoom.FormattingEnabled = True
        Me.cbRoom.ItemHeight = 23
        Me.cbRoom.Location = New System.Drawing.Point(15, 65)
        Me.cbRoom.Name = "cbRoom"
        Me.cbRoom.Size = New System.Drawing.Size(220, 29)
        Me.cbRoom.TabIndex = 4
        Me.cbRoom.UseSelectable = True
        '
        'cbCust
        '
        Me.cbCust.FormattingEnabled = True
        Me.cbCust.ItemHeight = 23
        Me.cbCust.Location = New System.Drawing.Point(15, 137)
        Me.cbCust.Name = "cbCust"
        Me.cbCust.Size = New System.Drawing.Size(220, 29)
        Me.cbCust.TabIndex = 5
        Me.cbCust.UseSelectable = True
        '
        'dtCheckIn
        '
        Me.dtCheckIn.FontSize = ReaLTaiizor.Extension.Poison.PoisonDateTimeSize.Medium
        Me.dtCheckIn.Location = New System.Drawing.Point(15, 330)
        Me.dtCheckIn.MinimumSize = New System.Drawing.Size(0, 29)
        Me.dtCheckIn.Name = "dtCheckIn"
        Me.dtCheckIn.Size = New System.Drawing.Size(220, 29)
        Me.dtCheckIn.TabIndex = 6
        '
        'dtCheckOut
        '
        Me.dtCheckOut.FontSize = ReaLTaiizor.Extension.Poison.PoisonDateTimeSize.Medium
        Me.dtCheckOut.Location = New System.Drawing.Point(15, 394)
        Me.dtCheckOut.MinimumSize = New System.Drawing.Size(0, 29)
        Me.dtCheckOut.Name = "dtCheckOut"
        Me.dtCheckOut.Size = New System.Drawing.Size(220, 29)
        Me.dtCheckOut.TabIndex = 7
        '
        'btnCalculate
        '
        Me.btnCalculate.Highlight = True
        Me.btnCalculate.Location = New System.Drawing.Point(15, 521)
        Me.btnCalculate.Name = "btnCalculate"
        Me.btnCalculate.Size = New System.Drawing.Size(220, 35)
        Me.btnCalculate.TabIndex = 8
        Me.btnCalculate.Text = "💰 Calculate Total Fee"
        Me.btnCalculate.UseSelectable = True
        '
        'btnBook
        '
        Me.btnBook.Highlight = True
        Me.btnBook.Location = New System.Drawing.Point(15, 581)
        Me.btnBook.Name = "btnBook"
        Me.btnBook.Size = New System.Drawing.Size(220, 40)
        Me.btnBook.TabIndex = 8
        Me.btnBook.Text = "✓ Confirm Booking"
        Me.btnBook.UseSelectable = True
        '
        'btnBack
        '
        Me.btnBack.Location = New System.Drawing.Point(130, 540)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(105, 35)
        Me.btnBack.TabIndex = 8
        Me.btnBack.Text = "← Back"
        Me.btnBack.UseSelectable = True
        '
        'PoisonLabel1
        '
        Me.PoisonLabel1.AutoSize = True
        Me.PoisonLabel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.PoisonLabel1.Location = New System.Drawing.Point(340, 73)
        Me.PoisonLabel1.Name = "PoisonLabel1"
        Me.PoisonLabel1.Size = New System.Drawing.Size(150, 19)
        Me.PoisonLabel1.TabIndex = 9
        Me.PoisonLabel1.Text = "📅 New Room Booking"
        Me.PoisonLabel1.UseCustomForeColor = True
        '
        'PoisonPanel1
        '
        Me.PoisonPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PoisonPanel1.Controls.Add(Me.lblRoomInfo)
        Me.PoisonPanel1.Controls.Add(Me.PoisonLabel8)
        Me.PoisonPanel1.Controls.Add(Me.lblDays)
        Me.PoisonPanel1.Controls.Add(Me.PoisonLabel6)
        Me.PoisonPanel1.Controls.Add(Me.lblGuestPhone)
        Me.PoisonPanel1.Controls.Add(Me.PoisonLabel4)
        Me.PoisonPanel1.Controls.Add(Me.lblRoom)
        Me.PoisonPanel1.Controls.Add(Me.cbRoom)
        Me.PoisonPanel1.Controls.Add(Me.btnBook)
        Me.PoisonPanel1.Controls.Add(Me.PoisonLabel3)
        Me.PoisonPanel1.Controls.Add(Me.btnCalculate)
        Me.PoisonPanel1.Controls.Add(Me.cbCust)
        Me.PoisonPanel1.Controls.Add(Me.dtCheckOut)
        Me.PoisonPanel1.Controls.Add(Me.lblGuestName)
        Me.PoisonPanel1.Controls.Add(Me.dtCheckIn)
        Me.PoisonPanel1.Controls.Add(Me.lblDate)
        Me.PoisonPanel1.Controls.Add(Me.lblFee)
        Me.PoisonPanel1.Controls.Add(Me.lblReturn)
        Me.PoisonPanel1.HorizontalScrollbarBarColor = True
        Me.PoisonPanel1.HorizontalScrollbarHighlightOnWheel = False
        Me.PoisonPanel1.HorizontalScrollbarSize = 10
        Me.PoisonPanel1.Location = New System.Drawing.Point(23, 110)
        Me.PoisonPanel1.Name = "PoisonPanel1"
        Me.PoisonPanel1.Size = New System.Drawing.Size(250, 642)
        Me.PoisonPanel1.TabIndex = 10
        Me.PoisonPanel1.VerticalScrollbarBarColor = True
        Me.PoisonPanel1.VerticalScrollbarHighlightOnWheel = False
        Me.PoisonPanel1.VerticalScrollbarSize = 10
        '
        'lblRoomInfo
        '
        Me.lblRoomInfo.AutoSize = True
        Me.lblRoomInfo.Location = New System.Drawing.Point(17, 227)
        Me.lblRoomInfo.Name = "lblRoomInfo"
        Me.lblRoomInfo.Size = New System.Drawing.Size(105, 19)
        Me.lblRoomInfo.TabIndex = 14
        Me.lblRoomInfo.Text = "Select room first"
        '
        'PoisonLabel8
        '
        Me.PoisonLabel8.AutoSize = True
        Me.PoisonLabel8.Location = New System.Drawing.Point(17, 445)
        Me.PoisonLabel8.Name = "PoisonLabel8"
        Me.PoisonLabel8.Size = New System.Drawing.Size(61, 19)
        Me.PoisonLabel8.TabIndex = 13
        Me.PoisonLabel8.Text = "Total Fee"
        '
        'lblDays
        '
        Me.lblDays.AutoSize = True
        Me.lblDays.Location = New System.Drawing.Point(83, 483)
        Me.lblDays.Name = "lblDays"
        Me.lblDays.Size = New System.Drawing.Size(46, 19)
        Me.lblDays.TabIndex = 12
        Me.lblDays.Text = "0 days"
        '
        'PoisonLabel6
        '
        Me.PoisonLabel6.AutoSize = True
        Me.PoisonLabel6.Location = New System.Drawing.Point(106, 445)
        Me.PoisonLabel6.Name = "PoisonLabel6"
        Me.PoisonLabel6.Size = New System.Drawing.Size(103, 19)
        Me.PoisonLabel6.TabIndex = 11
        Me.PoisonLabel6.Text = "Duration of Stay"
        '
        'lblGuestPhone
        '
        Me.lblGuestPhone.AutoSize = True
        Me.lblGuestPhone.Location = New System.Drawing.Point(15, 198)
        Me.lblGuestPhone.Name = "lblGuestPhone"
        Me.lblGuestPhone.Size = New System.Drawing.Size(46, 19)
        Me.lblGuestPhone.TabIndex = 10
        Me.lblGuestPhone.Text = "Phone"
        '
        'PoisonLabel4
        '
        Me.PoisonLabel4.AutoSize = True
        Me.PoisonLabel4.Location = New System.Drawing.Point(15, 179)
        Me.PoisonLabel4.Name = "PoisonLabel4"
        Me.PoisonLabel4.Size = New System.Drawing.Size(83, 19)
        Me.PoisonLabel4.TabIndex = 9
        Me.PoisonLabel4.Text = "Guest Details"
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(441, 773)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(105, 35)
        Me.btnClear.TabIndex = 11
        Me.btnClear.Text = "🔄 Clear"
        Me.btnClear.UseSelectable = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(291, 773)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(105, 35)
        Me.btnRefresh.TabIndex = 12
        Me.btnRefresh.Text = "↻ Refresh"
        Me.btnRefresh.UseSelectable = True
        '
        'PoisonPanel2
        '
        Me.PoisonPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PoisonPanel2.Controls.Add(Me.dgvBookings)
        Me.PoisonPanel2.HorizontalScrollbarBarColor = True
        Me.PoisonPanel2.HorizontalScrollbarHighlightOnWheel = False
        Me.PoisonPanel2.HorizontalScrollbarSize = 10
        Me.PoisonPanel2.Location = New System.Drawing.Point(290, 110)
        Me.PoisonPanel2.Name = "PoisonPanel2"
        Me.PoisonPanel2.Size = New System.Drawing.Size(672, 482)
        Me.PoisonPanel2.TabIndex = 13
        Me.PoisonPanel2.VerticalScrollbarBarColor = True
        Me.PoisonPanel2.VerticalScrollbarHighlightOnWheel = False
        Me.PoisonPanel2.VerticalScrollbarSize = 10
        '
        'PoisonLabel2
        '
        Me.PoisonLabel2.AutoSize = True
        Me.PoisonLabel2.Location = New System.Drawing.Point(808, 73)
        Me.PoisonLabel2.Name = "PoisonLabel2"
        Me.PoisonLabel2.Size = New System.Drawing.Size(125, 19)
        Me.PoisonLabel2.TabIndex = 14
        Me.PoisonLabel2.Text = "📋 Booking History"
        '
        'PoisonTile1
        '
        Me.PoisonTile1.ActiveControl = Nothing
        Me.PoisonTile1.Location = New System.Drawing.Point(23, 758)
        Me.PoisonTile1.Name = "PoisonTile1"
        Me.PoisonTile1.Size = New System.Drawing.Size(250, 50)
        Me.PoisonTile1.TabIndex = 15
        Me.PoisonTile1.Text = "Total Bookings: 0"
        Me.PoisonTile1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.PoisonTile1.UseSelectable = True
        '
        'txtSearch
        '
        '
        '
        '
        Me.txtSearch.CustomButton.Image = Nothing
        Me.txtSearch.CustomButton.Location = New System.Drawing.Point(196, 1)
        Me.txtSearch.CustomButton.Name = ""
        Me.txtSearch.CustomButton.Size = New System.Drawing.Size(21, 21)
        Me.txtSearch.CustomButton.Style = ReaLTaiizor.[Enum].Poison.ColorStyle.Red
        Me.txtSearch.CustomButton.TabIndex = 1
        Me.txtSearch.CustomButton.Theme = ReaLTaiizor.[Enum].Poison.ThemeStyle.Light
        Me.txtSearch.CustomButton.UseSelectable = True
        Me.txtSearch.CustomButton.Visible = False
        Me.txtSearch.Lines = New String(-1) {}
        Me.txtSearch.Location = New System.Drawing.Point(526, 69)
        Me.txtSearch.MaxLength = 32767
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtSearch.PromptText = "Search bookings..."
        Me.txtSearch.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSearch.SelectedText = ""
        Me.txtSearch.SelectionLength = 0
        Me.txtSearch.SelectionStart = 0
        Me.txtSearch.ShortcutsEnabled = True
        Me.txtSearch.Size = New System.Drawing.Size(218, 23)
        Me.txtSearch.TabIndex = 16
        Me.txtSearch.UseSelectable = True
        Me.txtSearch.WaterMark = "Search bookings..."
        Me.txtSearch.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.txtSearch.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'PoisonLabel5
        '
        Me.PoisonLabel5.AutoSize = True
        Me.PoisonLabel5.Location = New System.Drawing.Point(441, 73)
        Me.PoisonLabel5.Name = "PoisonLabel5"
        Me.PoisonLabel5.Size = New System.Drawing.Size(71, 19)
        Me.PoisonLabel5.TabIndex = 17
        Me.PoisonLabel5.Text = "🔍 Search"
        '
        'FrmBooking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1015, 831)
        Me.Controls.Add(Me.PoisonLabel5)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.PoisonTile1)
        Me.Controls.Add(Me.PoisonLabel2)
        Me.Controls.Add(Me.PoisonPanel2)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.PoisonPanel1)
        Me.Controls.Add(Me.PoisonLabel1)
        Me.Controls.Add(Me.btnBack)
        Me.Name = "FrmBooking"
        Me.Resizable = False
        Me.ShadowType = ReaLTaiizor.[Enum].Poison.FormShadowType.AeroShadow
        Me.Text = "Room Booking - Hotel Management"
        CType(Me.PoisonStyleManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvBookings, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PoisonPanel1.ResumeLayout(False)
        Me.PoisonPanel1.PerformLayout()
        Me.PoisonPanel2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PoisonStyleManager1 As ReaLTaiizor.Manager.PoisonStyleManager
    Friend WithEvents dgvBookings As ReaLTaiizor.Controls.PoisonDataGridView
    Friend WithEvents lblReturn As ReaLTaiizor.Controls.PoisonLabel
    Friend WithEvents lblGuestName As ReaLTaiizor.Controls.PoisonLabel
    Friend WithEvents lblFee As ReaLTaiizor.Controls.PoisonLabel
    Friend WithEvents PoisonLabel3 As ReaLTaiizor.Controls.PoisonLabel
    Friend WithEvents lblDate As ReaLTaiizor.Controls.PoisonLabel
    Friend WithEvents lblRoom As ReaLTaiizor.Controls.PoisonLabel
    Friend WithEvents btnBack As ReaLTaiizor.Controls.PoisonButton
    Friend WithEvents btnBook As ReaLTaiizor.Controls.PoisonButton
    Friend WithEvents btnCalculate As ReaLTaiizor.Controls.PoisonButton
    Friend WithEvents dtCheckOut As ReaLTaiizor.Controls.PoisonDateTime
    Friend WithEvents dtCheckIn As ReaLTaiizor.Controls.PoisonDateTime
    Friend WithEvents cbCust As ReaLTaiizor.Controls.PoisonComboBox
    Friend WithEvents cbRoom As ReaLTaiizor.Controls.PoisonComboBox
    Friend WithEvents PoisonLabel1 As ReaLTaiizor.Controls.PoisonLabel
    Friend WithEvents PoisonPanel1 As ReaLTaiizor.Controls.PoisonPanel
    Friend WithEvents btnClear As ReaLTaiizor.Controls.PoisonButton
    Friend WithEvents btnRefresh As ReaLTaiizor.Controls.PoisonButton
    Friend WithEvents PoisonPanel2 As ReaLTaiizor.Controls.PoisonPanel
    Friend WithEvents PoisonLabel2 As ReaLTaiizor.Controls.PoisonLabel
    Friend WithEvents PoisonTile1 As ReaLTaiizor.Controls.PoisonTile
    Friend WithEvents txtSearch As ReaLTaiizor.Controls.PoisonTextBox
    Friend WithEvents PoisonLabel5 As ReaLTaiizor.Controls.PoisonLabel
    Friend WithEvents lblGuestPhone As ReaLTaiizor.Controls.PoisonLabel
    Friend WithEvents PoisonLabel4 As ReaLTaiizor.Controls.PoisonLabel
    Friend WithEvents lblDays As ReaLTaiizor.Controls.PoisonLabel
    Friend WithEvents PoisonLabel6 As ReaLTaiizor.Controls.PoisonLabel
    Friend WithEvents PoisonLabel8 As ReaLTaiizor.Controls.PoisonLabel
    Friend WithEvents lblRoomInfo As ReaLTaiizor.Controls.PoisonLabel
End Class