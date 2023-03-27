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

        public async ValueTask<Quiz> CreateAsync(Quiz quiz)
        {
            var value = await _quizzes.AddAsync(quiz);
            await _context.SaveChangesAsync();
            return value.Entity;
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
        public async ValueTask<IEnumerable<Test>> GetTestsAsync(int category, int number = 5)
        {
            var random = new Random();
            return _tests.Where(x => x.TestType == category && !(x is ActiveTest))
                .ToList();
        }

        //This method can be used after ActiveTests are assigned to Quiz object
        public async ValueTask<double> GetResultAsync(Quiz quiz)
        {
            quiz.Result = quiz.ActiveTests.Count(x => x.Result) / (double)quiz.ActiveTests.Count() * 100;
            return quiz.Result;
        }

        public async ValueTask<Quiz> GetAsync(int id)
        {
            return await _quizzes.Include(x => x.ActiveTests).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
