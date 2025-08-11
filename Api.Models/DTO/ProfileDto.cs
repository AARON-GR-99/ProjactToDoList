namespace Api.Models.DTO;

public class ProfileDto
{
    public int ProfileId { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}