using System;
using Project.Service.Models;
using Microsoft.EntityFrameworkCore;
using Project.Service.Abstract;
using Microsoft.Extensions.Options;
using Project.Service.Exceptions;

namespace Project.Service.Repositories;

public class VehicleMakeRepository : IVehicleMakeRepository
{
  private readonly ProjectDbContext _context;
  private readonly int _pageSize;

  public VehicleMakeRepository(ProjectDbContext context, IOptions<Pagination> pagination)
  {
    _context = context;
    _pageSize = pagination.Value.PageSize;
  }

  public async Task<int> Count()
  {
    return await _context.VehicleMakes.CountAsync();
  }

  public async Task<int> TotalPages()
  {
    var count = await Count();
    int totalPages = (int)Math.Ceiling(count / (double)_pageSize);
    Console.WriteLine("===total pages count: " + totalPages);
    return totalPages;
  }

  public async Task<(List<VehicleMake>, int)> All(
    QueryParameters queryParams,
    Boolean includeVehicleModels = true)
  {
    var query = _context.VehicleMakes.AsQueryable().AsNoTracking();

    if (includeVehicleModels)
    {
      query.Include(c => c.VehicleModels);
    }

    if (queryParams.Filter != null)
    {
      query = query.Where(
        p => EF.Functions.ILike(p.Name, "%" + queryParams.Filter + "%") ||
        EF.Functions.ILike(p.Abrv, "%" + queryParams.Filter + "%"));
    }


    if (queryParams.OrderBy != null)
    {
      query = queryParams.Descending
        ? query.OrderByDescending(x => EF.Property<object>(x, queryParams.OrderBy))
        : query.OrderBy(x => EF.Property<object>(x, queryParams.OrderBy));
    }

    var TotalCount = await query.CountAsync();

    int realPageSize = queryParams.PageSize > 0 ? queryParams.PageSize : _pageSize;

    query = query.Skip((queryParams.Page - 1) * realPageSize).Take(realPageSize);

    var VehicleMakes = await query.ToListAsync();

    return (VehicleMakes, TotalCount);
  }

  public async Task<VehicleMake?> FindById(int id, Boolean includeVehicleModels = true)
  {
    var query = _context.VehicleMakes.AsQueryable().AsNoTracking();

    if (includeVehicleModels)
    {
      query = query.Include(c => c.VehicleModels);
    }

    var item = await query.FirstOrDefaultAsync(p => p.Id == id);

    if (item == null)
    {
      throw new RecordNotFoundException("Record not found");
    }

    return item; 
  }

  public async Task<VehicleMake> Create(VehicleMake vehicleMake)
  {
    _context.VehicleMakes.Add(vehicleMake);
    await _context.SaveChangesAsync();
    return vehicleMake;
  }

  public async Task<VehicleMake?> Update(VehicleMake vehicleMake)
  {
    var existingVehicleMake = await _context.VehicleMakes.FindAsync(vehicleMake.Id);

    if (existingVehicleMake == null)
    {
      return null;
    }

    existingVehicleMake.Name = vehicleMake.Name;
    existingVehicleMake.Abrv = vehicleMake.Abrv;

    await _context.SaveChangesAsync();

    return existingVehicleMake;
  }

  public async Task<Boolean> Delete(int id)
  {
    var vehicleMake = await FindById((int)id);

    if (vehicleMake == null)
    {
      return false;
    }

    if (vehicleMake.VehicleModels.Count > 0)
    {
      throw new InvalidOperationException("First remove related VehicleModels");
    }

    _context.VehicleMakes.Remove(vehicleMake);
    await _context.SaveChangesAsync();
    return true;
  }
}
