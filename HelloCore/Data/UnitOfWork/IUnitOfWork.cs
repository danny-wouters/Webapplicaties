using HelloCore.Data.Repository;
using HelloCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloCore.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<Bestelling> BestellingRepository { get; }
        Task Save();
    }
}
