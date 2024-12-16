using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Mvc.ViewModels;
using Project.Service.Abstract;
using Project.Service.Models;

namespace Project.Mvc.Controllers
{
    public class VehicleModelsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVehicleService _vehicleService;

        public VehicleModelsController(ILogger<HomeController> logger, IVehicleService vehicleService)
        {
            _logger = logger;
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var page = int.TryParse(Request.Query["page"], out var p) ? p : 1;
            var order = Request.Query["order"].ToString() ?? "asc";
            var filter = Request.Query["filter"].ToString() ?? "";
            ViewBag.filter = filter;

            var vehicleModels = await _vehicleService.VehicleModels(
                page: page,
                options: new Dictionary<string, object> { { "filter", filter } });

            return View(vehicleModels);
        }

        [HttpGet]
        public async Task<IActionResult> New(int vehicleMakeId, string? callbackUrl)
        {
            Console.WriteLine($"===vehicleMakeId: {vehicleMakeId}");
            ViewBag.CallbackUrl = callbackUrl;

            var vehicleMake = await _vehicleService.VehicleMakeById((int)vehicleMakeId);

            if (vehicleMake == null)
            {
                Console.WriteLine("===vehiclemake is null");
                return NotFound();
            }

            // var vehicleMakeList = await _vehicleService.VehicleMakes();
            // Создайте ViewModel с переданным vehicleMakeId
            var viewModel = new VehicleModelViewModel
            {
                vehicleModel = new VehicleModel { VehicleMakeId = vehicleMakeId },
                // vehicleMakeList = vehicleMakeList?.Select(vm => new SelectListItem
                // {
                //     Value = vm.Id.ToString(),
                //     Text = vm.Name
                // }) ?? new List<SelectListItem>(),
                selectedVehicleMake = vehicleMake
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Show(int? id)
        {
            Console.WriteLine($"===Show {id}");
            int vehicleModelsCount = await _vehicleService.VehicleModelsCount();
            if (id == null || vehicleModelsCount == 0)
            {
                return NotFound();
            }

            var vehicleModel = await _vehicleService.VehicleModelById((int)id);

            if (vehicleModel == null)
            {
                return NotFound();
            }
            return View(vehicleModel);
        }


        [HttpPost]
        public async Task<IActionResult> Create(VehicleModel vehicleModel, string? callbackUrl)
        {
            ViewBag.CallbackUrl = callbackUrl;
            var vehicleMake = await _vehicleService.VehicleMakeById((int)vehicleModel.VehicleMakeId);

            if (vehicleMake == null)
            {
                Console.WriteLine("===vehiclemake is null");
                return NotFound();
            }

            // var vehicleMakeList = await _vehicleService.VehicleMakes();
            var viewModel = new VehicleModelViewModel
            {
                vehicleModel = new VehicleModel { VehicleMakeId = vehicleModel.VehicleMakeId },
                // vehicleMakeList = vehicleMakeList?.Select(vm => new SelectListItem
                // {
                //     Value = vm.Id.ToString(),
                //     Text = vm.Name
                // }) ?? new List<SelectListItem>(),
                selectedVehicleMake = vehicleMake
            };


            if (!ModelState.IsValid)
            {
                Console.WriteLine($"===ModelState Invalid");
                return View("New", viewModel);
            }

            await _vehicleService.CreateVehicleModel(vehicleModel);

            if (callbackUrl != null)
            {
                Console.WriteLine($"===callbackUrl: {callbackUrl}");
                return Redirect(callbackUrl);
            }

            return RedirectToAction("Show", "VehicleMakes", new { id = vehicleModel.VehicleMakeId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            int vehicleModelsCount = await _vehicleService.VehicleModelsCount();
            if (id == null || vehicleModelsCount == 0)
            {
                return NotFound();
            }

            var vehicleModel = await _vehicleService.VehicleModelById((int)id);
            if (vehicleModel == null)
            {
                return NotFound();
            }

            Console.WriteLine("Controller:Edit==========================");
            Console.WriteLine($"id: {vehicleModel.Id}");
            Console.WriteLine($"name: {vehicleModel.Name}");
            Console.WriteLine($"abrv: {vehicleModel.Abrv}");
            Console.WriteLine($"vehicleMakeId: {vehicleModel.VehicleMakeId}");


            return View(vehicleModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(VehicleModel vehicleModel)
        {
            Console.WriteLine("Controller:Update==========================");
            Console.WriteLine($"id: {vehicleModel.Id}");
            Console.WriteLine($"name: {vehicleModel.Name}");
            Console.WriteLine($"abrv: {vehicleModel.Abrv}");
            Console.WriteLine($"vehicleMakeId: {vehicleModel.VehicleMakeId}");


            if (!ModelState.IsValid)
            {
                return View("Edit", vehicleModel);
            }

            await _vehicleService.UpdateVehicleModel(vehicleModel);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _vehicleService.DeleteVehicleModel(id);
            return RedirectToAction("Index");
        }

    }
}
