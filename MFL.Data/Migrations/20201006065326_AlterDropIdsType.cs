using Microsoft.EntityFrameworkCore.Migrations;

namespace MFL.Data.Migrations
{
    public partial class AlterDropIdsType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Drops",
                table: "WaiverTransactions");

            migrationBuilder.AddColumn<string>(
                name: "DropIds",
                table: "WaiverTransactions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DropIds",
                table: "WaiverTransactions");

            migrationBuilder.AddColumn<string>(
                name: "Drops",
                table: "WaiverTransactions",
                type: "text",
                nullable: true);
        }
    }
}
