using System;
using Asana.API.Enterprise;
using Asana.Core.Models;

namespace Asana.API.Database;

public class FakeDB
{
    public FakeDB()
    {
        ToDos = new List<ToDo>(){
                new ToDo(){Name = "ToDo One",  Description = "This is my first ToDo w/ ProjID 1", Id = 1, DueDate = DateTime.Now, ProjectId = 1, IsComplete = true},
                new ToDo(){Name = "ToDo Two",  Description = "This is my 2 ToDo w/ ProjID 1", Id = 2, DueDate = DateTime.Now, ProjectId = 1},
                new ToDo(){Name = "ToDo Three",  Description = "This is my 3 ToDo w/ ProjID 2", Id = 3, DueDate = DateTime.Now, ProjectId = 2},
                new ToDo(){Name = "ToDo Four",  Description = "This is my 4 ToDo w/ ProjID 2", Id = 4, DueDate = DateTime.Now, ProjectId = 2, IsComplete = true},
            };
    }
    private IEnumerable<ToDo> toDos;
    public IEnumerable<ToDo> ToDos
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
}
