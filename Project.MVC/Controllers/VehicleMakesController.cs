using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.MVC.Data;
using Project.MVC.Models;
using Project.Service.Interfaces;
using Project.Service.Models;
using Project.Service.Service;

namespace Project.MVC.Controllers
{
    public class VehicleMakesController : Controller
    {
        private readonly ProjectMVCContext _context;
        private readonly VehicleMakeService _vehicleMakeService;
        private readonly IMapper _mapper;

        public VehicleMakesController(ProjectMVCContext context, IMapper mapper)
        {
            _context = context;
            _vehicleMakeService = new VehicleMakeService(_context);
            _mapper = mapper;
        }    

        // GET: VehicleMakes
        public async Task<IActionResult> Index()
        {
            var makeList = await _vehicleMakeService.GetVehicleMakes();
            var makeVMList = _mapper.Map<List<VehicleMakeVM>>(makeList);

            return _context.VehicleMake != null ?
                        //View(await _context.VehicleMake.ToListAsync()) :                   
                          View(makeVMList) :
                          Problem("Entity set 'ProjectMVCContext.VehicleMake'  is null.");
        }

        // GET: VehicleMakes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VehicleMake == null)
            {
                return NotFound();
            }

            //var vehicleMake = await _context.VehicleMake
            //    .FirstOrDefaultAsync(m => m.Id == id);
            var vehicleMake = await _vehicleMakeService.GetById((int)id);
            var vehicleMakeVM = _mapper.Map<VehicleMakeVM>(vehicleMake);

            if (vehicleMake == null)
            {
                return NotFound();
            }

            return View(vehicleMakeVM);
        }

        // GET: VehicleMakes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VehicleMakes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Abrv")] VehicleMake vehicleMake)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(vehicleMake);
                //await _context.SaveChangesAsync();
                await _vehicleMakeService.Create(vehicleMake);
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleMake);
        }

        // GET: VehicleMakes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VehicleMake == null)
            {
                return NotFound();
            }

            //var vehicleMake = await _context.VehicleMake.FindAsync(id);
            var vehicleMake = await _vehicleMakeService.GetById((int)id);
            var vehicleMakeVM = _mapper.Map<VehicleMakeVM>(vehicleMake);
            if (vehicleMake == null)
            {
                return NotFound();
            }
            return View(vehicleMakeVM);
        }

        // POST: VehicleMakes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Abrv")] VehicleMakeVM vehicleMakeVM)
        {
            if (id != vehicleMakeVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(vehicleMake);
                    //await _context.SaveChangesAsync();
                    var vehicleMake = _mapper.Map<VehicleMake>(vehicleMakeVM);
                    await _vehicleMakeService.Update(vehicleMake);                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _vehicleMakeService.IfExists(vehicleMakeVM.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleMakeVM);
        }

        // GET: VehicleMakes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VehicleMake == null)
            {
                return NotFound();
            }

            //var vehicleMake = await _context.VehicleMake
            //    .FirstOrDefaultAsync(m => m.Id == id);
            var vehicleMake = await _vehicleMakeService.GetById((int)id);
            var vehicleMakeVM = _mapper.Map<VehicleMakeVM>(vehicleMake);
           
            if (vehicleMake == null)
            {
                return NotFound();
            }

            return View(vehicleMakeVM);
        }

        // POST: VehicleMakes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VehicleMake == null)
            {
                return Problem("Entity set 'ProjectMVCContext.VehicleMake'  is null.");
            }
            //var vehicleMake = await _context.VehicleMake.FindAsync(id);
            var vehicleMake = await _vehicleMakeService.GetById(id);
            if (vehicleMake != null)
            {
                await _vehicleMakeService.Delete(vehicleMake);
            }

            await _vehicleMakeService.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
