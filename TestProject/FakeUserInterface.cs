using System;
using Asana.CLI.Interfaces;

namespace TestProject;

public class FakeUserInterface : IUserInterface
{
    public Queue<string> Inputs = new Queue<string>();
    public List<string> Outputs = new List<string>();
    public void AddInput(string input) => Inputs.Enqueue(input);


    public void Write(string message) => Outputs.Add(message);
    public void WriteLine(string message) => Outputs.Add(message);
    public void WriteLine() => Outputs.Add("\n");
    public string? ReadLine() => Inputs.Count > 0 ? Inputs.Dequeue() : "";

}
