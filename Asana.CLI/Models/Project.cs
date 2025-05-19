using System;
using System.Runtime.CompilerServices;

namespace MyApp.Models
{
    public class Project
    {

        public void Use()
        {
            int selection = -1;
            while (selection != 0)
            {
                Console.WriteLine($"Project {name}");
                PrintOptions();
                selection = HandleMainMenuSelection(selection);
            }
        }

        public static string[] projectOptions = {
            "Create ToDo",
            "Delete ToDo",
            "Update ToDo",
            "List ToDos",
        };

        public void PrintOptions()
        {
            int optionIndex = 0;
            foreach (string option in projectOptions)
            {
                Console.WriteLine($"{optionIndex++ + 1}. {option}");
            }
            Console.WriteLine($"0. Back\n");

        }

        static public int HandleInput(string defaultRead)
        {
            var choice = Console.ReadLine() ?? defaultRead;
            int choiceInt;
            int.TryParse(choice, out choiceInt);

            return choiceInt;
        }

        public int HandleMainMenuSelection(int selection)
        {
            selection = HandleInput("0");
            Console.WriteLine();
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
                    ListTodos();
                    break;
                case 0:
                    // Exit case
                    break;
                default:
                    Console.WriteLine($"You did not enter a valid selection\n");
                    break;
            }

            return selection;
        }
        public void CreateTodo()
        {
            ToDo createTask = new ToDo();
            Console.Write($"Name: ");
            createTask.Name = Console.ReadLine();
            Console.Write($"Description: ");
            createTask.Description = Console.ReadLine();

            ToDos.Add(createTask);

            Console.WriteLine($"Todo '{createTask.Name}' created\n");
        }

        public void DeleteTodo()
        {
            if (ToDos.Count == 0)
            {
                Console.WriteLine($"This project has no ToDos\n");
                return;
            }
            int toDoIndex = SelectTodo("Which ToDo would you like to Delete?\n");
            if (toDoIndex == -1) return;

            ToDos.RemoveAt(toDoIndex);
        }

        public void UpdateTodo()
        {
            if (ToDos.Count == 0)
            {
                Console.WriteLine($"This project has no ToDos\n");
                return;
            }
            int toDoIndex = SelectTodo("Which ToDo would you like to Update?\n");
            if (toDoIndex == -1) return;

            int selection = -1;
            while (selection != 0)
            {
                Console.WriteLine($"ToDo {Name}");

                Console.WriteLine($"1. Change name");
                Console.WriteLine($"2. Change description");
                Console.WriteLine($"3. Change status");
                Console.WriteLine($"0. Back\n");

                selection = HandleInput("0");

                switch (selection)
                {
                    case 1:
                        Console.Write($"Name: ");
                        ToDos[toDoIndex].Name = Console.ReadLine();
                        Console.WriteLine($"Name Updated to {ToDos[toDoIndex].Name}\n");
                        break;
                    case 2:
                        Console.Write($"Description: ");
                        ToDos[toDoIndex].Description = Console.ReadLine();
                        Console.WriteLine($"{ToDos[toDoIndex].Name} Description Updated\n");
                        break;
                    case 3:
                        ToDos[toDoIndex].IsComplete = !ToDos[toDoIndex].IsComplete;
                        Console.WriteLine($"{ToDos[toDoIndex].Name} Status Updated to {(ToDos[toDoIndex].IsComplete ? "Complete" : "Incomplete")}\n");
                        break;
                    case 0:
                        // Exit case
                        break;
                    default:
                        Console.WriteLine($"You did not enter a valid selection\n");
                        break;
                }
            }
        }

        public void ListTodos()
        {
            if (ToDos.Count() == 0)
            {
                Console.WriteLine($"This project has no ToDos\n");
                return;
            }
            int toDoIndex = 0;
            foreach (ToDo toDo in ToDos)
                Console.WriteLine($"{toDoIndex++ + 1}. {toDo}");
        }

        public int SelectTodo(string prompt, string defaultRead = "0")
        {
            string readIndex = "";
            int toDoIndex;

            while (!int.TryParse(readIndex, out toDoIndex) || toDoIndex < 1 || toDoIndex > ToDos.Count())
            {
                Console.Write(this);
                Console.WriteLine("0. Back\n");

                Console.WriteLine(prompt);
                readIndex = Console.ReadLine() ?? defaultRead;
                if (readIndex == "0")
                    return -1;
            }

            Console.WriteLine();

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

        public float CompletePercent()
        {
            float complete = 0;
            float incomplete = 0;
            foreach (ToDo toDo in ToDos)
            {
                if (toDo.IsComplete)
                    complete++;
                else
                    incomplete++;
            }

            if (incomplete == 0) return 1.0f;

            return complete / ToDos.Count();
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
            string projToString = $"Project {name} - {description}\n";

            int toDoIndex = 0;
            foreach (ToDo toDo in toDos)
                projToString += $"{toDoIndex++ + 1}: {toDo.Name} - {toDo.IsComplete} - {toDo.Description}\n";

            return projToString;
        }

    }
}