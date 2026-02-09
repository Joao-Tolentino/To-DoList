// A simple to-do list with checkboxes and a UI, order options, and a description field alongside the entry title.
// Imports
using System.Text.Json;

namespace ToDoList
{
    // Definition of the entry class, for each individual entry in the to-do list.
    public class Entry
    {
        // Define the properties of an entry in the to-do list.
        public string  Title { get; set;}
        public string Description { get; set;}
        public string Priority { get; set;}
        public DateOnly DueDate { get; set;}
        public DateOnly CreatedAt { get; set;}
        public bool IsCompleted { get; set;}

        // Define the constructor of the class and initialization of the properties.
        public Entry(string title, string description, string priority, DateOnly dueDate)
        {
            Title = title.UppercaseFirstWord();
            Description = description;
            Priority = priority.UppercaseFirstWord();
            CreatedAt = DateOnly.FromDateTime(DateTime.Now);
            DueDate = dueDate;
            IsCompleted = false;
        }

        // Define a method to change the description of an entry.
        public void ChangeDescription(string description)
        {
            Description = description;
        }

        // Define a method to change the priority of an entry.
        public void ChangePriority(string priority)
        {
            // check if input is valid.
            switch (priority.ToLower())
            {
                case "low":
                case "medium":
                case "high":
                    Priority = priority;
                    break;
                default:
                    Console.WriteLine("Invalid priority. Please enter 'Low', 'Medium', or 'High'.");
                    break;
            }
        }

        // Define a method to change the due date of an entry.
        public void ChangeDueDate(DateOnly newDueDate)
        {
            DueDate = newDueDate;
        }

        // Define a method to work as a checkbox, toggling the status.
        public void ToggleCompleted()
        {
            IsCompleted = !IsCompleted;
        }
    }

    public static class StringExtensions
    {
        // Define an extension method to convert the first letter of a string to uppercase and the rest to lowercase.
        public static string UppercaseFirstWord ( this string value )
        {
            if (String.IsNullOrEmpty(value))
                return "";

            value = value.ToLower();
            return Char.ToUpper(value[0]) + value.Substring(1);
        }
    }

    public static class ListSaveLoad
    {
        public static void SaveList(List<Entry> entries, string filePath)
        {
            string json = JsonSerializer.Serialize(entries);
            File.WriteAllText(filePath, json);
        }

