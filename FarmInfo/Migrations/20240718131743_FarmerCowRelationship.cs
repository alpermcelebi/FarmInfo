using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmInfo.Migrations
{
    /// <inheritdoc />
    public partial class FarmerCowRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FarmerId",
                table: "Cows",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cows_FarmerId",
                table: "Cows",
                column: "FarmerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cows_Farmers_FarmerId",
                table: "Cows",
                column: "FarmerId",
                principalTable: "Farmers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cows_Farmers_FarmerId",
                table: "Cows");

            migrationBuilder.DropIndex(
                name: "IX_Cows_FarmerId",
                table: "Cows");

            migrationBuilder.DropColumn(
                name: "FarmerId",
                table: "Cows");
        }
    }
}
