using HelloCore.Data.Repository;
using HelloCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloCore.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HelloCoreContext _context;
        private IGenericRepository<Bestelling> bestellingRepository;


        public UnitOfWork(HelloCoreContext context)
        {
            _context = context;
        }

        public IGenericRepository<Bestelling> BestellingRepository
        {
            get
            {
                if (this.bestellingRepository == null)
                {
                    this.bestellingRepository = new GenericRepository<Bestelling>(_context);
                }
                return bestellingRepository;
            }
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

    }
}
