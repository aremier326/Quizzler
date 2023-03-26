using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Quizzler.Dal.Interfaces.Entities.Identity;
using Quizzler.Dal.Interfaces.Entities;

namespace Quizzler.Dal.Data.DbContextData
{
    public class MainDbContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {

        }

        #region DbSets

        DbSet<Test> Tests { get; set; }
        DbSet<ActiveTest> ActiveTests { get; set; }
        DbSet<Quiz> Quizzes { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .ToTable("QuizUsers");

            modelBuilder.Entity<Quiz>()
                .ToTable("Quizzes");

            modelBuilder.Entity<Test>()
                .ToTable("Tests");

            modelBuilder.Entity<Quiz>()
                .HasMany(t => t.ActiveTests);

            modelBuilder.Entity<User>()
                .HasMany(q => q.Quizzes);
        }
    }
}
