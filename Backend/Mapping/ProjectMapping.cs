using Backend.Dtos;
using Backend.Entities;

namespace Backend.Mapping;

public static class ProjectMapping
{
    public static ProjectDto MapToDto(Project project)
    {
        return new ProjectDto
        {
            ProjectId = project.ProjectId,
            Name = project.Name,
            Description = project.Description,
            RepositoryUrl = project.RepositoryUrl,
            ProjectUrl = project.ProjectUrl,
            ImageUrl = project.ImageUrl,
            CreatedDate = project.CreatedDate,
            TagNames = project.ProjectTags?.Select(pt => pt.Tag?.Name ?? string.Empty).ToList() ?? new List<string>()
        };
    }

}