using Emata.API.Models;
using Microsoft.EntityFrameworkCore;
using Emata.Shared.Shared;

namespace Emata.API.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Question> Questions { get; set; } = null!;
        public DbSet<TestSession> TestSessions { get; set; } = null!;
        public DbSet<TestSessionQuestion> TestSessionQuestions { get; set; } = null!;
        public DbSet<TestResult> TestResults { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            //convert all enums to string
            configurationBuilder.Properties<Enum>().HaveConversion<string>()
                .HaveMaxLength(Constants.StringMaxLength.SHORT_DESCRIPTION_LENGTH);

            configurationBuilder.Properties<decimal>().HavePrecision(18, 6);
            configurationBuilder.Properties<double>().HavePrecision(18, 2);

        }
    }
}
