using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmInfo.Migrations
{
    /// <inheritdoc />
    public partial class cowid2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthRecords_Cows_cowId",
                table: "HealthRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_MilkProductionRecords_Cows_CowId",
                table: "MilkProductionRecords");

            migrationBuilder.RenameColumn(
                name: "cowId",
                table: "HealthRecords",
                newName: "CowId");

            migrationBuilder.RenameIndex(
                name: "IX_HealthRecords_cowId",
                table: "HealthRecords",
                newName: "IX_HealthRecords_CowId");

            migrationBuilder.AlterColumn<int>(
                name: "CowId",
                table: "MilkProductionRecords",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CowId",
                table: "HealthRecords",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthRecords_Cows_CowId",
                table: "HealthRecords",
                column: "CowId",
                principalTable: "Cows",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MilkProductionRecords_Cows_CowId",
                table: "MilkProductionRecords",
                column: "CowId",
                principalTable: "Cows",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthRecords_Cows_CowId",
                table: "HealthRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_MilkProductionRecords_Cows_CowId",
                table: "MilkProductionRecords");

            migrationBuilder.RenameColumn(
                name: "CowId",
                table: "HealthRecords",
                newName: "cowId");

            migrationBuilder.RenameIndex(
                name: "IX_HealthRecords_CowId",
                table: "HealthRecords",
                newName: "IX_HealthRecords_cowId");

            migrationBuilder.AlterColumn<int>(
                name: "CowId",
                table: "MilkProductionRecords",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "cowId",
                table: "HealthRecords",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HealthRecords_Cows_cowId",
                table: "HealthRecords",
                column: "cowId",
                principalTable: "Cows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MilkProductionRecords_Cows_CowId",
                table: "MilkProductionRecords",
                column: "CowId",
                principalTable: "Cows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
