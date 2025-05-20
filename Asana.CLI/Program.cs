using System;
using Asana.CLI.Interfaces;
using Asana.CLI.IO;
using MyApp.Models;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IUserInterface ui = new ConsoleUserInterface();
            AsanaUnit unit = new AsanaUnit(ui);
            unit.Run();
        }
    }
}