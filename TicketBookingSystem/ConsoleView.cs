using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBookingSystem
{
    internal class ConsoleView
    {
        public void MainMenu()
        {
            while (true)
            {
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. User Account Management");
                Console.WriteLine("2. Events Account Management");
                Console.WriteLine("3. Tickets Account Manangement");
                Console.WriteLine("4. Bookings ");
                Console.WriteLine("E. Exit");
                Console.Write("Enter your choice: ");

                // Read the user's choice as a string
                string choice = Console.ReadLine();
                Console.Clear();
                // Check if the input is a valid integer


                // Handle the user's choice
                switch (choice.ToUpper())
                {
                    case "E":
                        Console.WriteLine("Exiting the application.");
                        return;
                    case "1":
                        Console.WriteLine("You selected: User Account");
                        UserAccount userAccount = new UserAccount();
                        userAccount.Run();
                        break;
                    case "2":
                        Console.WriteLine("You selected: Events");
                        Events events = new Events();   
                        events.Run();
                        break;
                    case "3":
                        Console.WriteLine("You selected: Tickets");
                        TicketTypeManager ticketTypeManager = new TicketTypeManager();
                        ticketTypeManager.Run();
                        break;
                    case "4":
                        Console.WriteLine("You selected: Bookings");
                        BookingManager bookingManager = new BookingManager();
                        bookingManager.Run();
                        break; 
                    default:
                        Console.WriteLine("Invalid option. Please enter a valid option.");
                        break;
                }
                Console.WriteLine("Press Enter to Continue.....");
            }
        }
        }
    }

