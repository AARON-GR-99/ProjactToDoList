namespace Data.Entities;

public class User
{
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;

    public ICollection<Task>? Tasks { get; set; }
}