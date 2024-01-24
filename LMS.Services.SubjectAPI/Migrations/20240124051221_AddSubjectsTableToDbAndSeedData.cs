using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LMS.Services.SubjectAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddSubjectsTableToDbAndSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Credits = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectId);
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "SubjectId", "Code", "Credits", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "CS101", 3, "An introductory course covering the fundamentals of computer science.", "Introduction to Computer Science" },
                    { 2, "WD200", 4, "Basic concepts and skills for web development.", "Web Development Basics" },
                    { 3, "DBMS301", 3, "Study of database systems and SQL.", "Database Management Systems" },
                    { 4, "ALGO401", 4, "Advanced course on algorithm design and analysis.", "Algorithm Design" },
                    { 5, "SEC101", 3, "Introduction to cybersecurity principles and practices.", "Cybersecurity Fundamentals" },
                    { 6, "DS201", 3, "Study of fundamental data structures.", "Data Structures" },
                    { 7, "SE401", 4, "Principles and practices of software engineering.", "Software Engineering" },
                    { 8, "AI501", 4, "Exploration of artificial intelligence concepts and techniques.", "Artificial Intelligence" },
                    { 9, "MAD301", 3, "Development of mobile applications for various platforms.", "Mobile App Development" },
                    { 10, "NETSEC401", 4, "Security aspects of computer networks.", "Network Security" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subjects");
        }
    }
}
