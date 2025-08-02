using System;
using Asana.API.Database;
using Asana.Core.Models;

namespace Asana.API.Enterprise;

public class ProjectEC
{
    public async Task<IEnumerable<Project>> GetProjects()
    {
        var projects = await ProjectDB.GetProjects();
        return projects.Take(100);
    }

    public async Task<IEnumerable<Project>> GetProjects(bool Expand = false)
    {
        var projects = await ProjectDB.GetProjects(Expand);
        return projects.Take(100);
    }

    public async Task<Project?> GetProjectById(int id, bool Expand = false)
    {
        var projects = await GetProjects(Expand);
        return projects.FirstOrDefault(t => t.Id == id);
    }

    public async Task<Project?> Delete(int id)
    {
        Project? projectToDelete = await GetProjectById(id);
        if (projectToDelete != null)
        {
            await ProjectDB.Delete(projectToDelete.DbId);
        }
        return projectToDelete;
    }


    public async Task<Project?> AddUpdateProject(Project? project)
    {
        var retMe = await ProjectDB.AddUpdateProject(project);
        return retMe;
    }

}
