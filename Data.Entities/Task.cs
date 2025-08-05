namespace Data.Entities;

public class Task
{
    public int TaskId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; } = false;
    public DateTime CreatedDate { get; set; } = DateTime.Now;


    public int UserId { get; set; }
    public User? User { get; set; }

    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    

}