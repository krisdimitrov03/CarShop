using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarShop.Infrastructure.Data.Migrations
{
    public partial class WheelsTypeRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_WheelsTypes_WheelsTypeId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "WheelsTypes");

            migrationBuilder.DropIndex(
                name: "IX_Cars_WheelsTypeId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "WheelsTypeId",
                table: "Cars");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WheelsTypeId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "WheelsTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WheelsTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_WheelsTypeId",
                table: "Cars",
                column: "WheelsTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_WheelsTypes_WheelsTypeId",
                table: "Cars",
                column: "WheelsTypeId",
                principalTable: "WheelsTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
