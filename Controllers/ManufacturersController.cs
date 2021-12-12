using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proiect_Neag_Cristina.Data;
using Proiect_Neag_Cristina.Models;
using Proiect_Neag_Cristina.Models.StoreViewModels;

namespace Proiect_Neag_Cristina.Controllers
{
    public class ManufacturersController : Controller
    {
        private readonly PerfumeStoreContext _context;

        public ManufacturersController(PerfumeStoreContext context)
        {
            _context = context;
        }

        // GET: Manufacturers
        public async Task<IActionResult> Index(int? id, int? perfumeID)
        {
            var viewModel = new ManufacturerIndexData();
            viewModel.Manufacturers = await _context.Manufacturer
            .Include(i => i.ManufacturedPerfumes)
            .ThenInclude(i => i.Perfume)
            .ThenInclude(i => i.Orders)
            .ThenInclude(i => i.Customer)
            .AsNoTracking()
            .OrderBy(i => i.Name)
            .ToListAsync();
            if (id != null)
            {
                ViewData["ManufacturerID"] = id.Value;
                Manufacturer manufacturer = viewModel.Manufacturers.Where(
                i => i.ID == id.Value).Single();
                viewModel.Perfumes = manufacturer.ManufacturedPerfumes.Select(s => s.Perfume);
            }
            if (perfumeID != null)
            {
                ViewData["PerfumeID"] = perfumeID.Value;
                viewModel.Orders = viewModel.Perfumes.Where(
                x => x.ID == perfumeID).Single().Orders;
            }
            return View(viewModel);
        }

        // GET: Manufacturers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manufacturer = await _context.Manufacturer
                .FirstOrDefaultAsync(m => m.ID == id);
            if (manufacturer == null)
            {
                return NotFound();
            }

            return View(manufacturer);
        }

        // GET: Manufacturers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Manufacturers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Cui")] Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(manufacturer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(manufacturer);
        }

        // GET: Manufacturers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var manufacturer = await _context.Manufacturer
            .Include(i => i.ManufacturedPerfumes).ThenInclude(i => i.Perfume)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);
            if (manufacturer == null)
            {
                return NotFound();
            }
            PopulateManufacturedPerfumesData(manufacturer);
            return View(manufacturer);
        }
        private void PopulateManufacturedPerfumesData(Manufacturer manufacturer)
        {
            var allPerfumes = _context.Perfumes;
            var manufacturerPerfumes = new HashSet<int>(manufacturer.ManufacturedPerfumes.Select(c => c.PerfumeID));
            var viewModel = new List<ManufacturedPerfumesData>();
            foreach (var perfume in allPerfumes)
            {
                viewModel.Add(new ManufacturedPerfumesData
                {
                    PerfumeID = perfume.ID,
                    Name = perfume.Name,
                    IsManufactured = manufacturerPerfumes.Contains(perfume.ID)
                });
            }
            ViewData["Books"] = viewModel;
        }

        // POST: Manufacturers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedPerfume)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manufacturerToUpdate = await _context.Manufacturer
            .Include(i => i.ManufacturedPerfumes)
            .ThenInclude(i => i.Perfume)
            .FirstOrDefaultAsync(m => m.ID == id);
            if (await TryUpdateModelAsync<Manufacturer>(
            manufacturerToUpdate,
            "",
            i => i.Name, i => i.Cui))
            {
                UpdatePublishedBooks(selectedPerfume, manufacturerToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, ");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdatePublishedBooks(selectedPerfume, manufacturerToUpdate);
            PopulateManufacturedPerfumesData(manufacturerToUpdate);
            return View(manufacturerToUpdate);
        }
        private void UpdatePublishedBooks(string[] selectedPerfumes, Manufacturer manufacturerToUpdate)
        {
            if (selectedPerfumes == null)
            {
                manufacturerToUpdate.ManufacturedPerfumes = new List<ManufacturedPerfumes>();
                return;
            }
            var selectedPerfumesHS = new HashSet<string>(selectedPerfumes);
            var manufacturedPerfumes = new HashSet<int>
            (manufacturerToUpdate.ManufacturedPerfumes.Select(c => c.Perfume.ID));
            foreach (var perfume in _context.Perfumes)
            {
                if (selectedPerfumesHS.Contains(perfume.ID.ToString()))
                {
                    if (!manufacturedPerfumes.Contains(perfume.ID))
                    {
                        manufacturerToUpdate.ManufacturedPerfumes.Add(new ManufacturedPerfumes { ManufacturerID = manufacturerToUpdate.ID, PerfumeID = perfume.ID });
                    }
                }
                else
                {
                    if (manufacturedPerfumes.Contains(perfume.ID))
                    {
                        ManufacturedPerfumes perfumeToRemove = manufacturerToUpdate.ManufacturedPerfumes.FirstOrDefault(i => i.PerfumeID == perfume.ID);
                        _context.Remove(perfumeToRemove);
                    }
                }
            }
        }

        // GET: Manufacturers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manufacturer = await _context.Manufacturer
                .FirstOrDefaultAsync(m => m.ID == id);
            if (manufacturer == null)
            {
                return NotFound();
            }

            return View(manufacturer);
        }

        // POST: Manufacturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var manufacturer = await _context.Manufacturer.FindAsync(id);
            _context.Manufacturer.Remove(manufacturer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ManufacturerExists(int id)
        {
            return _context.Manufacturer.Any(e => e.ID == id);
        }
    }
}
