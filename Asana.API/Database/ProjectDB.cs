using System;
using Asana.API.DTOs;
using Asana.API.Enterprise;
using Asana.Core.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Asana.API.Database;

public static class ProjectDB
{
    static FirebaseService firebase = new FirebaseService("https://csharpmills-default-rtdb.firebaseio.com/", authToken: null);

    public static async Task<int> GetNextProjectKey()
    {
        var projects = await firebase.GetAsync<Dictionary<string, ProjectDTO>>("projects");

        if (projects == null || !projects.Any())
            return 1;

        int maxId = projects.Values.Max(t => t.Id);
        return maxId + 1;
    }

    public static async Task<List<Project>> GetProjects(bool Expand = false)
    {


        var data = await firebase.GetAsync<Dictionary<string, ProjectDTO>>("projects");

        if (data == null)
            return new List<Project>();

        var projectDTOs = data.Select(kvp =>
        {
            kvp.Value.DbId = kvp.Key;
            return kvp.Value;
        }).ToList();

        if (Expand)
        {
            var expandProjectDTOs = new List<ProjectDTO>();
            foreach (var proj in projectDTOs)
            {
                var toDos = await ToDoDB.Get();
                proj.ToDoDTOList = toDos.Where(t => t.ProjectId == proj.Id).Select(t => DtoMapper.ToDoToToDoDTO(t)).ToList();
                expandProjectDTOs.Add(proj);
            }

            var expandProjects = expandProjectDTOs.Select(pd => DtoMapper.ProjectFromProjectDTO(pd)).ToList();

            return expandProjects;
        }

        var projects = projectDTOs.Select(pd => DtoMapper.ProjectFromProjectDTO(pd)).ToList();
        return projects;
    }

    public static async Task Delete(string DbId)
    {
        await firebase.DeleteAsync($"projects/{DbId}");
    }

    public static async Task<Project?> AddUpdateProject(Project? project)
    {
        if (project == null)
            return project;

        var dto = DtoMapper.ProjectToProjectDTO(project);
        // If its a new Project then add it
        if (dto.Id == 0)
        {
            dto.Id = await GetNextProjectKey();
            await firebase.PushAsync("projects", dto);
        }
        // If its an existing project, overwrite the old one
        else
        {
            // await Delete(dto.DbId);
            // await firebase.PushAsync("projects", dto);
            await firebase.SetAsync("projects", dto);
        }
        return DtoMapper.ProjectFromProjectDTO(dto);

    }
}
