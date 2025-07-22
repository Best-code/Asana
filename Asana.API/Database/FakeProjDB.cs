using System;
using Asana.API.Enterprise;
using Asana.Core.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Asana.API.Database;

public static class FakeProjDB
{

    private static List<Project> projects = new()
    {
            new Project() { Name = "Project 1", Id = 1, Description = "This is project one" },
            new Project() { Name = "Project 2", Id = 2, Description = "This is project two" },
    };

    public static List<Project> Projects
    {

        get => projects;
        set
        {
            if (value != projects)
            {
                projects = value;
            }
        }
    }

    public static int NextProjectKey
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

    public static List<Project> GetProjects(bool Expand = false)
    {
        if (Expand)
        {
            var projectList = new List<Project>();
            foreach (var proj in Projects)
            {
                proj.ToDoList = FakeDB.ToDos.Where(t => t.ProjectId == proj.Id).ToList();
                projectList.Add(proj);
            }

            return projectList;
        }

        return Projects;
    }

    public static Project? AddUpdateProject(Project? Project)
    {
        if (Project == null)
            return Project;

        // If its a new Project then add it
        if (Project.Id == 0)
        {
            Project.Id = NextProjectKey;
            Projects.Add(Project);
        }
        // If its an existing Project, overwrite the old one
        else
        {
            var dbProject = Projects.FirstOrDefault(t => t.Id == Project.Id);
            if (dbProject != null)
            {
                var index = Projects.IndexOf(dbProject);
                // FakeDB.Projects.RemoveAt(index);
                // FakeDB.Projects.Insert(index, Project);
                Projects[index] = Project;
            }
        }
        return Project;
    }
}
