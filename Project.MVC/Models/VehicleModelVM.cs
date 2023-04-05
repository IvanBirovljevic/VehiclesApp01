using Project.Service.Models;

namespace Project.MVC.Models
{
    public class VehicleModelVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Abrv { get; set; }
        public int MakeId { get; set; }
        public virtual VehicleMake Make { get; set; }
    }
}
