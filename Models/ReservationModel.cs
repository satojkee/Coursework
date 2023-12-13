using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace Hotel.Models
{
    public class ReservationModel
    {
        public int Id { get; set; }

        [Required]
        public int ApartmentId { get; set; }

        [Required(ErrorMessage = "Поле є обов'язковим")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Будь ласка, введіть реальне ім'я.")]
        public string ClientName { get; set; }

        [Required(ErrorMessage = "Поле є обов'язковим")]
		[DataType(DataType.PhoneNumber)]
		[StringLength(10, MinimumLength = 10, ErrorMessage = "Будь ласка, дотримуйтеся вказаного формату.")]
		public string ClientTel { get; set; }

        [Required(ErrorMessage = "Поле є обов'язковим")]
		[DataType(DataType.EmailAddress)]
		[StringLength(100, ErrorMessage = "Будь ласка, дотримуйтеся вказаного формату.")]
		public string ClientEmail { get; set; }

        [Required(ErrorMessage = "Поле є обов'язковим")]
		[DataType(DataType.Date)]
        [Remote(action: "IsRangePossible", controller: "Validation", AdditionalFields = "EvictionDate,ApartmentId", ErrorMessage = "Будь ласка, оберіть дійсну дату.")]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime ArrivingDate { get; set; }

        [Required(ErrorMessage = "Поле є обов'язковим")]
		[DataType(DataType.Date)]
		[Remote(action: "IsRangePossible", controller: "Validation", AdditionalFields = "ArrivingDate,ApartmentId", ErrorMessage = "Будь ласка, оберіть дійсну дату.")]
		[DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
		public DateTime EvictionDate { get; set; }

        public ApartmentModel Apartment { get; set; }
    }
}
