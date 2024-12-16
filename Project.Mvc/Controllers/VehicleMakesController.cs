using Microsoft.AspNetCore.Mvc;
using Project.Mvc.ViewModels;

// using Project.Mvc.ViewModels;
using Project.Service.Abstract;
using Project.Service.Exceptions;
using Project.Service.Models;
// using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;


namespace Project.Mvc.Controllers
{
    public class VehicleMakesController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVehicleService _vehicleService;

        public VehicleMakesController(ILogger<HomeController> logger, IVehicleService vehicleService)
        {
            _logger = logger;
            _vehicleService = vehicleService;
        }

        // [HttpGet]
        // public async Task<IActionResult> Index()
        // {
        //     var page = int.TryParse(Request.Query["page"], out var p) ? p : 1;
        //     var orderField = Request.Query["orderField"].ToString() ?? "Name";
        //     var orderDir = Request.Query["order"].FirstOrDefault()?.ToLower() ?? "asc";
        //     orderDir = new[] { "asc", "desc" }.Contains(orderDir) ? orderDir : "asc";
        //     var filter = Request.Query["filter"].ToString() ?? "";

        //     // var totalPages = (int)Math.Ceiling((double)ViewBag.TotalCount / ViewBag.PageSize);
        //     var queryParams = new Dictionary<string, string>();

        //     // ===========================
        //     foreach (var key in HttpContext.Request.Query.Keys)
        //     {
        //         var value = HttpContext.Request.Query[key];
        //         if (!string.IsNullOrEmpty(value))
        //         {
        //             queryParams[key] = value.ToString();
        //         }
        //     }


        //     // ==============================
        //     // TODO: to viewModel?
        //     ViewBag.Page = page;
        //     ViewBag.TotalPages = await _vehicleService.VehicleMakesTotalPages();
        //     ViewBag.Filter = filter;
        //     ViewBag.orderField = orderField;
        //     ViewBag.orderDir = orderDir;
        //     ViewBag.queryParams = queryParams;

        //     var vehicleMakes = await _vehicleService.VehicleMakes(
        //         page: page,
        //         options: new Dictionary<string, object>
        //         {
        //             { "filter", filter },
        //             { "orderField", orderField },
        //             { "orderDir", orderDir }
        //         });

        //     return View(vehicleMakes);
        // }


        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] QueryParameters queryParams)
        {
            Console.WriteLine($"=== Page: {queryParams.Page}, PageSize: {queryParams.PageSize}");
            ViewBag.ErrorMessage = TempData["ErrorMessage"];

            var PagedVehicleMakes = await _vehicleService.VehicleMakesPaged(queryParams);
            var viewModel = new VehicleMakeViewModel
            {
                QueryParams = queryParams,
                PagedResult = PagedVehicleMakes
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Show(int? id)
        {
            try
            {
                int vehicleMakesCount = await _vehicleService.VehicleMakesCount();
                // if (id == null || vehicleMakesCount == 0)
                // {
                //     return NotFound();
                // }

                var vehicleMake = await _vehicleService.VehicleMakeById((int)id);

                if (vehicleMake == null)
                {
                    return NotFound();
                }
                return View(vehicleMake);
            }
            catch (ServiceException ex)
            {
                TempData["ErrorMessage"] = "Error";
                return RedirectToAction("Index");
            }


        }

        [HttpPost]
        public async Task<IActionResult> Create(VehicleMake vehicleMake)
        {
            if (!ModelState.IsValid)
            {
                return View("New", vehicleMake);
            }

            await _vehicleService.CreateVehicleMake(vehicleMake);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                int vehicleMakesCount = await _vehicleService.VehicleMakesCount();
                // if (id == null || vehicleMakesCount == 0)
                // {
                //     return NotFound();
                // }

                var vehicleMake = await _vehicleService.VehicleMakeById((int)id);
                if (vehicleMake == null)
                {
                    return NotFound();
                }
                return View(vehicleMake);
            }
            catch (ServiceException ex)
            {
                TempData["ErrorMessage"] = "Error";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(VehicleMake vehicleMake)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", vehicleMake);
            }

            await _vehicleService.UpdateVehicleMake(vehicleMake);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _vehicleService.DeleteVehicleMake(id);
                return RedirectToAction("Index");
            }
            catch (ServiceException ex)
            {
                TempData["ErrorMessage"] = "Error";
                return RedirectToAction("Index");
            }
        }

    }
}
