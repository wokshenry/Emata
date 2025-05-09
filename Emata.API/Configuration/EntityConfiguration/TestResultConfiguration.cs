using Emata.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Emata.API.Configuration.EntityConfiguration
{
    public class TestResultConfiguration : IEntityTypeConfiguration<TestResult>
    {
        public void Configure(EntityTypeBuilder<TestResult> builder)
        {
            builder.HasKey(tr => tr.Id);

            builder.HasOne(tr => tr.Session)
                .WithOne()
                .HasForeignKey<TestResult>(tr => tr.SessionId);
        }
    }
}
