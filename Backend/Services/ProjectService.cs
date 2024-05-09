using Backend.Data;
using Backend.Dtos;
using Backend.Entities;
using Backend.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public class ProjectService(ProjectDbContext db)
{
    private readonly ProjectDbContext _db = db;

    // Get all projects
    public async Task<List<ProjectDto>> GetAllProjectsAsync()
    {
        var projects = await _db.Projects
            .Include(p => p.ProjectTags)
            .ThenInclude(pt => pt.Tag)
            .AsNoTracking()
            .ToListAsync();
            

        return projects.Select(p => ProjectMapping.MapToDto(p)).ToList();
    }

    // Get a project by id
    public async Task<ProjectDto?> GetProjectByIdAsync(int id)
    {
        var project = await _db.Projects
            .Include(p => p.ProjectTags)
            .ThenInclude(pt => pt.Tag)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.ProjectId == id);
            
        return project is null ? null : ProjectMapping.MapToDto(project);
    }

    // Create a new project
    public async Task<Project> CreateProjectAsync(CreateProjectDto newProject)
    {
        var project = new Project
        {
            Name = newProject.Name,
            Description = newProject.Description,
            RepositoryUrl = newProject.RepositoryUrl,
            ProjectUrl = newProject.ProjectUrl,
            ImageUrl = newProject.ImageUrl,
            CreatedDate = newProject.CreatedDate,
            ProjectTags = new List<ProjectTag>()
        };

        if (newProject.TagNames is not null)
        {
            var uniqueTags = new HashSet<string>(newProject.TagNames);
            foreach (var tagName in uniqueTags)
            {
                var tag = await _db.Tags.FirstOrDefaultAsync(t => t.Name == tagName)
                          ?? new Tag { Name = tagName };
                if (tag.TagId == 0)
                {
                    _db.Tags.Add(tag);
                }
                project.ProjectTags.Add(new ProjectTag { Project = project, Tag = tag });
            }
        }

        _db.Projects.Add(project);
        await _db.SaveChangesAsync();

        return project;
    }

    // Update a project
    public async Task<ProjectDto?> UpdateProjectAsync(int id, UpdateProjectDto updateDto)
    {
        var project = await _db.Projects
            .Include(p => p.ProjectTags)
            .ThenInclude(pt => pt.Tag)
            .FirstOrDefaultAsync(p => p.ProjectId == id);

        if (project is null)
        {
            return null;
        }

        if (updateDto.Name is not null)
        {
            project.Name = updateDto.Name;
        }

        if (updateDto.Description is not null)
        {
            project.Description = updateDto.Description;
        }

        if (updateDto.RepositoryUrl is not null)
        {
            project.RepositoryUrl = updateDto.RepositoryUrl;
        }

        if (updateDto.ProjectUrl is not null)
        {
            project.ProjectUrl = updateDto.ProjectUrl;
        }

        if (updateDto.ImageUrl is not null)
        {
            project.ImageUrl = updateDto.ImageUrl;
        }

        if (updateDto.CreatedDate != default)
        {
            project.CreatedDate = updateDto.CreatedDate;
        }

        // Handle tags updates
        if (updateDto.TagNames != null)
        {
            // Safe access to Tag names, avoiding null references
            var existingTags = project.ProjectTags
                .Where(pt => pt.Tag != null)
                .Select(pt => pt.Tag!.Name)
                .ToList();

            var tagsToAdd = updateDto.TagNames.Except(existingTags).ToList();
            var tagsToRemove = existingTags.Except(updateDto.TagNames).ToList();

            // Adding new tags
            foreach (var tag in tagsToAdd)
            {
                var newTag = await _db.Tags.FirstOrDefaultAsync(t => t.Name == tag) ?? new Tag { Name = tag };
                if (newTag.TagId == 0) // The tag does not exist in the database, add it
                {
                    _db.Tags.Add(newTag);
                }
                project.ProjectTags.Add(new ProjectTag { Project = project, Tag = newTag });
            }

            // Removing tags
            foreach (var tagName in tagsToRemove)
            {
                var tagToRemove = project.ProjectTags.FirstOrDefault(pt => pt.Tag?.Name == tagName); // Safe check for Tag name
                if (tagToRemove != null)
                {
                    project.ProjectTags.Remove(tagToRemove);
                }
            }
        }


        await _db.SaveChangesAsync();

        return ProjectMapping.MapToDto(project);
    }

    // Delete a project
    public async Task<bool> DeleteProjectAsync(int projectId)
    {
        var project = await _db.Projects
            .Include(p => p.ProjectTags)
            .FirstOrDefaultAsync(p => p.ProjectId == projectId);

        if (project == null)
        {
            return false;
        }

        _db.Projects.Remove(project);
        await _db.SaveChangesAsync();
        return true;
    }
}
