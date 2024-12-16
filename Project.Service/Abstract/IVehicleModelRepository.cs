using System;
using Project.Service.Models;

namespace Project.Service.Abstract;

public interface IVehicleModelRepository
{
  Task <int> Count();
  Task<List<VehicleModel>> All(
    int page, 
    int pageSize, 
    Boolean includeVehicleMakes = true,
    Dictionary<string, object>? options = null);
  Task<List<VehicleModel>?> AllByVehicleMakeId(int vehicleMakeId);
  Task<VehicleModel?> FindById(int id, Boolean includeVehicleMake = true);
  Task<VehicleModel> Create(VehicleModel vehicleModel);
  Task<VehicleModel?> Update(VehicleModel vehicleModel);
  Task<Boolean> Delete(int id);
}
