using System;
using System.Collections.Generic;
using System.IO;

namespace TicketBookingSystem
{
    internal class BookingManager
    {
        List<Booking> bookings = new List<Booking>(); // List to store booking details
        string dataFilePath = "booking_data.txt"; // Text file to store booking data

        int nextBookingId = 1; // Track the next available booking ID

        // Load user data, event data, and ticket type data
        Dictionary<int, UserAccount.User> users;
        Dictionary<int, Events.Event> events;
        Dictionary<int, TicketTypeManager.TicketType> ticketTypes;

        public static void ManageBookings()
        {
            BookingManager bookingManager = new BookingManager();
            bookingManager.Run();
        }

        public void Run()
        {
            LoadUserData();  // Load user data
            LoadEventData(); // Load event data
            LoadTicketTypeData(); // Load ticket type data
            LoadBookingData(); // Load booking data

            while (true)
            {
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Create Booking");
                Console.WriteLine("2. View All Bookings");
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
                        Console.WriteLine("You selected: Create Booking");
                        CreateBooking();
                        SaveBookingData();
                        Console.Clear();
                        break;
                    case "2":
                        Console.WriteLine("You selected: View All Bookings");
                        ViewAllBookings();
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please enter a valid option.");
                        break;
                }
            }
        }

        // Booking class to store booking details
        public class Booking
        {
            public int Id { get; set; }
            public int UserId { get; set; }
            public int EventId { get; set; }
            public int TicketTypeId { get; set; }

            // Additional data
            public string Username { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string EventName { get; set; }
            public string Location { get; set; }
            public string TicketType { get; set; }
            public decimal Price { get; set; }
        }

