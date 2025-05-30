using System;
using System.Runtime.CompilerServices;
using Asana.CLI.Interfaces;
using Asana.CLI.Models;
using Asana.CLI.Services;
using Microsoft.VisualBasic;


public static class AsanaUnit
{
    // Main loop
    public static void Run()
    {
        int selection = -1;
        while (selection != 0)
        {
            PrintUnitOptions();
            selection = HandleUnitOptionSelection(selection);
        }
    }
    private static string[] UnitOptions = {
            "Create Project",
            "Delete Project",
            "Update Project",
            "List All Projects",
            "List All ToDos",
        };


    // Prints out the Unit Options
    private static void PrintUnitOptions()
    {
        int optionIndex = 0;
        foreach (string option in UnitOptions)
        {
            Console.WriteLine($"{optionIndex++ + 1}. {option}");
        }
        Console.WriteLine("0. Exit\n");
    }

    private static int HandleUnitOptionSelection(int selection)
    {
        selection = ProjectService.HandleInput("0");
        Console.WriteLine();
        switch (selection)
        {
            case 1:
                UnitService.CreateProject();
                break;
            case 2:
                UnitService.DeleteProject();
                break;
            case 3:
                UnitService.UpdateProject();
                break;
            case 4:
                UnitService.ListProjects();
                break;
            case 5:
                UnitService.ListAllTodos();
                break;
            case 0:
                // Exit case
                break;
            default:
                Console.WriteLine($"You did not enter a valid selection\n");
                break;
        }

        return selection;
    }




}
