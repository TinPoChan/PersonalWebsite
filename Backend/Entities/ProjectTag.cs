namespace Backend.Entities;
public class ProjectTag
{
    public int ProjectId { get; set; }
    public required Project Project { get; set; }

    public int TagId { get; set; }
    public required Tag Tag { get; set; }
}