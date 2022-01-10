using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Moldovan_Andrea_SADE_Proiect.Data;
using Moldovan_Andrea_SADE_Proiect.Models;
using Moldovan_Andrea_SADE_Proiect.Models.CafeViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Moldovan_Andrea_SADE_Proiect.Controllers
{
    [Authorize(Policy = "OnlyMarketing")]
    public class SuppliersController : Controller
    {
        private readonly CafeContext _context;

        public SuppliersController(CafeContext context)
        {
            _context = context;
        }

        // GET: Suppliers
        public async Task<IActionResult> Index(int? id, int? coffeeID)
        {
            var viewModel = new SupplierIndexData();
            viewModel.Suppliers = await _context.Suppliers
            .Include(i => i.SuppliedCoffees)
            .ThenInclude(i => i.Coffee)
            .ThenInclude(i => i.Invoices)
            .ThenInclude(i => i.Customer)
            .AsNoTracking()
            .OrderBy(i => i.SupplierName)
            .ToListAsync();
            if (id != null)
            {
                ViewData["SupplierID"] = id.Value;
                Supplier supplier = viewModel.Suppliers.Where(
                i => i.ID == id.Value).Single();
                viewModel.Coffees = supplier.SuppliedCoffees.Select(s => s.Coffee);
            }
            if (coffeeID != null)
            {
                ViewData["CoffeeID"] = coffeeID.Value;
                viewModel.Invoices = viewModel.Coffees.Where(
                x => x.ID == coffeeID).Single().Invoices;
            }
            return View(viewModel);
        }
    

        // GET: Suppliers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // GET: Suppliers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,SupplierName,Address")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(supplier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        // GET: Suppliers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var supplier = await _context.Suppliers
            .Include(i => i.SuppliedCoffees).ThenInclude(i => i.Coffee)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);
            if (supplier == null)
            {
                return NotFound();
            }
            PopulateSuppliedCoffeeData(supplier);
            return View(supplier);

        }
        private void PopulateSuppliedCoffeeData(Supplier supplier)
        {
            var allCoffees = _context.Coffees;
            var supplierCoffee = new HashSet<int>(supplier.SuppliedCoffees.Select(c => c.CoffeeID));
            var viewModel = new List<SuppliedCoffeeData>();
            foreach (var coffee in allCoffees)
            {
                viewModel.Add(new SuppliedCoffeeData
                {
                    CoffeeID = coffee.ID,
                    Name = coffee.Name,
                    IsSupplied = supplierCoffee.Contains(coffee.ID)
                });
            }
            ViewData["Coffees"] = viewModel;
        }


        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedCoffees)
        {
            if (id == null)
            {
                return NotFound();
            }
            var supplierToUpdate = await _context.Suppliers
            .Include(i => i.SuppliedCoffees)
            .ThenInclude(i => i.Coffee)
            .FirstOrDefaultAsync(i => i.ID == id);

            if (await TryUpdateModelAsync<Supplier>(
            supplierToUpdate,
            "",
            i => i.SupplierName, i => i.Address))
            {
                UpdateSuppliedCoffees(selectedCoffees, supplierToUpdate);
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
            UpdateSuppliedCoffees(selectedCoffees, supplierToUpdate);
            PopulateSuppliedCoffeeData(supplierToUpdate);
            return View(supplierToUpdate);
        }
        private void UpdateSuppliedCoffees(string[] selectedCoffees, Supplier supplierToUpdate)
        {
            if (selectedCoffees == null)
            {
                supplierToUpdate.SuppliedCoffees = new List<SuppliedCoffee>();
                return;
            }
            var selectedCoffeesHS = new HashSet<string>(selectedCoffees);
            var suppliedCoffees = new HashSet<int>
            (supplierToUpdate.SuppliedCoffees.Select(c => c.Coffee.ID));
            foreach (var coffee in _context.Coffees)
            {
                if (selectedCoffeesHS.Contains(coffee.ID.ToString()))
                {
                    if (!suppliedCoffees.Contains(coffee.ID))
                    {
                        supplierToUpdate.SuppliedCoffees.Add(new SuppliedCoffee
                        {
                            SupplierID = supplierToUpdate.ID,
                            CoffeeID = coffee.ID
                        });
                    }
                }
                else
                {
                    if (suppliedCoffees.Contains(coffee.ID))
                    {
                        SuppliedCoffee coffeeToRemove = supplierToUpdate.SuppliedCoffees.FirstOrDefault(i=> i.CoffeeID == coffee.ID);
                        _context.Remove(coffeeToRemove);
                    }
                }
            }
        }

        // GET: Suppliers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupplierExists(int id)
        {
            return _context.Suppliers.Any(e => e.ID == id);
        }
    }
}
