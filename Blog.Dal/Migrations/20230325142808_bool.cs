using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.Dal.Migrations
{
    public partial class @bool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "debd927f-41b9-4c7b-ba90-ef9a396f6f3f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "52c1e246-3f63-4c27-878d-e4b5b5a6f1f2", "4c7d925a-65d6-43eb-899e-a3788d295a13", "Member", "MEMBER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "52c1e246-3f63-4c27-878d-e4b5b5a6f1f2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "debd927f-41b9-4c7b-ba90-ef9a396f6f3f", "b509c1fb-d20a-4059-9c40-a6aa1ed3a0f5", "Member", "MEMBER" });
        }
    }
}
