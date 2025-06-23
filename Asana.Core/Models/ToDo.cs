using Asana.Core.Interfaces;

namespace Asana.Core.Models;

public class ToDo : INameDescription
{
    private readonly ToDoIdGenerator tIdGen = new ToDoIdGenerator();
    public ToDo()
    {
        id = tIdGen.GetNextId();
    }
    private int? id;
    public int? Id
    {
        get { return id; }
        set
        {
            if (value != id)
                id = value;
        }
    }

    private DateTime dueDate;
    public DateTime DueDate
    {
        get
        {
            return dueDate;
        }
        set
        {
            if (value != dueDate)
                dueDate = value;
        }
    }

    private string name;
    public string Name
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

    private int? priority;
    public int? Priority
    {
        get { return priority; }
        set
        {
            if (value != priority)
                priority = value;
        }
    }

    private bool isComplete = false;
    public bool IsComplete
    {
        get { return isComplete; }
        set
        {
            if (value != isComplete)
                isComplete = value;
        }
    }

    private int? projectId;
    public int? ProjectId
    {
        get { return projectId; }
        set
        {
            if (value != projectId)
                projectId = value;
        }
    }


    public override string ToString()
    {
        return $"{name} - {isComplete} - {description}";
    }
}
