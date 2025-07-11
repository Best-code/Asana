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


    public UnitService()
    {
        Projects = new ObservableCollection<Project>() {
            new Project(){Name = "Project One",  Description = "This is my first project", Id = 1},
            new Project(){Name = "Project Two",  Description = "This is my second project", Id = 2},
        };
    }

    // Add a project to the array
    public Project? AddUpdateProject(Project project)
    {

        // This is the add part. If the toDo only has a place holder ID / 0 then give it a the real next id and add it to the collection
        if (project != null && project.Id == 0)
        {
            // project.Id = pIdGen.GetNextId();
            Projects.Add(project);
        }

        return project;
    }

    public Project? GetProjectAt(int index)
    {
        return Projects.ElementAtOrDefault(index);
    }

    public Project? GetProjectByName(string name)
    {
        return Projects.FirstOrDefault(p => p.Name == name);
    }

    public Project? GetProjectById(int id)
    {
        return Projects.FirstOrDefault(p => p.Id == id);
    }

    // Delete a project
    public bool DeleteProject(Project? project)
    {
        if (project == null) return false;
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

    public int NextKey
    {
        get
        {
            if (Projects.Any())
            {
                return Projects.Select(p => p.Id).Max() + 1;
            }
            return 1;
        }
    }

}
