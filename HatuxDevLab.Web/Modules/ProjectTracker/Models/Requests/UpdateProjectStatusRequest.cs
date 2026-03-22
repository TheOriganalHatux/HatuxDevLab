using HatuxDevLab.Web.Modules.ProjectTracker.Models;

namespace HatuxDevLab.Web.Modules.ProjectTracker.Models.Requests;

public class UpdateProjectStatusRequest
{
    public ProjectStatus? Status { get; set; }
}