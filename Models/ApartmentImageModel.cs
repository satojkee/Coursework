using System.ComponentModel.DataAnnotations;

namespace Hotel.Models
{
    public class ApartmentImageModel
    {
        public int Id {  get; set; }

        [Required]
        public int ApartmentId { get; set; }

        [Required(ErrorMessage = "Поле є обов'язковим")]
        public string Url {  get; set; }

        public ApartmentModel Apartment { get; set; }
    }
}
