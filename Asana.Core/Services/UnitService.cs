using System.Collections.ObjectModel;
using Asana.Core.Models;
namespace Asana.Core.Services;

public class UnitService
{

    private ObservableCollection<Project>? _projectsList;
    public ObservableCollection<Project> Projects
    {
        get
        {
            return _projectsList ?? new ObservableCollection<Project>();
        }

        private set
        {
            if (value != _projectsList)
            {
                _projectsList = value;
            }
        }
    }

    // Singleton Set up
    private static UnitService? instance;
    public static UnitService Current
    {
        get
        {
            if (instance == null)
            {
                instance = new UnitService();
            }

            return instance;
        }
    }

    private readonly ProjectIdGenerator pIdGen = new ProjectIdGenerator();
    
    public UnitService()
    {
        Projects = new ObservableCollection<Project>() {
            new Project(){Name = "Project One",  Description = "This is my first project", Id = 1},
            new Project(){Name = "Project Two",  Description = "This is my second project", Id = 2},
        };
    }


    // Create a project
    public void CreateProject(string projectName, string projectDescription)
    {
        Project newProject = new Project() { Name = projectName, Description = projectDescription };
        AddProject(newProject);
    }

    // Add a project to the array
    public bool AddProject(Project project)
    {
        if (project == null)
            return false;

        Projects.Add(project);
        return true;
    }

    public Project GetProjectAt(int index)
    {
        return Projects.ElementAtOrDefault(index) ?? new Project();
    }

    public Project GetProjectByName(string name)
    {
        return Projects.FirstOrDefault(p => p.Name == name) ?? new Project();
    }

    // Delete a project
    public bool DeleteProject(Project project)
    {
        return Projects.Remove(project);
    }

    // Update project name
    public bool UpdateProjectName(int projectIndex, string name)
    {
        if (Projects.Count == 0)
            return false;
        if (projectIndex < 0 || projectIndex >= Projects.Count())
            return false;
        if (name == "")
            return false;

        Projects[projectIndex].Name = name;
        return true;
    }

    // Update project description
    public bool UpdateProjectDescription(int projectIndex, string description)
    {
        if (Projects.Count == 0)
            return false;
        if (projectIndex < 0 || projectIndex >= Projects.Count())
            return false;
        if (description == "")
            return false;


        Projects[projectIndex].Description = description;
        return true;
    }

}
