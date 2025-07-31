using System.Collections.ObjectModel;
using Asana.Core.Interfaces;
using Asana.Core.Services;
using Microsoft.VisualBasic;

namespace Asana.Core.Models;

public class Project : INameDescription
{
    public Project()
    {
        Id = 0;
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

    public string? DbId { get; set; }

    private string? name;
    public string Name
    {
        get { return name ?? ""; }
        set
        {
            if (value != name)
                name = value;
        }
    }

    private string? description;
    public string Description
    {
        get { return description ?? ""; }
        set
        {
            if (value != description)
                description = value;
        }
    }

    public List<ToDo>? ToDoList { get; set; }

}
