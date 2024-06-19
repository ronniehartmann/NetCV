using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSettingShowVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HideFooter",
                table: "Settings",
                newName: "ShowVersion");

            migrationBuilder.AddColumn<bool>(
                name: "ShowFooter",
                table: "Settings",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ShowFooter", "ShowVersion" },
                values: new object[] { false, true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowFooter",
                table: "Settings");

            migrationBuilder.RenameColumn(
                name: "ShowVersion",
                table: "Settings",
                newName: "HideFooter");

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 1L,
                column: "HideFooter",
                value: false);
        }
    }
}
