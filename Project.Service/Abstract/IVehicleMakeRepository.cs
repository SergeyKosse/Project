using System;
using Project.Service.Models;

namespace Project.Service.Abstract;

public interface IVehicleMakeRepository
{
  Task<int> Count();
  Task<int> TotalPages();
  // Task<List<VehicleMake>> All(
  //   int page, 
  //   int pageSize, 
  //   Boolean includeVehicleModels, 
  //   Dictionary<string, object>? options = null);
  Task<(List<VehicleMake>, int)> All(
    QueryParameters queryParams,
    Boolean includeVehicleModels = true);
  Task<VehicleMake?> FindById(int id, Boolean includeVehicleModels);
  Task<VehicleMake> Create(VehicleMake vehicleMake);
  Task<VehicleMake?> Update(VehicleMake vehicleMake);
  Task<Boolean> Delete(int id);
}
