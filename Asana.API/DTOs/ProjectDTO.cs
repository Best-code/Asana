using Asana.Core.Models;
public class ProjectDTO
{
    public int Id { get; set; }
    public string? DbId { get; set; }
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public List<ToDoDTO>? ToDoDTOList { get; set; }
    public string DtoProjTest = "SUPER AWESOME PRJOECT DTO";
}