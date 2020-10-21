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
    public class OpleidingController : Controller
    {
        private readonly HelloCoreContext _context;

        public OpleidingController(HelloCoreContext context)
        {
            _context = context;
        }

        // GET: Opleiding
        public async Task<IActionResult> Index()
        {
            return View(await _context.Opleiding.ToListAsync());
        }

        // GET: Opleiding/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opleiding = await _context.Opleiding
                .FirstOrDefaultAsync(m => m.OpleidingID == id);
            if (opleiding == null)
            {
                return NotFound();
            }

            return View(opleiding);
        }

        // GET: Opleiding/Create
        public IActionResult Create()
        {
            CreateOpleidingViewModel viewModel = new CreateOpleidingViewModel
            {
                Opleiding = new Opleiding(),
                KlantenLijst = new SelectList(_context.Klanten, "KlantID", "VolledigeNaam"),
                GeselecteerdeKlanten = new List<int>()
            };
            return View(viewModel);
        }

        // POST: Opleiding/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateOpleidingViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                List<KlantOpleiding> nieuweKlanten = new List<KlantOpleiding>();
                if (viewModel.GeselecteerdeKlanten == null)
                {
                    viewModel.GeselecteerdeKlanten = new List<int>();
                }
                foreach(int klantID in viewModel.GeselecteerdeKlanten)
                {
                    nieuweKlanten.Add(new KlantOpleiding
                    {
                        KlantID = klantID,
                        OpleidingID = viewModel.Opleiding.OpleidingID
                    });
                }

                _context.Add(viewModel.Opleiding);
                await _context.SaveChangesAsync();

                Opleiding opleiding = await _context.Opleiding.Include(o => o.KlantOpleidingen)
                    .SingleOrDefaultAsync(x => x.OpleidingID == viewModel.Opleiding.OpleidingID);
                opleiding.KlantOpleidingen.AddRange(nieuweKlanten);
                _context.Update(opleiding);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Opleiding/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opleiding = await _context.Opleiding.FindAsync(id);
            if (opleiding == null)
            {
                return NotFound();
            }
            return View(opleiding);
        }

        // POST: Opleiding/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OpleidingID,Naam,Prijs,AantalLesuren")] Opleiding opleiding)
        {
            if (id != opleiding.OpleidingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(opleiding);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OpleidingExists(opleiding.OpleidingID))
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
            return View(opleiding);
        }

        // GET: Opleiding/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opleiding = await _context.Opleiding
                .FirstOrDefaultAsync(m => m.OpleidingID == id);
            if (opleiding == null)
            {
                return NotFound();
            }

            return View(opleiding);
        }

        // POST: Opleiding/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var opleiding = await _context.Opleiding.FindAsync(id);
            _context.Opleiding.Remove(opleiding);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OpleidingExists(int id)
        {
            return _context.Opleiding.Any(e => e.OpleidingID == id);
        }
    }
}
