using System;
using Asana.API.Enterprise;
using Asana.Core.Models;

namespace Asana.API.Database;

public static class FakeDB
{
    private static List<ToDo> toDos = new List<ToDo>(){
                new ToDo(){Name = "ToDo One",  Description = "This is my first ToDo w/ ProjID 1", Id = 1, DueDate = DateTime.Now, ProjectId = 1, IsComplete = true},

                new ToDo(){Name = "ToDo Two",  Description = "This is my 2 ToDo w/ ProjID 1", Id = 2, DueDate = DateTime.Now, ProjectId = 1},

                new ToDo(){Name = "ToDo Three",  Description = "This is my 3 ToDo w/ ProjID 2", Id = 3, DueDate = DateTime.Now, ProjectId = 2},

                new ToDo(){Name = "ToDo Four",  Description = "This is my 4 ToDo w/ ProjID 2", Id = 4, DueDate = DateTime.Now, ProjectId = 2, IsComplete = true},
            };
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

    public static List<ToDo> Get()
    {
        return ToDos;
    }

    public static ToDo? AddUpdateToDo(ToDo? toDo)
    {
        if (toDo == null)
            return toDo;

        // If its a new ToDo then add it
        if (toDo.Id == 0)
        {
            toDo.Id = NextToDoKey;
            ToDos.Add(toDo);
        }
        // If its an existing ToDo, overwrite the old one
        else
        {
            var dbToDo = ToDos.FirstOrDefault(t => t.Id == toDo.Id);
            if (dbToDo != null)
            {
                var index = ToDos.IndexOf(dbToDo);
                // FakeDB.ToDos.RemoveAt(index);
                // FakeDB.ToDos.Insert(index, toDo);
                ToDos[index] = toDo;
            }
        }
        return toDo;
    }
}
