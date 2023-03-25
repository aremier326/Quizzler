using Microsoft.EntityFrameworkCore;
using Quizzler.Bll.Interfaces.Interfaces;
using Quizzler.Dal.Data.DbContextData;
using Quizzler.Dal.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzler.Bll.Services
{
    public class ActiveTestService : IActiveTestService
    {
        protected readonly MainDbContext _context;

        protected readonly DbSet<ActiveTest> _activeTests;

        public ActiveTestService(MainDbContext context)
        {
            _context = context;
            _activeTests = _context.Set<ActiveTest>();
        }

        public async ValueTask CreateAsync(ActiveTest activeTest)
        {
            await _activeTests.AddAsync(activeTest);
            await _context.SaveChangesAsync();
        }

        public async ValueTask UpdateAsync(ActiveTest activeTest)
        {
            _activeTests.Update(activeTest);
            await _context.SaveChangesAsync();
        }

        public async ValueTask DeleteAsync(ActiveTest activeTest)
        {
            _activeTests.Remove(activeTest);
            await _context.SaveChangesAsync();
        }
    }
}
