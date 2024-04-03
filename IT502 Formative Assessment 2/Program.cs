using System;
using System.IO;

class Program
{
    // Function to add rooms
    static void Add()
    { 
            string fileName = "/Users/apple/Desktop/Shy/FileObject/lhms_studentid.txt";
            Console.WriteLine("Adding rooms...");

            try
            {
                Console.Write("Enter the total number of rooms: ");
                int totalRooms = Convert.ToInt32(Console.ReadLine());

                using (StreamWriter writer = File.AppendText(fileName))
            { 

                    // Write room numbers with timestamp
                    for (int roomNumber = 1; roomNumber <= totalRooms; roomNumber++)
                    {
                        string sDateTime = DateTime.Now.ToString();
                        writer.WriteLine(DateTime.Now.ToString() + " | Room " + roomNumber);
                    }
                }

                Console.WriteLine("Added " + totalRooms + " rooms successfully.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Invalid input! Please enter a valid number.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }


    // Function to display rooms from file
    static void Display()
    {
        Console.WriteLine("Displaying rooms...");
        try
        {
            string fileName = "/Users/apple/Desktop/Shy/FileObject/lhms_studentid.txt";
            string[] lines = File.ReadAllLines(fileName);

            if (lines.Length == 0)
            {
                Console.WriteLine("No rooms found.");
            }
            else
            {
                Console.WriteLine("Rooms:");
                foreach (string line in lines)
                {
                    Console.WriteLine(line); // Display each line (room number)
                }
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Error: File not found.");
        }
        catch (Exception)
        {
            Console.WriteLine("Error: Unable to display rooms.");
        }
    }


    // Function to allocate rooms
    static void Allocate()
    { 
        string fileName = "/Users/apple/Desktop/Shy/FileObject/lhms_studentid.txt";

        try
        {
            string[] lines = File.ReadAllLines(fileName);

            Console.WriteLine("Available Rooms:");
            foreach (string line in lines)
            {
                if (!line.StartsWith("Allocated Room"))
                {
                    Console.WriteLine(line);
                }
            }

            Console.Write("Enter the room number to allocate: ");
            int roomNumber = Convert.ToInt32(Console.ReadLine());

            bool roomBooked = false;

            List<string> updatedLines = new List<string>();

            foreach (string line in lines)
            {
                if (line.Contains("Room " + roomNumber))
                {
                    roomBooked = true;
                    updatedLines.Add(DateTime.Now.ToString() + " | Allocated Room " + roomNumber);
                }
                else
                {
                    updatedLines.Add(line);
                }
            }

            if (roomBooked)
            {
                // Update the file with the allocated room
                File.WriteAllLines(fileName, updatedLines);
                Console.WriteLine("Room " + roomNumber + " Successfully Allocated.");
            }
            else
            {
                Console.WriteLine("Room " + roomNumber + " is already booked or does not exist.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    // Function to deallocate rooms
    static void Deallocate()
    {
        try
        {
            string fileName = "/Users/apple/Desktop/Shy/FileObject/lhms_studentid.txt";

            // Read all lines from the file
            string[] lines = File.ReadAllLines(fileName);

            if (lines.Length == 0)
            {
                Console.WriteLine("No rooms are currently allocated.");
                return;
            }

            // Display allocated rooms
            Console.WriteLine("Allocated rooms:");
            foreach (string line in lines)
            {
                if (line.Contains("Allocated Room"))
                {
                    Console.WriteLine(line);
                }
            }

            // Prompt user to select a room to deallocate
            Console.Write("Enter the room number to deallocate: ");
            int roomNumber = Convert.ToInt32(Console.ReadLine());

            // Check if the selected room is already allocated
            bool roomFound = false;
            List<string> updatedAllocatedRooms = new List<string>();
            foreach (string line in lines)
            {
                if (line.Contains("Allocated Room " + roomNumber))
                {
                    roomFound = true;
                    Console.WriteLine("Room " + roomNumber + " deallocated.");

                    // Remove "Allocated" from the line and add it to the updated list
                    updatedAllocatedRooms.Add(line.Replace("Allocated ", "" ));
                }
                else
                {
                    updatedAllocatedRooms.Add(line);
                }
            }

            // Update the file with deallocated rooms
            File.WriteAllLines(fileName, updatedAllocatedRooms);

            if (roomFound)
            {
                Console.WriteLine("Room " + roomNumber + " has been deallocated.");
            }
            else
            {
                Console.WriteLine("Room " + roomNumber + " is not currently allocated.");
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Error: File not found.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }


    // Function to display room allocation details
    static void Details()
    {
        Console.WriteLine("Displaying room allocation details...");
        try
        {
            string fileName = "/Users/apple/Desktop/Shy/FileObject/lhms_studentid.txt";
            string[] lines = File.ReadAllLines(fileName);

            // Check if there are any allocated rooms to display
            bool allocatedRoomsFound = false;

            foreach (string line in lines)
            {
                if (line.Contains("Allocated Room"))
                {
                    allocatedRoomsFound = true;
                    Console.WriteLine(line); // Display each allocated room
                }
            }

            if (allocatedRoomsFound == false)
            {
                Console.WriteLine("No allocated rooms found.");
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Error: File not found.");
        }
        catch (Exception)
        {
            Console.WriteLine("Error: Unable to display room allocation details.");
        }
    }

    static void SaveToFile()
    {
        try
        {
            string inputFileName = "/Users/apple/Desktop/Shy/FileObject/lhms_studentid.txt";
            string backupFileName = "/Users/apple/Desktop/Shy/FileObject/lhms_room_allocation_backup.txt";

            // Read all lines from the input file
            string[] lines = File.ReadAllLines(inputFileName);

            // Filter lines containing "Allocated Room"
            List<string> allocatedLines = new List<string>();
            foreach (string line in lines)
            {
                if (line.Contains("Allocated Room"))
                {
                    allocatedLines.Add(line);
                }
            }

            // If there are allocated rooms, save them to the backup file
            if (lines.Length > 0)
            {
                File.WriteAllLines(backupFileName, allocatedLines);
                Console.WriteLine("Allocated Rooms Saved");
            }
            else
            {
                Console.WriteLine("No allocated rooms saved");
            }
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Error: Unable to save room allocation data to file.");
        }
    }

    // Function to show room allocation from file
    static void ShowFromFile()
    {
        try
        {
            string fileName = "/Users/apple/Desktop/Shy/FileObject/lhms_room_allocation_backup.txt";

            // Check if input file exists
            if (File.Exists(fileName))
            {
                // Read content of input file and display
                string content = File.ReadAllText(fileName);
                Console.WriteLine("Room allocation details:");
                Console.WriteLine(content);
            }
            else
            {
                Console.WriteLine("Error: File not found.");
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Error: Unable to show room allocation.");
        }
    }

    // Function to display the menu
    static void Menyu()
    {
        Console.Clear();
        Console.WriteLine("******************************************************");
        Console.WriteLine("            LANGHAM HOTEL MANAGEMENT SYSTEM            ");
        Console.WriteLine("                      MENU                             ");
        Console.WriteLine("******************************************************");
        Console.WriteLine("1. Add Rooms");
        Console.WriteLine("2. Display Rooms");
        Console.WriteLine("3. Allocate Rooms");
        Console.WriteLine("4. De-Allocate Rooms");
        Console.WriteLine("5. Display Room Allocation Details");
        Console.WriteLine("6. Billing");
        Console.WriteLine("7. Save the Room Allocations To a File");
        Console.WriteLine("8. Show the Room Allocations From a File");
        Console.WriteLine("9. Exit");
        Console.Write("Enter your choice: ");

        try
        {
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Add();
                    break;
                case 2:
                    Display();
                    break;
                case 3:
                    Allocate();
                    break;
                case 4:
                    Deallocate();
                    break;
                case 5:
                    Details();
                    break;
                case 6:
                    Console.WriteLine("Billing Feature is Under Construction and will be added soon...!!!");
                    break;
                case 7:
                    SaveToFile();
                    break;
                case 8:
                    ShowFromFile();
                    break;
                case 9:
                    Console.WriteLine("Exiting...");
                    return; // Exit the function
                default:
                    Console.WriteLine("Invalid choice! Please enter a number from 1 to 9.");
                    break;
            }
            Console.WriteLine("\nPress enter to select Menu item again");
            Console.ReadKey();
            Menyu(); // Recursively call the function to show the menu again
    }
        catch (FormatException)
        {
            Console.WriteLine("Error: Invalid input! Please enter a number.");
        }
    }

    // Main method to start the program
    static void Main(string[] args)
    {
        Menyu(); // Start the menu
        }
}
