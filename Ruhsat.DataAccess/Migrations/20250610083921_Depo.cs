using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RuhsaProject.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Depo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ruhsatlar_RuhsatImzalar_RuhsatImzaId",
                table: "Ruhsatlar");

            migrationBuilder.DropTable(
                name: "RuhsatImzalar");

            migrationBuilder.DropIndex(
                name: "IX_Ruhsatlar_RuhsatImzaId",
                table: "Ruhsatlar");

            migrationBuilder.DropColumn(
                name: "RuhsatImzaId",
                table: "Ruhsatlar");

            migrationBuilder.CreateTable(
                name: "Depolar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RuhsatSinifiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Depolar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Depolar_RuhsatSiniflari_RuhsatSinifiId",
                        column: x => x.RuhsatSinifiId,
                        principalTable: "RuhsatSiniflari",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "c55e6588-e2cd-4e93-926c-786d411a6381");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "9080883e-71fb-4414-b031-9042a1acbed8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c9115f2e-4954-4fc0-948b-ed3776a1eb98", "AQAAAAIAAYagAAAAEExX9VdeTtvZge3voCxFU1ePlGSWxQXqNqPBnY9KQrD+4DnovasbwDnGmJitTIeFqQ==", "2925b81f-65f4-42e0-83f0-2e58f518e49b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "603ee846-5f41-488f-9f71-1b7f9a215546", "AQAAAAIAAYagAAAAENumyySS9xuUpiJWQkWlv4dklt/X4MHBJ/cNk8e8OzeaPOr6DtF1jbMzWfFpdNxhVQ==", "7a33989d-bf44-4ad0-a40d-a5d390696884" });

            migrationBuilder.CreateIndex(
                name: "IX_Depolar_RuhsatSinifiId",
                table: "Depolar",
                column: "RuhsatSinifiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Depolar");

            migrationBuilder.AddColumn<int>(
                name: "RuhsatImzaId",
                table: "Ruhsatlar",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RuhsatImzalar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdSoyad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Unvan1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Unvan2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RuhsatImzalar", x => x.Id);
                });

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
    }
}
