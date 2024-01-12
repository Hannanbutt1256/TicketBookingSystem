using System;
using System.Collections.Generic;
using System.IO;
using static TicketBookingSystem.UserAccount;

namespace TicketBookingSystem
{
    internal class UserAccount
    {
        List<User> users = new List<User>(); // List to store user details
        string dataFilePath = "user_data.txt"; // Text file to store user data

        int nextUserId = 1; // Track the next available user ID
                            // Call the method on the instance

        public static void AccountUser()
        {
            UserAccount userAccount = new UserAccount();
            userAccount.Run();
        }

        public void Run()
        {
            LoadUserData(); // Load user data from the text file

                while (true)
                {
                    Console.WriteLine("Select an option:");
                    Console.WriteLine("1. New User Registration");
                    Console.WriteLine("2. All Registered User Details");
                    Console.WriteLine("3. Update User Details");
                    Console.WriteLine("4. Delete User");
                    Console.WriteLine("M. Main Menu");
                    Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();
                Console.Clear();


                switch (choice.ToUpper())
                {
                    case "M":
                        Console.WriteLine("Exiting to the main menu ");
                        return;
                    case "1":
                        Console.WriteLine("You selected: New User Registration");
                        RegisterNewUser();
                        SaveUserData();
                        Console.Clear();
                        break;
                    case "2":
                        Console.WriteLine("You selected: All Registered User Details");
                        DisplayAllUsers();
                        Console.WriteLine("All users are displayed");
                        break;
                    case "3":
                        Console.WriteLine("You selected: Update User Details");
                        UpdateUserDetails();
                        SaveUserData(); Console.Clear();
                        break;
                    case "4":
                        Console.WriteLine("You selected: Delete User");
                        DeleteUser();
                        SaveUserData(); Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please enter a valid option.");
                        break;

                }
            }
        }

        // User class to store user details
        public class User
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
        }

        // Function to register a new user
        void RegisterNewUser()
        {
            User newUser = new User();

            newUser.Id = nextUserId++;

            Console.Write("Enter Username: ");
            newUser.Username = Console.ReadLine();

            Console.Write("Enter Email: ");
            newUser.Email = Console.ReadLine();

            Console.Write("Enter Phone Number: ");
            newUser.PhoneNumber = Console.ReadLine();

            users.Add(newUser);
            Console.WriteLine("User registered successfully.");
        }

        // Function to display all registered users
       public void DisplayAllUsers()
        {
            if (users.Count == 0)
            {
                Console.WriteLine("No users registered yet.");
            }
            else
            {
                Console.WriteLine("Registered User Details:");
                foreach (var user in users)
                {
                    Console.WriteLine($"ID: {user.Id}, Username: {user.Username}, Email: {user.Email}, Phone Number: {user.PhoneNumber}");
                }
            }
        }

        // Function to update user details based on ID
        void UpdateUserDetails()
        {
            DisplayAllUsers();
            Console.Write("Enter the ID of the user to update: ");
           
            if (int.TryParse(Console.ReadLine(), out int userIdToUpdate))
            {
                User userToUpdate = users.Find(u => u.Id == userIdToUpdate);

                if (userToUpdate != null)
                {
                    Console.Write("Enter new Name: ");
                    userToUpdate.Username = Console.ReadLine();

                    Console.Write("Enter new Email: ");
                    userToUpdate.Email = Console.ReadLine();

                    Console.Write("Enter new Phone Number: ");
                    userToUpdate.PhoneNumber = Console.ReadLine();

                    Console.WriteLine("User details updated successfully.");
                }
                else
                {
                    Console.WriteLine($"User with ID '{userIdToUpdate}' not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid user ID.");
            }
        }

        // Function to delete a user based on ID
        void DeleteUser()
        {
            DisplayAllUsers();
            Console.Write("Enter the ID of the user to delete: ");
            
            if (int.TryParse(Console.ReadLine(), out int userIdToDelete))
            {
                User userToDelete = users.Find(u => u.Id == userIdToDelete);

                if (userToDelete != null)
                {
                    users.Remove(userToDelete);
                    Console.WriteLine("User deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"User with ID '{userIdToDelete}' not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid user ID.");
            }
        }

        // Function to load user data from the text file
        public void LoadUserData()
        {
            if (File.Exists(dataFilePath))
            {
                try
                {
                    string[] lines = File.ReadAllLines(dataFilePath);
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split('\t');
                        if (parts.Length == 4) // Modified to include ID
                        {
                            User loadedUser = new User
                            {
                                Id = int.Parse(parts[0]),
                                Username = parts[1],
                                Email = parts[2],
                                 PhoneNumber = parts[3]
                            };
                            users.Add(loadedUser);

                            // Update the next user ID to avoid conflicts
                            if (loadedUser.Id >= nextUserId)
                            {
                                nextUserId = loadedUser.Id + 1;
                            }
                        }
                    }
                    Console.WriteLine("User data loaded successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading user data: {ex.Message}");
                }
            }
        }

        // Function to save user data to the text file
        void SaveUserData()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(dataFilePath))
                {
                    foreach (User user in users)
                    {
                        // Use tab character as the delimiter
                        writer.WriteLine($"{user.Id}\t{user.Username}\t{user.Email}\t{user.PhoneNumber}");
                    }
                }
                Console.WriteLine("User data saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving user data: {ex.Message}");
            }
        }
    }
}