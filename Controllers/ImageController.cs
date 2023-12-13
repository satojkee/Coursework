using Hotel.Interfaces;
using Hotel.Models;
using Hotel.ViewModels;

using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers
{
    public class ImageController : Controller
    {
        private readonly IApartmentRepository _apartmentRepository;

        public ImageController(IApartmentRepository apartmentRepository)
        {
            _apartmentRepository = apartmentRepository;
        }

        [HttpGet]
        public ActionResult Create(int id)
        {
            /* Redners ApartmentImageModel create form */
            ApartmentViewModel? apartment = _apartmentRepository.GetViewApartment(id);

            ViewBag.apartmentId = id;

            if (apartment != null) return View();

			return Redirect("/Apartment");
		}

		[HttpGet]
		public async Task<ActionResult> Delete(int id)
		{
            /* Removes ApartmentImageModel by its id */
            await _apartmentRepository.RemoveImage(id);

            return Redirect("/Apartment");
		}

		[HttpPost]
        public async Task<ActionResult> Create(ApartmentImageModel image)
        {
            /* Add's the new ApartmentImageModel to the db. */
            await _apartmentRepository.AddApartmentImage(image);

            return RedirectToAction(nameof(Create));
        }
    }
}
