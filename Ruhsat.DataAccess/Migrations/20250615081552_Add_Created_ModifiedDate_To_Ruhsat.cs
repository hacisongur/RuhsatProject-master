using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RuhsaProject.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Add_Created_ModifiedDate_To_Ruhsat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Ruhsatlar",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Ruhsatlar",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "41a45c0d-42a5-4427-8f59-181f1cdd0a9a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "6c180dfa-add7-4f30-8ce2-24cd24c17c2f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "092e3f00-f546-42cf-9651-0d998f9de53b", "AQAAAAIAAYagAAAAEEi91Wvn0bUBI3HupSjxJ2jbYLTAslJbo3Am+UnRlpK9yZ/tZSL2RhpWD2et1pr0OQ==", "a4d4ca75-c2ee-41fd-b8a7-8235443ad443" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f88d4c4e-1591-45a8-bbb5-00b8104ae77f", "AQAAAAIAAYagAAAAELnzZY7NrhKge4R6VUREsaSGPdAw4qxxuUYbPPzmVSBFwlpXg3Bae5UGv9s7lLr4FQ==", "037bbfd8-af47-48be-ae91-17cb47e497a5" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Ruhsatlar");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Ruhsatlar");

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
    }
}
