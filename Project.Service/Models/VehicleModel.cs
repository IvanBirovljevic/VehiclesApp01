using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Service.Interfaces;

namespace Project.Service.Models
{
    public class VehicleModel : IVehicleModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Abrv { get; set; }
        public int MakeId { get; set; }

        public virtual VehicleMake? Make { get; set; }
    }
}
