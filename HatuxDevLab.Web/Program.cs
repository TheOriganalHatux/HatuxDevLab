using HatuxDevLab.Web.Models.Requests;
using HatuxDevLab.Web.Models.Responses;
using HatuxDevLab.Web.Services;
using HatuxDevLab.Web.Modules.ProjectTracker.Models.Requests;
using HatuxDevLab.Web.Modules.ProjectTracker.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<GreetingService>();
builder.Services.AddSingleton<ProjectTrackerService>();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
    .WithStaticAssets();

app.MapGet("/api/test", (string? name) =>
{
    var userName = name ?? "Unknown";
    return Results.Ok(new
    {
        message = $"Hello {userName}"
    });
});

app.MapPost("/api/post-test", (UserRequest request, GreetingService service) =>
{
    if (string.IsNullOrWhiteSpace(request.Name))
    {
        return Results.BadRequest(new { error = "Name is required" });
    }

    var message = service.CreateGreeting(request.Name);

    return Results.Ok(new
    {
        message = message
    });
});

app.MapPost("/api/projects", (CreateProjectRequest request, ProjectTrackerService service) =>
{
    if (string.IsNullOrWhiteSpace(request.Name))
    {
        return Results.BadRequest(new
        {
            error = "Project name is required"
        });
    }

    var createdProject = service.CreateProject(request);

    return Results.Ok(createdProject);
});

app.MapGet("/api/projects", (ProjectTrackerService service) =>
{
    var projects = service.GetAllProjects();

    return Results.Ok(projects);
});

app.MapGet("/api/projects/{id:int}", (int id, ProjectTrackerService service) =>
{
    var project = service.GetProjectById(id);

    if (project is null)
    {
        return Results.NotFound(new
        {
            error = $"Project with id {id} was not found"
        });
    }

    return Results.Ok(project);
});

app.MapPut("/api/projects/{id:int}/status", (int id, UpdateProjectStatusRequest request, ProjectTrackerService service) =>
{
    if (request.Status is null)
    {
        return Results.BadRequest(new
        {
            error = "Status is required"
        });
    }

    var updatedProject = service.UpdateProjectStatus(id, request);

    if (updatedProject is null)
    {
        return Results.NotFound(new
        {
            error = $"Project with id {id} was not found"
        });
    }

    return Results.Ok(updatedProject);
});
app.MapPut("/api/projects/{id:int}", (int id, UpdateProjectRequest request, ProjectTrackerService service) =>
{
    if (string.IsNullOrWhiteSpace(request.Name))
    {
        return Results.BadRequest(new
        {
            error = "Project name is required"
        });
    }

    if (request.Priority is null)
    {
        return Results.BadRequest(new
        {
            error = "Priority is required"
        });
    }

    var updatedProject = service.UpdateProject(id, request);

    if (updatedProject is null)
    {
        return Results.NotFound(new
        {
            error = $"Project with id {id} was not found"
        });
    }

    return Results.Ok(updatedProject);
});

app.Run();

