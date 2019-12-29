using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Farm.Animals.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Animal");

            migrationBuilder.CreateSequence(
                name: "Animalseq",
                schema: "Animal",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "Animal",
                schema: "Animal",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SequenceHiLo),
                    AnimalType = table.Column<int>(nullable: false),
                    ShortName = table.Column<string>(maxLength: 20, nullable: false),
                    LongName = table.Column<string>(maxLength: 200, nullable: false),
                    BirthDay = table.Column<DateTime>(nullable: true),
                    EntryDate = table.Column<DateTime>(nullable: true),
                    LeaveDate = table.Column<DateTime>(nullable: true),
                    DeathDate = table.Column<DateTime>(nullable: true),
                    Gender = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animal", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Animal",
                schema: "Animal");

            migrationBuilder.DropSequence(
                name: "Animalseq",
                schema: "Animal");
        }
    }
}
