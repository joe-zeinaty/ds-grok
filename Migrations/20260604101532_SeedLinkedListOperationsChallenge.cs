using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D.S_Grok.Migrations
{
    /// <inheritdoc />
    public partial class SeedLinkedListOperationsChallenge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Challenges",
                columns: new[] { "ChallengeID", "ChallengeType", "Description", "LevelID", "Title", "XPReward" },
                values: new object[] { 2, "ListOperations", "Practice insertion, deletion, and searching in a linked list.", 3, "Linked List Operations Simulator", 100 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Challenges",
                keyColumn: "ChallengeID",
                keyValue: 2);
        }
    }
}
