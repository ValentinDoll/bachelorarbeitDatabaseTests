using BachelorWebApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BachelorWebApp.Data.DBService;

public class ProjectService
{
    private readonly ProjectDbContext _context;

    public ProjectService(ProjectDbContext context)
    {
        _context = context;
    }

    public async Task<Project> CreateProjectAsync(Project project)
    {
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
        return project;
    }

    public async Task<long[]> CreateProjectsAsync(List<Project> projects, int runs)
    {
        long[] times = new long[runs];
        for (int i = 0; i < runs; i++)
        {
        
            var stopwatch = Stopwatch.StartNew();

                foreach (var project in projects)
                {
                    _context.Projects.Add(project);
                    await _context.SaveChangesAsync();
                }
            stopwatch.Stop();
            
            _context.Projects.RemoveRange(projects);
            await _context.SaveChangesAsync();
            times[i] = stopwatch.ElapsedMilliseconds;
        }
        return times;
    }

    public async Task<long[]> GetProjectsAsync(List<Project> projects, int runs)
    {
        long[] times = new long[runs];
        Project? projectResult;


        for (int i = 0; i < runs; i++)
        {
            _context.Projects.AddRange(projects);
            await _context.SaveChangesAsync();

            var stopwatch = Stopwatch.StartNew();
    
            foreach (var project in projects)
            {
                projectResult = await _context.Projects.FirstOrDefaultAsync(p => p.Id == project.Id);
            }

            stopwatch.Stop();
            times[i] = stopwatch.ElapsedMilliseconds;

            _context.Projects.RemoveRange(projects);
            await _context.SaveChangesAsync();
        }


        return times;
    }


    public async Task<long[]> DeleteProjectsAsync(List<Project> projects, int runs)
    {
        long[] times = new long[runs];

        for (int i = 0; i < runs; i++)
        {
            _context.Projects.AddRange(projects);
            await _context.SaveChangesAsync();

            var stopwatch = Stopwatch.StartNew();

            foreach (var project in projects)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            }

            stopwatch.Stop();
            times[i] = stopwatch.ElapsedMilliseconds;
        }  
        return times;
    }

    public async Task<List<Project>> GetAllProjectsAsync()
    {
        return await _context.Projects.ToListAsync();
    }

    public async Task<Project?> GetProjectByIdAsync(Guid id)
    {
        return await _context.Projects
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Project?> UpdateProjectAsync(Guid id, Project updatedProject)
    {
        var existingProject = await _context.Projects
            .FirstOrDefaultAsync(p => p.Id == id);

        if (existingProject == null) return null;

        existingProject.Name = updatedProject.Name;
        existingProject.StartDate = updatedProject.StartDate;
        existingProject.Status = updatedProject.Status;
        existingProject.Description = updatedProject.Description;
        existingProject.Reason = updatedProject.Reason;

        await _context.SaveChangesAsync();
        return existingProject;
    }

    public async Task<bool> DeleteProjectAsync(Guid id)
    {


         var project = await _context.Projects
             .FirstOrDefaultAsync(p => p.Id == id);

         if (project == null) return false;

         _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
        return true;

    }
}

