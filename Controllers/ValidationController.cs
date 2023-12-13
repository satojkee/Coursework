using Hotel.Interfaces;
using Hotel.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers
{
	public class ValidationController : Controller
	{
		private readonly IApartmentRepository _apartmentRepository;

		public ValidationController(IApartmentRepository apartmentRepository)
		{
			_apartmentRepository = apartmentRepository;
		}

        [AcceptVerbs("Get", "Post")]
        public JsonResult IsFloorAvailable(int Value, int Id)
        {
            /* It's used to avoid Room.RealId collisions. */
            FloorModel? floor = _apartmentRepository
                .GetFloors()
                .Where(x => x.Value == Value && x.Id != Id)
                .FirstOrDefault();

            return Json(floor == null);
        }

        [AcceptVerbs("Get", "Post")]
		public JsonResult IsRealIdExists(int RealId, int Id)
		{
			/* It's used to avoid Room.RealId collisions. */
			ApartmentModel? apartment = _apartmentRepository
				.GetApartments()
				.Where(x => x.RealId == RealId && x.Id != Id)
				.FirstOrDefault();

			return Json(apartment == null);
		}

		[AcceptVerbs("Get", "Post")]
		public JsonResult IsRangePossible(DateTime ArrivingDate, DateTime EvictionDate, int ApartmentId)
		{
			/* This method validates Entered Arriving and Eviction dates.
			Even if one Date equals MinimalDate -> skips validation.
			It follows that rules: 
				1. IF Arriving date > Eviction Date -> false
				2. IF Arriving date or Eviction Date are empty (has MinValue) -> true
				3. IF Eviction Date < Todays date -> false 

			Also, it takes on existing Reservations.
			*/

			ReservationModel[] reservations = _apartmentRepository
				.GetApartmentReservations(ApartmentId)
				.OrderByDescending(x => x.EvictionDate)
				.ToArray();

			Array.Reverse(reservations);

			if (EvictionDate == DateTime.MinValue || ArrivingDate == DateTime.MinValue)
				return Json(true);

            if (EvictionDate > ArrivingDate && ArrivingDate >= DateTime.Today)
			{
				if (reservations.Length == 0)
					return Json(true);

				for (int i = 0; i < reservations.Length; i++)
				{
					if (ArrivingDate < reservations[reservations.Length - 1].EvictionDate)
					{
						if (EvictionDate <= reservations[i].ArrivingDate)
						{
							if (i - 1 >= 0)
							{
								if (ArrivingDate >= reservations[i - 1].EvictionDate)
									return Json(true);
							}
							else
							{
								break;
							}
						}
					}
					else
					{
						return Json(true);
					}
				}
			}

			return Json(false);	
		}
	}
}
