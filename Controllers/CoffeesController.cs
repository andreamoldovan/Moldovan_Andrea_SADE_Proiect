using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Moldovan_Andrea_SADE_Proiect.Data;
using Moldovan_Andrea_SADE_Proiect.Models;
using Microsoft.AspNetCore.Authorization;

namespace Moldovan_Andrea_SADE_Proiect.Controllers
{
    [Authorize(Roles = "Employee")]
    public class CoffeesController : Controller
    {
        private readonly CafeContext _context;

        public CoffeesController(CafeContext context)
        {
            _context = context;
        }

        // GET: Coffees

        [AllowAnonymous]
        public async Task<IActionResult> Index(
             string sortOrder,
             string currentFilter,
             string searchString,
             int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
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
            var coffees = from b in _context.Coffees
                        select b;
            if (!String.IsNullOrEmpty(searchString))
            {
                coffees = coffees.Where(s => s.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    coffees = coffees.OrderByDescending(b => b.Name);
                    break;
                case "Price":
                    coffees = coffees.OrderBy(b => b.Price);
                    break;
                case "price_desc":
                    coffees = coffees.OrderByDescending(b => b.Price);
                    break;
                default:
                    coffees = coffees.OrderBy(b => b.Name);
                    break;
            }
            int pageSize = 2;
            return View(await PaginatedList<Coffee>.CreateAsync(coffees.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Coffees/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coffee = await _context.Coffees
                .Include(s => s.Invoices)
                .ThenInclude(e => e.Customer)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (coffee == null)
            {
                return NotFound();
            }

            return View(coffee);
        }

        // GET: Coffees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Coffees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Taste,Weight,Price")] Coffee coffee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(coffee);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex*/)
            {

                ModelState.AddModelError("", "Unable to save changes. " +
                "Try again, and if the problem persists ");
            }
            return View(coffee);
        }

        // GET: Coffees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coffee = await _context.Coffees.FindAsync(id);
            if (coffee == null)
            {
                return NotFound();
            }
            return View(coffee);
        }

        // POST: Coffees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coffeeToUpdate = await _context.Coffees.FirstOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<Coffee>(coffeeToUpdate,
            "",
            s => s.Name, s => s.Taste, s => s.Weight, s => s.Price))
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
            return View(coffeeToUpdate);
        }

        // GET: Coffees/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coffee = await _context.Coffees
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (coffee == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                "Delete failed. Try again";
            }

            return View(coffee);
        }

        // POST: Coffees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coffee = await _context.Coffees.FindAsync(id);
            if (coffee == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Coffees.Remove(coffee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {

                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool CoffeeExists(int id)
        {
            return _context.Coffees.Any(e => e.ID == id);
        }
    }
}
