using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proiect_Neag_Cristina.Data;
using Proiect_Neag_Cristina.Models;

namespace Proiect_Neag_Cristina.Controllers
{
    [Authorize(Roles = "Employee")]
    public class PerfumesController : Controller
    {
        private readonly PerfumeStoreContext _context;

        public PerfumesController(PerfumeStoreContext context)
        {
            _context = context;
        }

        // GET: Perfumes
        [AllowAnonymous]
        public async Task<IActionResult> Index(string sortOrder,
                string currentFilter,
                string searchString,
                int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["WeightSortParm"] = sortOrder == "Weight" ? "weight_desc" : "Weight";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            var perfumes = from p in _context.Perfumes
                        select p;
            if (!String.IsNullOrEmpty(searchString))
            {
                perfumes = perfumes.Where(s => s.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    perfumes = perfumes.OrderByDescending(b => b.Name);
                    break;
                case "Weight":
                    perfumes = perfumes.OrderBy(b => b.Weight);
                    break;
                case "weight_desc":
                    perfumes = perfumes.OrderByDescending(b => b.Weight);
                    break;
                case "Price":
                    perfumes = perfumes.OrderBy(b => b.Price);
                    break;
                case "price_desc":
                    perfumes = perfumes.OrderByDescending(b => b.Price);
                    break;
                default:
                    perfumes = perfumes.OrderBy(b => b.Name);
                    break;
            }

            int pageSize = 2;
            return View(await PaginatedList<Perfume>.CreateAsync(perfumes.AsNoTracking(), pageNumber ?? 1, pageSize));

        }

        // GET: Perfumes/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perfume = await _context.Perfumes
                .Include(s => s.Orders)
                .ThenInclude(e => e.Customer)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (perfume == null)
            {
                return NotFound();
            }

            return View(perfume);
        }

        // GET: Perfumes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Perfumes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Brand,Weight,Price")] Perfume perfume)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(perfume);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }catch (DbUpdateException /*ex*/)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists ");
            }
           
            return View(perfume);
        }

        // GET: Perfumes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perfume = await _context.Perfumes.FindAsync(id);
            if (perfume == null)
            {
                return NotFound();
            }
            return View(perfume);
        }

        // POST: Perfumes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPerfume(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perfumeToUpdate = await _context.Perfumes.FirstOrDefaultAsync(s => s.ID == id);

            if (await TryUpdateModelAsync<Perfume>(
                perfumeToUpdate,
                "",
                s => s.Brand,
                s => s.Weight,
                s => s.Price))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists");
                }
            }
            return View(perfumeToUpdate);
        }

        // GET: Perfumes/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perfume = await _context.Perfumes
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (perfume == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                "Delete failed. Try again";
            }

            return View(perfume);
        }

        // POST: Perfumes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var perfume = await _context.Perfumes.FindAsync(id);

            if (perfume == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Perfumes.Remove(perfume);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
            
        }

        private bool PerfumeExists(int id)
        {
            return _context.Perfumes.Any(e => e.ID == id);
        }
    }
}
