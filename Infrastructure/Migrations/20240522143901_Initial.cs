using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Contents",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contents", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Experiences",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Company = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Text = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiences", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Hobbies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Icon = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hobbies", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Level = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Contents",
                columns: new[] { "Id", "Key", "Value" },
                values: new object[,]
                {
                    { 1L, "PROFILE_FULLNAME", "Edit the value in the administrator panel." },
                    { 2L, "PROFILE_EMPLOYMENT", "Edit the value in the administrator panel." },
                    { 3L, "ABOUT_TEXT", "Edit the value in the administrator panel." },
                    { 4L, "ABOUT_BIRTH_DATE", "25/10/2004" },
                    { 5L, "ABOUT_EMAIL", "Edit the value in the administrator panel." },
                    { 6L, "ABOUT_EMAIL_LINK", "mailto:maxmuster@example.com" },
                    { 7L, "ABOUT_PHONE", "Edit the value in the administrator panel." },
                    { 8L, "ABOUT_PHONE_LINK", "tel:0123456789" },
                    { 9L, "ABOUT_GITHUB", "Edit the value in the administrator panel." },
                    { 10L, "ABOUT_GITHUB_LINK", "https://github.com/maxmuster" },
                    { 11L, "ABOUT_RESIDENCE", "Edit the value in the administrator panel." }
                });

            migrationBuilder.InsertData(
                table: "Experiences",
                columns: new[] { "Id", "Company", "EndDate", "StartDate", "Text" },
                values: new object[,]
                {
                    { 1L, "Example", null, new DateOnly(2020, 8, 1), "Edit the value in the administrator panel." },
                    { 2L, "Elpmaxe", new DateOnly(2020, 7, 31), new DateOnly(2016, 1, 1), "Edit the value in the administrator panel." }
                });

            migrationBuilder.InsertData(
                table: "Hobbies",
                columns: new[] { "Id", "Icon", "Text" },
                values: new object[,]
                {
                    { 1L, "code", "Edit the value in the administrator panel." },
                    { 2L, "code", "Edit the value in the administrator panel." }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Level", "Name" },
                values: new object[] { 1L, 70, "Edit the value in the administrator panel." });

            migrationBuilder.CreateIndex(
                name: "IX_Contents_Key",
                table: "Contents",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_Name",
                table: "Skills",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contents");

            migrationBuilder.DropTable(
                name: "Experiences");

            migrationBuilder.DropTable(
                name: "Hobbies");

            migrationBuilder.DropTable(
                name: "Skills");
        }
    }
}
