using System;
using System.Runtime.CompilerServices;
using Asana.CLI.Interfaces;
using Asana.CLI.Models;
using Asana.CLI.Services;


public class Project
{
    private readonly ProjectIdGenerator pIdGen = new ProjectIdGenerator();
    public ProjectService projSvc;
    public Project(string name)
    {
        id = pIdGen.GetNextId();
        this.name = name;
        projSvc = new ProjectService(name, id);
    }

    public void Run()
    {
        projSvc.Run(name ?? "Project"); 
    }

    private int id;
    public int Id
    {
        get { return id; }
        set
        {
            if (value != id)
                id = value;
        }
    }

    private string? name;
    public string? Name
    {
        get { return name; }
        set
        {
            if (value != name)
                name = value;
        }
    }

    private string? description;
    public string? Description
    {
        get { return description; }
        set
        {
            if (value != description)
                description = value;
        }
    }

    // Calculate what percent of tasks in this project are complete
    public float CompletePercent()
    {
        float complete = 0;
        float incomplete = 0;
        foreach (ToDo toDo in projSvc.ToDos)
        {
            if (toDo.IsComplete)
                complete++;
            else
                incomplete++;
        }

        if (incomplete == 0) return 1.0f;

        return complete / projSvc.ToDos.Count();
    }


}
