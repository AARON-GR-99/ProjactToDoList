namespace Data.Entities
{
    public class Profile
    {
        public int ProfileId { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        
        public ICollection<User>? Users { get; set; }
    }
}