using Asana.Core.Models;
namespace Asana.Core.Services;

public class UnitService
{

    // Create a project
    public void CreateProject(AsanaUnit unit, string projectName, string projectDescription)
    {
        if (projectName == "") projectName = "Project";
        Project newProject = new Project(projectName, projectDescription);
        AddProject(unit, newProject);
    }

    // Add a project to the array
    public void AddProject(AsanaUnit unit, Project project)
    {
        unit.Projects.Add(project);
    }

    // Delete a project
    public bool DeleteProject(AsanaUnit unit, int projectIndex)
    {
        if (unit.Projects.Count == 0)
            return false;
        if (projectIndex < 1 || projectIndex > unit.Projects.Count())
            return false;

        unit.Projects.RemoveAt(projectIndex);
        return true;
    }

    // Update project name
    public bool UpdateProjectName(AsanaUnit unit, int projectIndex, string name)
    {
        if (unit.Projects.Count == 0)
            return false;
        if (projectIndex < 0 || projectIndex >= unit.Projects.Count())
            return false;
        if (name == "")
            return false;

        unit.Projects[projectIndex].Name = name;
        return true;
    }

    // Update project description
    public bool UpdateProjectDescription(AsanaUnit unit, int projectIndex, string description)
    {
        if (unit.Projects.Count == 0)
            return false;
        if (projectIndex < 0 || projectIndex >= unit.Projects.Count())
            return false;
        if (description == "")
            return false;


        unit.Projects[projectIndex].Description = description;
        return true;
    }

}
