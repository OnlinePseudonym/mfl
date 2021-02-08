using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace MFL.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RotoworldId = table.Column<int>(nullable: false),
                    StatsId = table.Column<int>(nullable: false),
                    StatsGlobalId = table.Column<int>(nullable: false),
                    SportsdataId = table.Column<byte[]>(nullable: false),
                    RotowireId = table.Column<int>(nullable: false),
                    CbsId = table.Column<int>(nullable: false),
                    NflId = table.Column<string>(maxLength: 50, nullable: true),
                    EspnId = table.Column<int>(nullable: false),
                    FleaflickerId = table.Column<int>(nullable: false),
                    DraftYear = table.Column<int>(nullable: false),
                    DraftPick = table.Column<int>(nullable: false),
                    DraftRound = table.Column<int>(nullable: false),
                    DraftTeam = table.Column<string>(maxLength: 50, nullable: true),
                    Jersey = table.Column<int>(nullable: false),
                    FullName = table.Column<string>(maxLength: 110, nullable: true),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Height = table.Column<int>(nullable: false),
                    Weight = table.Column<int>(nullable: false),
                    Birthdate = table.Column<DateTime>(nullable: false),
                    Position = table.Column<string>(maxLength: 10, nullable: true),
                    Team = table.Column<string>(maxLength: 50, nullable: true),
                    Status = table.Column<string>(maxLength: 50, nullable: true),
                    TeamId = table.Column<int>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WaiverTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    LeagueId = table.Column<int>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    Drops = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaiverTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WaiverTransactions_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WaiverTransactions_PlayerId",
                table: "WaiverTransactions",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WaiverTransactions");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
