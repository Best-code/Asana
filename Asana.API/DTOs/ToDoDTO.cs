public class ToDoDTO
{
    public int Id { get; set; }
    public string? DbId { get; set; }
    public DateTime? DueDate { get; set; }
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public int Priority { get; set; }
    public bool IsComplete { get;  set;}
    public int ProjectId { get; set; }
    public string DtoTest = "SUPER AEWSOME TODO DTO";
}