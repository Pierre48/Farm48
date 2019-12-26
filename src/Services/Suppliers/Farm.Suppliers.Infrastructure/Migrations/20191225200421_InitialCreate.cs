using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Farm.Suppliers.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "supplier");

            migrationBuilder.CreateSequence(
                name: "supplierseq",
                schema: "supplier",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "supplier",
                schema: "supplier",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SequenceHiLo),
                    ShortName = table.Column<string>(maxLength: 20, nullable: false),
                    LongName = table.Column<string>(maxLength: 200, nullable: false),
                    AddressLine1 = table.Column<string>(maxLength: 200, nullable: false),
                    ZipCode = table.Column<string>(maxLength: 8, nullable: false),
                    City = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_supplier", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "supplier",
                schema: "supplier");

            migrationBuilder.DropSequence(
                name: "supplierseq",
                schema: "supplier");
        }
    }
}