        public static List<Entry> LoadList(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found. Returning an empty list.");
                return new List<Entry>();
            }

            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Entry>>(json) ?? new List<Entry>();
        }
    }

    class App
    {
        public static void Main(string[] args)
        {
            // Define a list to hold the entries
            List<Entry> entries = new List<Entry>();

            // Define a flag for the user interactions in loop.
            bool flag = true;

            // Define the variables to be used for user input.
            int choice = 0;
            string title = "";
            string description = "";
            string priority = "";
            int toggleIndex = 0;
            DateOnly dueDate = DateOnly.FromDateTime(DateTime.Now);

            // Load the list from a file if it exists.
            string filePath = "./data.json";
            entries = ListSaveLoad.LoadList(filePath);

            // Main loop
            while (true)
            {   
                // Define the variable to hold the index for the change functions.
                int changeIndex = 0;
                // Display the to-do list already in the system.
                Console.WriteLine("\n-----------------------To-Do List-----------------------");
                for (int i = 0; i < entries.Count; i++)
                {
                    var entry = entries[i];
                    // Display each entry individually with its properties.
                    Console.WriteLine($"{i + 1}. [{(entry.IsCompleted ? "x" : " ")}] {entry.Title} \n{entry.Priority} priority \nDue to: {entry.DueDate} \nCreation: {entry.CreatedAt} \nDescription: {entry.Description}.");
                    // Print a separation line in between entries for better visualization.
                    Console.WriteLine("---------------------------------------------------------");
                }

                // Display available options and guide user input.
                Console.WriteLine("\nOptions:");
                Console.WriteLine("1. Add Entry");
                Console.WriteLine("2. Toggle Entry Completion");
                Console.WriteLine("3. Change Entry Description");
                Console.WriteLine("4. Change Entry Priority ");
                Console.WriteLine("5. Change Entry Due Date");
                Console.WriteLine("6. Delete an Entry");
                Console.WriteLine("7. Exit and Save");

                do
                {   
                    
                    Console.Write("Enter your choice: ");
                    flag = int.TryParse(Console.ReadLine(), out choice);
                    if (!flag)
                    {
                        Console.WriteLine("Invalid input. Please enter a number corresponding to the options.");
                    }
                }while (!flag);

                switch (choice)
                {
                    case 1:
                        // Create a new entry and append it to the list.
                        do
                        {
                            Console.Write("Enter title: ");
                            title = Console.ReadLine();
                            if(string.IsNullOrEmpty(title))
                            {
                                Console.WriteLine("Title cannot be empty. Please enter a valid title.");
                            }
                        }while (string.IsNullOrEmpty(title));
                        do
                        {
                            Console.Write("Enter description: ");
                            description = Console.ReadLine();
                            if(string.IsNullOrEmpty(description))
                            {
                                Console.WriteLine("Description cannot be empty. Please enter a valid description.");
                            }
                        }while (string.IsNullOrEmpty(description));
                        do
                        {
                            do
                            { 
                                Console.Write("Enter priority (Low, Medium, High): ");
                                priority = Console.ReadLine();
                                if(string.IsNullOrEmpty(priority))
                                {
                                    Console.WriteLine("Priority cannot be empty. Please enter a valid priority.");
                                    flag = false;
                                }
                            }while (string.IsNullOrEmpty(priority));
                            if(priority.ToLower() != "low" && priority.ToLower() != "medium" && priority.ToLower() != "high")
                            {
                                Console.WriteLine("Invalid priority. Please enter 'Low', 'Medium', or 'High'.");
                                flag = false;
                            }
                        }while (!flag);
                        Console.Write("Enter due date (DD/MM/YYYY): ");
                        do
                        {
                            flag = DateOnly.TryParse(Console.ReadLine(), out dueDate);
                            if (!flag)
                            {
                                Console.WriteLine("Invalid date format. Please enter a valid date. Format: DD/MM/YYYY");
                            }
                        }while (!flag);
                        entries.Add(new Entry(title, description, priority, dueDate));
                        break;
                    case 2:
                        // Toggle the completion status of an entry.
                        Console.Write("Enter entry number to toggle: ");
                        do
                        {
                            flag = int.TryParse(Console.ReadLine(), out toggleIndex);
                            toggleIndex -= 1; // Adjust for 0-based index.
                            if (toggleIndex >= 0 && toggleIndex < entries.Count)
                            {
                                entries[toggleIndex].ToggleCompleted();
                            }
                            else
                            {
                                Console.WriteLine("Invalid entry number. Please try again.");
                                flag = false;
                            }
                        }while (!flag);
                        break;
                    case 3:
                        // Get the entry and update its description.
                        Console.Write("Enter entry number to change description: ");
                        do
                        {
                            flag = int.TryParse(Console.ReadLine(), out toggleIndex);
                            toggleIndex -= 1; // Adjust for 0-based index.
                            if (changeIndex >= 0 && changeIndex < entries.Count)
                            {
                                do
                                {
                                    Console.Write("Enter new description: ");
                                    description = Console.ReadLine();
                                    if(string.IsNullOrEmpty(description))
                                    {
                                        Console.WriteLine("Description cannot be empty. Please enter a valid description.");
                                        flag = false;
                                    }
                                }while (string.IsNullOrEmpty(description));
                                entries[changeIndex].ChangeDescription(description);
                            }else
                            {
                                Console.WriteLine("Invalid entry number. Please try again.");
                                flag = false;
                            }
                        }while (!flag);
                        break;
                    case 4:
                        // Get the entry and update its priority.
                        Console.Write("Enter entry number to change priority: ");
                        do
                        {
                            flag = int.TryParse(Console.ReadLine(), out toggleIndex);
                            toggleIndex -= 1; // Adjust for 0-based index.
                            if (changeIndex >= 0 && changeIndex < entries.Count)
                            {
                                do
                                {
                                    Console.Write("Enter new priority (Low, Medium, High): ");
                                    priority = Console.ReadLine();
                                    if(priority.ToLower() != "low" && priority.ToLower() != "medium" && priority.ToLower() != "high")
                                    {
                                        Console.WriteLine("Invalid priority. Please enter 'Low', 'Medium', or 'High'.");
                                        flag = false;
                                    }
                                }while (string.IsNullOrEmpty(priority));

                                entries[changeIndex].ChangePriority(priority);
                            }
                        }while (!flag);
                        break;
                    case 5:
                        // Get the entry and update its due date.
                        Console.Write("Enter entry number to change due date: ");
                        do
                        {
                            flag = int.TryParse(Console.ReadLine(), out toggleIndex);
                            toggleIndex -= 1; // Adjust for 0-based index.
                            if (changeIndex >= 0 && changeIndex < entries.Count)
                            {
                                while(!flag)
                                {
                                    Console.Write("Enter new due date (DD/MM/YYYY): ");
                                    flag = DateOnly.TryParse(Console.ReadLine(), out dueDate);
                                    if (!flag)
                                    {
                                        Console.WriteLine("Invalid date format. Please enter a valid date. Format: DD/MM/YYYY");
                                    }
                                }
                                entries[changeIndex].ChangeDueDate(dueDate);
                            }
                            else
                            {
                                Console.WriteLine("Invalid entry number. Please try again.");
                                flag = false;
                            }
                        }while (!flag);
                        break;
                    case 6:
                        // Delete an entry.
                        Console.Write("Enter entry number to delete: ");
                        do
                        {
                            flag = int.TryParse(Console.ReadLine(), out toggleIndex);
                            toggleIndex -= 1; // Adjust for 0-based index.
                            if (toggleIndex >= 0 && toggleIndex < entries.Count)
                            {
                                entries.RemoveAt(toggleIndex);
                            }
                            else
                            {
                                Console.WriteLine("Invalid entry number. Please try again.");
                                flag = false;
                            }
                        }while (!flag); 
                        break;
                    case 7:
                        //Save and break the loop to exit the application.
                        ListSaveLoad.SaveList(entries, filePath);
                        return;
                    default:
                        // Allow user to retry in case of invalid input.
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }    
    }
}