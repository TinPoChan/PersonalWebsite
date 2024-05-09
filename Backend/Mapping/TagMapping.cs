using Backend.Dtos;
using Backend.Entities;

namespace Backend.Mapping;

public static class TagMapping
{
    public static TagDto MapToDto(Tag tag)
    {
        return new TagDto
        {
            TagId = tag.TagId,
            Name = tag.Name
        };
    }
}