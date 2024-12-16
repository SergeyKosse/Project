using Microsoft.EntityFrameworkCore;
using Project.Service.Models;


namespace Project.Service;

public class ProjectDbContext : DbContext
{
  public ProjectDbContext(DbContextOptions<ProjectDbContext> options) 
    : base(options)
  {
  }

  public DbSet<VehicleModel> VehicleModels { get; set; }
  public DbSet<VehicleMake> VehicleMakes { get; set; }
}
