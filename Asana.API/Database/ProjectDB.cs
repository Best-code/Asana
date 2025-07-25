using System;
using Asana.API.Enterprise;
using Asana.Core.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Asana.API.Database;

public static class ProjectDB
{
    static FirebaseService firebase = new FirebaseService("https://csharpmills-default-rtdb.firebaseio.com/", authToken: null);

    public static async Task<int> GetNextProjectKey()
    {
        var projects = await firebase.GetAsync<Dictionary<string, Project>>("projects");

        if (projects == null || !projects.Any())
            return 1;

        int maxId = projects.Values.Max(t => t.Id);
        return maxId + 1;
    }

    public static async Task<List<Project>> GetProjects(bool Expand = false)
    {


        var data = await firebase.GetAsync<Dictionary<string, Project>>("projects");

        if (data == null)
            return new List<Project>();

        var projects = data.Select(kvp =>
        {
            kvp.Value.dbId = kvp.Key;
            return kvp.Value;
        }).ToList();

        if (Expand)
        {
            var expandProjects = new List<Project>();
            foreach (var proj in projects)
            {
                var toDos = await ToDoDB.Get();
                proj.ToDoList = toDos.Where(t => t.ProjectId == proj.Id).ToList();
                expandProjects.Add(proj);
            }

            return expandProjects;
        }

        return projects;
    }

    public static async Task Delete(string dbId)
    {
        await firebase.DeleteAsync($"projects/{dbId}");
    }

    public static async Task<Project?> AddUpdateProject(Project? project)
    {
        if (project == null)
            return project;

        // If its a new Project then add it
        if (project.Id == 0)
        {
            project.Id = await GetNextProjectKey();
            await firebase.PushAsync("projects", project);
        }
        // If its an existing project, overwrite the old one
        else
        {
            await Delete(project.dbId);
            await firebase.PushAsync("projects", project);
        }
        return project;

    }
}
