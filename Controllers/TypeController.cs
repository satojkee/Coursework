using Hotel.Interfaces;
using Hotel.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers
{
	public class TypeController : Controller
	{
		private readonly IApartmentRepository _apartmentRepository;

		public TypeController(IApartmentRepository apartmentRepository)
		{
			_apartmentRepository = apartmentRepository;
		}

		[HttpGet]
		public ActionResult Index()
		{
			/* Renders page that contains all ApartmentTypes */
			return View(_apartmentRepository.GetApartmentTypes());
		}

		[HttpGet]
		public ActionResult Create()
		{
			/* Renders CreateType form */
			return View();
		}

		[HttpPost]
		public async Task<ActionResult> Create(ApartmentTypeModel type)
		{
			/* Handles CreateApartmentType form submission */
			await _apartmentRepository.AddApartmentType(type);

			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public ActionResult Edit(int id)
		{
			/* Renders EditApartmentType form */
			ApartmentTypeModel? type = _apartmentRepository.GetApartmentType(id);

			if (type != null)
				return View(type);

			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		public async Task<ActionResult> Edit(ApartmentTypeModel type)
		{
            /* Updates specified ApartmentTypeModel by its id. 
             Handles EditApartmentType form submission. */
            await _apartmentRepository.EditType(type);

			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<ActionResult> Delete(int id)
		{
			/* Removes ApartmentTypeModel by its id */
			await _apartmentRepository.RemoveType(id);

			return RedirectToAction(nameof(Index));
		}
	}
}
