using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Service.Models;

public class VehicleMake
{

  public VehicleMake(){

  }
  
  public VehicleMake(int id, string name, string abrv)
  {
    Id = id;
    Name = name;
    Abrv = abrv;
  }

  
  
  public int Id { get; set; }
  
  public virtual ICollection<VehicleModel> VehicleModels { get; set; } = new List<VehicleModel>();

  [Required]
  [Display(Name = "Name")]
  public string Name { get; set;} = string.Empty;
  
  [Display(Name = "Abrv")]
  public string? Abrv {get; set;}
}
