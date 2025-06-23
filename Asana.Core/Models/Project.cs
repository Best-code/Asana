using Asana.Core.Interfaces;
using Asana.Core.Services;
using Microsoft.VisualBasic;

namespace Asana.Core.Models;

public class Project : INameDescription
{
    private readonly ProjectIdGenerator pIdGen;
    public ProjectService projSvc;

    private List<ToDo> _toDoList;
    public List<ToDo> ToDos
    {
        get
        {
            return _toDoList.Take(100).ToList();
        }
        private set
        {
            if (value != _toDoList)
                _toDoList = value;
        }
    }

    public Project(string name, string description)
    {
        pIdGen = new ProjectIdGenerator();
        id = pIdGen.GetNextId();
        this.name = name;
        this.description = description;
        projSvc = new ProjectService();

        ToDos = new List<ToDo>() {
            new ToDo(){Name = "ToDo One",  Description = "This is my first ToDo", ProjectId = pIdGen.GetNextId(), DueDate = new DateTime(2025,6,25)},
            new ToDo(){Name = "ToDo Two",  Description = "This is my second ToDo", ProjectId = pIdGen.GetNextId(), DueDate = new DateTime(2025,6,25)},
            new ToDo(){Name = "ToDo Three",  Description = "This is my third ToDo", ProjectId = pIdGen.GetNextId(), DueDate = new DateTime(2025,6,25)},
        };

        ToDos.First().IsComplete = true;
        ToDos.Last().IsComplete = true;
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

    private string description;
    public string Description
    {
        get { return description; }
        set
        {
            if (value != description)
                description = value;
        }
    }

    // Calculate what percent of tasks in this project are complete
    public float CompletePercent
    {
        get
        {
            float complete = 0;
            float incomplete = 0;
            foreach (ToDo toDo in ToDos)
            {
                if (toDo.IsComplete)
                    complete++;
                else
                    incomplete++;
            }

            if (incomplete == 0) return 1.0f;

            return complete / ToDos.Count();
        }
    }


}
