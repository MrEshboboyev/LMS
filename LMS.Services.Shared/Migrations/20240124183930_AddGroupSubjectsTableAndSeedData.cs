using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LMS.Services.Shared.Migrations
{
    /// <inheritdoc />
    public partial class AddGroupSubjectsTableAndSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupSubjects",
                columns: table => new
                {
                    GroupSubjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupSubjects", x => x.GroupSubjectId);
                });

            migrationBuilder.InsertData(
                table: "GroupSubjects",
                columns: new[] { "GroupSubjectId", "GroupId", "SubjectId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 9, 3 },
                    { 4, 2, 4 },
                    { 5, 3, 5 },
                    { 7, 1, 10 },
                    { 8, 3, 4 },
                    { 9, 3, 2 },
                    { 10, 2, 9 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupSubjects");
        }
    }
}
