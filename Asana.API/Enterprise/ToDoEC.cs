using System;
using Asana.API.Database;
using Asana.Core.Models;

namespace Asana.API.Enterprise;

public class ToDoEC
{
    public IEnumerable<ToDo> GetToDos()
    {
        return FakeDB.Get().Take(100);
    }

    public ToDo? GetToDoById(int id)
    {
        return FakeDB.Get().FirstOrDefault(t => t.Id == id);
    }

    public ToDo? Delete(int id)
    {
        ToDo? toDoToDelete = GetToDoById(id);
        if (toDoToDelete != null)
        {
            FakeDB.ToDos.Remove(toDoToDelete);
        }
        return toDoToDelete;
    }

    public ToDo? AddUpdateToDo(ToDo? toDo)
    {
        FakeDB.AddUpdateToDo(toDo);
        return toDo;
    }

}
