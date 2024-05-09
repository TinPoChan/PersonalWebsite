using System.ComponentModel.DataAnnotations;

namespace Frontend.Models
{
    public class ProjectSummary
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }
        
        [Required]
        public required string Description { get; set; }

        [Required]
        [Url]
        public required string RepositoryUrl { get; set; }

        [Url]
        public string? ProjectUrl { get; set; }
        
        [Required]
        [Url]
        public required string ImageUrl { get; set; }

        [Required]
        public required string[] Tags { get; set; }

        public DateOnly CreatedDate { get; set; }
    }
}