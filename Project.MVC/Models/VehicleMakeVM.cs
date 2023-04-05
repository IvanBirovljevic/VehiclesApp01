using Project.Service.Models;

namespace Project.MVC.Models
{
    public class VehicleMakeVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Abrv { get; set; }

        public virtual ICollection<VehicleModel>? VehicleModels { get; set; }
    }
}
