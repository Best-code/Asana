using System;
using Asana.API.Enterprise;
using Asana.Core.Models;

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
}
