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
    public class VehicleModelsController : Controller
    {
        private readonly ProjectMVCContext _context;
        private readonly VehicleModelService _vehicleModelService;
        private readonly IMapper _mapper;

        public VehicleModelsController(ProjectMVCContext context, IMapper mapper)
        {
            _context = context;
            _vehicleModelService = new VehicleModelService(_context);
            _mapper = mapper;
        }

        // GET: VehicleModels
        public async Task<IActionResult> Index()
        {
            var modelList = await _vehicleModelService.GetVehicleModels();
            var modelVMList = _mapper.Map<List<VehicleModelVM>>(modelList);

            return _context.VehicleModel != null ?
                    View(modelVMList) :
                    Problem("Entity set 'ProjectMVCContext.VehicleModel' is null");
        }

        // GET: VehicleModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VehicleModel == null)
            {
                return NotFound();
            }

            var vehicleModel = await _vehicleModelService.GetById((int)id);
            var vehicleModelVM = _mapper.Map<VehicleModelVM>(vehicleModel);

            if (vehicleModel == null)
            {
                return NotFound();
            }

            return View(vehicleModelVM);
        }

        // GET: VehicleModels/Create
        public IActionResult Create()
        {
            ViewData["MakeId"] = new SelectList(_context.VehicleMake, "Id", "Name");
            return View();
        }

        // POST: VehicleModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Abrv,MakeId")] VehicleModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(vehicleModel);
                //await _context.SaveChangesAsync();
                await _vehicleModelService.Create(vehicleModel);
                return RedirectToAction(nameof(Index));
            }
            ViewData["MakeId"] = new SelectList(_context.VehicleMake, "Id", "Name", vehicleModel.MakeId);
            return View(vehicleModel);
        }

        // GET: VehicleModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VehicleModel == null)
            {
                return NotFound();
            }

            //var vehicleModel = await _context.VehicleModel.FindAsync(id);
            var vehicleModel = await _vehicleModelService.GetById((int)id);
            var vehicleModelVM = _mapper.Map<VehicleModelVM>(vehicleModel);

            if (vehicleModel == null)
            {
                return NotFound();
            }
            ViewData["MakeId"] = new SelectList(_context.VehicleMake, "Id", "Name", vehicleModel.MakeId);
            return View(vehicleModelVM);
        }

        // POST: VehicleModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Abrv,MakeId")] VehicleModelVM vehicleModelVM)
        {
            if (id != vehicleModelVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(vehicleModel);
                    //await _context.SaveChangesAsync();
                    var vehicleModel = _mapper.Map<VehicleModel>(vehicleModelVM);
                    await _vehicleModelService.Update(vehicleModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _vehicleModelService.IfExists(vehicleModelVM.Id))
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
            ViewData["MakeId"] = new SelectList(_context.VehicleMake, "Id", "Name", vehicleModelVM.MakeId);
            return View(vehicleModelVM);
        }

        // GET: VehicleModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VehicleModel == null)
            {
                return NotFound();
            }

            //var vehicleModel = await _context.VehicleModel
            //    .Include(v => v.Make)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            var vehicleModel = await _vehicleModelService.GetById((int)id);
            var vehicleModelVM = _mapper.Map<VehicleModelVM>(vehicleModel);
            if (vehicleModel == null)
            {
                return NotFound();
            }

            return View(vehicleModelVM);
        }

        // POST: VehicleModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VehicleModel == null)
            {
                return Problem("Entity set 'ProjectMVCContext.VehicleModel'  is null.");
            }
            //var vehicleModel = await _context.VehicleModel.FindAsync(id);
            var vehicleModel = await _vehicleModelService.GetById(id);
            if (vehicleModel != null)
            {
                await _vehicleModelService.Delete(vehicleModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /* private bool VehicleModelExists(int id)
         {
           return (_context.VehicleModel?.Any(e => e.Id == id)).GetValueOrDefault();
         }*/

        [HttpGet("exists/{id}")]
        public async Task<IActionResult> CheckIfVehicleModelExists(int id)
        {
            var vehicleModelExists = await _vehicleModelService.IfExists(id);
            return Ok(vehicleModelExists);
        }
    }
}
