using System;
using Asana.API.Database;
using Asana.Core.Models;

namespace Asana.API.Enterprise;

public class ToDoEC
{
    public IEnumerable<ToDo> GetToDos()
    {
        return new FakeDB().ToDos;
    }
}
