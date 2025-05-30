using Asana.Core.Interfaces;

namespace Asana.Core.Models;

public class ProjectIdGenerator : IIdGenerator
{
    private int _currentId = 0;
    public int GetNextId() => ++_currentId;
}

public class ToDoIdGenerator : IIdGenerator
{
    private int _currentId = 0;
    public int GetNextId() => ++_currentId;
}
