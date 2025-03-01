using Microsoft.AspNetCore.Mvc;
using BachelorWebApp.Data.Models;
using BachelorWebApp.Data.DBService;

namespace BachelorWebApp.Data.Controller;

[ApiController]
[Route("[controller]")]
public class ProjectController : ControllerBase
{
    private readonly ProjectService _projectService;

    public ProjectController(ProjectService projectService)
    {
        _projectService = projectService;
    }


    private List<Project> GenerateProjects(int count)
    {
        var projects = new List<Project>();
        for (int i = 0; i < count; i++)
        {
            var project = new Project
            {
                Id = Guid.NewGuid(),
                Name = $"Project {i}",
                Description = $"Description {i}",
                Reason = $"Reason {i}",
                StartDate = DateTime.UtcNow.AddDays(-i),
                Status = (i % 2 == 0) ? "Active" : "Pending"
            };
            projects.Add(project);
        }
        return projects;
    }

    [HttpPost("{count:int}/{runs:int}")]
    public async Task<IActionResult> CreateProjectBatch(int count, int runs)
    {
        var projects = GenerateProjects(count);

        long[] time = await _projectService.CreateProjectsAsync(projects, runs);

        return Ok($"Zeiten in ms: {string.Join(", ", time)}");
    }
    
    [HttpGet("{count:int}/{runs:int}")]
    public async Task<IActionResult> GetProjectBatch(int count, int runs)
    {
        var projects = GenerateProjects(count);

       long[] time = await _projectService.GetProjectsAsync(projects, runs);

        return Ok($"Zeiten in ms: {string.Join(", ", time)}");
    }

    [HttpDelete("{count:int}/{runs:int}")]
    public async Task<IActionResult> DeleteProjectBatch(int count, int runs)
    {
        var projects = GenerateProjects(count);

        long[] time = await _projectService.DeleteProjectsAsync(projects, runs);

        return Ok($"Zeiten in ms: {string.Join(", ", time)}");
    }


    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] Project project)
    {
        var createdProject = await _projectService.CreateProjectAsync(project);
        return CreatedAtAction(nameof(GetProjectById), new { id = createdProject.Id }, createdProject);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProjects()
    {
        var projects = await _projectService.GetAllProjectsAsync();
        return Ok(projects);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetProjectById(Guid id)
    {
        var project = await _projectService.GetProjectByIdAsync(id);
        if (project == null) return NotFound();
        return Ok(project);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateProject(Guid id, [FromBody] Project updatedProject)
    {
        var updated = await _projectService.UpdateProjectAsync(id, updatedProject);
        if (updated == null) return NotFound();
        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProject(Guid id)
    {
        var success = await _projectService.DeleteProjectAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}
