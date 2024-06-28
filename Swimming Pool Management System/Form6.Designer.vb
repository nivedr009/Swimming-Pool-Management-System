<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form6
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form6))
        Label1 = New Label()
        GroupBox1 = New GroupBox()
        TextBox3 = New TextBox()
        Label5 = New Label()
        RichTextBox1 = New RichTextBox()
        TextBox2 = New TextBox()
        TextBox1 = New TextBox()
        Label4 = New Label()
        Label3 = New Label()
        Label2 = New Label()
        Button1 = New Button()
        Button2 = New Button()
        Button3 = New Button()
        GroupBox1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe Script", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(281, 9)
        Label1.Name = "Label1"
        Label1.Size = New Size(239, 33)
        Label1.TabIndex = 0
        Label1.Text = "Facility Registration"
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(TextBox3)
        GroupBox1.Controls.Add(Label5)
        GroupBox1.Controls.Add(RichTextBox1)
        GroupBox1.Controls.Add(TextBox2)
        GroupBox1.Controls.Add(TextBox1)
        GroupBox1.Controls.Add(Label4)
        GroupBox1.Controls.Add(Label3)
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Location = New Point(206, 60)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(381, 343)
        GroupBox1.TabIndex = 1
        GroupBox1.TabStop = False
        ' 
        ' TextBox3
        ' 
        TextBox3.Location = New Point(128, 292)
        TextBox3.Name = "TextBox3"
        TextBox3.Size = New Size(225, 27)
        TextBox3.TabIndex = 10
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(23, 298)
        Label5.Name = "Label5"
        Label5.Size = New Size(98, 20)
        Label5.TabIndex = 9
        Label5.Text = "Max Capacity"
        ' 
        ' RichTextBox1
        ' 
        RichTextBox1.Location = New Point(128, 131)
        RichTextBox1.Name = "RichTextBox1"
        RichTextBox1.Size = New Size(225, 141)
        RichTextBox1.TabIndex = 8
        RichTextBox1.Text = ""
        ' 
        ' TextBox2
        ' 
        TextBox2.Location = New Point(128, 77)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(225, 27)
        TextBox2.TabIndex = 7
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(128, 26)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(225, 27)
        TextBox1.TabIndex = 4
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(23, 135)
        Label4.Name = "Label4"
        Label4.Size = New Size(85, 20)
        Label4.TabIndex = 6
        Label4.Text = "Description"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(23, 82)
        Label3.Name = "Label3"
        Label3.Size = New Size(98, 20)
        Label3.TabIndex = 5
        Label3.Text = "Facility Name"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(23, 31)
        Label2.Name = "Label2"
        Label2.Size = New Size(73, 20)
        Label2.TabIndex = 4
        Label2.Text = "Facility ID"
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(206, 409)
        Button1.Name = "Button1"
        Button1.Size = New Size(94, 29)
        Button1.TabIndex = 0
        Button1.Text = "Save"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(502, 409)
        Button2.Name = "Button2"
        Button2.Size = New Size(94, 29)
        Button2.TabIndex = 2
        Button2.Text = "Go back"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(353, 409)
        Button3.Name = "Button3"
        Button3.Size = New Size(94, 29)
        Button3.TabIndex = 3
        Button3.Text = "Reset"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Form6
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), Image)
        ClientSize = New Size(800, 450)
        Controls.Add(Button3)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Controls.Add(GroupBox1)
        Controls.Add(Label1)
        Name = "Form6"
        Text = "Form6"
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Button3 As Button
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Label5 As Label
End Class
