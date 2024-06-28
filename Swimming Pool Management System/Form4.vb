﻿Imports System.Text.RegularExpressions
Imports System.Data
Imports System.Data.SqlClient

Public Class Form4


    ' SQL connection string
    Dim connectionString As String = "Data Source=DESKTOP-O16HO1G\SQLEXPRESS;Initial Catalog='Swimming Pool Management System';Integrated Security=True;"

    Private Function GenerateNextCustomerIDFromDatabase() As String
        Dim nextCustomerID As String = "C01" ' Default value

        Try
            ' SQL query to get the last customer ID from the database
            Dim queryLastCustomerID As String = "SELECT TOP 1 Cust_ID FROM Customer_table ORDER BY CAST(SUBSTRING(Cust_ID, 2, LEN(Cust_ID)) AS INT) DESC"

            Using connection As New SqlConnection(connectionString)
                Using command As New SqlCommand(queryLastCustomerID, connection)
                    connection.Open()
                    Dim lastCustomerID As String = Convert.ToString(command.ExecuteScalar())

                    ' If there are records in the database, generate the next customer ID
                    If Not String.IsNullOrEmpty(lastCustomerID) Then
                        ' Extract the numeric part of the last customer ID
                        Dim numericPart As String = lastCustomerID.Substring(1)
                        ' Convert the numeric part to integer and increment by 1
                        Dim nextNumericPart As Integer = Convert.ToInt32(numericPart) + 1
                        ' Generate the next customer ID by combining the prefix ("C") and the incremented numeric part
                        nextCustomerID = "C" & nextNumericPart.ToString("D2")
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error generating next customer ID: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return nextCustomerID
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

        Dim regexCustID As New Regex("^C\d+$")




        ' Customer ID Presence Check
        If String.IsNullOrWhiteSpace(TextBox1.Text) Then
            MessageBox.Show("Customer ID cannot be blank", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox1.Focus()
            Return False
        ElseIf Not regexCustID.IsMatch(TextBox1.Text) Then
            MessageBox.Show("Invalid Customer ID", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' Clear all textboxes
        TextBox1.Text = "C"
        TextBox2.Text = ""
        TextBox3.Text = "+91"
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Close the current form (Form4) and open the first form (Form3) again
        Dim form3 As New Form3()
        form3.Show()
        Me.Close()
    End Sub

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Customer ID to start with "C"
        TextBox1.Text = "C"

        ' Set initial text of phone number textbox to "+91"
        TextBox3.Text = "+91"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Perform frontend validations
        If Not ValidateInputs() Then
            Return
        End If

        ' Check if Customer_ID already exists
        Dim customerIdExists As Boolean = False
        Dim queryCustomerIdExists As String = "SELECT COUNT(*) FROM Customer_table WHERE Cust_ID = @customerId"

        Try
            Using connection As New SqlConnection(connectionString)
                Using commandCheckCustomerId As New SqlCommand(queryCustomerIdExists, connection)
                    connection.Open()
                    commandCheckCustomerId.Parameters.AddWithValue("@customerId", TextBox1.Text)
                    Dim count As Integer = Convert.ToInt32(commandCheckCustomerId.ExecuteScalar())
                    customerIdExists = (count > 0)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error checking Customer ID: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        If customerIdExists Then
            MessageBox.Show("Customer ID is already taken.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Insert data into the Customer_table
        Dim query As String = "INSERT INTO [dbo].[Customer_table] ([Cust_ID], [name], [phn_no]) " &
                              "VALUES (@Cust_ID, @name, @phn_no)"

        Try
            Using connection As New SqlConnection(connectionString)
                Using command As New SqlCommand(query, connection)
                    ' Add parameters
                    command.Parameters.AddWithValue("@Cust_ID", TextBox1.Text)
                    command.Parameters.AddWithValue("@name", TextBox2.Text)
                    command.Parameters.AddWithValue("@phn_no", TextBox3.Text)

                    connection.Open()
                    command.ExecuteNonQuery()

                    MessageBox.Show("Customer Registered Successfully", "Confirmation", MessageBoxButtons.OK)
                    ClearInputs()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error inserting data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub ClearInputs()
        ' Clear all textboxes
        TextBox1.Text = "C"
        TextBox2.Text = ""
        TextBox3.Text = "+91"
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

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        ' Generate the next customer ID and display it in TextBox1
        TextBox1.Text = GenerateNextCustomerIDFromDatabase()

        ' Disable TextBox1 to prevent editing
        TextBox1.Enabled = False

    End Sub
End Class