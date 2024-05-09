namespace Backend.Dtos;

public class ProjectDto
{       
        public int ProjectId { get; set; }
        public required string Name { get; set; }
        
        public required string Description { get; set; }

        public required string RepositoryUrl { get; set; }

        public string? ProjectUrl { get; set; }

        public required string ImageUrl { get; set; }

        public List<string>? TagNames { get; set; }

        public DateOnly CreatedDate { get; set; }
}
