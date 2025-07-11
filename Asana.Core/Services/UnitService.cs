using System.Collections.ObjectModel;
using Asana.Core.Models;
using Asana.Core.Util;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
namespace Asana.Core.Services;

public class UnitService
{

    private ObservableCollection<Project> _projectsList;
    public ObservableCollection<Project> Projects
    {
        get
        {
            return _projectsList;
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
        refresh();
    }

    private void refresh()
    {
        var projectData = new WebRequestHandler().Get("/api/Project").Result;
        // Splitting these up so that it works with IEnumerable into Observable collection
        var listProjects = JsonConvert.DeserializeObject<List<Project>>(projectData) ?? new List<Project>();
        Projects = new ObservableCollection<Project>(listProjects);
    }

    // Add a project to the array
    public Project? AddUpdateProject(Project project)
    {
        if (project == null)
            return null;


        var isNewProject = project.Id == 0;
        var projectData = new WebRequestHandler().Post($"/api/Project", project).Result;
        var projectToAddUpdate = JsonConvert.DeserializeObject<Project>(projectData);

        if (projectToAddUpdate != null)
        {
            // Update Portion
            if (!isNewProject)
            {
                // If it already exist, remove it then replace it with an updated copy at the same position 
                var existingToDo = _projectsList.FirstOrDefault(t => t.Id == projectToAddUpdate.Id);
                if (existingToDo != null)
                {
                    var index = _projectsList.IndexOf(existingToDo);
                    _projectsList.RemoveAt(index);
                    _projectsList.Insert(index, projectToAddUpdate);
                }
            }
            else
            {
                Projects.Add(projectToAddUpdate);
            }
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
