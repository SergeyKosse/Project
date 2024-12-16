using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Project.Service.Models;

public class VehicleModel
{

  public VehicleModel(){

  }
  public VehicleModel(int id, int vehicleMakeId, string name, string abrv)
  {
    Id = id;
    VehicleMakeId = vehicleMakeId;
    Name = name;
    Abrv = abrv;
  }


  public int Id { get; set; }
  
  public int VehicleMakeId { get; set; }
  
  public VehicleMake? VehicleMake { get; set; }
  
  [Required]
  [Display(Name = "Name")]
  public string Name { get; set;} = string.Empty;
  
  [Required]
  [Display(Name = "Abrv")]
  public string? Abrv {get; set;} = string.Empty;

}
