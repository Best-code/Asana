using Asana.Core.Interfaces;

namespace Asana.Core.Models;

public class ProjectIdGenerator : IIdGenerator
{
    private int _currentId = 0;
    public int GetNextId() => ++_currentId;
    public int ShowNextId() => _currentId + 1;


    private static ProjectIdGenerator? instance;
    public static ProjectIdGenerator Current
    {
        get
        {
            if (instance == null)
            {
                instance = new ProjectIdGenerator();
            }
            return instance;
        }
    }

    private ProjectIdGenerator() { }
}

public class ToDoIdGenerator : IIdGenerator
{

    private int _currentId = 0;
    public int GetNextId() => ++_currentId;
    public int ShowNextId() => _currentId + 1;


    private static ToDoIdGenerator? instance;
    public static ToDoIdGenerator Current
    {
        get
        {
            if (instance == null)
            {
                instance = new ToDoIdGenerator();
            }

            return instance;
        }
    }

    private ToDoIdGenerator() { }
}
