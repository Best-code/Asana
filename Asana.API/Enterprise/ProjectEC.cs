using System;
using Asana.API.Database;
using Asana.Core.Models;

namespace Asana.API.Enterprise;

public class ProjectEC
{
    public IEnumerable<Project> GetProjects()
    {
        return FakeProjDB.Projects.Take(100);
    }

    public Project? GetProjectById(int id)
    {
        return GetProjects().FirstOrDefault(t => t.Id == id);
    }

    public Project? Delete(int id)
    {
        Project? ProjectToDelete = GetProjectById(id);
        if (ProjectToDelete != null)
        {
            FakeProjDB.Projects.Remove(ProjectToDelete);
        }
        return ProjectToDelete;
    }

    public Project? AddUpdateProject(Project? project)
    {
        FakeProjDB.AddUpdateProject(project);
        return project;
    }

}
