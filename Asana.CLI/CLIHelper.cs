using Asana.Core.Models;
using Asana.Core.Interfaces;
using Asana.Core.Services;
using System.Collections.ObjectModel;
namespace Asana.CLI;

public static class CLIHelper
{

    // Main loop
    public static void run()
    {
        List<Project> Projects = UnitService.Current.Projects.ToList();
        int selection = -1;
        while (selection != 0)
        {
            PrintUnitOptions();
            selection = HandleUnitOptionSelection(Projects);
        }
    }

    // Prints out the Unit Options
    private static void PrintUnitOptions()
    {
        Console.WriteLine("1. Create Project");
        Console.WriteLine("2. Delete Project");
        Console.WriteLine("3. Update Project");
        Console.WriteLine("4. List All Projects");
        Console.WriteLine("5. List All Todos");
        Console.WriteLine("0. Exit\n");
    }

    public static int HandleUnitMenu(string defaultRead)
    {
        var choice = Console.ReadLine() ?? defaultRead;
        int choiceInt;
        int.TryParse(choice, out choiceInt);

        return choiceInt;
    }

    private static int HandleUnitOptionSelection(List<Project> Projects)
    {
        int selection = HandleUnitMenu("0");
        Console.WriteLine();
        switch (selection)
        {
            case 1:
                Console.Write($"Name: ");
                var projectName = Console.ReadLine() ?? "Project";
                if (projectName == "") projectName = "Project";
                Console.Write($"Description: ");
                var projectDescription = Console.ReadLine() ?? "";
                Console.WriteLine();
                Project addProject = new Project() { Name = projectName, Description = projectDescription };

                UnitService.Current.AddUpdateProject(addProject);
                break;
            case 2:
                int projIndex = SelectItem("Which Project would you like to Delete?\n", Projects);
                if (projIndex == -1) break;
                UnitService.Current.DeleteProject(UnitService.Current.GetProjectAt(projIndex));
                break;
            case 3:
                UpdateProject(Projects);
                break;
            // List all projects
            case 4:
                {
                    if (!Projects.Any())
                    {
                        Console.WriteLine($"There are no Projects - Create a project first to add ToDos\n");
                        break;
                    }

                    int index = 1;
                    foreach (Project project in Projects)
                    {
                        Console.WriteLine($"{index}. {project.Name} - {project.Description} - {project.CompletePercent * 100}%");
                    }
                    Console.WriteLine();
                }
                break;
            // List all todos
            case 5:
                {
                    if (!Projects.Any()) Console.WriteLine($"There are no Units - Create a unit first to add ToDos");

                    int pIndex = 1;
                    foreach (Project project in Projects)
                    {
                        int tIndex = 1;
                        Console.WriteLine($"{pIndex}. {project.Name} - {project.Description} - {project.CompletePercent * 100}% Complete");
                        if (project.ToDos.Any())
                        {
                            foreach (ToDo toDo in project.ToDos)
                            {
                                Console.WriteLine($">  {tIndex}. {toDo.Name} - {(toDo.IsComplete ? "Complete" : "Incomplete")} - {toDo.Description}");
                            }
                            Console.WriteLine();
                        }
                        else
                            Console.WriteLine($">  No ToDos\n");
                    }
                    Console.WriteLine();
                }
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


    public static void UpdateProject(List<Project> Projects)
    {
        if (Projects.Count == 0)
        {
            Console.WriteLine($"This Unit has no Projects\n");
            return;
        }
        // Select the project to update
        int projectIndex = SelectItem("Which Project would you like to Update?\n", Projects);
        if (projectIndex == -1) return;

        // After you have selected the project to update
        // Select which part you would like to update for that project
        int selection = -1;
        while (selection != 0)
        {
            Console.WriteLine($"Project {Projects[projectIndex].Name}");
            Console.WriteLine($"1. Change name");
            Console.WriteLine($"2. Change description");
            Console.WriteLine($"3. Create ToDo");
            Console.WriteLine($"4. Delete ToDo");
            Console.WriteLine($"5. Update ToDos");
            Console.WriteLine($"6. List ToDos");
            Console.WriteLine($"0. Exit Project {Projects[projectIndex].Name}\n");

            // Take input but bad input defaults to 0
            selection = HandleUnitMenu("0");
            Project currentProject = Projects[projectIndex];

            Console.WriteLine();
            switch (selection)
            {
                // Update name
                case 1:
                    Console.Write($"Name: ");
                    if (UnitService.Current.UpdateProjectName(projectIndex, Console.ReadLine() ?? "Project"))
                        Console.WriteLine($"Name Updated to {currentProject.Name}\n");
                    break;
                // Update description
                case 2:
                    Console.Write($"Description: ");
                    if (UnitService.Current.UpdateProjectDescription(projectIndex, Console.ReadLine() ?? ""))
                        Console.WriteLine($"{currentProject.Name} Description Updated\n");
                    break;
                // Create ToDo
                case 3:
                    Console.Write($"Name: ");
                    var toDoName = Console.ReadLine() ?? "ToDo";
                    if (toDoName == "") toDoName = "ToDo";
                    Console.Write($"Description: ");
                    var toDoDescription = Console.ReadLine() ?? "";
                    Console.WriteLine();
                    ToDo addMe = new ToDo() { Name = toDoName, Description = toDoDescription, DueDate = DateTime.Now };

                    ProjectService.Current.AddUpdateToDo(addMe);
                    break;
                // Delete ToDo
                case 4:
                    if (!currentProject.ToDos.Any())
                    {
                        Console.WriteLine($"This project has no ToDos yet\n");
                        break;
                    }
                    // int toDoIndex = SelectItem(unit, "Which ToDo would you like to Delete?\n", currentProject.ToDos);
                    
                    // TODO: Update this to work with GetById not index
                    // int toDoIndex = 0;
                    // if (toDoIndex == -1) break;
                    // ProjectService.Current.DeleteTodo(ProjectService.Current.GetToDoById(toDoIndex));
                    break;
                // Update ToDo
                case 5:
                    if (!ProjectService.Current.ToDos.Any())
                    {
                        Console.WriteLine($"This project has no ToDos yet\n");
                        break;
                    }
                    UpdateTodo(currentProject);
                    break;
                case 6:
                    int tIndex = 1;
                    if (ProjectService.Current.ToDos.Any())
                    {
                        foreach (ToDo toDo in ProjectService.Current.ToDos)
                        {
                            Console.WriteLine($">  {tIndex}. {toDo.Name} - {(toDo.IsComplete ? "Complete" : "Incomplete")} - {toDo.Description}");
                        }
                        Console.WriteLine();
                    }
                    else
                        Console.WriteLine($">  No ToDos\n");
                    break;
                case 0:
                    break;
                default:
                    Console.WriteLine($"You did not enter a valid selection\n");
                    break;
            }
        }
    }

    private static void UpdateTodo(Project project)
    {
        int toDoIndex = SelectItem<ToDo>("Which ToDo would you like to Update?\n", project.ToDos.ToList());
        if (toDoIndex == -1) return;

        int selection = -1;
        while (selection != 0)
        {
            Console.WriteLine($"ToDo {project.Name}");

            Console.WriteLine($"1. Change name");
            Console.WriteLine($"2. Change description");
            Console.WriteLine($"3. Change status");
            Console.WriteLine($"0. Back\n");

            selection = HandleUnitMenu("0");

            switch (selection)
            {
                case 1:
                    Console.Write($"Name: ");
                    if (UpdateTodoName(toDoIndex, Console.ReadLine() ?? "ToDo"))
                        Console.WriteLine($"Name Updated to {project.ToDos[toDoIndex].Name}\n");
                    break;
                case 2:
                    Console.Write($"Description: ");
                    if (UpdateTodoDescription(toDoIndex, Console.ReadLine() ?? "ToDo"))
                        Console.WriteLine($"{project.ToDos[toDoIndex].Name} Description Updated\n");
                    break;
                case 3:
                    UpdateTodoStatus(toDoIndex, !project.ToDos[toDoIndex].IsComplete);
                    Console.WriteLine($"{project.ToDos[toDoIndex].Name} Status Updated to {(project.ToDos[toDoIndex].IsComplete ? "Complete" : "Incomplete")}\n");
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

    // Ensures you select a valid item with bounds checking - Returns the display value -1 for 0 index
    private static int SelectItem<T>(string prompt, List<T> itemsList, string defaultRead = "0") where T : INameDescription
    {
        string readIndex = "";
        int projectIndex;

        while (!int.TryParse(readIndex, out projectIndex) || projectIndex < 1 || projectIndex > itemsList.Count())
        {
            // Print each project
            int index = 1;
            foreach (T item in itemsList)
            {
                Console.WriteLine($"{index++}. {item.Name} - {item.Description}");
            }
            Console.WriteLine("0. Back");

            // Print prompt and get number
            Console.WriteLine(prompt);
            readIndex = Console.ReadLine() ?? defaultRead;
            if (readIndex == "0")
                return -1;
        }

        Console.WriteLine();

        return projectIndex - 1;
    }


    public static bool UpdateTodoName(int toDoIndex, string name)
    {
        if (ProjectService.Current.ToDos.Count == 0)
            return false;
        if (toDoIndex < 0 || toDoIndex > ProjectService.Current.ToDos.Count())
            return false;

        ProjectService.Current.ToDos[toDoIndex].Name = name;
        return true;
    }

    public static bool UpdateTodoDescription(int toDoIndex, string description)
    {
        if (ProjectService.Current.ToDos.Count == 0)
            return false;
        if (toDoIndex < 0 || toDoIndex > ProjectService.Current.ToDos.Count())
            return false;


        ProjectService.Current.ToDos[toDoIndex].Description = description;
        return true;
    }

    public static bool UpdateTodoDueDate(int toDoIndex, DateTime dueDate)
    {
        if (ProjectService.Current.ToDos.Count == 0)
            return false;
        if (toDoIndex < 0 || toDoIndex > ProjectService.Current.ToDos.Count())
            return false;

        ProjectService.Current.ToDos[toDoIndex].DueDate = dueDate;
        return true;
    }

    public static bool UpdateTodoStatus(int toDoIndex, bool status)
    {
        if (ProjectService.Current.ToDos.Count == 0)
            return false;
        if (toDoIndex < 0 || toDoIndex > ProjectService.Current.ToDos.Count())
            return false;

        ProjectService.Current.ToDos[toDoIndex].IsComplete = status;
        return true;
    }

}
