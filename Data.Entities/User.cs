namespace Data.Entities;

public class User
{
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    
    public int ProfileId { get; set; }
    public Profile? Profile { get; set; }

    public ICollection<Task>? Tasks { get; set; }
}