using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Interfaces
{
    public interface IVehicleModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Abrv { get; set; }

        public int MakeId { get; set; }

        public VehicleMake Make { get; set; }
    }
}
