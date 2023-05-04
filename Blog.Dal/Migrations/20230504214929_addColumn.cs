using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.Dal.Migrations
{
    public partial class addColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "30542669-1f98-4104-ad0a-dc0657dae34d");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "61c64e4c-d050-4fb3-a2df-6da2b316bfa9", "1c1ceef4-4ce3-479c-a221-c8668c4a8fe1" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1c1ceef4-4ce3-479c-a221-c8668c4a8fe1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "61c64e4c-d050-4fb3-a2df-6da2b316bfa9");

            migrationBuilder.AddColumn<string>(
                name: "Deneme",
                table: "Comments",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bc58d50f-5fd3-4419-a5b0-22c7c2cdb2ef", "4e6a1b4c-2deb-41ac-84fd-e1c3539148fc", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "221b04d4-b906-44cf-8442-542d1b739bb9", "d289f318-b87c-43ae-86ae-73e985256d1f", "member", "MEMBER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedDate", "Email", "EmailConfirmed", "FirstName", "ImagePath", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Statu", "TwoFactorEnabled", "UserName" },
                values: new object[] { "5f4189e0-303d-4b30-9429-8f40e1478583", 0, "3ea532f3-e130-4785-9ba0-64cc606b3699", new DateTime(2023, 5, 5, 0, 49, 29, 344, DateTimeKind.Local).AddTicks(6694), "admin@gmail.com", true, "admin", null, "admin", false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "admin123", "AQAAAAEAACcQAAAAEEEfscKoQ/iETYPpZsrMp3kiiMDsYnZK8sS0tZxDujYIcn3KT5Ll+nTL73HmTxc0cg==", null, false, "a1add38d-a2a8-46a8-a233-28e2536a7686", 1, false, "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "5f4189e0-303d-4b30-9429-8f40e1478583", "bc58d50f-5fd3-4419-a5b0-22c7c2cdb2ef" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "221b04d4-b906-44cf-8442-542d1b739bb9");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "5f4189e0-303d-4b30-9429-8f40e1478583", "bc58d50f-5fd3-4419-a5b0-22c7c2cdb2ef" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bc58d50f-5fd3-4419-a5b0-22c7c2cdb2ef");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f4189e0-303d-4b30-9429-8f40e1478583");

            migrationBuilder.DropColumn(
                name: "Deneme",
                table: "Comments");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1c1ceef4-4ce3-479c-a221-c8668c4a8fe1", "c1eb1e93-9dbe-4dc5-8d5d-ed90a7b31419", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "30542669-1f98-4104-ad0a-dc0657dae34d", "295d530e-8239-4652-b827-a03da8052fa9", "member", "MEMBER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedDate", "Email", "EmailConfirmed", "FirstName", "ImagePath", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Statu", "TwoFactorEnabled", "UserName" },
                values: new object[] { "61c64e4c-d050-4fb3-a2df-6da2b316bfa9", 0, "7057199e-b3f4-42a8-a20c-8f985b9704dd", new DateTime(2023, 5, 4, 23, 32, 51, 604, DateTimeKind.Local).AddTicks(4392), "admin@gmail.com", true, "admin", null, "admin", false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "admin123", "AQAAAAEAACcQAAAAEAAxhh/OCY3f+m+e3CA8JOYH5HEPVSAtlDoxbe/WmaBtXeFNGyIPuvESI7PiafmwEA==", null, false, "0e7aea3f-729b-4dc7-8988-af050b846d3d", 1, false, "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "61c64e4c-d050-4fb3-a2df-6da2b316bfa9", "1c1ceef4-4ce3-479c-a221-c8668c4a8fe1" });
        }
    }
}
