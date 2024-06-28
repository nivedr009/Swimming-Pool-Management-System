Imports System.Text.RegularExpressions
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Data
Imports System.Data.SqlClient

Public Class Form6

    ' SQL connection string
    Dim connectionString As String = "Data Source=DESKTOP-O16HO1G\SQLEXPRESS;Initial Catalog='Swimming Pool Management System';Integrated Security=True;"


    Private Function GenerateNextFacilityIDFromDatabase() As String
        Dim nextFacilityID As String = "F01" ' Default value

        Try
            ' SQL query to get the last facility ID from the database
            Dim queryLastFacilityID As String = "SELECT TOP 1 Facility_ID FROM Facility_table ORDER BY CAST(SUBSTRING(Facility_ID, 2, LEN(Facility_ID)) AS INT) DESC"

            Using connection As New SqlConnection(connectionString)
                Using command As New SqlCommand(queryLastFacilityID, connection)
                    connection.Open()
                    Dim lastFacilityID As String = Convert.ToString(command.ExecuteScalar())

                    ' If there are records in the database, generate the next facility ID
                    If Not String.IsNullOrEmpty(lastFacilityID) Then
                        ' Extract the numeric part of the last facility ID
                        Dim numericPart As String = lastFacilityID.Substring(1)
                        ' Convert the numeric part to an integer and increment by 1
                        Dim nextNumericPart As Integer = Convert.ToInt32(numericPart) + 1
                        ' Generate the next facility ID by combining the prefix ("F") and the incremented numeric part
                        nextFacilityID = "F" & nextNumericPart.ToString("D2")
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error generating next facility ID: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return nextFacilityID
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

    Private Function ValidateCapacityInput(ByVal input As String) As Boolean
        ' Maximum Capacity Presence Check
        If String.IsNullOrWhiteSpace(input) Then
            MessageBox.Show("Maximum capacity cannot be blank", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox3.Focus()
            Return False
        End If

        ' Maximum Capacity Number Only Check
        Dim capacity As Integer
        If Not Integer.TryParse(input, capacity) Then
            MessageBox.Show("Maximum capacity must be a valid number", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox3.Focus()
            Return False
        End If

        ' Maximum Capacity 0 Check
        If capacity = 0 Then
            MessageBox.Show("Maximum capacity cannot be zero", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        ' Maximum Capacity Number Starting with 0 Check
        If input.StartsWith("0") Then
            MessageBox.Show("Maximum capacity cannot start with 0", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox3.Focus()
            Return False
        End If

        ' Maximum Capacity 2 Digit Number Check
        If capacity < 10 Or capacity > 100 Then
            MessageBox.Show("Maximum capacity should be between 10 and 100", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox3.Focus()
            Return False
        End If

        ' All validation passed
        Return True
    End Function


    ' Frontend validations
    Private Function ValidateInputs() As Boolean

        Dim regexFacilityID As New Regex("^F\d+$")


        ' Facility ID Presence Check
        If String.IsNullOrWhiteSpace(TextBox1.Text) Then
            MessageBox.Show("Facility ID cannot be blank", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox1.Focus()
            Return False
        ElseIf Not regexFacilityID.IsMatch(TextBox1.Text) Then
            MessageBox.Show("Invalid Facility ID", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox1.Focus()
            Return False
        End If

        'Facility Name Presence check
        If String.IsNullOrWhiteSpace(TextBox2.Text) Then
            MessageBox.Show("Facility Name cannot be blank.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        ElseIf Not TextBox2.Text.All(Function(c) Char.IsLetterOrDigit(c) Or c = " ") Then
            MessageBox.Show("Invalid Facility Name", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox2.Focus()
            Return False
        ElseIf ContainsRepeatedCharacters(TextBox2.Text) Then
            MessageBox.Show("Facility Name should not contain repeated characters", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox2.Focus()
            Return False
        End If

        'Description Presence check
        If String.IsNullOrWhiteSpace(RichTextBox1.Text) Then
            MessageBox.Show("Description cannot be blank.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            RichTextBox1.Focus()
            Return False
        End If

        ' Validate Maximum Capacity input
        If Not ValidateCapacityInput(TextBox3.Text) Then
            Return False
        End If

        Return True

    End Function
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Close the current form (Form6) and open the first form (Form3) again
        Dim form3 As New Form3()
        form3.Show()
        Me.Close()
    End Sub

    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Facility ID to start with "F"
        TextBox1.Text = "F"
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Perform frontend validations
        If Not ValidateInputs() Then
            Return
        End If

        ' Check if Facility_ID already exists
        Dim facilityIdExists As Boolean = False
        Dim queryFacilityIdExists As String = "SELECT COUNT(*) FROM Facility_table WHERE Facility_ID = @facilityId"

        Try
            Using connection As New SqlConnection(connectionString)
                Using commandCheckFacilityId As New SqlCommand(queryFacilityIdExists, connection)
                    connection.Open()
                    commandCheckFacilityId.Parameters.AddWithValue("@facilityId", TextBox1.Text)
                    Dim count As Integer = Convert.ToInt32(commandCheckFacilityId.ExecuteScalar())
                    facilityIdExists = (count > 0)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error checking Facility ID: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        If facilityIdExists Then
            MessageBox.Show("Facility ID is already taken.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If


        ' Insert data into the Facility_table
        Dim query As String = "INSERT INTO [dbo].[Facility_table] ([Facility_ID], [name], [description], [max_capacity]) " &
                      "VALUES (@Facility_ID, @name, @description, @max_capacity)"

        Try
            Using connection As New SqlConnection(connectionString)
                Using command As New SqlCommand(query, connection)
                    ' Add parameters
                    command.Parameters.AddWithValue("@Facility_ID", TextBox1.Text)
                    command.Parameters.AddWithValue("@name", TextBox2.Text)
                    command.Parameters.AddWithValue("@description", RichTextBox1.Text)
                    command.Parameters.AddWithValue("@max_capacity", Convert.ToInt32(TextBox3.Text))

                    connection.Open()
                    command.ExecuteNonQuery()

                    MessageBox.Show("Facility Registered Successfully", "Confirmation", MessageBoxButtons.OK)
                    ClearInputs()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error inserting data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ClearInputs()
        ' Clear all textboxes
        TextBox1.Text = "F"
        TextBox2.Text = ""
        RichTextBox1.Text = ""
        TextBox3.Text = ""

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' Clear all textboxes
        TextBox1.Text = "F"
        TextBox2.Text = ""
        RichTextBox1.Text = ""
        TextBox3.Text = ""
    End Sub

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged
        If RichTextBox1.TextLength > 0 Then
            ' Get the current selection start position
            Dim selectionStart As Integer = RichTextBox1.SelectionStart

            ' Capitalize the first letter
            RichTextBox1.Text = Char.ToUpper(RichTextBox1.Text(0)) + RichTextBox1.Text.Substring(1)

            ' Restore the selection start position
            RichTextBox1.SelectionStart = selectionStart
        End If

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        ' Generate the next facility ID and display it in TextBox1
        TextBox1.Text = GenerateNextFacilityIDFromDatabase()

        ' Disable TextBox1 to prevent editing
        TextBox1.Enabled = False

    End Sub
End Class