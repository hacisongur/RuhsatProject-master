using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RuhsaProject.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ApplyCascadeDeleteForDepoBilgileriAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "bae8919b-a488-4873-b1e7-fae64e4c46a3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "6339cca6-e144-4a36-9977-2613c39d2da4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "12e66e68-7129-4b42-bcc6-15840a4afb2e", "AQAAAAIAAYagAAAAEBWaxZs4szNDhhbFXLRpEWaPJQv9XN7d+wyPlZbyFUgSNXLlUPkAeMidUhDDaYBkow==", "3f16e555-d7e4-4fa8-bb76-453a710932a9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7f7a969a-78a0-49c9-8c76-ca7808ee8915", "AQAAAAIAAYagAAAAEFXYZ2MMw2uOP5NzCMlBsTdKMyTZAA6G8hZyohCeyVrVhMzi5cbhw8oqwO2xABpXCg==", "e3402bc6-5d8d-417f-aef9-4312a17842e9" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
