using Hotel.Interfaces;
using Hotel.Models;
using Hotel.ViewModels;

using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers
{
    public class ApartmentController : Controller
    {
        private readonly IApartmentRepository _apartmentRepository;

        public ApartmentController(IApartmentRepository apartmentRepository)
        {
            _apartmentRepository = apartmentRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(_apartmentRepository.GetViewApartments());
        }

        [HttpGet]
        public ActionResult Order(int id)
        {
            /* Renders CreateOrder template. 
            Also, attaches selected AppartmentId to the ViewBag */
            ApartmentViewModel? apartment = _apartmentRepository.GetViewApartment(id);

            if (apartment != null)
                ViewBag.Apartment = apartment;
            else
                return RedirectToAction(nameof(Index));

            return View();
        }

		[HttpPost]
		public async Task<ActionResult> Order(ReservationModel reservation)
		{
            /* Handles CreateOrder form submission. (POST) request. 
            Returns ThankYou page View on success. */
            if (await _apartmentRepository.AddReservation(reservation))
            {
				ViewBag.Person = reservation.ClientName;

				return View("~/Views/Apartment/ThankYou.cshtml");
			}

            throw new NotImplementedException();

		}

		[HttpGet]
        public ActionResult Details(int id)
        {
			/* 
            1 step -> It tries to find specified apartment in the database.
            2 step -> IF loaded instance isn't nullable -> returns Details View() 
            Otherwise -> Redirects to Index.
            */
			ApartmentViewModel? room = _apartmentRepository.GetViewApartment(id);

            if (room != null) 
                return View(room);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Create()
        {
            /* Renders Create Page for ApartmentModel */
            ViewBag.Floors = _apartmentRepository.GetFloors();
            ViewBag.ApartmentTypes = _apartmentRepository.GetApartmentTypes();

            return View();
        }

		[HttpGet]
		public ActionResult Edit(int id)
		{
            /* Renders EditApartment form page.
             Also, attaches collections of FloorModel and ApertmentTypeModel */
            ApartmentModel? apartment = _apartmentRepository.GetApartment(id);

            if (apartment != null)
            {
				ViewBag.Floors = _apartmentRepository.GetFloors();
				ViewBag.ApartmentTypes = _apartmentRepository.GetApartmentTypes();

				return View(apartment);
			}

            return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		public async Task<ActionResult> Edit(ApartmentModel apartment)
		{
            /* Updates ApartmentModel instance by its id. 
            Nothing will happen if there's no ApartmentModel with specified id. */
            await _apartmentRepository.EditApartment(apartment);

            return RedirectToAction(nameof(Index));
		}

		[HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            /* Removes ApartmentModel instance by its id. 
            Nothing will happen if there's no ApartmentModel with specified id. */
            await _apartmentRepository.RemoveApartment(id);
            
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<ActionResult> Create(ApartmentModel apartment)
        {
            /* It handles CreateApartment form submission. */
            await _apartmentRepository.AddApartment(apartment);

            return RedirectToAction(nameof(Index));
        }
    }
}
