using System;
using System.Threading.Tasks;
using Asana.API.Enterprise;
using Asana.Core.Models;

namespace Asana.API.Database;

public static class ToDoDB
{

    static FirebaseService firebase = new FirebaseService("https://csharpmills-default-rtdb.firebaseio.com/", authToken: null);

    // Add a todo


    private static List<ToDo> toDos = new List<ToDo>();
    public static List<ToDo> ToDos
    {
        get => toDos;
        set
        {
            if (value != toDos)
            {
                toDos = value;
            }
        }
    }

    public static int NextToDoKey
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

    public static async Task<List<ToDo>> Get()
    {
        var data = await firebase.GetAsync<Dictionary<string, ToDo>>("todos");

        if (data == null)
            return new List<ToDo>();

        var todos = data.Select(kvp =>
        {
            kvp.Value.dbId = kvp.Key;
            return kvp.Value;
        }).ToList();

        return todos;
    }

    public static async Task Delete(string dbId)
    {
        await firebase.DeleteAsync($"todos/{dbId}");
    }



    public static async Task<ToDo?> AddUpdateToDo(ToDo? toDo)
    {
        if (toDo == null)
            return toDo;

        // If its a new ToDo then add it
        if (toDo.Id == 0)
        {
            toDo.Id = NextToDoKey;
            await firebase.PushAsync("todos", toDo);
        }
        // If its an existing ToDo, overwrite the old one
        else
        {
            await new ToDoEC().Delete(toDo.Id);
            await firebase.PushAsync("todos", toDo);
        }
        return toDo;
    }
}
