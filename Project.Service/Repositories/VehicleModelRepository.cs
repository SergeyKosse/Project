using System;
using Project.Service.Models;
using Microsoft.EntityFrameworkCore;
using Project.Service.Abstract;
using Microsoft.Extensions.Options;

namespace Project.Service.Repositories;

public class VehicleModelRepository : IVehicleModelRepository
{
  private readonly ProjectDbContext _context;
  private readonly int _pageSize;

  public VehicleModelRepository(ProjectDbContext context, IOptions<Pagination> pagination)
  {
    _context = context;
    _pageSize = pagination.Value.PageSize;
  }

  public async Task<int> Count()
  {
    return await _context.VehicleModels.CountAsync();
  }

  public async Task<List<VehicleModel>> All(
    int page,
    int pageSize = 0,
    Boolean includeVehicleMakes = true,
    Dictionary<string, object>? options = null)
  {
    var query = _context.VehicleModels.AsQueryable().AsNoTracking();

    int realPageSize = pageSize > 0 ? pageSize : _pageSize;

    var ordering = options != null && options.TryGetValue("order", out var order) && order != null
    ? order.ToString() : "asc";

    var filtering = options != null && options.TryGetValue("filter", out var filter) && filter != null
    ? filter.ToString() : null;

    if (filtering != null)
    {
      query = query.Where(
        p => EF.Functions.ILike(p.Name, "%" + filtering + "%") ||
        EF.Functions.ILike(p.Abrv, "%" + filtering + "%"));
    }

    if (includeVehicleMakes)
    {
      query = query.Include(c => c.VehicleMake);
    }

    return await query
      .Skip((page - 1) * realPageSize)
      .Take(_pageSize)
      .ToListAsync();
  }

  public async Task<List<VehicleModel>?> AllByVehicleMakeId(int vehicleMakeId)
  {
    return await _context.VehicleModels
      .AsNoTracking()
      .Where(x => x.VehicleMakeId == vehicleMakeId)
      .ToListAsync();
  }

  public async Task<VehicleModel?> FindById(int id, Boolean includeVehicleMake = true)
  {
    var query = _context.VehicleModels.AsQueryable().AsNoTracking();

    if (includeVehicleMake)
    {
      query = query.Include(c => c.VehicleMake);
    }

    return await query.FirstOrDefaultAsync(p => p.Id == id);
  }

  public async Task<VehicleModel> Create(VehicleModel vehicleModel)
  {
    await _context.VehicleModels.AddAsync(vehicleModel);
    await _context.SaveChangesAsync();
    return vehicleModel;

  }

  public async Task<VehicleModel?> Update(VehicleModel vehicleModel)
  {
    Console.WriteLine("Repo:Update==========================");
    Console.WriteLine($"id: {vehicleModel.Id}");
    Console.WriteLine($"name: {vehicleModel.Name}");
    Console.WriteLine($"abrv: {vehicleModel.Abrv}");
    Console.WriteLine($"vehicleMakeId: {vehicleModel.VehicleMakeId}");

    var originalVehicleModel = await FindById(vehicleModel.Id);
    if (originalVehicleModel == null)
    {
      return null;
    }

    originalVehicleModel.Name = vehicleModel.Name;
    originalVehicleModel.Abrv = vehicleModel.Abrv;
    
    _context.VehicleModels.Update(originalVehicleModel);

    await _context.SaveChangesAsync();
    return originalVehicleModel;
  }

  public async Task<Boolean> Delete(int id)
  {
    var vehicleModel = await FindById((int)id);

    if (vehicleModel == null)
    {
      return false;
    }

    _context.VehicleModels.Remove(vehicleModel);
    await _context.SaveChangesAsync();
    return true;
  }
}
