<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmRequests
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.PoisonStyleManager1 = New ReaLTaiizor.Manager.PoisonStyleManager(Me.components)
        Me.dgvReturnRequests = New ReaLTaiizor.Controls.PoisonDataGridView()
        Me.PoisonLabel1 = New ReaLTaiizor.Controls.PoisonLabel()
        Me.PoisonLabel2 = New ReaLTaiizor.Controls.PoisonLabel()
        Me.btnBack = New ReaLTaiizor.Controls.PoisonButton()
        Me.btnConfirmReturn = New ReaLTaiizor.Controls.PoisonButton()
        Me.btnApproveRent = New ReaLTaiizor.Controls.PoisonButton()
        Me.dgvRentRequests = New ReaLTaiizor.Controls.PoisonDataGridView()
        Me.PoisonLabel3 = New ReaLTaiizor.Controls.PoisonLabel()
        Me.btnRefreshRent = New ReaLTaiizor.Controls.PoisonButton()
        Me.btnRefreshReturn = New ReaLTaiizor.Controls.PoisonButton()
        Me.PoisonPanel1 = New ReaLTaiizor.Controls.PoisonPanel()
        Me.PoisonPanel2 = New ReaLTaiizor.Controls.PoisonPanel()
        Me.PoisonTile1 = New ReaLTaiizor.Controls.PoisonTile()
        Me.PoisonTile2 = New ReaLTaiizor.Controls.PoisonTile()
        Me.btnRejectRent = New ReaLTaiizor.Controls.PoisonButton()
        Me.btnViewDetails = New ReaLTaiizor.Controls.PoisonButton()
        CType(Me.PoisonStyleManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvReturnRequests, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvRentRequests, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PoisonPanel1.SuspendLayout()
        Me.PoisonPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'PoisonStyleManager1
        '
        Me.PoisonStyleManager1.Owner = Me
        '
        'dgvReturnRequests
        '
        Me.dgvReturnRequests.AllowUserToAddRows = False
        Me.dgvReturnRequests.AllowUserToDeleteRows = False
        Me.dgvReturnRequests.AllowUserToResizeRows = False
        Me.dgvReturnRequests.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvReturnRequests.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvReturnRequests.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.dgvReturnRequests.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvReturnRequests.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvReturnRequests.ColumnHeadersHeight = 35
        Me.dgvReturnRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(136, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(136, Byte), Integer))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvReturnRequests.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvReturnRequests.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvReturnRequests.EnableHeadersVisualStyles = False
        Me.dgvReturnRequests.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.dgvReturnRequests.GridColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvReturnRequests.Location = New System.Drawing.Point(0, 0)
        Me.dgvReturnRequests.MultiSelect = False
        Me.dgvReturnRequests.Name = "dgvReturnRequests"
        Me.dgvReturnRequests.ReadOnly = True
        Me.dgvReturnRequests.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvReturnRequests.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvReturnRequests.RowHeadersVisible = False
        Me.dgvReturnRequests.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvReturnRequests.RowTemplate.Height = 30
        Me.dgvReturnRequests.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvReturnRequests.Size = New System.Drawing.Size(1050, 210)
        Me.dgvReturnRequests.Style = ReaLTaiizor.[Enum].Poison.ColorStyle.Blue
        Me.dgvReturnRequests.TabIndex = 1
        '
        'PoisonLabel1
        '
        Me.PoisonLabel1.AutoSize = True
        Me.PoisonLabel1.Location = New System.Drawing.Point(23, 115)
        Me.PoisonLabel1.Name = "PoisonLabel1"
        Me.PoisonLabel1.Size = New System.Drawing.Size(190, 19)
        Me.PoisonLabel1.TabIndex = 2
        Me.PoisonLabel1.Text = "📝 Pending Booking Requests"
        '
        'PoisonLabel2
        '
        Me.PoisonLabel2.AutoSize = True
        Me.PoisonLabel2.Location = New System.Drawing.Point(23, 385)
        Me.PoisonLabel2.Name = "PoisonLabel2"
        Me.PoisonLabel2.Size = New System.Drawing.Size(178, 19)
        Me.PoisonLabel2.TabIndex = 3
        Me.PoisonLabel2.Text = "🔄 Pending Return Requests"
        '
        'btnBack
        '
        Me.btnBack.Location = New System.Drawing.Point(965, 615)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(105, 35)
        Me.btnBack.TabIndex = 4
        Me.btnBack.Text = "← Back"
        Me.btnBack.UseSelectable = True
        '
        'btnConfirmReturn
        '
        Me.btnConfirmReturn.Highlight = True
        Me.btnConfirmReturn.Location = New System.Drawing.Point(23, 615)
        Me.btnConfirmReturn.Name = "btnConfirmReturn"
        Me.btnConfirmReturn.Size = New System.Drawing.Size(135, 35)
        Me.btnConfirmReturn.TabIndex = 5
        Me.btnConfirmReturn.Text = "✓ Confirm Return"
        Me.btnConfirmReturn.UseSelectable = True
        '
        'btnApproveRent
        '
        Me.btnApproveRent.Highlight = True
        Me.btnApproveRent.Location = New System.Drawing.Point(23, 340)
        Me.btnApproveRent.Name = "btnApproveRent"
        Me.btnApproveRent.Size = New System.Drawing.Size(135, 35)
        Me.btnApproveRent.TabIndex = 6
        Me.btnApproveRent.Text = "✓ Approve Booking"
        Me.btnApproveRent.UseSelectable = True
        '
        'dgvRentRequests
        '
        Me.dgvRentRequests.AllowUserToAddRows = False
        Me.dgvRentRequests.AllowUserToDeleteRows = False
        Me.dgvRentRequests.AllowUserToResizeRows = False
        Me.dgvRentRequests.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvRentRequests.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvRentRequests.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.dgvRentRequests.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvRentRequests.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvRentRequests.ColumnHeadersHeight = 35
        Me.dgvRentRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(136, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(136, Byte), Integer))
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvRentRequests.DefaultCellStyle = DataGridViewCellStyle5
        Me.dgvRentRequests.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvRentRequests.EnableHeadersVisualStyles = False
        Me.dgvRentRequests.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.dgvRentRequests.GridColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvRentRequests.Location = New System.Drawing.Point(0, 0)
        Me.dgvRentRequests.MultiSelect = False
        Me.dgvRentRequests.Name = "dgvRentRequests"
        Me.dgvRentRequests.ReadOnly = True
        Me.dgvRentRequests.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvRentRequests.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.dgvRentRequests.RowHeadersVisible = False
        Me.dgvRentRequests.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvRentRequests.RowTemplate.Height = 30
        Me.dgvRentRequests.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvRentRequests.Size = New System.Drawing.Size(1050, 210)
        Me.dgvRentRequests.Style = ReaLTaiizor.[Enum].Poison.ColorStyle.Blue
        Me.dgvRentRequests.TabIndex = 7
        '
        'PoisonLabel3
        '
        Me.PoisonLabel3.AutoSize = True
        Me.PoisonLabel3.Location = New System.Drawing.Point(380, 73)
        Me.PoisonLabel3.Name = "PoisonLabel3"
        Me.PoisonLabel3.Size = New System.Drawing.Size(336, 19)
        Me.PoisonLabel3.TabIndex = 8
        Me.PoisonLabel3.Text = "🏨 Booking && Return Request Management"
        Me.PoisonLabel3.UseCustomForeColor = True
        Me.PoisonLabel3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        '
        'btnRefreshRent
        '
        Me.btnRefreshRent.Location = New System.Drawing.Point(304, 340)
        Me.btnRefreshRent.Name = "btnRefreshRent"
        Me.btnRefreshRent.Size = New System.Drawing.Size(105, 35)
        Me.btnRefreshRent.TabIndex = 9
        Me.btnRefreshRent.Text = "↻ Refresh"
        Me.btnRefreshRent.UseSelectable = True
        '
        'btnRefreshReturn
        '
        Me.btnRefreshReturn.Location = New System.Drawing.Point(304, 615)
        Me.btnRefreshReturn.Name = "btnRefreshReturn"
        Me.btnRefreshReturn.Size = New System.Drawing.Size(105, 35)
        Me.btnRefreshReturn.TabIndex = 10
        Me.btnRefreshReturn.Text = "↻ Refresh"
        Me.btnRefreshReturn.UseSelectable = True
        '
        'PoisonPanel1
        '
        Me.PoisonPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PoisonPanel1.Controls.Add(Me.dgvRentRequests)
        Me.PoisonPanel1.HorizontalScrollbarBarColor = True
        Me.PoisonPanel1.HorizontalScrollbarHighlightOnWheel = False
        Me.PoisonPanel1.HorizontalScrollbarSize = 10
        Me.PoisonPanel1.Location = New System.Drawing.Point(23, 140)
        Me.PoisonPanel1.Name = "PoisonPanel1"
        Me.PoisonPanel1.Size = New System.Drawing.Size(1052, 212)
        Me.PoisonPanel1.TabIndex = 11
        Me.PoisonPanel1.VerticalScrollbarBarColor = True
        Me.PoisonPanel1.VerticalScrollbarHighlightOnWheel = False
        Me.PoisonPanel1.VerticalScrollbarSize = 10
        '
        'PoisonPanel2
        '
        Me.PoisonPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PoisonPanel2.Controls.Add(Me.dgvReturnRequests)
        Me.PoisonPanel2.HorizontalScrollbarBarColor = True
        Me.PoisonPanel2.HorizontalScrollbarHighlightOnWheel = False
        Me.PoisonPanel2.HorizontalScrollbarSize = 10
        Me.PoisonPanel2.Location = New System.Drawing.Point(23, 410)
        Me.PoisonPanel2.Name = "PoisonPanel2"
        Me.PoisonPanel2.Size = New System.Drawing.Size(1052, 212)
        Me.PoisonPanel2.TabIndex = 12
        Me.PoisonPanel2.VerticalScrollbarBarColor = True
        Me.PoisonPanel2.VerticalScrollbarHighlightOnWheel = False
        Me.PoisonPanel2.VerticalScrollbarSize = 10
        '
        'PoisonTile1
        '
        Me.PoisonTile1.ActiveControl = Nothing
        Me.PoisonTile1.Location = New System.Drawing.Point(23, 656)
        Me.PoisonTile1.Name = "PoisonTile1"
        Me.PoisonTile1.Size = New System.Drawing.Size(200, 50)
        Me.PoisonTile1.TabIndex = 13
        Me.PoisonTile1.Text = "Pending Bookings: 0"
        Me.PoisonTile1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.PoisonTile1.UseSelectable = True
        '
        'PoisonTile2
        '
        Me.PoisonTile2.ActiveControl = Nothing
        Me.PoisonTile2.Location = New System.Drawing.Point(240, 656)
        Me.PoisonTile2.Name = "PoisonTile2"
        Me.PoisonTile2.Size = New System.Drawing.Size(200, 50)
        Me.PoisonTile2.TabIndex = 14
        Me.PoisonTile2.Text = "Pending Returns: 0"
        Me.PoisonTile2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.PoisonTile2.UseSelectable = True
        '
        'btnRejectRent
        '
        Me.btnRejectRent.Location = New System.Drawing.Point(164, 340)
        Me.btnRejectRent.Name = "btnRejectRent"
        Me.btnRejectRent.Size = New System.Drawing.Size(135, 35)
        Me.btnRejectRent.TabIndex = 15
        Me.btnRejectRent.Text = "✖ Reject Booking"
        Me.btnRejectRent.UseSelectable = True
        '
        'btnViewDetails
        '
        Me.btnViewDetails.Location = New System.Drawing.Point(164, 615)
        Me.btnViewDetails.Name = "btnViewDetails"
        Me.btnViewDetails.Size = New System.Drawing.Size(135, 35)
        Me.btnViewDetails.TabIndex = 16
        Me.btnViewDetails.Text = "👁 View Details"
        Me.btnViewDetails.UseSelectable = True
        '
        'FrmRequests
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1100, 730)
        Me.Controls.Add(Me.btnViewDetails)
        Me.Controls.Add(Me.btnRejectRent)
        Me.Controls.Add(Me.PoisonTile2)
        Me.Controls.Add(Me.PoisonTile1)
        Me.Controls.Add(Me.PoisonPanel2)
        Me.Controls.Add(Me.PoisonPanel1)
        Me.Controls.Add(Me.btnRefreshReturn)
        Me.Controls.Add(Me.btnRefreshRent)
        Me.Controls.Add(Me.PoisonLabel3)
        Me.Controls.Add(Me.btnApproveRent)
        Me.Controls.Add(Me.btnConfirmReturn)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.PoisonLabel2)
        Me.Controls.Add(Me.PoisonLabel1)
        Me.Name = "FrmRequests"
        Me.Padding = New System.Windows.Forms.Padding(20, 60, 20, 20)
        Me.Resizable = False
        Me.ShadowType = ReaLTaiizor.[Enum].Poison.FormShadowType.AeroShadow
        Me.Text = "Booking && Return Requests - Hotel Management"
        CType(Me.PoisonStyleManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvReturnRequests, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvRentRequests, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PoisonPanel1.ResumeLayout(False)
        Me.PoisonPanel2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PoisonStyleManager1 As ReaLTaiizor.Manager.PoisonStyleManager
    Friend WithEvents dgvReturnRequests As ReaLTaiizor.Controls.PoisonDataGridView
    Friend WithEvents PoisonLabel1 As ReaLTaiizor.Controls.PoisonLabel
    Friend WithEvents PoisonLabel2 As ReaLTaiizor.Controls.PoisonLabel
    Friend WithEvents btnBack As ReaLTaiizor.Controls.PoisonButton
    Friend WithEvents btnConfirmReturn As ReaLTaiizor.Controls.PoisonButton
    Friend WithEvents btnApproveRent As ReaLTaiizor.Controls.PoisonButton
    Friend WithEvents dgvRentRequests As ReaLTaiizor.Controls.PoisonDataGridView
    Friend WithEvents PoisonLabel3 As ReaLTaiizor.Controls.PoisonLabel
    Friend WithEvents btnRefreshRent As ReaLTaiizor.Controls.PoisonButton
    Friend WithEvents btnRefreshReturn As ReaLTaiizor.Controls.PoisonButton
    Friend WithEvents PoisonPanel1 As ReaLTaiizor.Controls.PoisonPanel
    Friend WithEvents PoisonPanel2 As ReaLTaiizor.Controls.PoisonPanel
    Friend WithEvents PoisonTile1 As ReaLTaiizor.Controls.PoisonTile
    Friend WithEvents PoisonTile2 As ReaLTaiizor.Controls.PoisonTile
    Friend WithEvents btnRejectRent As ReaLTaiizor.Controls.PoisonButton
    Friend WithEvents btnViewDetails As ReaLTaiizor.Controls.PoisonButton
End Class