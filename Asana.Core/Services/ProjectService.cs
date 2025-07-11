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



    private ProjectService()
    {
        Refresh();
    }

    private void Refresh()
    {
        var todoData = new WebRequestHandler().Get("/api/ToDo").Result;

        // Splitting these up so that it works with IEnumerable into Observable collection
        var listToDos = JsonConvert.DeserializeObject<List<ToDo>>(todoData) ?? new List<ToDo>();
        ToDos = new ObservableCollection<ToDo>(listToDos);
    }



    public ToDo? AddUpdateToDo(ToDo? toDo)
    {
        if (toDo == null)
            return toDo;


        var isNewToDo = toDo.Id == 0;
        var todoData = new WebRequestHandler().Post($"/api/ToDo", toDo).Result;
        var toDoToAddUpdate = JsonConvert.DeserializeObject<ToDo>(todoData);

        if (toDoToAddUpdate != null)
        {
            // Update Portion
            if (!isNewToDo)
            {
                // If it already exist, remove it then replace it with an updated copy at the same position 
                var existingToDo = _toDoList.FirstOrDefault(t => t.Id == toDoToAddUpdate.Id);
                if (existingToDo != null)
                {
                    var index = _toDoList.IndexOf(existingToDo);
                    _toDoList.RemoveAt(index);
                    _toDoList.Insert(index, toDoToAddUpdate);
                }
            }
            else
            {
                ToDos.Add(toDoToAddUpdate);
            }
        }

        return toDo;
    }
    // Deletes a passed in ToDo from the Collection
    public ToDo? DeleteTodo(int id)
    {
        var localToDo = GetToDoById(id);
        if (localToDo == null)
            return null;

        var todoData = new WebRequestHandler().Delete($"/api/ToDo/{id}").Result;
        var toDoToDelete = JsonConvert.DeserializeObject<ToDo>(todoData);

        if (toDoToDelete != null)
        {
            ToDos.Remove(localToDo ?? new ToDo());
        }
        return localToDo;
    }

    public ToDo? GetToDoById(int id)
    {
        return ToDos.FirstOrDefault(t => t.Id == id);
    }

    public int NextKey
    {
        get
        {
            if (ToDos.Any())
            {
                return ToDos.Select(t => t.Id).Max() + 1;
            }
            return 1;
        }
    }

}
