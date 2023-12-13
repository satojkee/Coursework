using Hotel.Models;
using Hotel.ViewModels;

namespace Hotel.Interfaces
{
    public interface IApartmentRepository
    {
        public List<FloorModel> GetFloors();

        public List<ApartmentTypeModel> GetApartmentTypes();

        public ICollection<ApartmentModel> GetApartments();

        public ICollection<ReservationModel> GetReservations();

        public ICollection<ApartmentViewModel> GetViewApartments();

        public ICollection<ReservationModel> GetApartmentReservations(int id);

		public ApartmentViewModel? GetViewApartment(int id);

		public ApartmentModel? GetApartment(int id);

        public FloorModel? GetFloor(int id);

        public Task<bool> RemoveFloor(int id);

        public Task<bool> RemoveApartment(int id);

        public Task<bool> RemoveImage(int id);

		public Task<bool> Save();

        public Task<bool> EditApartment(ApartmentModel updatedInstance);

        public Task<bool> EditFloor(FloorModel updatedFloor);

        public Task<bool> AddFloor(FloorModel floor);

		public Task<bool> AddApartmentType(ApartmentTypeModel apartmentType);

        public Task<bool> AddApartment(ApartmentModel apartment);

        public Task<bool> AddApartmentImage(ApartmentImageModel apartmentImage);

        public Task<bool> AddReservation(ReservationModel reservation);
	}
}
