using System;
using Asana.CLI.Interfaces;

namespace Asana.CLI.Models;

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
