using Hotel.Interfaces;
using Hotel.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers
{
    public class FloorController : Controller
    {
        private readonly IApartmentRepository _apartmentRepository;

        public FloorController(IApartmentRepository apartmentRepository)
        {
            _apartmentRepository = apartmentRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            /* Renders page that contains all Floors */
            return View(_apartmentRepository.GetFloors());
        }

        [HttpGet]
        public ActionResult Create()
        {
            /* Renders CreateFloor form */
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(FloorModel floor)
        {
            /* Floor values equals floor id. 
            So, just throwing an empty model to the repo. */
            await _apartmentRepository.AddFloor(floor);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            /* Renders CreateFloor form */
            FloorModel? floor = _apartmentRepository.GetFloor(id);

            if (floor != null)
                return View(floor);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(FloorModel floor)
        {
            /* Floor values equals floor id. 
            So, just throwing an empty model to the repo. */
            await _apartmentRepository.EditFloor(floor);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            /* Removes FloorModel by its id*/
            await _apartmentRepository.RemoveFloor(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
