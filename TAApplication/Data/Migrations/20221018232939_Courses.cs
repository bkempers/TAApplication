using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TAApplication.Data.Migrations
{
    public partial class Courses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Uid",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Uid",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Application",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    currentDegree = table.Column<int>(type: "int", nullable: false),
                    currentDepartment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UofU_GPA = table.Column<float>(type: "real", nullable: false),
                    desiredHours = table.Column<int>(type: "int", nullable: false),
                    availableBefore = table.Column<bool>(type: "bit", nullable: false),
                    numSemesters = table.Column<int>(type: "int", nullable: false),
                    personalStatement = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: true),
                    transferSchool = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    linkedInURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    resumeFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Application_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    semesterOffered = table.Column<int>(type: "int", nullable: false),
                    yearOffered = table.Column<int>(type: "int", nullable: false),
                    courseTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    courseDepartment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    courseNumber = table.Column<int>(type: "int", nullable: false),
                    courseSection = table.Column<int>(type: "int", nullable: false),
                    courseDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    profUNID = table.Column<int>(type: "int", nullable: false),
                    profName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    daysOffered = table.Column<DateTime>(type: "datetime2", nullable: false),
                    timeOffered = table.Column<DateTime>(type: "datetime2", nullable: false),
                    courseLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    creditHours = table.Column<int>(type: "int", nullable: false),
                    courseEnrollment = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Course_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Uid",
                table: "AspNetUsers",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Application_UserId",
                table: "Application",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Course_UserId",
                table: "Course",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Application");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Uid",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Uid",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Uid",
                table: "AspNetUsers",
                column: "Uid",
                unique: true,
                filter: "[Uid] IS NOT NULL");
        }
    }
}
