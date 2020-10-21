using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HelloCore.Data;
using HelloCore.Models;

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
            return View();
        }

        // POST: Opleiding/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OpleidingID,Naam,Prijs,AantalLesuren")] Opleiding opleiding)
        {
            if (ModelState.IsValid)
            {
                _context.Add(opleiding);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(opleiding);
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
