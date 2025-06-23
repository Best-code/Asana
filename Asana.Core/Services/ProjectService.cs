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
            new ToDo(){Name = "ToDo One",  Description = "This is my first ToDo", Id = tIdGen.GetNextId(), DueDate = DateTime.Now},
            new ToDo(){Name = "ToDo Two",  Description = "This is my second ToDo", Id = tIdGen.GetNextId(), DueDate = DateTime.Now},
            new ToDo(){Name = "ToDo Three",  Description = "This is my third ToDo", Id = tIdGen.GetNextId(), DueDate = DateTime.Now},
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
        ToDos.Add(toDo);
    }

    public bool DeleteTodo(int toDoIndex)
    {
        if (ToDos.Count == 0)
            return false;
        if (toDoIndex < 0 || toDoIndex >= ToDos.Count())
            return false;

        ToDos.RemoveAt(toDoIndex);
        return true;
    }

    // public bool UpdateTodoName(int toDoIndex, string name)
    // {
    //     if (ToDos.Count == 0)
    //         return false;
    //     if (toDoIndex < 0 || toDoIndex > ToDos.Count())
    //         return false;

    //     ToDos[toDoIndex].Name = name;
    //     return true;
    // }

    // public bool UpdateTodoDescription(int toDoIndex, string description)
    // {
    //     if (ToDos.Count == 0)
    //         return false;
    //     if (toDoIndex < 0 || toDoIndex > ToDos.Count())
    //         return false;


    //     ToDos[toDoIndex].Description = description;
    //     return true;
    // }

    // public bool UpdateTodoDueDate(int toDoIndex, DateTime dueDate)
    // {
    //     if (ToDos.Count == 0)
    //         return false;
    //     if (toDoIndex < 0 || toDoIndex > ToDos.Count())
    //         return false;

    //     ToDos[toDoIndex].DueDate = dueDate;
    //     return true;
    // }

    // public bool UpdateTodoStatus(int toDoIndex, bool status)
    // {
    //     if (ToDos.Count == 0)
    //         return false;
    //     if (toDoIndex < 0 || toDoIndex > ToDos.Count())
    //         return false;

    //     ToDos[toDoIndex].IsComplete = status;
    //     return true;
    // }

}
