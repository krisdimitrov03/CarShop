using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarShop.Infrastructure.Data.Migrations
{
    public partial class x : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExtraId",
                table: "OrdersExtras",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Extras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Extras", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrdersExtras_ExtraId",
                table: "OrdersExtras",
                column: "ExtraId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersExtras_Extras_ExtraId",
                table: "OrdersExtras",
                column: "ExtraId",
                principalTable: "Extras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdersExtras_Extras_ExtraId",
                table: "OrdersExtras");

            migrationBuilder.DropTable(
                name: "Extras");

            migrationBuilder.DropIndex(
                name: "IX_OrdersExtras_ExtraId",
                table: "OrdersExtras");

            migrationBuilder.DropColumn(
                name: "ExtraId",
                table: "OrdersExtras");
        }
    }
}
