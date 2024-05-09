using Backend.Data;
using Backend.Dtos;
using Backend.Entities;
using Backend.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public class TagService(ProjectDbContext db)
{
    private readonly ProjectDbContext _db = db;

    // Get all tags
    public async Task<List<TagDto>> GetAllTagsAsync()
    {
        var tags = await _db.Tags
            .AsNoTracking()
            .ToListAsync();

        return tags.Select(t => TagMapping.MapToDto(t)).ToList();
    }

    // Get a tag by id
    public async Task<TagDto?> GetTagByIdAsync(int id)
    {
        var tag = await _db.Tags
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TagId == id);

        return tag is null ? null : TagMapping.MapToDto(tag);
    }

    // Create a new tag
    public async Task<Tag> CreateTagAsync(CreateTagDto newTag)
    {
        var tag = new Tag
        {
            Name = newTag.Name
        };

        _db.Tags.Add(tag);
        await _db.SaveChangesAsync();

        return tag;
    }

    // Update a tag
    public async Task<Tag?> UpdateTagAsync(int id, UpdateTagDto updatedTag)
    {
        var tag = await _db.Tags.FirstOrDefaultAsync(t => t.TagId == id);
        if (tag is null)
        {
            return null;
        }

        tag.Name = updatedTag.Name;

        await _db.SaveChangesAsync();

        return tag;
    }

    // Delete a tag
    public async Task<bool> DeleteTagAsync(int id)
    {
        var tag = await _db.Tags.FirstOrDefaultAsync(t => t.TagId == id);
        if (tag is null)
        {
            return false;
        }

        _db.Tags.Remove(tag);
        await _db.SaveChangesAsync();

        return true;
    }
}