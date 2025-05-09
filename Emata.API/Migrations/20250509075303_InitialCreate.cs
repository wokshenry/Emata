using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Emata.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Options = table.Column<string>(type: "text", nullable: false),
                    CorrectOptionIndex = table.Column<int>(type: "integer", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: true),
                    DifficultyLevel = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    UserAnswersJson = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestSessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TotalQuestions = table.Column<int>(type: "integer", nullable: false),
                    CorrectAnswers = table.Column<int>(type: "integer", nullable: false),
                    TimeTaken = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestResults_TestSessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "TestSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestSessionQuestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderIndex = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestSessionQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestSessionQuestions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestSessionQuestions_TestSessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "TestSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Category", "CorrectOptionIndex", "DifficultyLevel", "Options", "Text" },
                values: new object[,]
                {
                    { new Guid("2d9dd2d0-25ce-47b4-9a2c-c6adb9024b61"), "Data Structures", 1, "Medium", "Queue;Stack;Linked List;Tree;Graph", "Which data structure operates on a LIFO principle?" },
                    { new Guid("3a049314-b09a-411d-ba3d-b71a989444d6"), "Algorithms", 4, "Easy", "O(1);O(log n);O(n);O(n log n);O(n²)", "Which Big O notation represents the worst performance?" },
                    { new Guid("53f6a56b-e80b-4992-8168-0e14710e6c6d"), "Networking", 0, "Hard", "Hypertext Transfer Protocol;Hypertext Test Protocol;Hyper Transfer Text Protocol;High Transfer Text Protocol;Hypertext Transfer Procedure", "What does HTTP stand for?" },
                    { new Guid("6f9d50a7-d740-4d92-8ee9-7a4d4e05351e"), "Geography", 2, "Hard", "London;Berlin;Paris;Madrid;Rome", "What is the capital of France?" },
                    { new Guid("b31f62f5-bba8-4582-b19b-1400d90efbdd"), "Mathematics", 1, "Hard", "12;19;24;14;None of the above", "What is the result of 5 + 7 * 2?" },
                    { new Guid("cc4f86fe-9789-49fc-9fe5-ebf73485f315"), "Programming", 2, "Medium", "if (x > y) {};if x > y {};if x > y:;if (x > y):;if x > y then", "What is the correct syntax for an if statement in Python?" },
                    { new Guid("cce917bd-c030-4281-9538-7bd8a88440e5"), "Database", 2, "Easy", "It can contain NULL values;It can have duplicate values;It uniquely identifies each record in a table;It can be modified easily;It allows multiple columns with the same name", "Which of the following is a primary key constraint in SQL?" },
                    { new Guid("d72e0d79-ee81-4989-82c8-b1fd322597eb"), "Programming", 3, "Medium", "Angular;React;Vue;Laravel;Svelte", "Which of these is not a JavaScript framework?" },
                    { new Guid("e0b423bf-d15f-4dee-b321-6eed79de9049"), "Programming", 4, "Medium", "int;float;char;string;variant", "Which of the following is not a valid C# data type?" },
                    { new Guid("fa17bb2c-b9ff-4b79-891b-3c4a969ef116"), "Mathematics", 0, "Hard", "11;20;14;5;None of the above", "What is the result of 3 + 2 * 4?" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestResults_SessionId",
                table: "TestResults",
                column: "SessionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestSessionQuestions_QuestionId",
                table: "TestSessionQuestions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_TestSessionQuestions_SessionId",
                table: "TestSessionQuestions",
                column: "SessionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestResults");

            migrationBuilder.DropTable(
                name: "TestSessionQuestions");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "TestSessions");
        }
    }
}
