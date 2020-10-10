using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HelloCore.Data;
using HelloCore.Models;
using HelloCore.ViewModels;

namespace HelloCore.Controllers
{
    public class BestellingController : Controller
    {
        private readonly HelloCoreContext _context;

        public BestellingController(HelloCoreContext context)
        {
            _context = context;
        }

        // GET: Bestelling
        public async Task<IActionResult> Index()
        {
            ListBestellingViewModel viewModel = new ListBestellingViewModel();
            viewModel.Bestellingen = await _context.Bestellingen.Include(b => b.Klant).ToListAsync();
            return View(viewModel);
        }

        // GET: Bestelling gefilterd op artikel
        public async Task<IActionResult> Search(ListBestellingViewModel viewModel)
        {
            if (!string.IsNullOrEmpty(viewModel.ArtikelSearch))
            {
                viewModel.Bestellingen = await _context.Bestellingen.Include(b => b.Klant)
                    .Where(b => b.Artikel.Contains(viewModel.ArtikelSearch)).ToListAsync();
            }
            else
            {
                viewModel.Bestellingen = await _context.Bestellingen.Include(b => b.Klant).ToListAsync();
            }

            return View("Index", viewModel);
        }

        // GET: Bestelling/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestelling = await _context.Bestellingen
                .Include(b => b.Klant)
                .FirstOrDefaultAsync(m => m.BestellingID == id);
            if (bestelling == null)
            {
                return NotFound();
            }

            return View(bestelling);
        }

        // GET: Bestelling/Create
        public IActionResult Create()
        {
            CreateBestellingViewModel viewModel = new CreateBestellingViewModel();
            viewModel.Bestelling = new Bestelling();
            viewModel.Klanten = new SelectList(_context.Klanten, "KlantID", "VolledigeNaam");
            return View(viewModel);
        }

        // POST: Bestelling/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBestellingViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(viewModel.Bestelling);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            viewModel.Klanten = new SelectList(_context.Klanten, "KlantID", "VolledigeNaam", viewModel.Bestelling.KlantID);
            return View(viewModel);
        }

        // GET: Bestelling/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestelling = await _context.Bestellingen.FindAsync(id);
            if (bestelling == null)
            {
                return NotFound();
            }
            ViewData["KlantID"] = new SelectList(_context.Klanten, "KlantID", "Naam", bestelling.KlantID);
            return View(bestelling);
        }

        // POST: Bestelling/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BestellingID,Artikel,Prijs,KlantID")] Bestelling bestelling)
        {
            if (id != bestelling.BestellingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bestelling);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BestellingExists(bestelling.BestellingID))
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
            ViewData["KlantID"] = new SelectList(_context.Klanten, "KlantID", "Naam", bestelling.KlantID);
            return View(bestelling);
        }

        // GET: Bestelling/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestelling = await _context.Bestellingen
                .Include(b => b.Klant)
                .FirstOrDefaultAsync(m => m.BestellingID == id);
            if (bestelling == null)
            {
                return NotFound();
            }

            return View(bestelling);
        }

        // POST: Bestelling/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bestelling = await _context.Bestellingen.FindAsync(id);
            _context.Bestellingen.Remove(bestelling);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BestellingExists(int id)
        {
            return _context.Bestellingen.Any(e => e.BestellingID == id);
        }
    }
}
