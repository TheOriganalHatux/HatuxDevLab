namespace HatuxDevLab.Web.Modules.ProjectTracker.Models.Requests;

public class UpdateProjectRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public ProjectPriority? Priority { get; set; }
}