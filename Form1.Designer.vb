<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.lvMoves = New System.Windows.Forms.ListView()
        Me.chCoup = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chWhite = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chBlack = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(12, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(444, 447)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'lvMoves
        '
        Me.lvMoves.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chCoup, Me.chWhite, Me.chBlack})
        Me.lvMoves.FullRowSelect = True
        Me.lvMoves.GridLines = True
        Me.lvMoves.HideSelection = False
        Me.lvMoves.Location = New System.Drawing.Point(488, 56)
        Me.lvMoves.MultiSelect = False
        Me.lvMoves.Name = "lvMoves"
        Me.lvMoves.Size = New System.Drawing.Size(214, 332)
        Me.lvMoves.TabIndex = 11
        Me.lvMoves.UseCompatibleStateImageBehavior = False
        Me.lvMoves.View = System.Windows.Forms.View.Details
        '
        'chCoup
        '
        Me.chCoup.Text = "n°"
        '
        'chWhite
        '
        Me.chWhite.Text = "White"
        '
        'chBlack
        '
        Me.chBlack.Text = "Black"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(750, 132)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(131, 23)
        Me.Button2.TabIndex = 13
        Me.Button2.Text = "Rejoindre une partie"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(832, 247)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(0, 13)
        Me.Label1.TabIndex = 14
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(910, 530)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.lvMoves)
        Me.Controls.Add(Me.PictureBox1)
        Me.MinimumSize = New System.Drawing.Size(768, 480)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PGN Export"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lvMoves As System.Windows.Forms.ListView
    Friend WithEvents chCoup As System.Windows.Forms.ColumnHeader
    Friend WithEvents chWhite As System.Windows.Forms.ColumnHeader
    Friend WithEvents chBlack As System.Windows.Forms.ColumnHeader
    Friend WithEvents Button2 As Button
    Friend WithEvents Label1 As Label
End Class
