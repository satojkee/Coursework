using System.ComponentModel.DataAnnotations;

namespace Hotel.Models
{
    public class ApartmentTypeModel
    {
        public int Id {  get; set; }

        [Required(ErrorMessage = "Поле є обов'язковим")]
        public string Name { get; set; }

        public IEnumerable<ApartmentModel> Apartments { get; set; }
    }
}
