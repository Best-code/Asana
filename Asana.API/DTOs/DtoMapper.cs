using System;
using Asana.Core.Models;

namespace Asana.API.DTOs;

public static class DtoMapper
{
    public static ProjectDTO ProjectToProjectDTO(Project project)
    {
        return new ProjectDTO
        {
            Id = project.Id,
            DbId = project.DbId,
            Name = project.Name,
            Description = project.Description,
            ToDoDTOList = project.ToDoList?.Select(t => ToDoToToDoDTO(t)).ToList()
        };
    }

    public static Project ProjectFromProjectDTO(ProjectDTO projectDTO)
    {
        return new Project
        {
            Id = projectDTO.Id,
            DbId = projectDTO.DbId,
            Name = projectDTO.Name,
            Description = projectDTO.Description ?? "",
            ToDoList = projectDTO.ToDoDTOList?.Select(t => ToDoFromToDoDTO(t)).ToList()
        };
    }

    public static ToDoDTO ToDoToToDoDTO(ToDo toDo)
    {
        return new ToDoDTO
        {
            Id = toDo.Id,
            DbId = toDo.DbId,
            DueDate = toDo.DueDate,
            Name = toDo.Name,
            Description = toDo.Description,
            Priority = toDo.Priority,
            IsComplete = toDo.IsComplete,
            ProjectId = toDo.ProjectId
        };
    }

      public static ToDo ToDoFromToDoDTO(ToDoDTO toDoDTO)
    {
        return new ToDo
        {
            Id = toDoDTO.Id,
            DbId = toDoDTO.DbId,
            DueDate = toDoDTO.DueDate ?? DateTime.Now,
            Name = toDoDTO.Name,
            Description = toDoDTO.Description ?? "",
            Priority = toDoDTO.Priority,
            IsComplete = toDoDTO.IsComplete,
            ProjectId = toDoDTO.ProjectId
        };
    }
}
