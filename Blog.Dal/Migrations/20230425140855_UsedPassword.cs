using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.Dal.Migrations
{
    public partial class UsedPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "UsedPasswords",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Statu = table.Column<int>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    AppUserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsedPasswords", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UsedPasswords_AspNetUsers_AppUserID",
                        column: x => x.AppUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ac2099d2-009d-4c4f-9ddd-d2c7a11415cd", "338bdef3-aa5e-4b17-9792-5693f37997ba", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3d575ed8-e02d-4815-ae0f-9104a261ce7b", "dbf2ab9c-c470-4c13-9a83-7295385b4b79", "member", "MEMBER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedDate", "Email", "EmailConfirmed", "FirstName", "ImagePath", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Statu", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c45c9e1b-7df9-4ede-862e-3c5f3cc86739", 0, "8c194fc3-3a15-41db-8b60-610fbd350691", new DateTime(2023, 4, 25, 17, 8, 55, 493, DateTimeKind.Local).AddTicks(3189), "admin@gmail.com", true, "admin", null, "admin", false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", null, "AQAAAAEAACcQAAAAEFc3Ju2qv53gcXk/56nnK2086b28r2qd61rZ8yuaKL6VSFYiUs1O5OBPZQ0vIEtMOA==", null, false, "2d0644b7-ee48-4e05-bd19-956110d817f1", 1, false, "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "c45c9e1b-7df9-4ede-862e-3c5f3cc86739", "ac2099d2-009d-4c4f-9ddd-d2c7a11415cd" });

            migrationBuilder.CreateIndex(
                name: "IX_UsedPasswords_AppUserID",
                table: "UsedPasswords",
                column: "AppUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsedPasswords");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d575ed8-e02d-4815-ae0f-9104a261ce7b");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "c45c9e1b-7df9-4ede-862e-3c5f3cc86739", "ac2099d2-009d-4c4f-9ddd-d2c7a11415cd" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ac2099d2-009d-4c4f-9ddd-d2c7a11415cd");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c45c9e1b-7df9-4ede-862e-3c5f3cc86739");

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
    }
}
