using Asana.Core.Services;

namespace Asana.Core.Models;
public class AsanaUnit
{
    public List<Project> Projects { get; set; } = new();
    public UnitService unitSvc = new(); 
}
