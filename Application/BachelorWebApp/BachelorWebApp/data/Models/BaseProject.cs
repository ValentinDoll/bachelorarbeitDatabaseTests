namespace BachelorWebApp.Data.Models;

public class BaseProject
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public DateTime StartDate { get; set; }
    public required string Status { get; set; }
}

