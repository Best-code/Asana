using System;
using System.Threading.Tasks;
using Asana.API.Enterprise;
using Asana.Core.Models;

namespace Asana.API.Database;

public static class ToDoDB
{

    static FirebaseService firebase = new FirebaseService("https://csharpmills-default-rtdb.firebaseio.com/", authToken: null);

    public static async Task<int> GetNextToDoKey()
    {
        var toDos = await firebase.GetAsync<Dictionary<string, ToDo>>("todos");

        if (toDos == null || !toDos.Any())
            return 1;

        int maxId = toDos.Values.Max(t => t.Id);
        return maxId + 1;
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
            toDo.Id = await GetNextToDoKey();
            await firebase.PushAsync("todos", toDo);
        }
        // If its an existing ToDo, overwrite the old one
        else
        {
            await Delete(toDo.dbId);
            await firebase.PushAsync("todos", toDo);
        }
        return toDo;
    }
}
