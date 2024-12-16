using System;
using Project.Service.Repositories;
using Project.Service.Abstract;
using Project.Service.Models;
using Project.Service.Exceptions;

namespace Project.Service.Managers;

public class VehicleService : IVehicleService
{
  private readonly IVehicleModelRepository _vehicleModelRepository;
  private readonly IVehicleMakeRepository _vehicleMakeRepository;

  public VehicleService(
    IVehicleMakeRepository vehicleMakeRepository,
    IVehicleModelRepository vehicleModelRepository)
  {
    _vehicleMakeRepository = vehicleMakeRepository;
    _vehicleModelRepository = vehicleModelRepository;
  }

  public async Task<VehicleMake?> VehicleMakeById(int id, Boolean includeVehicleModels = true)
  {
    try 
    {
      var vehicleMake = await _vehicleMakeRepository.FindById(id, includeVehicleModels);
      return vehicleMake;
    }
    catch (RecordNotFoundException ex)
    {
      throw new ServiceException("Record dot found: " + ex.Message);
    }
    
  }

  // public async Task<List<VehicleMake>?> VehicleMakes(
  //   int page = 1,
  //   int pageSize = 0,
  //   Boolean includeVehicleModels = true,
  //   Dictionary<string, object>? options = null)
  // {
  //   return await _vehicleMakeRepository.All(page, pageSize, includeVehicleModels, options);
  // }

  public async Task<PagedResult<VehicleMake>> VehicleMakesPaged(
    QueryParameters queryParams,
    Boolean includeVehicleModels = true)
  {
    var (vehicleMakes, totalCount) = await _vehicleMakeRepository.All(queryParams, includeVehicleModels);

    return new PagedResult<VehicleMake>
    {
      Items = vehicleMakes,
      TotalCount = totalCount,
      Page = queryParams.Page,
      PageSize = queryParams.PageSize
    };
  }

  public async Task<int> VehicleMakesCount()
  {
    return await _vehicleMakeRepository.Count();
  }

  public async Task<int> VehicleMakesTotalPages()
  {
    return await _vehicleMakeRepository.TotalPages();
  }

  public async Task<VehicleMake> CreateVehicleMake(VehicleMake vehicleMake)
  {
    return await _vehicleMakeRepository.Create(vehicleMake);
  }

  public async Task<VehicleMake?> UpdateVehicleMake(VehicleMake vehicleMake)
  {
    return await _vehicleMakeRepository.Update(vehicleMake);
  }

  public async Task<Boolean> DeleteVehicleMake(int id)
  {
    return await _vehicleMakeRepository.Delete(id);
  }



  public async Task<VehicleModel?> VehicleModelById(int id, Boolean includeVehicleMake = true)
  {
    return await _vehicleModelRepository.FindById(id, includeVehicleMake);
  }

  public async Task<List<VehicleModel>?> VehicleModels(
    int page = 1,
    int pageSize = 0,
    Boolean includeVehicleMakes = true,
    Dictionary<string, object>? options = null)
  {
    return await _vehicleModelRepository.All(page, pageSize, includeVehicleMakes, options);
  }

  public async Task<List<VehicleModel>?> VehicleModelsByVehicleMakeId(int vehicleMakeId)
  {
    return await _vehicleModelRepository.AllByVehicleMakeId(vehicleMakeId);
  }

  public async Task<int> VehicleModelsCount()
  {
    return await _vehicleModelRepository.Count();
  }

  public async Task<VehicleModel> CreateVehicleModel(VehicleModel vehicleModel)
  {
    return await _vehicleModelRepository.Create(vehicleModel);
  }

  public async Task<VehicleModel?> UpdateVehicleModel(VehicleModel vehicleModel)
  {
    return await _vehicleModelRepository.Update(vehicleModel);
  }

  public async Task<Boolean> DeleteVehicleModel(int id)
  {
    return await _vehicleModelRepository.Delete(id);
  }
}
