using HatuxDevLab.Web.Modules.ProjectTracker.Models;

namespace HatuxDevLab.Web.Modules.ProjectTracker.Models.Responses;

public class ProjectResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ProjectStatus Status { get; set; }
    public ProjectPriority Priority { get; set; }
    public DateTime CreatedAtUtc { get; set; }
}