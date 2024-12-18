using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ContactList
{
    // Represents a contact
    class Contact
    {
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }

    // Main class for user interaction
    class ContactListCLI
    {
        private static List<Contact> contacts = new List<Contact>();

        static void Main(string[] args)
        {
            LoadContacts();
            SortContactsByName();
            ShowMainMenu();
        }

        // Displays the main menu
        static void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Contact List");
            Console.WriteLine("----------------------------");
            Console.WriteLine("1. Add Contact");
            Console.WriteLine("2. Remove Contact");
            Console.WriteLine("3. List Contacts");
            Console.WriteLine("4. Search Contact");
            Console.WriteLine("5. Exit");
            Console.WriteLine("----------------------------");
            int option = GetValidNumber("Choose a number: ");
            switch (option)
            {
                case 1: AddContact(); break;
                case 2: RemoveContact(); break;
                case 3: ListContacts(); break;
                case 4: SearchContact(); break;
                case 5: Console.WriteLine("Goodbye!"); break;
                default: ShowMainMenu(); break;
            }
        }

        // Loads the contacts from a JSON file
        static void LoadContacts()
        {
            string filePath = "contacts.json";
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                contacts = JsonSerializer.Deserialize<List<Contact>>(json) ?? new List<Contact>();
            }
        }

        // Adds a new contact
        static void AddContact()
        {
            Console.Clear();
            Console.WriteLine("You need to enter a name, and at least a phone number or an email.");
            Console.WriteLine("--------------------------------");
            string name = GetValidName("Name: ");

            bool addPhoneNumber = false;
            bool addEmail = false;
            do
            {
                addPhoneNumber = GetYesOrNo("Do you want to add a phone number? (y/n) ");
                addEmail = GetYesOrNo("Do you want to add an email? (y/n) ");

                if (!addPhoneNumber && !addEmail)
                {
                    Console.WriteLine("ERROR: You must add at least one of the fields: phone number or email.");
                }
            } while (!addPhoneNumber && !addEmail);

            string? phoneNumber = addPhoneNumber ? GetValidPhoneNumber() : null;
            string? email = addEmail ? GetValidEmail("Email: ") : null;

            contacts.Add(new Contact { Name = name, PhoneNumber = phoneNumber, Email = email });
            SaveContacts();
            Console.WriteLine("Contact saved successfully!");
            Console.WriteLine("--------------------------------\n");

            if (GetYesOrNo("Do you want to add another contact? (Y/N): "))
                ShowMainMenu();
        }

        // Saves the contacts to the JSON file
        static void SaveContacts()
        {
            string filePath = "contacts.json";
            string json = JsonSerializer.Serialize(contacts, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        // Removes a contact from the list
        static void RemoveContact()
        {
            Console.Clear();
            if (contacts.Count == 0)
            {
                Console.WriteLine("No contacts available to remove.");
                Console.WriteLine("--------------------------------\n");
                return;
            }

            bool removeAnother = true;
            while (removeAnother)
            {
                Console.WriteLine("List of Contacts:");
                ListContacts();

                int index = -1;
                while (index < 1 || index > contacts.Count)
                {
                    Console.WriteLine($"Enter the index of the contact you want to remove (1 to {contacts.Count}):");
                    if (int.TryParse(Console.ReadLine(), out index) && index >= 1 && index <= contacts.Count)
                    {
                        contacts.RemoveAt(index - 1);
                        SaveContacts();
                        Console.WriteLine("Contact removed successfully!");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"ERROR: Invalid index. Please enter a valid number between 1 and {contacts.Count}.");
                    }
                }

                Console.WriteLine("--------------------------------\n");
                removeAnother = GetYesOrNo("Do you want to go back to the menu? (Y/N): ");
                if (removeAnother) ShowMainMenu();
            }
        }

        // Lists all the contacts
        static void ListContacts()
        {
            if (contacts.Count == 0)
            {
                Console.WriteLine("No contacts found.");
                return;
            }

            int i = 0;
            foreach (var contact in contacts)
            {
                i++;
                Console.WriteLine($"{i}. Name: {contact.Name ?? "No Name"}, Phone: {contact.PhoneNumber ?? "No Phone"}, Email: {contact.Email ?? "No Email"}");
            }

            Console.WriteLine("--------------------------------\n");
            if (GetYesOrNo("Do you want to remove a contact? (Y/N): "))
                ShowMainMenu();
        }

        // Sorts the contacts by name
        static void SortContactsByName()
        {
            contacts = contacts.OrderBy(c => c.Name).ToList();
        }

        // Searches for a contact by name
        static void SearchContact()
        {
            Console.Clear();
            string targetName = GetValidName("Enter the name to search: ");

            int low = 0, high = contacts.Count - 1;

            while (low <= high)
            {
                int mid = (low + high) / 2;
                int comparison = string.Compare(contacts[mid].Name ?? "", targetName, StringComparison.OrdinalIgnoreCase);

                if (comparison == 0)
                {
                    Console.WriteLine($"Contact found: {contacts[mid].Name}, {contacts[mid].PhoneNumber}, {contacts[mid].Email}");
                    return;
                }
                else if (comparison < 0) low = mid + 1;
                else high = mid - 1;
            }

            Console.WriteLine("Contact not found.");
            Console.WriteLine("--------------------------------");
            if (GetYesOrNo("Do you want to search another contact? (Y/N): "))
                ShowMainMenu();
        }

        // Function to validate and get an email
        static string GetValidEmail(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();
                if (string.IsNullOrEmpty(input) || (input.Contains('@') && input.Contains('.')))
                    return input;
                else
                {
                    Console.WriteLine("Invalid email. Please enter a valid email.");
                }
            }
        }

        // Function to validate a phone number
        static string GetValidPhoneNumber()
        {
            while (true)
            {
                Console.Write("Enter a phone number (numbers only, e.g., 234567890): ");
                string? number = Console.ReadLine();

                if (number.Length == 10 && number.All(char.IsDigit))
                {
                    string formattedNumber = number.Insert(3, "-").Insert(7, "-");
                    return formattedNumber;
                }
                else
                {
                    Console.WriteLine("Invalid phone number. Please enter exactly 10 digits.");
                }
            }
        }

        // Function to validate a name
        static string GetValidName(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("ERROR: Invalid name. Please try again.");
                }
            }
        }

        // Function to get a valid number for the menu
        static int GetValidNumber(string prompt)
        {
            int number;
            while (true)
            {
                Console.Write(prompt);
                var input = Console.ReadLine();

                if (int.TryParse(input, out number) && number >= 1 && number <= 5)
                {
                    return number;
                }
                else
                {
                    Console.WriteLine("ERROR: Invalid number. Please enter a valid number between 1 and 5.");
                }
            }
        }

        // Function to ask the user if they want to continue
        static bool GetYesOrNo(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine()?.Trim().ToLower();

                if (input == "y")
                {
                    return true;
                }
                else if (input == "n")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'y' for Yes or 'n' for No.");
                }
            }
        }
    }
}
