namespace Academics.Models
{
    public class Office : Entity
    {
        public string? OfficeName { get; set; }
        public string? OfficeLocation { get; set; }
        public bool? OfficeEnabled { get; set; } = false;

        public Instructor? Instructor { get; set; }

    }
}
