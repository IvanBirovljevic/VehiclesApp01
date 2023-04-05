using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Interfaces
{
    public interface IVehicleModelService
    {
        Task<List<VehicleModel>> GetVehicleModels();
        Task Create(VehicleModel vehicleModel);
        Task<VehicleModel> GetById(int id);
        Task Update(VehicleModel vehicleModel);
        Task Delete(VehicleModel vehicleModel);
        Task SaveChanges();
        Task<bool> IfExists(int id);
    }
}
