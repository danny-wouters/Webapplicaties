using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HelloCore.Data;
using HelloCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using HelloCore.Data.UnitOfWork;

namespace HelloCore.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BestellingController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public BestellingController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Bestelling
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bestelling>>> GetBestellingen()
        {
            return await _uow.BestellingRepository.GetAll().ToListAsync();
        }

        // GET: api/Bestelling/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bestelling>> GetBestelling(int id)
        {
            Bestelling bestelling = _uow.BestellingRepository.GetById(id);

            if (bestelling == null)
            {
                return NotFound();
            }

            return bestelling;
        }

        // GET: api/Bestelling/lijst
        [Authorize]
        [HttpGet("lijst")]
        public async Task<ActionResult<IEnumerable<Bestelling>>> GetBestellingenlijst()
        {
            return await _uow.BestellingRepository.GetAll().Include(b => b.Klant).ToListAsync();
        }

        // PUT: api/Bestelling/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBestelling(int id, Bestelling bestelling)
        {
            if (id != bestelling.BestellingID)
            {
                return BadRequest();
            }

            _uow.BestellingRepository.Update(bestelling);
            _uow.Save();

            return NoContent();
        }

        // POST: api/Bestelling
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Bestelling>> PostBestelling(Bestelling bestelling)
        {
            _uow.BestellingRepository.Create(bestelling);
            _uow.Save();

            return CreatedAtAction("GetBestelling", new { id = bestelling.BestellingID }, bestelling);
        }

        // DELETE: api/Bestelling/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Bestelling>> DeleteBestelling(int id)
        {
            Bestelling bestelling = _uow.BestellingRepository.GetById(id);
            if (bestelling == null)
            {
                return NotFound();
            }

            _uow.BestellingRepository.Delete(bestelling);
            _uow.Save();

            return NoContent();
        }

        private bool BestellingExists(int id)
        {
            return (_uow.BestellingRepository.GetById(id) != null);
        }
    }
}
