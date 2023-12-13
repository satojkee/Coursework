using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Models
{
    public class FloorModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле є обов'язковим")]
        [Remote(action: "IsFloorAvailable", controller: "Validation", AdditionalFields = "Id", ErrorMessage = "Такий поверх вже існує.")]
        public int Value { get; set; }

        public IEnumerable<ApartmentModel> Apartments { get; set; }
    }
}
