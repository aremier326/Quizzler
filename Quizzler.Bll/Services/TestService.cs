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



        public async ValueTask<ActiveTest> CheckTest(Test test, string selectedOption)
        {
            if (test == null) throw new ArgumentNullException(nameof(test));

            var activeTest = ToActiveTest(test);

            if (activeTest.CorrectAnswer == selectedOption)
            {
                activeTest.Result = true;
            }
            return activeTest;
        }

        public async ValueTask<Test> Get(int id)
        {
            return await _tests.FindAsync(id);
        }

        public ActiveTest ToActiveTest(Test test)
        {
            return new ActiveTest
            {
                Answer1 = test.Answer1,
                Answer2 = test.Answer2,
                Answer3 = test.Answer3,
                Answer4 = test.Answer4,
                TestType = test.TestType,
                CorrectAnswer = test.CorrectAnswer,
                Question = test.Question
            };
        }
    }
}
