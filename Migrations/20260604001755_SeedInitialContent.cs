using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace D.S_Grok.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Achievements",
                columns: new[] { "AchievementID", "Description", "RequirementType", "RequirementValue", "Title", "XPReward" },
                values: new object[,]
                {
                    { 1, "Complete your first lesson.", "CompletedLessons", 1, "First Steps", 25 },
                    { 2, "Complete Level 1.", "CompletedLevel", 1, "Pointer Explorer", 50 },
                    { 3, "Complete Level 2.", "CompletedLevel", 2, "List Master", 50 },
                    { 4, "Complete Level 3.", "CompletedLevel", 3, "Linked List Expert", 100 },
                    { 5, "Achieve 100% on any quiz.", "PerfectQuizScore", 100, "Quiz Champion", 75 }
                });

            migrationBuilder.InsertData(
                table: "Levels",
                columns: new[] { "LevelID", "Description", "LevelNumber", "RequiredXP", "Title" },
                values: new object[,]
                {
                    { 1, "Introduction to memory references and pointer concepts.", 1, 0, "Pointer Fundamentals" },
                    { 2, "Introduction to nodes, head pointer, and traversal.", 2, 150, "Linked Lists" },
                    { 3, "Insertion, deletion, searching, and updating nodes.", 3, 350, "Linked List Operations" }
                });

            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "LessonID", "Content", "LessonOrder", "LevelID", "Title", "XPReward" },
                values: new object[,]
                {
                    { 1, "A pointer is a variable that stores the memory address of another variable.", 1, 1, "What is a Pointer?", 50 },
                    { 2, "Pointers can reference values and be reassigned to point to different memory locations.", 2, 1, "Pointer Assignment and References", 50 },
                    { 3, "A node is a structure that stores data and a reference to the next node.", 1, 2, "What is a Node?", 50 },
                    { 4, "A linked list is created by connecting nodes together using pointers.", 2, 2, "Creating a Linked List", 50 },
                    { 5, "Traversal means moving from one node to the next until the end of the list is reached.", 3, 2, "Traversing a Linked List", 50 },
                    { 6, "Insertion requires updating pointers so that the new node becomes part of the list.", 1, 3, "Inserting a Node", 50 },
                    { 7, "Deletion removes a node by updating the previous node pointer to skip the removed node.", 2, 3, "Deleting a Node", 50 },
                    { 8, "Searching checks each node until the target value is found, then the node data may be updated.", 3, 3, "Searching and Updating Nodes", 50 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "AchievementID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "AchievementID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "AchievementID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "AchievementID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "AchievementID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "LessonID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "LessonID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "LessonID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "LessonID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "LessonID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "LessonID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "LessonID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "LessonID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Levels",
                keyColumn: "LevelID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Levels",
                keyColumn: "LevelID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Levels",
                keyColumn: "LevelID",
                keyValue: 3);
        }
    }
}
