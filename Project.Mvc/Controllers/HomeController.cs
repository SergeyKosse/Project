using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project.Mvc.Models;
using Project.Service.Abstract;

namespace Project.Mvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IVehicleService _vehicleService;

    public HomeController(ILogger<HomeController> logger, IVehicleService vehicleService)
    {
        _logger = logger;
        _vehicleService = vehicleService;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.vehicleModelsCount = await _vehicleService.VehicleModelsCount();
        // ViewBag.vehicleModels = _vehicleService.VehicleModels;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
