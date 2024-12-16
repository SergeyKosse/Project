using System;
using Project.Service.Models;

namespace Project.Service.Abstract;

public interface IVehicleService
{
  // VehicleMake
  Task<VehicleMake?> VehicleMakeById(int id, Boolean includeVehicleModels = true);
  
  // Task<List<VehicleMake>?> VehicleMakes(
  //     int page = 1, 
  //     int pageSize = 0, 
  //     Boolean includeVehicleModels = true, 
  //     Dictionary<string, object>? options = null);
  
  Task<PagedResult<VehicleMake>> VehicleMakesPaged(
    QueryParameters queryParams, 
    Boolean includeVehicleModels = true);
  
  Task<int> VehicleMakesCount();
  Task<int> VehicleMakesTotalPages();
  Task<VehicleMake> CreateVehicleMake(VehicleMake vehicleMake);
  Task<VehicleMake?> UpdateVehicleMake(VehicleMake vehicleMake);
  Task<Boolean> DeleteVehicleMake(int id);


  // VehicleModel
  Task<List<VehicleModel>?> VehicleModels(
    int page = 1, 
      int pageSize = 0, 
      Boolean includeVehicleMakes = true, 
      Dictionary<string, object>? options = null
  );
  Task<int> VehicleModelsCount();
  Task<VehicleModel?> VehicleModelById(int id, Boolean incdeVehicleModels = true);
  Task<List<VehicleModel>?> VehicleModelsByVehicleMakeId(int vehicleMakeId);
  Task<VehicleModel> CreateVehicleModel(VehicleModel vehicleModel);
  Task<VehicleModel?> UpdateVehicleModel(VehicleModel vehicleModel);
  Task<Boolean> DeleteVehicleModel(int id);
}
