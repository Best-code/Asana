using Asana.Core.Services;

namespace Asana.Core.Models;
public class AsanaUnit
{
    // public List<Project> Projects { get; set; } = new();
    public List<Project> Projects { get; set; } = new List<Project>()
    {
        new Project("Proj 1", "Desc 1"),
        new Project("Proj 2", "Desc 2"),
    };

    public UnitService unitSvc = new(); 
}
