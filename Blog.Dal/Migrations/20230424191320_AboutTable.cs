using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.Dal.Migrations
{
    public partial class AboutTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "530e7002-ebf5-4396-9b4e-aa25d5192501");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "e833c376-303f-4817-9751-d4088851b68f", "d621ebd6-b551-4325-863f-9521616904a8" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d621ebd6-b551-4325-863f-9521616904a8");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e833c376-303f-4817-9751-d4088851b68f");

            migrationBuilder.CreateTable(
                name: "Abouts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Statu = table.Column<int>(nullable: false),
                    AdSoyad = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Konu = table.Column<string>(nullable: true),
                    Aciklama = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abouts", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7f4202af-41bf-4a2b-baeb-f29d713ab324", "3f9a48b6-037e-4725-9a9d-e26ef9550f80", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d8ee8be6-0146-4ada-b7cc-6b6334f74560", "43fcf42e-ac38-46ba-bd5a-28da2600b310", "member", "MEMBER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedDate", "Email", "EmailConfirmed", "FirstName", "ImagePath", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Statu", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0afc43e1-6e2d-49ba-9f2a-9133db136ef3", 0, "c4498462-cab2-4e12-a25b-b2fde5d6497c", new DateTime(2023, 4, 24, 22, 13, 20, 354, DateTimeKind.Local).AddTicks(4294), "admin@gmail.com", true, "admin", null, "admin", false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", null, "AQAAAAEAACcQAAAAEOqUbCVEGoMVMGQpXwBoHf2x4k0/y5t1CobtgZW6MFaQjy9D1bOCPgKQTflKoY0daA==", null, false, "9da294d1-aef4-49cf-b5a0-7eb6da2db08a", 1, false, "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "0afc43e1-6e2d-49ba-9f2a-9133db136ef3", "7f4202af-41bf-4a2b-baeb-f29d713ab324" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abouts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d8ee8be6-0146-4ada-b7cc-6b6334f74560");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "0afc43e1-6e2d-49ba-9f2a-9133db136ef3", "7f4202af-41bf-4a2b-baeb-f29d713ab324" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f4202af-41bf-4a2b-baeb-f29d713ab324");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0afc43e1-6e2d-49ba-9f2a-9133db136ef3");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d621ebd6-b551-4325-863f-9521616904a8", "f3daccfe-f5c6-4c0c-b4a2-d37f64001715", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "530e7002-ebf5-4396-9b4e-aa25d5192501", "d4182685-3284-4951-ac75-fe9420d3fabc", "member", "MEMBER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedDate", "Email", "EmailConfirmed", "FirstName", "ImagePath", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Statu", "TwoFactorEnabled", "UserName" },
                values: new object[] { "e833c376-303f-4817-9751-d4088851b68f", 0, "17dc4e11-2112-4120-8573-4487dc075238", new DateTime(2023, 4, 20, 17, 47, 14, 42, DateTimeKind.Local).AddTicks(8270), "admin@gmail.com", true, "admin", null, "admin", false, null, null, "ADMIN@GMAIL.COM", null, "AQAAAAEAACcQAAAAEPJbJSws4kpKe3OZYusuZZEvg/C40AL9mdqJJcRyKTGBNW7fiekrr81+z95ci5K48g==", null, false, "c444a0fa-de8d-4c5d-baa3-90b51c04bafc", 1, false, "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "e833c376-303f-4817-9751-d4088851b68f", "d621ebd6-b551-4325-863f-9521616904a8" });
        }
    }
}
