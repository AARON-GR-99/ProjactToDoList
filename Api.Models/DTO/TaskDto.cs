namespace Api.Models.DTO;

public class TaskDto
{
    public int TaskId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public DateTime CreatedData { get; set; }


    public int UserId { get; set; }
    public int CategoryId { get; set; }
}
