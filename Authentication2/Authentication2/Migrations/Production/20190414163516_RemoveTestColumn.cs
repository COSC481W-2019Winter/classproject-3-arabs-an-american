using Microsoft.EntityFrameworkCore.Migrations;

namespace Authentication2.Migrations.Production
{
    public partial class RemoveTestColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestColumn",
                table: "Addresses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TestColumn",
                table: "Addresses",
                nullable: true);
        }
    }
}
