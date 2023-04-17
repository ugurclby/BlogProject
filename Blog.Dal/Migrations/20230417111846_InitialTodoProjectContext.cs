using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.Dal.Migrations
{
    public partial class InitialTodoProjectContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "52c1e246-3f63-4c27-878d-e4b5b5a6f1f2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bd7ad6c8-020a-40c4-aea8-5dc240dd80f2", "2c7a4c3c-8d8d-4760-a265-ae1d860aeb9d", "Member", "MEMBER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd7ad6c8-020a-40c4-aea8-5dc240dd80f2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "52c1e246-3f63-4c27-878d-e4b5b5a6f1f2", "4c7d925a-65d6-43eb-899e-a3788d295a13", "Member", "MEMBER" });
        }
    }
}
