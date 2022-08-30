using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    public partial class mig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("df0b8976-bb3d-47db-b225-077a1efcea08"), "92d1bc3b-c785-4373-863e-52ca3ee00947", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "chats",
                columns: new[] { "Id", "chatName", "dateCreated", "isPrivate" },
                values: new object[,]
                {
                    { new Guid("727f967f-7d1e-4ef9-a711-664e849b10ea"), "Work chat", new DateTime(2022, 8, 30, 14, 15, 54, 790, DateTimeKind.Local).AddTicks(6431), false },
                    { new Guid("937411f3-1c36-4122-aeea-75ecd6e38e0e"), "Group chat", new DateTime(2022, 8, 30, 14, 15, 54, 790, DateTimeKind.Local).AddTicks(6428), false },
                    { new Guid("e32da348-4603-49d0-be51-be9890a768bd"), "Some chat", new DateTime(2022, 8, 30, 14, 15, 54, 790, DateTimeKind.Local).AddTicks(6394), false }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("df0b8976-bb3d-47db-b225-077a1efcea08"));

            migrationBuilder.DeleteData(
                table: "chats",
                keyColumn: "Id",
                keyValue: new Guid("727f967f-7d1e-4ef9-a711-664e849b10ea"));

            migrationBuilder.DeleteData(
                table: "chats",
                keyColumn: "Id",
                keyValue: new Guid("937411f3-1c36-4122-aeea-75ecd6e38e0e"));

            migrationBuilder.DeleteData(
                table: "chats",
                keyColumn: "Id",
                keyValue: new Guid("e32da348-4603-49d0-be51-be9890a768bd"));

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
    }
}
