using System;
using Asana.API.Database;
using Asana.Core.Models;

namespace Asana.API.Enterprise;

public class ToDoEC
{
    public IEnumerable<ToDo> GetToDos()
    {
        return FakeDB.ToDos.Take(100);
    }

    public ToDo? GetToDoById(int id)
    {
        return GetToDos().FirstOrDefault(t => t.Id == id);
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
        // This is the add part. If the toDo only has a place holder ID / 0 then give it a the real next id and add it to the collection
        if (toDo != null && toDo.Id == 0)
        {
            toDo.Id = FakeDB.NextKey;
            FakeDB.ToDos.Add(toDo);
        }

        return toDo;
    }

}
