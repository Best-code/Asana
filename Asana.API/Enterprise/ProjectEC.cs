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

    public Project? AddUpdateProject(Project? Project)
    {
        // This is the add part. If the Project only has a place holder ID / 0 then give it a the real next id and add it to the collection
        if (Project != null && Project.Id == 0)
        {
            Project.Id = FakeProjDB.NextProjectKey;
            FakeProjDB.Projects.Add(Project);
        }

        return Project;
    }

}
