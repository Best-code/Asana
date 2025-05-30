using System;
namespace Asana.CLI.Services;

public static class UnitService
{
    private static List<Project> projects = new List<Project>();
    public static List<Project> Projects
    {
        get { return projects; }
        set
        {
            if (value != projects)
                projects = value;
        }
    }

    // Create a project
    public static void CreateProject()
    {
        Console.Write($"Name: ");
        // Set null or empty string to 'Project'
        var projectName = Console.ReadLine() ?? "Project";
        if (projectName == "") projectName = "Project";
        Console.Write($"Description: ");
        var projectDescription = Console.ReadLine(); ;


        Project createProject = new Project(projectName) { Description = projectDescription };
        projects.Add(createProject);

        Console.WriteLine($"Project '{createProject.Name}' created\n");

    }

    public static void DeleteProject()
    {
        if (projects.Count == 0)
        {
            Console.WriteLine($"This Unit has no Projects\n");
            return;
        }
        int toDoIndex = SelectProject("Which Project would you like to Delete?\n");
        if (toDoIndex == -1) return;

        Projects.RemoveAt(toDoIndex);
    }

    public static void UpdateProject()
    {
        if (Projects.Count == 0)
        {
            Console.WriteLine($"This Unit has no Projects\n");
            return;
        }
        // Select the project to update
        int projectIndex = SelectProject("Which Project would you like to Update?\n");
        if (projectIndex == -1) return;

        // After you have selected the project to update
        // Select which part you would like to update for that project
        int selection = -1;
        while (selection != 0)
        {
            Console.WriteLine($"Project {projects[projectIndex].Name}");
            Console.WriteLine($"1. Change name");
            Console.WriteLine($"2. Change description");
            Console.WriteLine($"3. Update ToDos");
            Console.WriteLine($"0. Exit Project {projects[projectIndex].Name}\n");

            // Take input but bad input defaults to 0
            selection = Project.HandleInput("0");

            Console.WriteLine();
            switch (selection)
            {
                case 1:
                    Console.Write($"Name: ");
                    // Set null or empty string to 'Project'
                    Projects[projectIndex].Name = Console.ReadLine() ?? "Project";
                    if (Projects[projectIndex].Name == "") Projects[projectIndex].Name = "Project";

                    Console.WriteLine($"Name Updated to {projects[projectIndex].Name}\n");
                    break;
                case 2:
                    Console.Write($"Description: ");
                    projects[projectIndex].Description = Console.ReadLine();
                    Console.WriteLine($"{projects[projectIndex].Name} Description Updated\n");
                    break;
                case 3:
                    // Enter than project loop where you see the options of each project like add task and stuff
                    projects[projectIndex].Run();
                    break;
                case 0:
                    break;
                default:
                    Console.WriteLine($"You did not enter a valid selection\n");
                    break;
            }
        }
    }

    // Ensures you select a valid project with bounds checking
    private static int SelectProject(string prompt, string defaultRead = "0")
    {
        string readIndex = "";
        int projectIndex;

        while (!int.TryParse(readIndex, out projectIndex) || projectIndex < 1 || projectIndex > projects.Count())
        {
            Console.Write(ToString());
            Console.WriteLine("0. Back");

            Console.WriteLine(prompt);
            readIndex = Console.ReadLine() ?? defaultRead;
            if (readIndex == "0")
                return -1;
        }

        Console.WriteLine();

        return projectIndex - 1;
    }


    public static void ListProjects()
    {
        Console.WriteLine($"Projects");

        if (UnitService.Projects.Count == 0)
        {
            Console.WriteLine($"This Unit has no Projects\n");
            return;
        }

        // Returns this.toString() which shows all Projects name, description
        Console.WriteLine(ToString());
    }

    public static void ListAllTodos()
    {
        int toDoCount = 0;
        // If no Projects then automatically no ToDos
        if (UnitService.Projects.Count == 0)
        {
            Console.WriteLine($"This Unit has no ToDos\n");
            return;
        }
        foreach (Project project in UnitService.Projects)
        {
            if (project.ToDos.Any())
            {
                // Prints Project.toString() which shows Name, Description
                //  and loops over all the tasks name, completion, description
                Console.WriteLine($"{project}\n");
                toDoCount++;
            }
        }

        if (toDoCount == 0)
            Console.WriteLine($"This Unit has no ToDos\n");
    }

    // Prints out name and description for each project
    public static string ToString()
    {
        string unitToString = "";

        int projIndex = 0;
        foreach (Project project in Projects)
            unitToString += $"{projIndex++ + 1}: {project.Name} - {project.Description}\n";

        return unitToString;
    }
}
