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
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly ProjectMVCContext _context;

        public VehicleMakeService(ProjectMVCContext context)
        {
            _context = context;
        }

        public Task<List<VehicleMake>> GetVehicleMakes()
        {
            return _context.VehicleMake.ToListAsync();
        }

        public async Task Create(VehicleMake vehicleMake)
        {
            _context.Add(vehicleMake);
           await _context.SaveChangesAsync();
        }

        public async Task<VehicleMake> GetById(int id)
        {
            return await _context.VehicleMake.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Update(VehicleMake vehicleMake)
        {
            try
            {
                _context.Update(vehicleMake);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }

        public async Task Delete(VehicleMake vehicleMake)
        {
            _context.VehicleMake.Remove(vehicleMake);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IfExists(int id)
        {
            return (_context.VehicleMake?.Any(e => e.Id == id)).GetValueOrDefault();
        }










    }
}
