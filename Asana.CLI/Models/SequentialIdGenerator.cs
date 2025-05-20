using System;
using Asana.CLI.Interfaces;

namespace Asana.CLI.Models;

public class SequentialIdGenerator : IIdGenerator
{
    private int _currentId = 0;
    public int GetNextId() => ++_currentId;
}
