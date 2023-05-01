using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.Dal.Migrations
{
    public partial class create_db_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsedPasswords_AspNetUsers_AppUserID",
                table: "UsedPasswords");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "27dfa9e3-5f30-42b7-8923-f7c66b1196c9");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "3a45fa99-bf3e-4bdd-a94d-f9653ad979be", "a22347d3-8704-424f-a176-d5a6c4629f72" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a22347d3-8704-424f-a176-d5a6c4629f72");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3a45fa99-bf3e-4bdd-a94d-f9653ad979be");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f20744a0-0e77-463c-ae85-6b50f6a1df1c", "609c411a-6455-4762-90f9-3400f5aaa8cb", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "da0c2544-2a84-4505-834c-823358b833e1", "88e3bef0-5ae2-4714-9203-04c4a7be931b", "member", "MEMBER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedDate", "Email", "EmailConfirmed", "FirstName", "ImagePath", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Statu", "TwoFactorEnabled", "UserName" },
                values: new object[] { "102485ef-40d6-4d4c-95b6-295011e265d4", 0, "d921fa03-d7ad-4cee-984c-23a8e05af793", new DateTime(2023, 4, 30, 16, 24, 49, 236, DateTimeKind.Local).AddTicks(7539), "admin@gmail.com", true, "admin", null, "admin", false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "admin123", "AQAAAAEAACcQAAAAEMgaqnH6gXcZiz08Z78DJWkAL1g9wygvyxacExRgtpxrdR6j5FHofIZiptSeoH5/rQ==", null, false, "a00b0114-32a4-4569-805a-8b822d5b32c7", 1, false, "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "102485ef-40d6-4d4c-95b6-295011e265d4", "f20744a0-0e77-463c-ae85-6b50f6a1df1c" });

            migrationBuilder.AddForeignKey(
                name: "FK_UsedPasswords_AspNetUsers_AppUserID",
                table: "UsedPasswords",
                column: "AppUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsedPasswords_AspNetUsers_AppUserID",
                table: "UsedPasswords");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "da0c2544-2a84-4505-834c-823358b833e1");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "102485ef-40d6-4d4c-95b6-295011e265d4", "f20744a0-0e77-463c-ae85-6b50f6a1df1c" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f20744a0-0e77-463c-ae85-6b50f6a1df1c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "102485ef-40d6-4d4c-95b6-295011e265d4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a22347d3-8704-424f-a176-d5a6c4629f72", "7afe92e9-ae7b-4b23-924d-4bb268901de2", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "27dfa9e3-5f30-42b7-8923-f7c66b1196c9", "54baacfd-87fc-4e38-85e5-db450ed82af6", "member", "MEMBER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedDate", "Email", "EmailConfirmed", "FirstName", "ImagePath", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Statu", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3a45fa99-bf3e-4bdd-a94d-f9653ad979be", 0, "ce00bac0-08ca-4cec-bc8b-3e9556f0fc16", new DateTime(2023, 4, 30, 16, 23, 36, 353, DateTimeKind.Local).AddTicks(2448), "admin@gmail.com", true, "admin", null, "admin", false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "admin123", "AQAAAAEAACcQAAAAEEp6AjnzU66MWiG+szFzFbcaojIr8G+lyhXe5xfrgyAdX6rL4aKsUuz3IIwRApHwzw==", null, false, "cec3f581-4f25-4120-b90d-289a367ab559", 1, false, "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "3a45fa99-bf3e-4bdd-a94d-f9653ad979be", "a22347d3-8704-424f-a176-d5a6c4629f72" });

            migrationBuilder.AddForeignKey(
                name: "FK_UsedPasswords_AspNetUsers_AppUserID",
                table: "UsedPasswords",
                column: "AppUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
