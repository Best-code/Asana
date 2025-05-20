using System;

namespace Asana.CLI.Interfaces
{
    public interface IUserInterface
    {
        void Write(string message);
        void WriteLine(string message);
        void WriteLine();
        string? ReadLine();
    }
}
