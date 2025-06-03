using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RuhsaProject.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddRuhsatImzaRelation1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ruhsatlar_RuhsatImzalar_RuhsatImzaId",
                table: "Ruhsatlar");

            migrationBuilder.DropIndex(
                name: "IX_Ruhsatlar_RuhsatImzaId",
                table: "Ruhsatlar");

            migrationBuilder.AlterColumn<int>(
                name: "RuhsatImzaId",
                table: "Ruhsatlar",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "488c1517-71ba-41fa-a8e6-aedbd3c7f31e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "d148a169-8f14-4feb-a4af-4e4c863b6387");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "72ed6564-988d-4573-82aa-b90903539c8e", "AQAAAAIAAYagAAAAEKV9RAn09poEUMiAymlldTqvg8Z5GiWC0drTn5BetTDXjSj7LEmTiqYdPjryMAYWgQ==", "2194e669-e8ce-4dff-a891-adb4840bdd50" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f86daef0-f77d-4575-9af9-2f61d0e4c1e2", "AQAAAAIAAYagAAAAEMWUxOuEXCL7KBw4W2otkpxBuWPDgYYOtSBfQ/XCbZ/FD21UZlTabJP9qF+Jtrk/gQ==", "7339b4d9-4cb2-4e54-aca3-34e161d76f0c" });

            migrationBuilder.CreateIndex(
                name: "IX_Ruhsatlar_RuhsatImzaId",
                table: "Ruhsatlar",
                column: "RuhsatImzaId",
                unique: true,
                filter: "[RuhsatImzaId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Ruhsatlar_RuhsatImzalar_RuhsatImzaId",
                table: "Ruhsatlar",
                column: "RuhsatImzaId",
                principalTable: "RuhsatImzalar",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ruhsatlar_RuhsatImzalar_RuhsatImzaId",
                table: "Ruhsatlar");

            migrationBuilder.DropIndex(
                name: "IX_Ruhsatlar_RuhsatImzaId",
                table: "Ruhsatlar");

            migrationBuilder.AlterColumn<int>(
                name: "RuhsatImzaId",
                table: "Ruhsatlar",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "8fa92ead-73f3-4b89-b88f-779e57dee023");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "2c5cd2ba-385a-4965-bea9-d7b9ca6162bc");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7d7b98b4-5722-4f57-978b-be25b431475a", "AQAAAAIAAYagAAAAEMcDwfcvDo+XTOIBHY1XKAWA5/ZkpY90Wz1BOuLNCNYJdLcB47T7i+heKofyS7jZJQ==", "eec81017-8de4-4ac8-8c01-3271a2ce92f4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f8f7c447-f87f-4e48-a554-6dd21c9dce6a", "AQAAAAIAAYagAAAAEIAe4safz99rnpdjJSDnrMu09d0KIpXOKHKnqKEvUAxPZyTT3fs2rvjjvPHN969PqQ==", "7a95e393-b4ef-4080-937a-98bb009620d5" });

            migrationBuilder.CreateIndex(
                name: "IX_Ruhsatlar_RuhsatImzaId",
                table: "Ruhsatlar",
                column: "RuhsatImzaId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ruhsatlar_RuhsatImzalar_RuhsatImzaId",
                table: "Ruhsatlar",
                column: "RuhsatImzaId",
                principalTable: "RuhsatImzalar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
