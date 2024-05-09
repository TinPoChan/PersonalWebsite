using System.Text.Json.Serialization;

namespace Backend.Entities;

public class Project
{
        public int ProjectId { get; set; }

        public required string Name { get; set; }
        
        public required string Description { get; set; }

        public required string RepositoryUrl { get; set; }

        public string? ProjectUrl { get; set; }

        public required string ImageUrl { get; set; }

        public DateOnly CreatedDate { get; set; }

        public List<ProjectTag> ProjectTags { get; set; } = new List<ProjectTag>();
}
