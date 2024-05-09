using Backend.Dtos;
using Backend.Mapping;
using Backend.Services;

namespace Backend.Apis;

public static class ProjectsEndpoints
{
    public static RouteGroupBuilder MapProjectsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/projects");

        // Get all projects 
        group.MapGet("", async (ProjectService projectService) =>
        {
            var projects = await projectService.GetAllProjectsAsync();
            return Results.Ok(projects);

        });

        // Get a project by id
        group.MapGet("/{id}", async (ProjectService projectService, int id) =>
        {
            var project = await projectService.GetProjectByIdAsync(id);
            if (project is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(project);
        });

        // Create a new project
        group.MapPost("/", async (CreateProjectDto newProject, ProjectService projectService) =>
        {
            var project = await projectService.CreateProjectAsync(newProject);
            var projectDto = ProjectMapping.MapToDto(project);
            return Results.Created($"/projects/{project.ProjectId}", projectDto);
        });

        // Update a project
        group.MapPut("/{id}", async (int id, UpdateProjectDto updatedProject, ProjectService projectService) =>
        {
            var project = await projectService.UpdateProjectAsync(id, updatedProject);
            if (project is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(project);
        });

        // Delete a project
        group.MapDelete("/{id}", async (int id, ProjectService projectService) =>
        {
            var deleted = await projectService.DeleteProjectAsync(id);
            if (!deleted)
            {
                return Results.NotFound();
            }
            return Results.NoContent();
        });


        return group;
    }
}