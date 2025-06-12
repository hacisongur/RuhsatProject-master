using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RuhsaProject.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Add_Depobilgi_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DepoBilgileri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RuhsatId = table.Column<int>(type: "int", nullable: false),
                    DepoId = table.Column<int>(type: "int", nullable: false),
                    DepoAdi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Bilgi = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepoBilgileri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepoBilgileri_Depolar_DepoId",
                        column: x => x.DepoId,
                        principalTable: "Depolar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DepoBilgileri_Ruhsatlar_RuhsatId",
                        column: x => x.RuhsatId,
                        principalTable: "Ruhsatlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "a0e12a3c-5361-41a4-b959-d2fba6d9b112");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "47a6ed49-8fa3-4b13-9fb2-b48ba2b4bf56");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "26177172-987a-4738-b25d-852a79c8b800", "AQAAAAIAAYagAAAAEAGZQeWAlWfP3RUlINaZuyFhfJq5Vnco4nBw0VivFw/h5Q1nm+PXSasnwknFc2z1NQ==", "a71f97a4-e887-4dd2-9ca4-956297504ce6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f6028872-9859-4fb4-9d08-c0c819564122", "AQAAAAIAAYagAAAAEEjrmOP8XK3tbCPx+YzQuFBa6NbOhxbXFnQaOuXFm3FuePnPm8dvDnUCiDsZmhmpBw==", "f94d66fa-8275-48c0-8254-2a22de509ace" });

            migrationBuilder.CreateIndex(
                name: "IX_DepoBilgileri_DepoId",
                table: "DepoBilgileri",
                column: "DepoId");

            migrationBuilder.CreateIndex(
                name: "IX_DepoBilgileri_RuhsatId",
                table: "DepoBilgileri",
                column: "RuhsatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepoBilgileri");

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
        }
    }
}
