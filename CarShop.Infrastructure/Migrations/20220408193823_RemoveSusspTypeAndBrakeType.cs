using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarShop.Infrastructure.Data.Migrations
{
    public partial class RemoveSusspTypeAndBrakeType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_BrakesTypes_BreaksTypeId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_SusspensionTypes_SusspensionTypeId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "BrakesTypes");

            migrationBuilder.DropTable(
                name: "SusspensionTypes");

            migrationBuilder.DropIndex(
                name: "IX_Cars_BreaksTypeId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_SusspensionTypeId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "BreaksTypeId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "SusspensionTypeId",
                table: "Cars");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BreaksTypeId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SusspensionTypeId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BrakesTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrakesTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SusspensionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SusspensionTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_BreaksTypeId",
                table: "Cars",
                column: "BreaksTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_SusspensionTypeId",
                table: "Cars",
                column: "SusspensionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_BrakesTypes_BreaksTypeId",
                table: "Cars",
                column: "BreaksTypeId",
                principalTable: "BrakesTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_SusspensionTypes_SusspensionTypeId",
                table: "Cars",
                column: "SusspensionTypeId",
                principalTable: "SusspensionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
