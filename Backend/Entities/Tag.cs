namespace Backend.Entities
{
    public class Tag
    {
        public int TagId { get; set; }

        public required string Name { get; set; }

        public List<ProjectTag> ProjectTags { get; set; } = new List<ProjectTag>();
    }
}