using Microsoft.EntityFrameworkCore;
using Quizzler.Bll.Interfaces.Interfaces;
using Quizzler.Dal.Data.DbContextData;
using Quizzler.Dal.Interfaces.Entities;
using Quizzler.Dal.Interfaces.Enums;
using System.Collections.Generic;

namespace Quizzler.Bll.Services
{
    public class QuizService : IQuizService
    {
        protected readonly MainDbContext _context;
        protected readonly DbSet<Quiz> _quizzes;
        protected readonly DbSet<Test> _tests;

        public QuizService(MainDbContext context)
        {
            _context = context;
            _quizzes = _context.Set<Quiz>();
            _tests = _context.Set<Test>();
        }

        public async ValueTask CreateAsync(Quiz quiz)
        {
            await _quizzes.AddAsync(quiz);
            await _context.SaveChangesAsync();
        }

        public async ValueTask UpdateAsync(Quiz quiz)
        {
            _quizzes.Update(quiz);
            await _context.SaveChangesAsync();
        }

        public async ValueTask DeleteAsync(Quiz quiz)
        {
            _quizzes.Remove(quiz);
            await _context.SaveChangesAsync();
        }

        //Method for getting questions for specific category.
        public async ValueTask<IEnumerable<Test>> GetTests(TestTypeEnum category, int number = 5)
        {
            var random = new Random();
            return _tests.Where(x => x.TestType == category)
                .OrderBy(x => random.Next())
                .Take(number)
                .ToList();
        }

        //This method can be used after ActiveTests are assigned to Quiz object
        public async ValueTask<int> GetResult(Quiz quiz)
        {
            int result = 0;
            foreach(var test in quiz.ActiveTests)
            {
                if (test.Result == true)
                    result++;
            }
            return result;
        }

        
    }
}
