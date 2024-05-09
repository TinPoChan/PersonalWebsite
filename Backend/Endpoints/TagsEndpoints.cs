using Backend.Dtos;
using Backend.Mapping;
using Backend.Services;

namespace Backend.Apis;

public static class TagsEndpoints
{
    public static RouteGroupBuilder MapTagsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/tags");
        //Get all tags
        group.MapGet("", async (TagService tagService) =>
        {
            var tags = await tagService.GetAllTagsAsync();
            return Results.Ok(tags);
        });

        //Get a tag by id
        group.MapGet("/{id}", async (TagService tagService, int id) =>
        {
            var tag = await tagService.GetTagByIdAsync(id);
            if (tag is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(tag);
        });

        //Create a new tag
        group.MapPost("/", async (CreateTagDto newTag, TagService tagService) =>
        {
            var tag = await tagService.CreateTagAsync(newTag);
            var tagDto = TagMapping.MapToDto(tag);
            return Results.Created($"/tags/{tag.TagId}", tagDto);
        });

        //Update a tag
        group.MapPut("/{id}", async (int id, UpdateTagDto updatedTag, TagService tagService) =>
        {
            var tag = await tagService.UpdateTagAsync(id, updatedTag);
            if (tag is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(tag);
        });

        //Delete a tag
        group.MapDelete("/{id}", async (int id, TagService tagService) =>
        {
            var deleted = await tagService.DeleteTagAsync(id);
            if (!deleted)
            {
                return Results.NotFound();
            }
            return Results.NoContent();
        });

        return group;
    }
}