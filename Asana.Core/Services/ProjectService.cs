using System.Collections.ObjectModel;
using Asana.Core.Models;
using Microsoft.VisualBasic;
namespace Asana.Core.Services;

public class ProjectService
{

    private ObservableCollection<ToDo> _toDoList;
    public ObservableCollection<ToDo> ToDos
    {
        get
        {
            return _toDoList;
        }

        private set
        {
            if (value != _toDoList)
            {
                _toDoList = value;
            }
        }
    }
    // Singleton Set up
    private static ProjectService? instance;
    public static ProjectService Current
    {
        get
        {
            if (instance == null)
            {
                instance = new ProjectService();
            }

            return instance;
        }
    }


    private readonly ToDoIdGenerator tIdGen = new ToDoIdGenerator();

    private ProjectService()
    {
        ToDos = new ObservableCollection<ToDo>() {
            new ToDo(){Name = "ToDo One",  Description = "This is my first ToDo w/ ProjID 1", Id = tIdGen.GetNextId(), DueDate = DateTime.Now, ProjectId = 1},
            new ToDo(){Name = "ToDo Two",  Description = "This is my second ToDo w/ ProjID 1", Id = tIdGen.GetNextId(), DueDate = DateTime.Now, ProjectId = 1},
            new ToDo(){Name = "ToDo Three",  Description = "This is my third ToDo w/ ProjID 2", Id = tIdGen.GetNextId(), DueDate = DateTime.Now, ProjectId = 2},
        };

        ToDos.First().IsComplete = true;
        ToDos.Last().IsComplete = true;
    }

    // public void CreateTodo(string toDoName, string toDoDescription, int projId, DateTime dueDate)
    // {
    //     if (toDoName == "") toDoName = "ToDo";
    //     ToDo createToDo = new ToDo() { Name = toDoName, Description = toDoDescription, ProjectId = projId, DueDate = dueDate };
    //     AddTodo(createToDo);
    // }

    public void AddTodo(ToDo toDo)
    {
        toDo.Id = tIdGen.GetNextId();
        ToDos.Add(toDo);
    }

    public ToDo GetToDoAt(int index)
    {
        return ToDos.ElementAtOrDefault(index) ?? new ToDo();
    }

    public bool DeleteTodo(ToDo toDo)
    {
        return ToDos.Remove(toDo);
    }

    public bool UpdateTodoName(int toDoIndex, string name)
    {
        if (ToDos.Count == 0)
            return false;
        if (toDoIndex < 0 || toDoIndex > ToDos.Count())
            return false;

        ToDos[toDoIndex].Name = name;
        return true;
    }

    public bool UpdateTodoDescription(int toDoIndex, string description)
    {
        if (ToDos.Count == 0)
            return false;
        if (toDoIndex < 0 || toDoIndex > ToDos.Count())
            return false;


        ToDos[toDoIndex].Description = description;
        return true;
    }

    public bool UpdateTodoDueDate(int toDoIndex, DateTime dueDate)
    {
        if (ToDos.Count == 0)
            return false;
        if (toDoIndex < 0 || toDoIndex > ToDos.Count())
            return false;

        ToDos[toDoIndex].DueDate = dueDate;
        return true;
    }

    public bool UpdateTodoStatus(int toDoIndex, bool status)
    {
        if (ToDos.Count == 0)
            return false;
        if (toDoIndex < 0 || toDoIndex > ToDos.Count())
            return false;

        ToDos[toDoIndex].IsComplete = status;
        return true;
    }

}
