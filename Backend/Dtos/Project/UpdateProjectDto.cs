namespace Backend.Dtos;

public record class UpdateProjectDto
{
        public string? Name { get; set; }
        
        public string? Description { get; set; }

        public string? RepositoryUrl { get; set; }

        public string? ProjectUrl { get; set; }

        public string? ImageUrl { get; set; }
        
        public List<string>? TagNames { get; set; }
        
        public DateOnly CreatedDate { get; set; }
}