using System;
using Project.Service.Models;

namespace Project.Mvc.ViewModels;

public class VehicleMakeViewModel
{
    public QueryParameters QueryParams { get; set; } = new QueryParameters();
    public PagedResult<VehicleMake> PagedResult { get; set; } = new PagedResult<VehicleMake>();
}