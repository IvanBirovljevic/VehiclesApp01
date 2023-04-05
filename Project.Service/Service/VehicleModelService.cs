using Microsoft.EntityFrameworkCore;
using Project.MVC.Data;
using Project.Service.Interfaces;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Service
{
    public class VehicleModelService : IVehicleModelService
    {
        private readonly ProjectMVCContext _context;

        public VehicleModelService(ProjectMVCContext context)
        {
            _context = context;
        }

        public Task<List<VehicleModel>> GetVehicleModels()
        {
            return _context.VehicleModel.ToListAsync();
        }

        public async Task Create(VehicleModel vehicleModel)
        {
            _context.Add(vehicleModel);
            await _context.SaveChangesAsync();
        }

        public async Task<VehicleModel> GetById(int id)
        {
            return await _context.VehicleModel.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Update(VehicleModel vehicleModel)
        {
            try
            {
                _context.Update(vehicleModel);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(VehicleModel vehicleModel)
        {
            _context.VehicleModel.Remove(vehicleModel);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IfExists(int id)
        {
            return (_context.VehicleModel?.Any(e=>e.Id==id)).GetValueOrDefault();
        }

    }
}
