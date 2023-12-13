using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Models
{

	public class ApartmentModel
    {
        public int Id { get; set; }

        [Required]
        public int FloorId { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим.")]
        [Remote(action: "IsRealIdExists", controller: "Validation", AdditionalFields="Id", ErrorMessage = "Кімната з таким номером вже існує.")]
        public int RealId { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим.")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим.")]
        public int TypeId { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим.")]
        public int Area { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим.")]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим.")]
        public int RoomsCount {  get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим.")]
        public bool HasConditioner { get; set; }

        public ApartmentTypeModel Type { get; set; }
        public FloorModel Floor { get; set; }
        public IEnumerable<ReservationModel> Reservations { get; set; }
        public IEnumerable<ApartmentImageModel> Images { get; set; }
    }
}
