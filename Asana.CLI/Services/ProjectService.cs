using System;

namespace Asana.CLI.Services;

public class ProjectService
{
    int id;
    string name;
    public ProjectService(string name, int id)
    {
        this.id = id;
        this.name = name;
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

    public void Run(string name)
    {
        int selection = -1;
        while (selection != 0)
        {
            Console.WriteLine($"Project {name}");
            PrintOptions();
            selection = HandleMainMenuSelection(selection);
        }
    }

    private static string[] projectOptions = {
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

        Console.Write($"Name: ");
        var toDoName = Console.ReadLine() ?? "ToDo";
        if (toDoName == "") toDoName = "ToDo";
        Console.Write($"Description: ");
        var toDoDescription = Console.ReadLine(); ;
        ToDo createTask = new ToDo(toDoName, id) { Description = toDoDescription };

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
            Console.WriteLine($"ToDo {name}");

            Console.WriteLine($"1. Change name");
            Console.WriteLine($"2. Change description");
            Console.WriteLine($"3. Change status");
            Console.WriteLine($"0. Back\n");

            selection = HandleInput("0");

            switch (selection)
            {
                case 1:
                    Console.Write($"Name: ");

                    // Set null or empty string to 'ToDo'
                    ToDos[toDoIndex].Name = Console.ReadLine() ?? "ToDo";
                    if (ToDos[toDoIndex].Name == "") ToDos[toDoIndex].Name = "ToDo";
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

    // Prints each todo in this project name, completion, description
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

        Console.WriteLine();
    }

    // Select a valid todo with bounds checking. Bad input results in 0
    public int SelectTodo(string prompt, string defaultRead = "0")
    {
        string readIndex = "";
        int toDoIndex;

        while (!int.TryParse(readIndex, out toDoIndex) || toDoIndex < 1 || toDoIndex > ToDos.Count())
        {
            Console.Write(ToString());
            Console.WriteLine("0. Back\n");

            Console.WriteLine(prompt);
            readIndex = Console.ReadLine() ?? defaultRead;
            if (readIndex == "0")
                return -1;
        }

        Console.WriteLine();

        return toDoIndex - 1;
    }

    // List Project Name, Description and loops over all the Tasks showing name, completion, description
    public override string ToString()
    {
        string projToString = $"Project {name}\n";

        int toDoIndex = 0;
        foreach (ToDo toDo in ToDos)
            projToString += $"{toDoIndex++ + 1}: {toDo.Name} - {toDo.IsComplete} - {toDo.Description}\n";

        return projToString;
    }

}
