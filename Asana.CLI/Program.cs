using System;
using MyApp.Models;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Project> projects = new List<Project>();
            Project basic = new Project { Name = "Basic ToDo Project", Description = "This is my basic todo project" };

            basic.Use();
        }
    }
}