using System;
using MyApp.Models;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<ToDo> ToDos = new List<ToDo>();

            int selection = 0;
            do
            {
                PrintOptions();
                selection = HandleSelection(selection, ToDos);
            } while (selection != 2);
            
        }

        static void PrintOptions()
        {
            Console.WriteLine($"1. Create ToDo");
            Console.WriteLine($"2. Exit");
        }

        static int HandleInput()
        {
            var choice = Console.ReadLine() ?? "2";
            int choiceInt;
            int.TryParse(choice, out choiceInt);

            return choiceInt;
        }

        static int HandleSelection(int selection, List<ToDo> ToDos)
        {
            selection = HandleInput();
            switch (selection)
            {
                case 1:
                    CreateTodo(ToDos);
                    break;
                case 2:
                    break;
                default:
                    Console.WriteLine($"You did not enter a valid selection");
                    break;
            }

            return selection;
        }

        static void CreateTodo(List<ToDo> ToDos)
        {
            ToDo createMe = new ToDo();
            Console.WriteLine($"Name: ");
            createMe.Name = Console.ReadLine();
            Console.WriteLine($"Description: ");
            createMe.Description = Console.ReadLine();

            ToDos.Add(createMe);            

            Console.WriteLine($"{createMe}");
            
        }

    }
}