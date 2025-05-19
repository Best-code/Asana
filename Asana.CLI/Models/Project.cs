using System;
using System.Runtime.CompilerServices;

namespace MyApp.Models
{
    public class Project
    {

        public void Use()
        {
            int selection = 0;
            do
            {
                PrintOptions();
                selection = HandleMainMenuSelection(selection);
            } while (selection != 4);

        }

        public static void PrintOptions()
        {
            Console.WriteLine($"1. Create ToDo");
            Console.WriteLine($"2. Delete ToDo");
            Console.WriteLine($"3. Update ToDo");
            Console.WriteLine($"4. Exit");
        }

        public int HandleInput(string defaultRead)
        {
            var choice = Console.ReadLine() ?? defaultRead;
            int choiceInt;
            int.TryParse(choice, out choiceInt);

            return choiceInt;
        }

        public int HandleMainMenuSelection(int selection)
        {
            selection = HandleInput("4");
            switch (selection)
            {
                case 1:
                    CreateTodo();
                    break;
                case 2:
                    DeleteTodo();
                    break;
                case 3:
                    UpdateTodo();
                    break;
                case 4:
                    // Exit case
                    break;
                default:
                    Console.WriteLine($"You did not enter a valid selection");
                    break;
            }

            return selection;
        }
        public void CreateTodo()
        {
            ToDo createMe = new ToDo();
            Console.WriteLine($"Name: ");
            createMe.Name = Console.ReadLine();
            Console.WriteLine($"Description: ");
            createMe.Description = Console.ReadLine();

            ToDos.Add(createMe);

            Console.WriteLine($"{createMe}");

        }

        public void DeleteTodo()
        {
            if (ToDos.Count == 0)
            {
                Console.WriteLine($"This project has no ToDos");
                return;
            }
            int toDoIndex = SelectTodo("Which ToDo would you like to Delete?");
            if (toDoIndex == -1) return;

            ToDos.RemoveAt(toDoIndex);
        }

        public void UpdateTodo()
        {
            if (ToDos.Count == 0)
            {
                Console.WriteLine($"This project has no ToDos");
                return;
            }
            int toDoIndex = SelectTodo("Which ToDo would you like to Update?");
            if (toDoIndex == -1) return;

            int selection = -1;
            while (selection != 4)
            {
                Console.WriteLine($"1. Change name");
                Console.WriteLine($"2. Change description");
                Console.WriteLine($"3. Change status");
                Console.WriteLine($"4. Exit");

                selection = HandleInput("4");

                switch (selection)
                {
                    case 1:
                        Console.WriteLine($"Enter new name");
                        ToDos[toDoIndex].Name = Console.ReadLine();
                        Console.WriteLine($"Name Updated");
                        break;
                    case 2:
                        Console.WriteLine($"Enter new description");
                        ToDos[toDoIndex].Description = Console.ReadLine();
                        Console.WriteLine($"Description Updated");
                        break;
                    case 3:
                        ToDos[toDoIndex].IsComplete = !ToDos[toDoIndex].IsComplete;
                        Console.WriteLine($"Status Updated");
                        break;
                    case 4:
                        // Exit case
                        break;
                    default:
                        Console.WriteLine($"You did not enter a valid selection");
                        break;
                }
            }
        }

        public int SelectTodo(string prompt, string defaultRead = "1")
        {
            string readIndex = "";
            int toDoIndex;

            while (!int.TryParse(readIndex, out toDoIndex) || toDoIndex < 1 || toDoIndex > ToDos.Count())
            {
                Console.WriteLine(this);
                Console.WriteLine("0. Back");

                Console.WriteLine(prompt);
                readIndex = Console.ReadLine() ?? defaultRead;
                if (readIndex == "0")
                    return -1;
            }

            return toDoIndex - 1;
        }

        private int? id;
        public int? Id
        {
            get { return id; }
            set
            {
                if (value != id)
                    id = value;
            }
        }

        private string? name;
        public string? Name
        {
            get { return name; }
            set
            {
                if (value != name)
                    name = value;
            }
        }

        private string? description;
        public string? Description
        {
            get { return description; }
            set
            {
                if (value != description)
                    description = value;
            }
        }

        private float? completePercent;
        public float? CompletePercent
        {
            get { return completePercent; }
            set
            {
                if (value != completePercent)
                    completePercent = value;
            }
        }

        private List<ToDo> toDos = new List<ToDo>();
        public List<ToDo> ToDos
        {
            get { return toDos; }
            set
            {
                if (value != toDos)
                    toDos = value;
            }
        }

        public override string ToString()
        {
            string projToString = $"{name} - {description}\n";

            int toDoIndex = 0;
            foreach (ToDo toDo in toDos)
                projToString += $"{toDoIndex++ + 1}: {toDo.Name} - {toDo.IsComplete} - {toDo.Description}\n";


            return $"{projToString}";
        }

    }
}