using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dd222062-41c3-495e-8201-235669afe698"));

            migrationBuilder.DeleteData(
                table: "chats",
                keyColumn: "Id",
                keyValue: new Guid("37c949d3-72a4-45c0-b5f3-b9a38e17ea95"));

            migrationBuilder.DeleteData(
                table: "chats",
                keyColumn: "Id",
                keyValue: new Guid("3835e706-a8d0-448f-b2ef-02240644b917"));

            migrationBuilder.DeleteData(
                table: "chats",
                keyColumn: "Id",
                keyValue: new Guid("6e80332d-f075-4a15-98bd-bb12d2189f9a"));

            migrationBuilder.AlterColumn<string>(
                name: "text",
                table: "messages",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("11b1f9d7-ec46-4333-910b-3b9c7f9c1d35"), "1a5bfafe-5586-460e-bafd-34ff8df1c65c", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "chats",
                columns: new[] { "Id", "chatName", "dateCreated", "isPrivate" },
                values: new object[,]
                {
                    { new Guid("71934da2-ad32-481b-8d0a-be88249c416a"), "Group chat", new DateTime(2022, 8, 28, 19, 18, 22, 519, DateTimeKind.Local).AddTicks(964), false },
                    { new Guid("f71302bd-2bda-4746-b2c8-a8cf06a8fc61"), "Some chat", new DateTime(2022, 8, 28, 19, 18, 22, 519, DateTimeKind.Local).AddTicks(930), false },
                    { new Guid("fe30b894-7984-4027-ba7c-c2748421a515"), "Work chat", new DateTime(2022, 8, 28, 19, 18, 22, 519, DateTimeKind.Local).AddTicks(967), false }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("11b1f9d7-ec46-4333-910b-3b9c7f9c1d35"));

            migrationBuilder.DeleteData(
                table: "chats",
                keyColumn: "Id",
                keyValue: new Guid("71934da2-ad32-481b-8d0a-be88249c416a"));

            migrationBuilder.DeleteData(
                table: "chats",
                keyColumn: "Id",
                keyValue: new Guid("f71302bd-2bda-4746-b2c8-a8cf06a8fc61"));

            migrationBuilder.DeleteData(
                table: "chats",
                keyColumn: "Id",
                keyValue: new Guid("fe30b894-7984-4027-ba7c-c2748421a515"));

            migrationBuilder.AlterColumn<string>(
                name: "text",
                table: "messages",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("dd222062-41c3-495e-8201-235669afe698"), "2187eae2-d4c5-4b13-bc15-a6b93408bb03", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "chats",
                columns: new[] { "Id", "chatName", "dateCreated", "isPrivate" },
                values: new object[,]
                {
                    { new Guid("37c949d3-72a4-45c0-b5f3-b9a38e17ea95"), "Some chat", new DateTime(2022, 8, 27, 8, 26, 45, 189, DateTimeKind.Local).AddTicks(9277), false },
                    { new Guid("3835e706-a8d0-448f-b2ef-02240644b917"), "Work chat", new DateTime(2022, 8, 27, 8, 26, 45, 189, DateTimeKind.Local).AddTicks(9313), false },
                    { new Guid("6e80332d-f075-4a15-98bd-bb12d2189f9a"), "Group chat", new DateTime(2022, 8, 27, 8, 26, 45, 189, DateTimeKind.Local).AddTicks(9310), false }
                });
        }
    }
}
