using Hotel.Interfaces;
using Hotel.Models;
using Hotel.Data;

using Microsoft.EntityFrameworkCore;
using Hotel.ViewModels;

namespace Hotel.Repository
{
    public class ApartmentRepository : IApartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public ApartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<FloorModel> GetFloors()
        {
            // This method returns all FloorModel instances stored in the db
            return _context.Floors.ToList();
        }

        public List<ApartmentTypeModel> GetApartmentTypes()
        {
			// This method returns all ApartmentTypeModel instances stored in the db
			return _context.ApartmentTypes.ToList();
        }

		public ICollection<ApartmentModel> GetApartments()
        {
            /* This method returns the collection of <ApartmentModel> objectes.
            Also, includes attached <RoomTypeModel>. 
            Use -> instance.Type.Property to access.*/
            return _context.Apartments
                .Include(t => t.Type)
                .Include(f => f.Floor)
                .Include(i => i.Images)
                .ToList();
        }

        public ICollection<ApartmentViewModel> GetViewApartments()
        {
			// This method returns the collection of <ApartmentViewModel> objectes
			return _context.Apartments
                .Include(t => t.Type)
                .Include(f => f.Floor)
                .Include(i => i.Images)
                .Select(r => new ApartmentViewModel(r))
                .ToList();
		}

        public ICollection<ReservationModel> GetReservations()
        {
            /* This method returns all <ReservationModel> instances stored in the db.
            Also, includes attached ApartmentModel. Use -> instance.Apartment.Property to access. */
            return _context.Reservations
                .Include(a => a.Apartment)
                .ToList();
        }

        public ICollection<ReservationModel> GetApartmentReservations(int id)
        {
            /* This method returns all reservation for the specified ApartmentModel */
            return _context.Reservations
                .Where(r => r.ApartmentId == id)
                .ToList();
        }

        public ApartmentViewModel? GetViewApartment(int id)
        {
			/* It tries to find the room by specified Id.
            Returns ApartmentViewModel instance or null. */
			return _context.Apartments
                .Include(t => t.Type)
                .Include(f => f.Floor)
                .Include(i => i.Images)
                .Where(t => t.Id == id)
                .Select(r => new ApartmentViewModel(r))
                .FirstOrDefault();
        }

		public ApartmentModel? GetApartment(int id)
		{
			/* It tries to find AppartmentModel by specified Id.
            Returns ApartmentViewModel instance or null. */
			return _context.Apartments
				.Include(t => t.Type)
				.Include(i => i.Images)
                .Include(f => f.Floor)
                .Where(t => t.Id == id)
				.FirstOrDefault();
		}

        public ApartmentTypeModel? GetApartmentType(int id)
        {
            /* It tries to find the room by specified Id.
            Returns ApartmentTypeModel instance or null. */
            return _context.ApartmentTypes
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }

        public FloorModel? GetFloor(int id)
        {
            /* Returns FloorModel instance by its id or null */
            return _context.Floors
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }

		public async Task<bool> Save()
        { 
            /* This method saves all changes to the db.
            Returns boolean representaion of that action.
            If something changed -> true, if not -> false */
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddFloor(FloorModel floor)
        {
            /* This method adds a new FloorModel to the db.
            Returns this.Save() as operation result. */
            floor.Id = 0;

            _context.Floors.Add(floor);

            return await Save();
        }

        public async Task<bool> AddApartmentType(ApartmentTypeModel apartmentType)
        {
            /* This method adds a new RoomTypeModel to the db.
            Returns this.Save() as operation result. */
            apartmentType.Id = 0;

            _context.ApartmentTypes.Add(apartmentType);

            return await Save();
        }

        public async Task<bool> AddApartmentImage(ApartmentImageModel apartmentImage)
        {
            /* This method adds a new ApartmentImageModel to the db.
            Returns this.Save() as operation result. */
            apartmentImage.Id = 0;

			_context.Images.Add(apartmentImage);

            return await Save();
        }

        public async Task<bool> AddApartment(ApartmentModel apartment)
        {
			/* This method adds a new ApartmentModel to the db.
            Returns this.Save() as operation result. */
			_context.Apartments.Add(apartment);

            return await Save();
        }

        public async Task<bool> AddReservation(ReservationModel reservation)
        {
            /* This method adds a new ReservationModel to the db.
            Returns this.Save() as operation result. */
            reservation.Id = 0;

			_context.Reservations.Add(reservation);

			return await Save();
		}

        public async Task<bool> RemoveApartment(int id)
        {
            /* This method removes ApartmentModel instance by its id. 
            Returns this.Save() as operation result. */
            ApartmentModel? instance = GetApartment(id);

            if (instance != null)
            {
				_context.Remove(instance);
			}

            return await Save();
        }

        public async Task<bool> RemoveFloor(int id)
        {
            /* This method removes FloorModel instance by its id. 
            Returns this.Save() as operation result. */
            FloorModel? instance = GetFloor(id);

            if (instance != null)
            {
                _context.Remove(instance);
            }

            return await Save();
        }

        public async Task<bool> RemoveType(int id)
        {
            /* This method removes ApartmentTypeModel instance by its id. 
            Returns this.Save() as operation result. */
            ApartmentTypeModel? instance = GetApartmentType(id);

            if (instance != null)
            {
                _context.Remove(instance);
            }

            return await Save();
        }

		public async Task<bool> RemoveImage(int id)
		{
			/* This method removes ApartmentImageModel instance by its id. 
            Returns this.Save() as operation result. */
			ApartmentImageModel? instance = _context.Images
                .Where(x => x.Id == id)
                .FirstOrDefault();

			if (instance != null)
			{
				_context.Remove(instance);
			}

			return await Save();
		}

		public async Task<bool> EditApartment(ApartmentModel updatedInstance)
        {
            /* This method updates ApartmentModel instance by its id. 
            Returns this.Save() as operation result. */
            ApartmentModel? storedInstance = GetApartment(updatedInstance.Id);

            if (storedInstance != null)
            {
                storedInstance.Area = updatedInstance.Area;
                storedInstance.Price = updatedInstance.Price;
                storedInstance.TypeId = updatedInstance.TypeId;
                storedInstance.Title = updatedInstance.Title;
                storedInstance.FloorId = updatedInstance.FloorId;
                storedInstance.RealId = updatedInstance.RealId;
                storedInstance.Description = updatedInstance.Description;
                storedInstance.ShortDescription = updatedInstance.ShortDescription;
                storedInstance.HasConditioner = updatedInstance.HasConditioner;
                storedInstance.RoomsCount = updatedInstance.RoomsCount;

				_context.Update(storedInstance);
			}

            return await Save();
        }

        public async Task<bool> EditFloor(FloorModel updatedFloor)
        {
            /* This method updates FloorModel instance by its id. 
            Returns this.Save() as operation result. */
            FloorModel? storedInstance = GetFloor(updatedFloor.Id);

            if (storedInstance != null)
            {
                storedInstance.Value = updatedFloor.Value;

                _context.Update(storedInstance);
            }

            return await Save();
        }
		public async Task<bool> EditType(ApartmentTypeModel updatedType)
		{
			/* This method updates FloorModel instance by its id. 
            Returns this.Save() as operation result. */
			ApartmentTypeModel? storedInstance = GetApartmentType(updatedType.Id);

			if (storedInstance != null)
			{
				storedInstance.Name = updatedType.Name;

				_context.Update(storedInstance);
			}

			return await Save();
		}
	}
}
