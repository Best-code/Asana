using System;
using System.Threading.Tasks;
using Asana.API.DTOs;
using Asana.API.Enterprise;
using Asana.Core.Models;

namespace Asana.API.Database;

public static class ToDoDB
{

    static FirebaseService firebase = new FirebaseService("https://csharpmills-default-rtdb.firebaseio.com/", authToken: null);

    public static async Task<int> GetNextToDoKey()
    {
        var toDos = await firebase.GetAsync<Dictionary<string, ToDoDTO>>("todos");

        if (toDos == null || !toDos.Any())
            return 1;

        int maxId = toDos.Values.Max(t => t.Id);
        return maxId + 1;
    }

    public static async Task<List<ToDo>> Get()
    {
        var data = await firebase.GetAsync<Dictionary<string, ToDoDTO>>("todos");

        if (data == null)
            return new List<ToDo>();

        var todoDTOs = data.Select(kvp =>
        {
            kvp.Value.DbId = kvp.Key;
            return kvp.Value;
        }).ToList();

        var todos = todoDTOs.Select(td => DtoMapper.ToDoFromToDoDTO(td)).ToList();

        return todos;
    }

    public static async Task Delete(string DbId)
    {
        await firebase.DeleteAsync($"todos/{DbId}");
    }



    public static async Task<ToDo?> AddUpdateToDo(ToDo? toDo)
    {
        if (toDo == null)
            return toDo;

        var dto = DtoMapper.ToDoToToDoDTO(toDo);
        // If its a new ToDo then add it
        if (dto.Id == 0)
        {
            dto.Id = await GetNextToDoKey();
            await firebase.PushAsync("todos", dto);
        }
        // If its an existing ToDo, overwrite the old one
        else
        {
            // await Delete(dto.DbId);
            // await firebase.PushAsync("todos", dto);
            await firebase.SetAsync("todos", dto);
        }

        return DtoMapper.ToDoFromToDoDTO(dto);
    }
}
