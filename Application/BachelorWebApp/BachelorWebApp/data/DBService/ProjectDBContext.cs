using BachelorWebApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.SqlClient;
using System.Data.Common;


namespace BachelorWebApp.Data.DBService;

public class ProjectDbContext : DbContext
{
    private readonly DbConnection _connection;
    public ProjectDbContext(DbConnection connection)
    {
        _connection = connection;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connection);
        base.OnConfiguring(optionsBuilder);
    }

    public override void Dispose()
    {
        _connection.Close();
        base.Dispose();
    }

    public DbSet<BaseProject> BaseProjects { get; set; }  = default!;
    public DbSet<Project> Projects { get; set; }  = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BaseProject>().ToTable("BaseProjects", schema: "dbo");
        modelBuilder.Entity<Project>().ToTable("Projects", schema: "dbo");
        modelBuilder.Entity<BaseProject>().ToTable(t => t.HasTrigger("BaseProjects"));


        modelBuilder.Entity<Project>()
            .HasOne<BaseProject>()
            .WithOne()
            .HasForeignKey<Project>(p => p.Id);
    }
}