        // Function to create a new booking
        void CreateBooking()
        {
            Booking newBooking = new Booking();
            newBooking.Id = nextBookingId++;
            UserAccount userAccount = new UserAccount();
            userAccount.LoadUserData();
            userAccount.DisplayAllUsers();
            
            Console.Write("Enter User ID: ");
            if (int.TryParse(Console.ReadLine(), out int userId))
            {
                if (users.ContainsKey(userId))
                {
                    UserAccount.User user = users[userId];
                    newBooking.UserId = userId;
                    newBooking.Username = user.Username;
                    newBooking.Email = user.Email;
                    newBooking.PhoneNumber = user.PhoneNumber;
                }
                else
                {
                    Console.WriteLine("User not found.");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid user ID.");
                return;
            }
            Events even = new Events();
            even.LoadEventData();
            even.DisplayAllEvents();
            
            Console.Write("Enter Event ID: ");
            if (int.TryParse(Console.ReadLine(), out int eventId))
            {
                if (events.ContainsKey(eventId))
                {
                    Events.Event evnt = events[eventId];
                    newBooking.EventId = eventId;
                    newBooking.EventName = evnt.EventName;
                    newBooking.Location = evnt.Location;
                }
                else
                {
                    Console.WriteLine("Event not found.");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid event ID.");
                return;
            }
            
            TicketTypeManager ticketTypeManager = new TicketTypeManager(); // Create an instance of TicketTypeManager
            ticketTypeManager.LoadTicketTypeData();
            ticketTypeManager.DisplayAllTicketTypes();
            
            Console.Write("Enter Ticket Type ID: ");
            if (int.TryParse(Console.ReadLine(), out int ticketTypeId))
            {
                if (ticketTypes.ContainsKey(ticketTypeId))
                {
                    TicketTypeManager.TicketType ticketType = ticketTypes[ticketTypeId];
                    newBooking.TicketTypeId = ticketTypeId;
                    newBooking.TicketType = ticketType.Type;
                    newBooking.Price = ticketType.Price;
                }
                else
                {
                    Console.WriteLine("Ticket Type not found.");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid ticket type ID.");
                return;
            }

            bookings.Add(newBooking);
            Console.WriteLine("Booking created successfully.");
        }

        // Function to view all bookings
        // Function to view all bookings
        void ViewAllBookings()
        {
            if (File.Exists(dataFilePath))
            {
                try
                {
                    string[] lines = File.ReadAllLines(dataFilePath);
                    if (lines.Length == 0)
                    {
                        Console.WriteLine("No bookings available.");
                    }
                    else
                    {
                        Console.WriteLine("All Bookings:");
                        foreach (string line in lines)
                        {
                            string[] parts = line.Split('\t');
                            if (parts.Length == 8)
                            {
                                int bookingId = int.Parse(parts[0]);
                                string username = parts[1];
                                string email = parts[2];
                                string phoneNumber = parts[3];
                                string eventName = parts[4];
                                string location = parts[5];
                                string ticketType = parts[6];
                                decimal price = decimal.Parse(parts[7]);

                                Console.WriteLine($"ID: {bookingId}, Username: {username}, " +
                                                  $"Email: {email}, Phone Number: {phoneNumber}, " +
                                                  $"Event Name: {eventName}, Location: {location}, " +
                                                  $"Ticket Type: {ticketType}, Price: {price}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading booking data: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("No bookings available.");
            }
        }




        void LoadUserData()
        {
            users = new Dictionary<int, UserAccount.User>();
            if (File.Exists("user_data.txt"))
            {
                string[] lines = File.ReadAllLines("user_data.txt");
                foreach (string line in lines)
                {
                    string[] parts = line.Split('\t');
                    if (parts.Length == 4)
                    {
                        int userId = int.Parse(parts[0]);
                        string username = parts[1];
                        string email = parts[2];
                        string phoneNumber = parts[3];

                        // Create a new User instance and add it to the dictionary
                        UserAccount.User user = new UserAccount.User
                        {
                            Id = userId,
                            Username = username,
                            Email = email,
                            PhoneNumber = phoneNumber
                        };
                        users.Add(userId, user);
                    }
                }
                Console.WriteLine("User data loaded successfully.");
            }
        }


        // Load event data from event_data.txt
        void LoadEventData()
        {
            events = new Dictionary<int, Events.Event>();
            if (File.Exists("event_data.txt"))
            {
                string[] lines = File.ReadAllLines("event_data.txt");
                foreach (string line in lines)
                {
                    string[] parts = line.Split('\t');
                    if (parts.Length == 3)
                    {
                        int eventId = int.Parse(parts[0]);
                        string eventName = parts[1];
                        string location = parts[2];

                        // Create a new Event instance and add it to the dictionary
                        Events.Event evnt = new Events.Event
                        {
                            Id = eventId,
                            EventName = eventName,
                            Location = location
                        };
                        events.Add(eventId, evnt);
                    }
                }
                Console.WriteLine("Event data loaded successfully.");
            }
        }


        // Load ticket type data from ticket_type_data.txt
        void LoadTicketTypeData()
        {
            ticketTypes = new Dictionary<int, TicketTypeManager.TicketType>();
            if (File.Exists("ticket_type_data.txt"))
            {
                string[] lines = File.ReadAllLines("ticket_type_data.txt");
                foreach (string line in lines)
                {
                    string[] parts = line.Split('\t');
                    if (parts.Length == 3)
                    {
                        int ticketTypeId = int.Parse(parts[0]);
                        string type = parts[1];
                        decimal price = decimal.Parse(parts[2]);

                        // Create a new TicketType instance and add it to the dictionary
                        TicketTypeManager.TicketType ticketType = new TicketTypeManager.TicketType
                     
                        {
                            Id = ticketTypeId,
                            Type = type,
                            Price = price
                        };
                        ticketTypes.Add(ticketTypeId, ticketType);
                    }
                }
                Console.WriteLine("Ticket Type data loaded successfully.");
            }
        }


        // Function to load booking data from the text file
        void LoadBookingData()
        {
            if (File.Exists(dataFilePath)) // Check if the data file exists
            {
                try
                {
                    string[] lines = File.ReadAllLines(dataFilePath);

                    foreach (string line in lines)
                    {
                        string[] parts = line.Split('\t');
                        if (parts.Length == 10) // Check if the line has the expected number of fields
                        {
                            Booking loadedBooking = new Booking
                            {
                                // Parse and assign values to properties of the Booking class
                                UserId = int.Parse(parts[0]),
                                EventId = int.Parse(parts[1]),
                                TicketTypeId = int.Parse(parts[2]),
                                Username = parts[3],
                                Email = parts[4],
                                PhoneNumber = parts[5],
                                EventName = parts[6],
                                Location = parts[7],
                                TicketType = parts[8],
                                Price = decimal.Parse(parts[9])
                            };

                            bookings.Add(loadedBooking);
                        }
                    }

                    Console.WriteLine("Booking data loaded successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading booking data: {ex.Message}");
                }
            }
        }


        // Function to save booking data to the text file
        void SaveBookingData()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(dataFilePath))
                {
                    foreach (Booking booking in bookings)
                    {
                        // Write the booking details to the data file, separating values with tabs
                        writer.WriteLine($"{booking.Id}\t{booking.Username}\t{booking.Email}\t{booking.PhoneNumber}\t{booking.EventName}\t{booking.Location}\t{booking.TicketType}\t{booking.Price}");
                    }
                }

                Console.WriteLine("Booking data saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving booking data: {ex.Message}");
            }
        }

    }
}
