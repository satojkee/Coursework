using Hotel.Models;

namespace Hotel.ViewModels
{
    public class ApartmentViewModel
    {
        public int Id { get; set; }
        public int RealId { get; set; }
        public int Price { get; set; }
        public int Floor { get; set; }
        public string Type { get; set; }
        public bool HasConditioner { get; set; }
        public string Title { get; set; }
        public int Area { get; set; }
        public int RoomsCount {  get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public List<ApartmentImageModel> Images = new List<ApartmentImageModel>();

        public ApartmentViewModel(ApartmentModel room)
        {
            Id = room.Id;
            Price = room.Price;
            Description = room.Description;
            Area = room.Area;
            RealId = room.RealId;
            Type = room.Type.Name;
            Floor = room.Floor.Value;
            Title = room.Title;
            HasConditioner = room.HasConditioner;
            ShortDescription = room.ShortDescription;
            RoomsCount = room.RoomsCount;

            if (room.Images != null)
            {
                Images.AddRange(room.Images);
            }
        }
    }
}
