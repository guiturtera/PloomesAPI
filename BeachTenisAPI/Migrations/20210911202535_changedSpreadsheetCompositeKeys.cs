using Microsoft.EntityFrameworkCore.Migrations;

namespace BeachTenisAPI.Migrations
{
    public partial class changedSpreadsheetCompositeKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Spreadsheets",
                table: "Spreadsheets");

            migrationBuilder.RenameColumn(
                name: "DayAndHour",
                table: "Spreadsheets",
                newName: "Day");

            migrationBuilder.AlterColumn<string>(
                name: "Coach",
                table: "Spreadsheets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Spreadsheets",
                table: "Spreadsheets",
                columns: new[] { "User", "Day" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Spreadsheets",
                table: "Spreadsheets");

            migrationBuilder.RenameColumn(
                name: "Day",
                table: "Spreadsheets",
                newName: "DayAndHour");

            migrationBuilder.AlterColumn<string>(
                name: "Coach",
                table: "Spreadsheets",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Spreadsheets",
                table: "Spreadsheets",
                columns: new[] { "User", "Coach" });
        }
    }
}
