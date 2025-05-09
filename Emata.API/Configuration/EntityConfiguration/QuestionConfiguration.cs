using Emata.API.Models;
using Emata.Shared.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Emata.API.Configuration.EntityConfiguration
{
    internal class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.Text)
            .HasMaxLength(Shared.Shared.Constants.StringMaxLength.DEFAULT_NAME_LENGTH)
            .IsRequired();

            builder.Property(q => q.Options)
                .HasConversion(
                    v => string.Join(";", v),
                    v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList());

            builder.HasData(
                new Question
                {
                    Id = Guid.Parse("e0b423bf-d15f-4dee-b321-6eed79de9049"),
                    Text = "Which of the following is not a valid C# data type?",
                    Options = new List<string> { "int", "float", "char", "string", "variant" },
                    CorrectOptionIndex = 4,
                    Category = "Programming",
                    DifficultyLevel = DifficultyLevel.Medium
                },
                new Question
                {
                    Id = Guid.Parse("b31f62f5-bba8-4582-b19b-1400d90efbdd"),
                    Text = "What is the result of 5 + 7 * 2?",
                    Options = new List<string> { "12", "19", "24", "14", "None of the above" },
                    CorrectOptionIndex = 1,
                    Category = "Mathematics",
                    DifficultyLevel = DifficultyLevel.Hard
                },
                new Question
                {
                    Id = Guid.Parse("d72e0d79-ee81-4989-82c8-b1fd322597eb"),
                    Text = "Which of these is not a JavaScript framework?",
                    Options = new List<string> { "Angular", "React", "Vue", "Laravel", "Svelte" },
                    CorrectOptionIndex = 3,
                    Category = "Programming",
                    DifficultyLevel = DifficultyLevel.Medium
                },
                new Question
                {
                    Id = Guid.Parse("6f9d50a7-d740-4d92-8ee9-7a4d4e05351e"),
                    Text = "What is the capital of France?",
                    Options = new List<string> { "London", "Berlin", "Paris", "Madrid", "Rome" },
                    CorrectOptionIndex = 2,
                    Category = "Geography",
                    DifficultyLevel = DifficultyLevel.Hard
                },
                new Question
                {
                    Id = Guid.Parse("cce917bd-c030-4281-9538-7bd8a88440e5"),
                    Text = "Which of the following is a primary key constraint in SQL?",
                    Options = new List<string> { "It can contain NULL values", "It can have duplicate values", "It uniquely identifies each record in a table", "It can be modified easily", "It allows multiple columns with the same name" },
                    CorrectOptionIndex = 2,
                    Category = "Database",
                    DifficultyLevel = DifficultyLevel.Easy
                },
                new Question
                {
                    Id = Guid.Parse("53f6a56b-e80b-4992-8168-0e14710e6c6d"),
                    Text = "What does HTTP stand for?",
                    Options = new List<string> { "Hypertext Transfer Protocol", "Hypertext Test Protocol", "Hyper Transfer Text Protocol", "High Transfer Text Protocol", "Hypertext Transfer Procedure" },
                    CorrectOptionIndex = 0,
                    Category = "Networking",
                    DifficultyLevel = DifficultyLevel.Hard
                },
                new Question
                {
                    Id = Guid.Parse("3a049314-b09a-411d-ba3d-b71a989444d6"),
                    Text = "Which Big O notation represents the worst performance?",
                    Options = new List<string> { "O(1)", "O(log n)", "O(n)", "O(n log n)", "O(n²)" },
                    CorrectOptionIndex = 4,
                    Category = "Algorithms",
                    DifficultyLevel = DifficultyLevel.Easy
                },
                new Question
                {
                    Id = Guid.Parse("cc4f86fe-9789-49fc-9fe5-ebf73485f315"),
                    Text = "What is the correct syntax for an if statement in Python?",
                    Options = new List<string> { "if (x > y) {}", "if x > y {}", "if x > y:", "if (x > y):", "if x > y then" },
                    CorrectOptionIndex = 2,
                    Category = "Programming",
                    DifficultyLevel = DifficultyLevel.Medium
                },
                new Question
                {
                    Id = Guid.Parse("2d9dd2d0-25ce-47b4-9a2c-c6adb9024b61"),
                    Text = "Which data structure operates on a LIFO principle?",
                    Options = new List<string> { "Queue", "Stack", "Linked List", "Tree", "Graph" },
                    CorrectOptionIndex = 1,
                    Category = "Data Structures",
                    DifficultyLevel = DifficultyLevel.Medium
                },
                new Question
                {
                    Id = Guid.Parse("fa17bb2c-b9ff-4b79-891b-3c4a969ef116"),
                    Text = "What is the result of 3 + 2 * 4?",
                    Options = new List<string> { "11", "20", "14", "5", "None of the above" },
                    CorrectOptionIndex = 0,
                    Category = "Mathematics",
                    DifficultyLevel = DifficultyLevel.Hard
                });
        }
    }
}
