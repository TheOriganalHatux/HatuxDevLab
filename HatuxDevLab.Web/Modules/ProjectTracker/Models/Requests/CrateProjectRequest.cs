using HatuxDevLab.Web.Modules.ProjectTracker.Models;

namespace HatuxDevLab.Web.Modules.ProjectTracker.Models.Requests;

public class CreateProjectRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public ProjectPriority? Priority { get; set; }
}