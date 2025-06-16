using Asana.Core.Interfaces;
using Asana.Core.Services;

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
        set
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
            new ToDo("ToDo One",   "This is my first ToDo",  pIdGen.GetNextId()),
            new ToDo("ToDo Two",   "This is my second ToDo", pIdGen.GetNextId()),
            new ToDo("ToDo Three", "This is my third ToDo",  pIdGen.GetNextId())
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
