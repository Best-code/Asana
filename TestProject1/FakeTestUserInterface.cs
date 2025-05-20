using System;
using Asana.CLI.Interfaces

namespace Asana.CLI.IO
{
    public class FakeTestUserInterface : IUserInterface
    {
        public void Write(string message) => Console.Write(message);
        public void WriteLine(string message) => Console.WriteLine(message);
        public void WriteLine() => Console.WriteLine();
        public string? ReadLine() => Console.ReadLine();
        
    }
}
