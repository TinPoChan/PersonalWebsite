using System.Text.Json.Serialization;

namespace Backend.Dtos;

public record class CreateProjectDto
{
        public required string Name { get; set; }
        
        public required string Description { get; set; }

        public required string RepositoryUrl { get; set; }

        public string? ProjectUrl { get; set; }

        public required string ImageUrl { get; set; }
        
        public List<string>? TagNames { get; set; }
        public DateOnly CreatedDate { get; set; }
}