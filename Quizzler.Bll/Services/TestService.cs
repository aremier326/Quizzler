using Microsoft.EntityFrameworkCore;
using Quizzler.Bll.Interfaces.Interfaces;
using Quizzler.Dal.Data.DbContextData;
using Quizzler.Dal.Interfaces.Entities;

namespace Quizzler.Bll.Services
{
    public class TestService : ITestService
    {
        protected readonly MainDbContext _context;
        protected readonly DbSet<Test> _tests;

        public TestService(MainDbContext context)
        {
            _context = context;
            _tests = _context.Set<Test>();
        }

        

        //public async ValueTask<ActiveTest> CheckTest(Test test, string selectedOption)
        //{
        //    if (test == null) throw new ArgumentNullException(nameof(test));

        //    ActiveTest activeTest = (ActiveTest)test;
            
        //    if (activeTest.CorrectAnswer == selectedOption)
        //    {
        //        activeTest.Result = true;
        //    }
        //    return activeTest;
        //}

        public async ValueTask<ActiveTest> CheckTest(int testId)
        {
            return new ActiveTest();
        }

        public async ValueTask<Test> Get(int id)
        {
            return await _tests.FindAsync(id);
        }
    }
}
