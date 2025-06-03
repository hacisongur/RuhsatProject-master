using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RuhsaProject.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddRuhsatImzaRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "f8063d13-6626-4025-8eb8-99ed2cf23750");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "e490c738-80a8-4bf0-921e-9d21712b8bfe");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "24048f42-f894-4edf-b1b6-996d41a8c2cd", "AQAAAAIAAYagAAAAECVcAzL75HFumd7t+cb9b4Mo4hZANh1nhJrH8kNn9fu9njKW80afEFt8EQ9y5F91mg==", "7703fdb7-bf9e-4536-a15e-f120205b3e97" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6bf014b3-44b4-4e2b-8ee8-ac294a4a6487", "AQAAAAIAAYagAAAAENgYJwzQ/G62btFEZAiA9W6bz+zolSNIjgX60Vwlp5pp6s7txw4BlFwAF4d0Drbllg==", "8bab0d95-c1d6-48b3-97cb-94e69d7ff331" });
        }
    }
}
