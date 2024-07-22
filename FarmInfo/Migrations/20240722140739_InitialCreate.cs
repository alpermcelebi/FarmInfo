using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmInfo.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthRecords_Cows_CowId",
                table: "HealthRecords");

            migrationBuilder.RenameColumn(
                name: "CowId",
                table: "HealthRecords",
                newName: "cowId");

            migrationBuilder.RenameIndex(
                name: "IX_HealthRecords_CowId",
                table: "HealthRecords",
                newName: "IX_HealthRecords_cowId");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Farmers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordSalt",
                table: "Farmers",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordHash",
                table: "Farmers",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthRecords_Cows_cowId",
                table: "HealthRecords",
                column: "cowId",
                principalTable: "Cows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthRecords_Cows_cowId",
                table: "HealthRecords");

            migrationBuilder.RenameColumn(
                name: "cowId",
                table: "HealthRecords",
                newName: "CowId");

            migrationBuilder.RenameIndex(
                name: "IX_HealthRecords_cowId",
                table: "HealthRecords",
                newName: "IX_HealthRecords_CowId");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Farmers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordSalt",
                table: "Farmers",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordHash",
                table: "Farmers",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HealthRecords_Cows_CowId",
                table: "HealthRecords",
                column: "CowId",
                principalTable: "Cows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
