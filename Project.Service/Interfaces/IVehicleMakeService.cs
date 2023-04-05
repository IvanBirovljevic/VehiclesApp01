using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Interfaces
{
    public interface IVehicleMakeService
    {
        Task<List<VehicleMake>> GetVehicleMakes();
        Task Create(VehicleMake vehicleMake);
        Task<VehicleMake> GetById(int id);
        Task Update(VehicleMake vehicleMake);
        Task Delete(VehicleMake vehicleMake);
        Task SaveChanges();
        Task<bool> IfExists(int id);

    }
}

