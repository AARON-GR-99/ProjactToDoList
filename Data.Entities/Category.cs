namespace Data.Entities;

public class Category
{
    public int CategoryId {  get; set; }
    public string CategoryName { get; set; } = string.Empty;

    public ICollection<Task>? tasks { get; set; } 
}