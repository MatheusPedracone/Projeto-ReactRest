using Microsoft.EntityFrameworkCore.Migrations;

namespace Chimera_v2.Migrations
{
    public partial class secondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "Clients");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "Clients",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
