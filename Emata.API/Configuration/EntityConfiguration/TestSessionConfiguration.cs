using Emata.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Emata.API.Configuration.EntityConfiguration
{
    public class TestSessionConfiguration : IEntityTypeConfiguration<TestSession>
    {
        public void Configure(EntityTypeBuilder<TestSession> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
