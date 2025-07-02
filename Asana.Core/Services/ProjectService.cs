using System.Collections.ObjectModel;
using Asana.Core.Models;
using Asana.Core.Util;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
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


    private readonly ToDoIdGenerator tIdGen = ToDoIdGenerator.Current;

    private ProjectService()
    {
        var todoData = new WebRequestHandler().Get("/api/ToDo").Result;
        
        // Splitting these up so that it works with IEnumerable into Observable collection
        var listToDos = JsonConvert.DeserializeObject<List<ToDo>>(todoData) ?? new List<ToDo>();
        ToDos = new ObservableCollection<ToDo>(listToDos);
    }



    public ToDo? AddUpdateToDo(ToDo? toDo)
    {
        // This is the add part. If the toDo only has a place holder ID / ShowNextId then give it a the real next id and add it to the collection
        if (toDo != null && toDo.Id == tIdGen.ShowNextId())
        {
            toDo.Id = tIdGen.GetNextId();
            ToDos.Add(toDo);
        }

        return toDo;

    }
    // Deletes a passed in ToDo from the Collection
    public ToDo? DeleteTodo(ToDo? toDo)
    {
        if (toDo != null)
            ToDos.Remove(toDo);
        return toDo;
    }

    public ToDo GetToDoAt(int index)
    {
        return ToDos.ElementAtOrDefault(index) ?? new ToDo();
    }

}
