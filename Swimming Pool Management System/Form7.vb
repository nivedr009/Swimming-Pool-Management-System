Imports System.Text.RegularExpressions
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Data
Imports System.Data.SqlClient

Public Class Form7

    ' SQL connection string
    Dim connectionString As String = "Data Source=DESKTOP-O16HO1G\SQLEXPRESS;Initial Catalog='Swimming Pool Management System';Integrated Security=True;"

    Private Function GenerateNextBookingIDFromDatabase() As String
        Dim nextBookingID As String = "B01" ' Default value

        Try
            ' SQL query to get the last booking ID from the database
            Dim queryLastBookingID As String = "SELECT TOP 1 Booking_ID FROM Booking_table ORDER BY CAST(SUBSTRING(Booking_ID, 2, LEN(Booking_ID)) AS INT) DESC"

            Using connection As New SqlConnection(connectionString)
                Using command As New SqlCommand(queryLastBookingID, connection)
                    connection.Open()
                    Dim lastBookingID As String = Convert.ToString(command.ExecuteScalar())

                    ' If there are records in the database, generate the next booking ID
                    If Not String.IsNullOrEmpty(lastBookingID) Then
                        ' Extract the numeric part of the last booking ID
                        Dim numericPart As String = lastBookingID.Substring(1)
                        ' Convert the numeric part to an integer and increment by 1
                        Dim nextNumericPart As Integer = Convert.ToInt32(numericPart) + 1
                        ' Generate the next booking ID by combining the prefix ("B") and the incremented numeric part
                        nextBookingID = "B" & nextNumericPart.ToString("D2")
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error generating next booking ID: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return nextBookingID
    End Function



    Private Function CustomerExists(customerId As String) As Boolean
        Dim query As String = "SELECT COUNT(*) FROM Customer_table WHERE Cust_ID = @customerId"
        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                connection.Open()
                command.Parameters.AddWithValue("@customerId", customerId)
                Dim count As Integer = Convert.ToInt32(command.ExecuteScalar())
                Return count > 0
            End Using
        End Using
    End Function

    Private Sub SaveToBookingTable()
        ' Extract data from controls
        Dim bookingId As String = TextBox1.Text
        Dim custId As String = TextBox2.Text
        Dim facilityId As String = ComboBox1.SelectedItem.ToString()
        Dim dateOfBooking As Date = DateTimePicker1.Value.Date
        Dim slot As String = ListBox1.SelectedItem.ToString()

        ' Format dateOfBooking to 'YYYY-MM-DD' format
        Dim dateOfBookingString As String = dateOfBooking.ToString("yyyy-MM-dd")

        ' SQL query to insert data into Booking_table
        Dim query As String = "INSERT INTO [dbo].[Booking_table] ([Booking_ID], [Cust_ID], [Facility_ID], [date_of_booking], [slot]) " &
                      "VALUES (@bookingId, @custId, @facilityId, @dateOfBooking, @slot)"

        Try
            Using connection As New SqlConnection(connectionString)
                Using command As New SqlCommand(query, connection)
                    ' Add parameters
                    command.Parameters.AddWithValue("@bookingId", bookingId)
                    command.Parameters.AddWithValue("@custId", custId)
                    command.Parameters.AddWithValue("@facilityId", facilityId)
                    command.Parameters.AddWithValue("@dateOfBooking", dateOfBookingString) ' Use the formatted date string
                    command.Parameters.AddWithValue("@slot", slot)

                    connection.Open()
                    command.ExecuteNonQuery()

                    MessageBox.Show("Slot Booked Successfully", "Confirmation", MessageBoxButtons.OK)
                    ClearInputs()

                    ' Uncheck CheckBox1 after the booking is saved
                    CheckBox1.Checked = False
                    ComboBox1.Enabled = True ' Enable ComboBox1 since availability is not checked
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error booking slot: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub




    Private Function SlotAlreadyBooked(custId As String, dateOfBooking As Date, slot As String) As Boolean
        ' Convert dateOfBooking to a string in the format "yyyy-MM-dd"
        Dim dateOfBookingString As String = dateOfBooking.ToString("yyyy-MM-dd")

        ' Query to check if slot has already been booked for the same customer on the same date and time
        Dim query As String = "SELECT COUNT(*) FROM Booking_table WHERE Cust_ID = @custId AND date_of_booking = @dateOfBooking AND slot = @slot"

        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@custId", custId)
                command.Parameters.AddWithValue("@dateOfBooking", dateOfBookingString) ' Pass the formatted date string
                command.Parameters.AddWithValue("@slot", slot)

                connection.Open()

                ' Execute the query and get the count of records
                Dim count As Integer = Convert.ToInt32(command.ExecuteScalar())

                ' If count is greater than 0, it means slot has already been booked
                Return count > 0


            End Using
        End Using
    End Function


    Private Sub PopulateFacilityIDs()
        ' Query to fetch all available facility IDs
        Dim query As String = "SELECT Facility_ID FROM Facility_table"

        ' Clear existing items in ComboBox1
        ComboBox1.Items.Clear()

        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                Try
                    connection.Open()
                    Dim reader As SqlDataReader = command.ExecuteReader()

                    ' Loop through the result set and add facility IDs to ComboBox1
                    While reader.Read()
                        ComboBox1.Items.Add(reader("Facility_ID").ToString())
                    End While

                    ' Close the reader
                    reader.Close()
                Catch ex As Exception
                    MessageBox.Show("Error while fetching facility IDs: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub


    ' Frontend validations
    Private Function ValidateInputs() As Boolean

        Dim regexBookingID As New Regex("^B\d+$")

        Dim regexCustID As New Regex("^C\d+$")

        Dim regexFacilityID As New Regex("^F\d+$")


        ' Booking ID Presence Check
        If String.IsNullOrWhiteSpace(TextBox1.Text) Then
            MessageBox.Show("Booking ID cannot be blank", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox1.Focus()
            Return False
        ElseIf Not regexBookingID.IsMatch(TextBox1.Text) Then
            MessageBox.Show("Invalid Booking ID", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox1.Focus()
            Return False
        End If

        ' Customer ID Presence Check
        If String.IsNullOrWhiteSpace(TextBox2.Text) Then
            MessageBox.Show("Customer ID cannot be blank", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox2.Focus()
            Return False
        ElseIf Not regexCustID.IsMatch(TextBox2.Text) Then
            MessageBox.Show("Invalid Customer ID", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox2.Focus()
            Return False
        End If

        ' Facility ID Presence Check for ComboBox1
        If ComboBox1.SelectedIndex = -1 Then
            MessageBox.Show("Please select a Facility ID", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ComboBox1.Focus()
            Return False
        End If

        ' Check if any slot is selected in ListBox1
        If ListBox1.SelectedIndex = -1 Then
            MessageBox.Show("Please select a time slot", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If


        Return True
    End Function

    Private Function FormatTimeSpan(slotStart As TimeSpan, slotEnd As TimeSpan) As String
        Dim startTime As DateTime = New DateTime().Add(slotStart)
        Dim endTime As DateTime = New DateTime().Add(slotEnd)
        Return startTime.ToString("h:mm tt") & " to " & endTime.ToString("h:mm tt")
    End Function



    Private Function GeneratePossibleTimeSlots() As List(Of Tuple(Of TimeSpan, TimeSpan))
        Dim possibleSlots As New List(Of Tuple(Of TimeSpan, TimeSpan))

        ' Define start and end times for slots
        Dim currentTime As TimeSpan = DateTime.Now.TimeOfDay ' Current time
        Dim startTime As TimeSpan = New TimeSpan(10, 0, 0) ' Start at 10:00 AM
        Dim endTime As TimeSpan = New TimeSpan(22, 0, 0) ' End at 10:00 PM
        Dim interval As TimeSpan = New TimeSpan(0, 30, 0) ' 30-minute intervals
        Dim slotDuration As TimeSpan = New TimeSpan(1, 0, 0) ' 1-hour duration for each slot

        ' Adjust current time to the next possible slot
        If currentTime < startTime Then
            ' Move to the next hour or half-hour interval
            currentTime = If(currentTime.Minutes < 30, New TimeSpan(currentTime.Hours, 30, 0), New TimeSpan(currentTime.Hours + 1, 0, 0))
        ElseIf currentTime > endTime Then
            Return possibleSlots ' No slots available if current time is past the end time
        Else
            ' Move to the next hour or half-hour interval
            currentTime = If(currentTime.Minutes < 30, New TimeSpan(currentTime.Hours, 30, 0), New TimeSpan(currentTime.Hours + 1, 0, 0))
        End If

        ' Generate time slots with 30-minute intervals, showing duration of 1 hour
        While currentTime.Add(slotDuration) <= endTime
            Dim slotStart As TimeSpan = currentTime
            Dim slotEnd As TimeSpan = currentTime.Add(slotDuration)
            possibleSlots.Add(New Tuple(Of TimeSpan, TimeSpan)(slotStart, slotEnd))
            currentTime = currentTime.Add(interval)
        End While

        Return possibleSlots
    End Function




    Private Sub PopulateTimeSlots()
        ' Clear existing items
        ListBox1.Items.Clear()

        ' Generate possible time slots
        Dim possibleSlots As List(Of Tuple(Of TimeSpan, TimeSpan)) = GeneratePossibleTimeSlots()

        ' Add the generated time slots to the ListBox
        For Each slot As Tuple(Of TimeSpan, TimeSpan) In possibleSlots
            ListBox1.Items.Add(FormatTimeSpan(slot.Item1, slot.Item2))
        Next
    End Sub



    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Close the current form (Form7) and open the first form (Form3) again
        Dim form3 As New Form3()
        form3.Show()
        Me.Close()
    End Sub

    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' Booking ID to start with "B"
        TextBox1.Text = "B"

        ' Customer ID to start with "C"
        TextBox2.Text = "C"

        'Disable manual entry into comboBox
        ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList

        ' Set DateTimePicker to today's date
        DateTimePicker1.Value = Date.Today

        ' Disable editing of the DateTimePicker
        DateTimePicker1.Enabled = False

        PopulateTimeSlots()

        ' Call the function to populate ComboBox1 with facility IDs
        PopulateFacilityIDs()

        ' Select the first time slot
        ListBox1.SelectedIndex = 0

        ' Select the first facility ID
        ComboBox1.SelectedIndex = 0

        ' Disable CheckBox1
        CheckBox1.Enabled = False

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim regexCustID As New Regex("^C\d+$")


        ' Customer ID Presence Check
        If String.IsNullOrWhiteSpace(TextBox2.Text) Then
            MessageBox.Show("Customer ID cannot be blank", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox2.Focus()
            Exit Sub
        ElseIf Not regexCustID.IsMatch(TextBox2.Text) Then
            MessageBox.Show("Invalid Customer ID", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox2.Focus()
            Exit Sub
        End If

        ' Check if the checkbox is checked
        If Not CheckBox1.Checked Then
            MessageBox.Show("Please check availability before booking.", "Availability Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ' Additional check: Ensure that the booking doesn't already exist for the same customer, facility, date, and slot
        Dim bookingId As String = TextBox1.Text
        Dim custId As String = TextBox2.Text
        Dim facilityId As String = ComboBox1.SelectedItem.ToString()
        Dim dateOfBooking As Date = DateTime.Today
        Dim slot As String = ListBox1.SelectedItem.ToString()

        ' Perform frontend validations
        If Not ValidateInputs() Then
            Exit Sub
        End If

        If ListBox1.SelectedItem Is Nothing Then
            MessageBox.Show("Please select a time slot", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ' Check if the customer exists
        If Not CustomerExists(custId) Then
            MessageBox.Show("Customer does not exist.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox2.Focus()
            Exit Sub
        End If

        ' Check if the slot is already booked for the same customer on the same date and time
        If SlotAlreadyBooked(custId, dateOfBooking, slot) Then
            MessageBox.Show("Slot already booked for the same customer, date, and time.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ' Enable ComboBox1 if slot is already booked
            ComboBox1.Enabled = True
            Exit Sub
        End If


        ' If all validations pass, proceed with saving
        SaveToBookingTable()

    End Sub

    Private Sub ClearInputs()
        ' Clear all textboxes
        TextBox1.Text = "B"
        TextBox2.Text = "C"

        ' Set the Facility ComboBox to Null
        ComboBox1.SelectedIndex = 0

        ' Set DateTimePicker to today's date
        DateTimePicker1.Value = Date.Today

        ' Deselect all items in ListBox1
        ListBox1.SelectedIndex = 0

        CheckBox1.Checked = False

        ComboBox1.Enabled = True ' Enable ComboBox1 since availability is not checked

        ListBox1.Enabled = True

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' Clear all textboxes
        TextBox1.Text = "B"
        TextBox2.Text = "C"

        ' Set the Facility ComboBox to Null
        ComboBox1.SelectedIndex = 0

        ' Set DateTimePicker to today's date
        DateTimePicker1.Value = Date.Today

        ' Deselect all items in ListBox1
        ListBox1.SelectedIndex = 0

        CheckBox1.Checked = False

        ComboBox1.Enabled = True ' Enable ComboBox1 since availability is not checked

        ListBox1.Enabled = True

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        ' Generate the next booking ID and display it in TextBox1
        TextBox1.Text = GenerateNextBookingIDFromDatabase()

        ' Disable TextBox1 to prevent editing
        TextBox1.Enabled = False

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Dim regexCustID As New Regex("^C\d+$")


        ' Customer ID Presence Check
        If String.IsNullOrWhiteSpace(TextBox2.Text) Then
            MessageBox.Show("Customer ID cannot be blank", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox2.Focus()
            Exit Sub

        ElseIf Not regexCustID.IsMatch(TextBox2.Text) Then
            MessageBox.Show("Invalid Customer ID", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox2.Focus()
            Exit Sub
        End If


        ' Retrieve the selected facility ID from ComboBox1
        Dim facilityId As String = If(ComboBox1.SelectedItem IsNot Nothing, ComboBox1.SelectedItem.ToString(), "")

        ' Retrieve the selected time slot from ListBox1
        Dim slot As String = If(ListBox1.SelectedItem IsNot Nothing, ListBox1.SelectedItem.ToString(), "")

        ' Check if a facility and time slot are selected
        If String.IsNullOrEmpty(facilityId) OrElse String.IsNullOrEmpty(slot) Then
            MessageBox.Show("Please select a facility and a time slot", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ' Query the database to count the number of bookings for the selected facility and time slot
        Dim countBookings As Integer = CountBookingsForFacilityAndSlot(facilityId, slot)

        ' Retrieve the maximum capacity of the selected facility from the database
        Dim maxCapacity As Integer = GetMaxCapacityForFacility(facilityId)

        ' Message string to display availability information
        Dim availabilityMessage As String = $"Facility: {facilityId}{Environment.NewLine}" &
                                    $"Time Slot: {slot}{Environment.NewLine}" &
                                    $"Maximum Capacity: {maxCapacity}{Environment.NewLine}" &
                                    $"Number of Bookings: {countBookings}{Environment.NewLine}"

        ' Determine availability status
        If countBookings < maxCapacity Then
            availabilityMessage &= "The selected facility is available for booking."
            CheckBox1.Checked = True ' Facility is available, so check the checkbox
            ComboBox1.Enabled = False ' Disable ComboBox1 since availability is checked
            ListBox1.Enabled = False ' Disable ListBox1 since availability is checked
        Else
            availabilityMessage &= "The selected facility is fully booked for the selected time slot."
            CheckBox1.Checked = False ' Facility is fully booked, so uncheck the checkbox
            ComboBox1.Enabled = True ' Enable ComboBox1 since availability is not checked
            ListBox1.Enabled = True ' Enable ListBox1 since availability is not checked
        End If

        ' Display the availability information in a message box
        MessageBox.Show(availabilityMessage, "Availability", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

    Private Function CountBookingsForFacilityAndSlot(facilityId As String, slot As String) As Integer
        ' Query the database to count the number of bookings for the selected facility and time slot
        Dim query As String = "SELECT COUNT(*) FROM Booking_table WHERE Facility_ID = @facilityId AND slot = @slot"
        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@facilityId", facilityId)
                command.Parameters.AddWithValue("@slot", slot)
                connection.Open()
                Return Convert.ToInt32(command.ExecuteScalar())
            End Using
        End Using
    End Function

    Private Function GetMaxCapacityForFacility(facilityId As String) As Integer
        ' Query the database to retrieve the maximum capacity of the selected facility
        Dim query As String = "SELECT Max_Capacity FROM Facility_table WHERE Facility_ID = @facilityId"
        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@facilityId", facilityId)
                connection.Open()
                Return Convert.ToInt32(command.ExecuteScalar())
            End Using
        End Using
    End Function
End Class