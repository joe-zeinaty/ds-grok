using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace D.S_Grok.Migrations
{
    /// <inheritdoc />
    public partial class SeedLevelOneQuiz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Quizzes",
                columns: new[] { "QuizID", "LevelID", "PassMark", "Title", "XPReward" },
                values: new object[] { 1, 1, 60, "Pointer Fundamentals Quiz", 100 });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "QuestionID", "Explanation", "QuestionText", "QuizID" },
                values: new object[,]
                {
                    { 1, "PTR stores the memory address of X, not the value stored inside X.", "After assigning PTR = &X, what value does PTR store?", 1 },
                    { 2, "&X represents the memory address of variable X.", "If X has value 10 and address 1001, what does &X represent?", 1 },
                    { 3, "NULL means the pointer is not currently pointing to any valid memory address.", "What does NULL mean when used as a pointer value?", 1 }
                });

            migrationBuilder.InsertData(
                table: "QuestionOptions",
                columns: new[] { "OptionID", "IsCorrect", "OptionText", "QuestionID" },
                values: new object[,]
                {
                    { 1, false, "10", 1 },
                    { 2, true, "1001", 1 },
                    { 3, false, "X", 1 },
                    { 4, false, "NULL", 1 },
                    { 5, false, "The value of X", 2 },
                    { 6, false, "The name of X", 2 },
                    { 7, true, "The address of X", 2 },
                    { 8, false, "The data type of X", 2 },
                    { 9, true, "The pointer has no target address", 3 },
                    { 10, false, "The pointer stores zero XP", 3 },
                    { 11, false, "The pointer stores the value 10", 3 },
                    { 12, false, "The pointer deletes the variable", 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "QuizID",
                keyValue: 1);
        }
    }
}
