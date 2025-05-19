using System;
using System.Runtime.CompilerServices;

namespace MyApp.Models
{
    public class AsanaUnit
    {

        static private string[] UnitOptions = {
            "Create Project",
            "Delete Project",
            "Update Project",
            "List All Projects",
            "List All ToDos",
        };


        public void Run()
        {
            int selection = -1;
            while (selection != 0)
            {
                PrintUnitOptions();
                selection = HandleUnitOptionSelection(selection);
            }
        }

        private void PrintUnitOptions()
        {
            int optionIndex = 0;
            foreach (string option in UnitOptions)
            {
                Console.WriteLine($"{optionIndex++ + 1}. {option}");
            }
            Console.WriteLine("0. Exit\n");
        }

        private int HandleUnitOptionSelection(int selection)
        {
            selection = Project.HandleInput("0");
            Console.WriteLine();
            switch (selection)
            {
                case 1:
                    CreateProject();
                    break;
                case 2:
                    DeleteProject();
                    break;
                case 3:
                    UpdateProject();
                    break;
                case 4:
                    ListProjects();
                    break;
                case 5:
                    ListAllTodos();
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

        private void CreateProject()
        {
            Project createProject = new Project();
            Console.Write($"Name: ");
            createProject.Name = Console.ReadLine();
            Console.Write($"Description: ");
            createProject.Description = Console.ReadLine();

            projects.Add(createProject);

            Console.WriteLine($"Project '{createProject.Name}' created\n");

        }

        private void DeleteProject()
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

        private void UpdateProject()
        {
            if (Projects.Count == 0)
            {
                Console.WriteLine($"This Unit has no Projects\n");
                return;
            }
            int projectIndex = SelectProject("Which Project would you like to Update?\n");
            if (projectIndex == -1) return;


            int selection = -1;
            while (selection != 0)
            {
                Console.WriteLine($"Project {projects[projectIndex].Name}");
                Console.WriteLine($"1. Change name");
                Console.WriteLine($"2. Change description");
                Console.WriteLine($"3. Update ToDos");
                Console.WriteLine($"0. Exit Project {projects[projectIndex].Name}\n");

                selection = Project.HandleInput("0");

                Console.WriteLine();
                switch (selection)
                {
                    case 1:
                        Console.Write($"Name: ");
                        Projects[projectIndex].Name = Console.ReadLine();
                        Console.WriteLine($"Name Updated to {projects[projectIndex].Name}\n");
                        break;
                    case 2:
                        Console.Write($"Description: ");
                        projects[projectIndex].Description = Console.ReadLine();
                        Console.WriteLine($"{projects[projectIndex].Name} Description Updated\n");
                        break;
                    case 3:
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

        private void ListProjects()
        {
            Console.WriteLine($"Projects");

            if (Projects.Count == 0)
            {
                Console.WriteLine($"This Unit has no Projects\n");
                return;
            }
            Console.WriteLine(this);
        }

        private void ListAllTodos()
        {
            int toDoCount = 0;
            if (Projects.Count == 0)
            {
                Console.WriteLine($"This Unit has no ToDos\n");
                return;
            }
            foreach (Project project in Projects)
            {
                if (project.ToDos.Any())
                {
                    Console.WriteLine($"{project}\n");
                    toDoCount++;
                }
            }

            if (toDoCount == 0)
                Console.WriteLine($"This Unit has no ToDos\n");
        }

        private int SelectProject(string prompt, string defaultRead = "0")
        {
            string readIndex = "";
            int projectIndex;

            while (!int.TryParse(readIndex, out projectIndex) || projectIndex < 1 || projectIndex > projects.Count())
            {
                Console.Write(this);
                Console.WriteLine("0. Back");

                Console.WriteLine(prompt);
                readIndex = Console.ReadLine() ?? defaultRead;
                if (readIndex == "0")
                    return -1;
            }

            Console.WriteLine();

            return projectIndex - 1;
        }

        private List<Project> projects = new List<Project>();
        public List<Project> Projects
        {
            get { return projects; }
            set
            {
                if (value != projects)
                    projects = value;
            }
        }

        public override string ToString()
        {
            string unitToString = "";

            int projIndex = 0;
            foreach (Project project in Projects)
                unitToString += $"{projIndex++ + 1}: {project.Name} - {project.Description}\n";

            return unitToString;
        }

    }
}
