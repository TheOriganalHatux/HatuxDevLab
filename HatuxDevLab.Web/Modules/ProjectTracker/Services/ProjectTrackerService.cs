using HatuxDevLab.Web.Modules.ProjectTracker.Models;
using HatuxDevLab.Web.Modules.ProjectTracker.Models.Requests;
using HatuxDevLab.Web.Modules.ProjectTracker.Models.Responses;

namespace HatuxDevLab.Web.Modules.ProjectTracker.Services;

public class ProjectTrackerService
{
    private readonly List<Project> _projects = [];
    private int _nextId = 1;

    public ProjectResponse CreateProject(CreateProjectRequest request)
    {
        var project = new Project
        {
            Id = _nextId++,
            Name = request.Name!.Trim(),
            Description = request.Description?.Trim(),
            Status = ProjectStatus.Planned,
            Priority = request.Priority ?? ProjectPriority.Medium,
            CreatedAtUtc = DateTime.UtcNow
        };

        _projects.Add(project);

        return MapToResponse(project);
    }

    public List<ProjectResponse> GetAllProjects()
    {
        return _projects
            .Select(MapToResponse)
            .ToList();
    }

    public ProjectResponse? GetProjectById(int id)
    {
        var project = _projects.FirstOrDefault(p => p.Id == id);

        if (project is null)
        {
            return null;
        }

        return MapToResponse(project);
    }

    public ProjectResponse? UpdateProjectStatus(int id, UpdateProjectStatusRequest request)
    {
        var project = _projects.FirstOrDefault(p => p.Id == id);

        if (project is null || request.Status is null)
        {
            return null;
        }

        project.Status = request.Status.Value;

        return MapToResponse(project);
    }

    private static ProjectResponse MapToResponse(Project project)
    {
        return new ProjectResponse
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            Status = project.Status,
            Priority = project.Priority,
            CreatedAtUtc = project.CreatedAtUtc
        };
    }
}