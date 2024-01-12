using System;
using System.Collections.Generic;
using System.IO;

namespace TicketBookingSystem
{
    internal class TicketTypeManager
    {
        List<TicketType> ticketTypes = new List<TicketType>(); // List to store ticket type details
        string dataFilePath = "ticket_type_data.txt"; // Text file to store ticket type data

        int nextTicketTypeId = 1; // Track the next available ticket type ID

        public static void ManageTicketTypes()
        {
            TicketTypeManager ticketTypeManager = new TicketTypeManager();
            ticketTypeManager.Run();
        }

        public void Run()
        {
            LoadTicketTypeData(); // Load ticket type data from the text file

            while (true)
            {
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Add Ticket Type");
                Console.WriteLine("2. Update Ticket Type");
                Console.WriteLine("3. Delete Ticket Type");
                Console.WriteLine("4. Display All Ticket Types");
                Console.WriteLine("M. Main Menu");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();
                Console.Clear();

                switch (choice.ToUpper())
                {
                    case "M":
                        Console.WriteLine("Exiting to the Main Menu.");
                        return;
                    case "1":
                        Console.WriteLine("You selected: Add Ticket Type");
                        AddTicketType();
                        SaveTicketTypeData();
                        Console.Clear();
                        break;
                    case "2":
                        Console.WriteLine("You selected: Update Ticket Type");
                        UpdateTicketType();
                        SaveTicketTypeData();
                        Console.Clear();
                        break;
                    case "3":
                        Console.WriteLine("You selected: Delete Ticket Type");
                        DeleteTicketType();
                        SaveTicketTypeData();
                        Console.Clear();
                        break;
                    case "4":
                        Console.WriteLine("You selected: Display All Ticket Types");
                        DisplayAllTicketTypes();
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please enter a valid option.");
                        break;
                }


            }
        }

        // TicketType class to store ticket type details
        public class TicketType
        {
            public int Id { get; set; }
            public string Type { get; set; }
            public decimal Price { get; set; }
        }

        // Function to add a new ticket type
        void AddTicketType()
        {
            TicketType newTicketType = new TicketType();

            newTicketType.Id = nextTicketTypeId++;

            Console.Write("Enter Ticket Type (Executive, Gold, Economy, Silver): ");
            newTicketType.Type = Console.ReadLine();

            Console.Write("Enter Price: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                newTicketType.Price = price;
                ticketTypes.Add(newTicketType);
                Console.WriteLine("Ticket Type added successfully.");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid price.");
            }
        }

        // Function to update ticket type details based on ID
        void UpdateTicketType()
        {
            DisplayAllTicketTypes();
            Console.Write("Enter the ID of the ticket type to update: ");
            
            if (int.TryParse(Console.ReadLine(), out int ticketTypeIdToUpdate))
            {
                TicketType ticketTypeToUpdate = ticketTypes.Find(tt => tt.Id == ticketTypeIdToUpdate);

                if (ticketTypeToUpdate != null)
                {
                    Console.Write("Enter new Ticket Type (Executive, Gold, Economy, Silver): ");
                    ticketTypeToUpdate.Type = Console.ReadLine();

                    Console.Write("Enter new Price: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal newPrice))
                    {
                        ticketTypeToUpdate.Price = newPrice;
                        Console.WriteLine("Ticket Type details updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid price.");
                    }
                }
                else
                {
                    Console.WriteLine($"Ticket Type with ID '{ticketTypeIdToUpdate}' not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid ticket type ID.");
            }
        }

        // Function to delete a ticket type based on ID
        void DeleteTicketType()
        {
            DisplayAllTicketTypes();
            Console.Write("Enter the ID of the ticket type to delete: ");
           
            if (int.TryParse(Console.ReadLine(), out int ticketTypeIdToDelete))
            {
                TicketType ticketTypeToDelete = ticketTypes.Find(tt => tt.Id == ticketTypeIdToDelete);

                if (ticketTypeToDelete != null)
                {
                    ticketTypes.Remove(ticketTypeToDelete);
                    Console.WriteLine("Ticket Type deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"Ticket Type with ID '{ticketTypeIdToDelete}' not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid ticket type ID.");
            }
        }

        // Function to display all ticket types
       public void DisplayAllTicketTypes()
        {
            
            if (ticketTypes.Count == 0)
            {
                Console.WriteLine("No ticket types available.");
            }
            else
            {
                Console.WriteLine("All Ticket Types:");
                foreach (var ticketType in ticketTypes)
                {
                    Console.WriteLine($"ID: {ticketType.Id}, Type: {ticketType.Type}, Price: {ticketType.Price}");
                }
            }
        }

        // Function to load ticket type data from the text file
       public void LoadTicketTypeData()
        {
            if (File.Exists(dataFilePath))
            {
                try
                {
                    string[] lines = File.ReadAllLines(dataFilePath);
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split('\t');
                        if (parts.Length == 3)
                        {
                            TicketType loadedTicketType = new TicketType
                            {
                                Id = int.Parse(parts[0]),
                                Type = parts[1],
                                Price = decimal.Parse(parts[2])
                            };
                            ticketTypes.Add(loadedTicketType);

                            // Update the next ticket type ID to avoid conflicts
                            if (loadedTicketType.Id >= nextTicketTypeId)
                            {
                                nextTicketTypeId = loadedTicketType.Id + 1;
                            }
                        }
                    }
                    Console.WriteLine("Ticket Type data loaded successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading Ticket Type data: {ex.Message}");
                }
            }
        }

        // Function to save ticket type data to the text file
        void SaveTicketTypeData()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(dataFilePath))
                {
                    foreach (TicketType ticketType in ticketTypes)
                    {
                        writer.WriteLine($"{ticketType.Id}\t{ticketType.Type}\t{ticketType.Price}");
                    }
                }
                Console.WriteLine("Ticket Type data saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving Ticket Type data: {ex.Message}");
            }
        }
    }
}
