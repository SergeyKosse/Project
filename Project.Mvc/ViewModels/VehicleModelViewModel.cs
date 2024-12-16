using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Service.Models;

namespace Project.Mvc.ViewModels;

public class VehicleModelViewModel
{
  public required VehicleModel vehicleModel { get; set; }
  public IEnumerable<SelectListItem> vehicleMakeList { get; set; } = new List<SelectListItem>();
  public required VehicleMake selectedVehicleMake { get; set; } 
}
