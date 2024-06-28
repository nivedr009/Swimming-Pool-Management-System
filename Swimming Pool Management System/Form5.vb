Imports System.Text.RegularExpressions
Imports System.Data
Imports System.Data.SqlClient

Public Class Form5

    ' SQL connection string
    Dim connectionString As String = "Data Source=DESKTOP-O16HO1G\SQLEXPRESS;Initial Catalog='Swimming Pool Management System';Integrated Security=True;"

    Private Function GenerateNextStaffIDFromDatabase() As String
        Dim nextStaffID As String = "S01" ' Default value

        Try
            ' SQL query to get the last staff ID from the database
            Dim queryLastStaffID As String = "SELECT TOP 1 Staff_ID FROM Staff_table ORDER BY CAST(SUBSTRING(Staff_ID, 2, LEN(Staff_ID)) AS INT) DESC"

            Using connection As New SqlConnection(connectionString)
                Using command As New SqlCommand(queryLastStaffID, connection)
                    connection.Open()
                    Dim lastStaffID As String = Convert.ToString(command.ExecuteScalar())

                    ' If there are records in the database, generate the next staff ID
                    If Not String.IsNullOrEmpty(lastStaffID) Then
                        ' Extract the numeric part of the last staff ID
                        Dim numericPart As String = lastStaffID.Substring(1)
                        ' Convert the numeric part to an integer and increment by 1
                        Dim nextNumericPart As Integer = Convert.ToInt32(numericPart) + 1
                        ' Generate the next staff ID by combining the prefix ("S") and the incremented numeric part
                        nextStaffID = "S" & nextNumericPart.ToString("D2")
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error generating next staff ID: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return nextStaffID
    End Function




    'Function to validate whether the phone number is a repeating number upto 6 digits
    Private Function ContainsRepeatingSequence(phoneNumber As String) As Boolean
        ' Remove the country code
        Dim numberWithoutCode As String = phoneNumber.Substring(3)

        ' Check if the remaining digits contain repeating sequences
        For i As Integer = 0 To numberWithoutCode.Length - 6 ' Start from the beginning and stop 6 digits before the end
            Dim currentDigit As Char = numberWithoutCode(i)
            Dim sequenceLength As Integer = 1

            ' Check subsequent digits for repetition
            For j As Integer = i + 1 To i + 5 ' Check the next 5 digits after the current digit
                If numberWithoutCode(j) = currentDigit Then
                    sequenceLength += 1
                    If sequenceLength = 6 Then ' A sequence of 6 repeating digits found
                        Return True
                    End If
                Else
                    Exit For ' Exit the inner loop if the sequence breaks
                End If
            Next j
        Next i

        ' No repeating sequences found
        Return False
    End Function



    ' Check if any character, number, or special character is repeated more than once
    Private Function ContainsRepeatedCharacters(input As String) As Boolean
        Dim count As Integer = 1
        For i As Integer = 1 To input.Length - 1
            If input(i) = input(i - 1) Then
                count += 1
                If count > 2 Then
                    Return True
                End If
            Else
                count = 1
            End If
        Next
        Return False
    End Function


    ' Frontend validations
    Private Function ValidateInputs() As Boolean

        Dim regexStaffID As New Regex("^S\d+$")

        ' Staff ID Presence Check
        If String.IsNullOrWhiteSpace(TextBox1.Text) Then
            MessageBox.Show("Staff ID cannot be blank", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox1.Focus()
            Return False
        ElseIf Not regexStaffID.IsMatch(TextBox1.Text) Then
            MessageBox.Show("Invalid Staff ID", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox1.Focus()
            Return False
        End If

        'Name Presence check
        If String.IsNullOrWhiteSpace(TextBox2.Text) Then
            MessageBox.Show("Name cannot be blank.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        ElseIf Not TextBox2.Text.All(Function(c) Char.IsLetter(c) Or c = " ") Then
            MessageBox.Show("Invalid Name", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox2.Focus()
            Return False
        ElseIf ContainsRepeatedCharacters(TextBox2.Text) Then
            MessageBox.Show("Name should not contain repeated characters", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox2.Focus()
            Return False
        End If

        ' Phone Number Presence check
        If String.IsNullOrWhiteSpace(TextBox3.Text) Then
            MessageBox.Show("Phone number cannot be blank", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox3.Focus()
            Return False
        ElseIf TextBox3.Text = "+91" Then
            MessageBox.Show("Please enter phone number ", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox3.Focus()
            Return False
        ElseIf Not TextBox3.Text.StartsWith("+91") OrElse TextBox3.Text.Length <> 13 OrElse Not TextBox3.Text.Substring(3).All(Function(c) Char.IsDigit(c)) OrElse TextBox3.Text.Substring(3, 1) Like "[0-6]" Then
            MessageBox.Show("Invalid Phone number", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox3.Focus()
            Return False
        ElseIf ContainsRepeatingSequence(TextBox3.Text) Then
            MessageBox.Show("Phone number contains a repeating sequence of more than 6 digits", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox3.Focus()
            Return False
        End If

        Return True

    End Function
    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Close the current form (Form4) and open the first form (Form3) again
        Dim form3 As New Form3()
        form3.Show()
        Me.Close()
    End Sub

    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Staff ID to start with "S"
        TextBox1.Text = "S"

        ' Set initial text of phone number textbox to "+91"
        TextBox3.Text = "+91"

        ' Add Designation to the ComboBox
        ComboBox1.Items.Add("Lifeguard")
        ComboBox1.Items.Add("Pool Manager")
        ComboBox1.Items.Add("Swimming Instructor")
        ComboBox1.Items.Add("Maintenance Staff")
        ComboBox1.Items.Add("Front Desk Attendant")

        ' Set a default selection if needed
        ComboBox1.SelectedIndex = 0

        'Disable manual entry into comboBox
        ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        If TextBox2.Text.Length > 0 Then
            ' Get the current selection start position
            Dim selectionStart As Integer = TextBox2.SelectionStart
            ' Capitalize the first letter
            TextBox2.Text = TextBox2.Text.Substring(0, 1).ToUpper() + TextBox2.Text.Substring(1)
            ' Restore the selection start position
            TextBox2.SelectionStart = selectionStart
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' Clear all textboxes
        TextBox1.Text = "S"
        TextBox2.Text = ""
        TextBox3.Text = "+91"

        ' Set the Designation ComboBox to default
        ComboBox1.SelectedIndex = 0
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Perform frontend validations
        If Not ValidateInputs() Then
            Return
        End If

        ' Check if Staff_ID already exists
        Dim staffIdExists As Boolean = False
        Dim queryStaffIdExists As String = "SELECT COUNT(*) FROM Staff_table WHERE Staff_ID = @staffId"

        Try
            Using connection As New SqlConnection(connectionString)
                Using commandCheckStaffId As New SqlCommand(queryStaffIdExists, connection)
                    connection.Open()
                    commandCheckStaffId.Parameters.AddWithValue("@staffId", TextBox1.Text)
                    Dim count As Integer = Convert.ToInt32(commandCheckStaffId.ExecuteScalar())
                    staffIdExists = (count > 0)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error checking Staff ID: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        If staffIdExists Then
            MessageBox.Show("Staff ID is already taken.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If


        ' Insert data into the Staff_table
        Dim query As String = "INSERT INTO [dbo].[Staff_table] ([Staff_ID], [name], [phn_no], [designation]) " &
                              "VALUES (@Staff_ID, @name, @phn_no, @designation)"

        Try
            Using connection As New SqlConnection(connectionString)
                Using command As New SqlCommand(query, connection)
                    ' Add parameters
                    command.Parameters.AddWithValue("@Staff_ID", TextBox1.Text)
                    command.Parameters.AddWithValue("@name", TextBox2.Text)
                    command.Parameters.AddWithValue("@phn_no", TextBox3.Text)
                    command.Parameters.AddWithValue("@designation", ComboBox1.SelectedItem.ToString())

                    connection.Open()
                    command.ExecuteNonQuery()

                    MessageBox.Show("Staff Registered Successfully", "Confirmation", MessageBoxButtons.OK)
                    ClearInputs()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error inserting data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub ClearInputs()
        ' Clear all textboxes
        TextBox1.Text = "S"
        TextBox2.Text = ""
        TextBox3.Text = "+91"

        ' Set the Designation ComboBox to default
        ComboBox1.SelectedIndex = 0
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        ' Generate the next staff ID and display it in TextBox1
        TextBox1.Text = GenerateNextStaffIDFromDatabase()

        ' Disable TextBox1 to prevent editing
        TextBox1.Enabled = False

    End Sub
End Class