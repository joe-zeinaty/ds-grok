using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D.S_Grok.Migrations
{
    /// <inheritdoc />
    public partial class SeedLinkedListChallenge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Challenges",
                columns: new[] { "ChallengeID", "ChallengeType", "Description", "LevelID", "Title", "XPReward" },
                values: new object[] { 1, "ListTraversal", "Build a linked list visually and observe how nodes are connected through pointers.", 2, "Create and Traverse a Linked List", 75 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Challenges",
                keyColumn: "ChallengeID",
                keyValue: 1);
        }
    }
}
