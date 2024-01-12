using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBookingSystem
{
    internal class Events
    {
        List<Event> events = new List<Event>();
        string dataFilePath = "event_data.txt";
        int nextEventId = 1;

        public static void EventAccount()
        {
            Events events = new Events();
            events.Run();

        }
        public void Run()
        {

            while (true)
            {
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Add Event");
                Console.WriteLine("2. Update Event");
                Console.WriteLine("3. Delete Event");
                Console.WriteLine("4. Display all Events");
                Console.WriteLine("M. Main Menu");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();
                Console.Clear();

                switch (choice.ToUpper())
                {
                    case "M":
                        Console.WriteLine("Exit to the Main Menu");
                        return;
                    case "1":
                        Console.WriteLine("Add an Event:");
                        AddEvent();
                        SaveEventData();
                        break;
                    case "2":
                        Console.WriteLine("Update an Event:");
                        UpdateEvent();
                        SaveEventData();
                        break;
                    case "3":
                        Console.WriteLine("Delete an Event:");
                        DeleteEvent();
                        SaveEventData();
                        break;
                    case "4":
                        Console.WriteLine("Display all Events:");
                        DisplayAllEvents();
                        break;
                    default:
                        Console.WriteLine("Invalid Option.Please Enter a valid option...");
                        break;
                }

            }

        }
       public class Event
        {
            public int Id { get; set; }
            public string EventName { get; set; }
            public string Location { get; set; }
        }

        // Function to add a new event
        private void AddEvent()
        {
            Event newEvent = new Event();

            newEvent.Id = nextEventId++;

            Console.Write("Enter Event Name: ");
            newEvent.EventName = Console.ReadLine();

            Console.Write("Enter Location: ");
            newEvent.Location = Console.ReadLine();

            events.Add(newEvent);
            Console.WriteLine("Event added successfully.");
        }

        // Function to update event details based on ID
        private void UpdateEvent()
        {
            DisplayAllEvents();
            Console.Write("Enter the ID of the event to update: ");
            
            if (int.TryParse(Console.ReadLine(), out int eventIdToUpdate))
            {
                Event eventToUpdate = events.Find(e => e.Id == eventIdToUpdate);

                if (eventToUpdate != null)
                {
                    Console.Write("Enter new Event Name: ");
                    eventToUpdate.EventName = Console.ReadLine();

                    Console.Write("Enter new Location: ");
                    eventToUpdate.Location = Console.ReadLine();

                    Console.WriteLine("Event details updated successfully.");
                }
                else
                {
                    Console.WriteLine($"Event with ID '{eventIdToUpdate}' not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid event ID.");
            }
        }

        // Function to delete an event based on ID
        private void DeleteEvent()
        {
            DisplayAllEvents();
            Console.Write("Enter the ID of the event to delete: ");
            
            if (int.TryParse(Console.ReadLine(), out int eventIdToDelete))
            {
                Event eventToDelete = events.Find(e => e.Id == eventIdToDelete);

                if (eventToDelete != null)
                {
                    events.Remove(eventToDelete);
                    Console.WriteLine("Event deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"Event with ID '{eventIdToDelete}' not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid event ID.");
            }
        }

        // Function to display all events
        public void DisplayAllEvents()
        {
            
            if (events.Count == 0)
            {
                Console.WriteLine("No events available.");
            }
            else
            {
                Console.WriteLine("All Events:");
                foreach (var e in events)
                {
                    Console.WriteLine($"ID: {e.Id}, Event Name: {e.EventName}, Location: {e.Location}");
                }
            }
        }

        // Function to load event data from the text file
        public void LoadEventData()
        {
            if (File.Exists(dataFilePath))
            {
                try
                {
                    string[] lines = File.ReadAllLines(dataFilePath);
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split('\t');
                        if (parts.Length == 3) // Modified to include ID
                        {
                            Event loadedEvent = new Event
                            {
                                Id = int.Parse(parts[0]),
                                EventName = parts[1],
                                Location = parts[2]
                            };
                            events.Add(loadedEvent);

                            // Update the next event ID to avoid conflicts
                            if (loadedEvent.Id >= nextEventId)
                            {
                                nextEventId = loadedEvent.Id + 1;
                            }
                        }
                    }
                    Console.WriteLine("Event data loaded successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading event data: {ex.Message}");
                }
            }
        }

        // Function to save event data to the text file
        private void SaveEventData()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(dataFilePath))
                {
                    foreach (Event e in events)
                    {
                        // Use tab character as the delimiter
                        writer.WriteLine($"{e.Id}\t{e.EventName}\t{e.Location}");
                    }
                }
                Console.WriteLine("Event data saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving event data: {ex.Message}");
            }
        }
    } 
}