using System;
using Asana.API.Database;
using Asana.Core.Models;

namespace Asana.API.Enterprise;

public class ToDoEC
{
    public async Task<IEnumerable<ToDo>> GetToDos()
    {
        return await ToDoDB.Get();
    }

    public async Task<ToDo?> GetToDoById(int id)
    {
        var toDos = await ToDoDB.Get();
        return toDos.FirstOrDefault(t => t.Id == id);
    }

    public async Task<ToDo?> Delete(int id)
    {
        ToDo? toDoToDelete = await GetToDoById(id);
        if (toDoToDelete != null)
        {
            // ToDoDB.ToDos.Remove(toDoToDelete);
            await ToDoDB.Delete(toDoToDelete.dbId);
        }
        return toDoToDelete;
    }

    public async Task<ToDo?> AddUpdateToDo(ToDo? toDo)
    {
        await ToDoDB.AddUpdateToDo(toDo);
        return toDo;
    }

}
