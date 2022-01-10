using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moldovan_Andrea_SADE_Proiect.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moldovan_Andrea_SADE_Proiect.Data;
using Moldovan_Andrea_SADE_Proiect.Models.CafeViewModels;

namespace Moldovan_Andrea_SADE_Proiect.Controllers
{
    public class HomeController : Controller
    {
        private readonly CafeContext _context;
        public HomeController(CafeContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Statistics()
        {
            IQueryable<InvoiceGroup> data =
            from invoice in _context.Invoices
            group invoice by invoice.InvoiceDate into dateGroup
            select new InvoiceGroup()
            {
                InvoiceDate = dateGroup.Key,
                CoffeeCount = dateGroup.Count()
            };
            return View(await data.AsNoTracking().ToListAsync());
        }
        /*
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        */
        public IActionResult Chat()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
