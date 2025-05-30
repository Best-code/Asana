using Asana.Core.Models;
namespace Asana.Core.Services;

public class ProjectService
{
    public void CreateTodo(Project project, string toDoName, string toDoDescription, int projId)
    {
        if (toDoName == "") toDoName = "ToDo";
        ToDo createToDo = new ToDo(toDoName, toDoDescription, projId);
        AddTodo(project, createToDo);
    }

    public void AddTodo(Project project, ToDo toDo)
    {
        project.ToDos.Add(toDo);
    }

    public bool DeleteTodo(Project project, int toDoIndex)
    {
        if (project.ToDos.Count == 0)
            return false;
        if (toDoIndex < 0 || toDoIndex > project.ToDos.Count())
            return false;

        project.ToDos.RemoveAt(toDoIndex);
        return true;
    }

    public bool UpdateTodoName(Project project, int toDoIndex, string name)
    {
        if (project.ToDos.Count == 0)
            return false;
        if (toDoIndex < 0 || toDoIndex > project.ToDos.Count())
            return false;
        if (name == "")
            return false;

        project.ToDos[toDoIndex].Name = name;
        return true;
    }

    public bool UpdateTodoDescription(Project project, int toDoIndex, string description)
    {
        if (project.ToDos.Count == 0)
            return false;
        if (toDoIndex < 0 || toDoIndex > project.ToDos.Count())
            return false;
        if (description == "")
            return false;

        project.ToDos[toDoIndex].Description = description;
        return true;
    }
    public bool UpdateTodoStatus(Project project, int toDoIndex, bool status)
    {
        if (project.ToDos.Count == 0)
            return false;
        if (toDoIndex < 0 || toDoIndex > project.ToDos.Count())
            return false;

        project.ToDos[toDoIndex].IsComplete = status;
        return true;
    }

}
