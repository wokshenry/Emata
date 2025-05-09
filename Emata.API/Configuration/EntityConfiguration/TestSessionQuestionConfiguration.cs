using Emata.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Emata.API.Configuration.EntityConfiguration
{
    public class TestSessionQuestionConfiguration : IEntityTypeConfiguration<TestSessionQuestion>
    {
        public void Configure(EntityTypeBuilder<TestSessionQuestion> builder)
        {
            builder.HasKey(tsq => tsq.Id);

            builder.HasOne(tsq => tsq.Session)
                .WithMany(ts => ts.SessionQuestions)
                .HasForeignKey(tsq => tsq.SessionId);

            builder.HasOne(tsq => tsq.Question)
                .WithMany()
                .HasForeignKey(tsq => tsq.QuestionId);
        }
    }
}
