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

    // Calculate what percent of tasks in this project are complete
    // public float CompletePercent
    // {
    //     get
    //     {
    //         float complete = 0;
    //         float incomplete = 0;
    //         foreach (ToDo toDo in ToDos ?? new ObservableCollection<ToDo>())
    //         {
    //             if (toDo.IsComplete)
    //                 complete++;
    //             else
    //                 incomplete++;
    //         }

    //         if (incomplete == 0) return 1.0f;

    //         return complete / (ToDos?.Count() ?? 1);
    //     }
    // }


}
