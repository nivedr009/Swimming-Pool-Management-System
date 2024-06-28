<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form7
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form7))
        Label1 = New Label()
        GroupBox1 = New GroupBox()
        CheckBox1 = New CheckBox()
        Button2 = New Button()
        Button3 = New Button()
        Button4 = New Button()
        ComboBox1 = New ComboBox()
        Button1 = New Button()
        TextBox2 = New TextBox()
        TextBox1 = New TextBox()
        ListBox1 = New ListBox()
        Label6 = New Label()
        Label5 = New Label()
        DateTimePicker1 = New DateTimePicker()
        Label4 = New Label()
        Label3 = New Label()
        Label2 = New Label()
        GroupBox1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe Script", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(344, 39)
        Label1.Name = "Label1"
        Label1.Size = New Size(100, 33)
        Label1.TabIndex = 0
        Label1.Text = "Booking"
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(CheckBox1)
        GroupBox1.Controls.Add(Button2)
        GroupBox1.Controls.Add(Button3)
        GroupBox1.Controls.Add(Button4)
        GroupBox1.Controls.Add(ComboBox1)
        GroupBox1.Controls.Add(Button1)
        GroupBox1.Controls.Add(TextBox2)
        GroupBox1.Controls.Add(TextBox1)
        GroupBox1.Controls.Add(ListBox1)
        GroupBox1.Controls.Add(Label6)
        GroupBox1.Controls.Add(Label5)
        GroupBox1.Controls.Add(DateTimePicker1)
        GroupBox1.Controls.Add(Label4)
        GroupBox1.Controls.Add(Label3)
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Location = New Point(12, 83)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(776, 312)
        GroupBox1.TabIndex = 1
        GroupBox1.TabStop = False
        ' 
        ' CheckBox1
        ' 
        CheckBox1.AutoSize = True
        CheckBox1.Location = New Point(193, 215)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.Size = New Size(18, 17)
        CheckBox1.TabIndex = 2
        CheckBox1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(279, 261)
        Button2.Name = "Button2"
        Button2.Size = New Size(94, 29)
        Button2.TabIndex = 3
        Button2.Text = "Go back"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(141, 261)
        Button3.Name = "Button3"
        Button3.Size = New Size(94, 29)
        Button3.TabIndex = 4
        Button3.Text = "Clear"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Button4
        ' 
        Button4.Location = New Point(36, 208)
        Button4.Name = "Button4"
        Button4.Size = New Size(151, 29)
        Button4.TabIndex = 10
        Button4.Text = "Check Availability"
        Button4.UseVisualStyleBackColor = True
        ' 
        ' ComboBox1
        ' 
        ComboBox1.FormattingEnabled = True
        ComboBox1.Location = New Point(141, 115)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(232, 28)
        ComboBox1.TabIndex = 8
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(17, 261)
        Button1.Name = "Button1"
        Button1.Size = New Size(94, 29)
        Button1.TabIndex = 2
        Button1.Text = "Book"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' TextBox2
        ' 
        TextBox2.Location = New Point(141, 71)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(232, 27)
        TextBox2.TabIndex = 7
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(141, 31)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(232, 27)
        TextBox1.TabIndex = 6
        ' 
        ' ListBox1
        ' 
        ListBox1.FormattingEnabled = True
        ListBox1.Location = New Point(484, 26)
        ListBox1.Name = "ListBox1"
        ListBox1.Size = New Size(272, 264)
        ListBox1.TabIndex = 5
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(400, 26)
        Label6.Name = "Label6"
        Label6.Size = New Size(78, 20)
        Label6.TabIndex = 4
        Label6.Text = "Time Slots"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(17, 161)
        Label5.Name = "Label5"
        Label5.Size = New Size(118, 20)
        Label5.TabIndex = 3
        Label5.Text = "Date of Booking"
        ' 
        ' DateTimePicker1
        ' 
        DateTimePicker1.Location = New Point(141, 158)
        DateTimePicker1.Name = "DateTimePicker1"
        DateTimePicker1.Size = New Size(232, 27)
        DateTimePicker1.TabIndex = 2
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(17, 120)
        Label4.Name = "Label4"
        Label4.Size = New Size(73, 20)
        Label4.TabIndex = 2
        Label4.Text = "Facility ID"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(17, 75)
        Label3.Name = "Label3"
        Label3.Size = New Size(91, 20)
        Label3.TabIndex = 1
        Label3.Text = "Customer ID"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(17, 34)
        Label2.Name = "Label2"
        Label2.Size = New Size(83, 20)
        Label2.TabIndex = 0
        Label2.Text = "Booking ID"
        ' 
        ' Form7
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), Image)
        ClientSize = New Size(800, 450)
        Controls.Add(GroupBox1)
        Controls.Add(Label1)
        Name = "Form7"
        Text = "Form7"
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents CheckBox1 As CheckBox
End Class
