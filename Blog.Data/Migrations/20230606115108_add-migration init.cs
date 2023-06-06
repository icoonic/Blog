using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Blog.Data.Migrations
{
    /// <inheritdoc />
    public partial class addmigrationinit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    LoginId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => x.LoginId);
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "Content", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "ImageId", "IsDeleted", "ModifiedBy", "ModifiedDate", "Title", "UserId", "ViewCount" },
                values: new object[,]
                {
                    { new Guid("743f973e-4b00-4271-8ec3-d8c2ff382f67"), new Guid("0eb98344-6ba2-40b7-a21f-c774e718cd96"), "Blog Sayfamıza hoşgeldiniz.2", "Admin Test", new DateTime(2023, 6, 6, 14, 51, 8, 192, DateTimeKind.Local).AddTicks(1166), null, null, new Guid("3509133c-5a93-4c02-9a55-ad7e02eaa1fd"), false, null, null, "Asp.net Core Blog2", new Guid("c5d057f3-9607-4f33-af58-10bfad7f664c"), 15 },
                    { new Guid("f9f0c896-8dc8-471d-919e-e5ba1894e4ee"), new Guid("abc8d06f-0a4b-4724-ba66-bf978e15b82d"), "Blog Sayfamıza hoşgeldiniz.", "Admin Test", new DateTime(2023, 6, 6, 14, 51, 8, 192, DateTimeKind.Local).AddTicks(1160), null, null, new Guid("abc8d06f-0a4b-4724-ba66-bf978e15b82d"), false, null, null, "Asp.net Core Blog", new Guid("36e8f3d6-0e25-4d65-8ca6-81ca01c49461"), 15 }
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0c2d5f25-251c-468d-8bcb-8af1ee12a89d"),
                column: "ConcurrencyStamp",
                value: "3994a4f5-bf96-45c9-803e-daaf81db3f5e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2420f0b7-ca5f-4721-8bcb-3c334928dc8d"),
                column: "ConcurrencyStamp",
                value: "fc218e89-c0bd-4545-8eee-f1b4c6e98028");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ea39bc82-671f-42fe-a370-b92abd43c5ac"),
                column: "ConcurrencyStamp",
                value: "52af2709-a749-49e1-9da0-128df0ace417");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("36e8f3d6-0e25-4d65-8ca6-81ca01c49461"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b45089d2-6d2e-4441-963f-9b425cf7c58c", "AQAAAAEAACcQAAAAEM/W6R6KrKLHoyPnUCK9CQQpith1qtXVuFQSgsMB3lbAcbNjBg5cbdYlKbg4jOJxjw==", "d4981046-32a6-4a48-8d4d-c359414eec02" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c5d057f3-9607-4f33-af58-10bfad7f664c"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4d2af902-ce9c-4fea-ac56-4e3c93d757df", "AQAAAAEAACcQAAAAEMv2gUEFMnqyM/Ux2MgmnUAdBDdVT5AZh4NfMOFdR7BUqtLErhSMxCN2dS3U+Qi6aQ==", "528bad46-e8b8-469b-ac87-726fde150a62" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0eb98344-6ba2-40b7-a21f-c774e718cd96"),
                column: "CreatedDate",
                value: new DateTime(2023, 6, 6, 14, 51, 8, 192, DateTimeKind.Local).AddTicks(1351));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("abc8d06f-0a4b-4724-ba66-bf978e15b82d"),
                column: "CreatedDate",
                value: new DateTime(2023, 6, 6, 14, 51, 8, 192, DateTimeKind.Local).AddTicks(1348));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("529fa156-f272-45de-9f77-28e33b37b85c"),
                column: "CreatedDate",
                value: new DateTime(2023, 6, 6, 14, 51, 8, 192, DateTimeKind.Local).AddTicks(1502));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("abc8d06f-0a4b-4724-ba66-bf978e15b82d"),
                column: "CreatedDate",
                value: new DateTime(2023, 6, 6, 14, 51, 8, 192, DateTimeKind.Local).AddTicks(1489));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logins");

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("743f973e-4b00-4271-8ec3-d8c2ff382f67"));

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("f9f0c896-8dc8-471d-919e-e5ba1894e4ee"));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0c2d5f25-251c-468d-8bcb-8af1ee12a89d"),
                column: "ConcurrencyStamp",
                value: "70f4a5ec-fdb2-4b46-bd33-8c948d1d3140");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2420f0b7-ca5f-4721-8bcb-3c334928dc8d"),
                column: "ConcurrencyStamp",
                value: "fa457b2a-8270-4d33-aadf-91d7d654a091");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ea39bc82-671f-42fe-a370-b92abd43c5ac"),
                column: "ConcurrencyStamp",
                value: "45fe8fee-303c-40d8-b691-92df0220b1de");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("36e8f3d6-0e25-4d65-8ca6-81ca01c49461"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3b0e0874-8c13-4e8f-8c48-33c3ed9b4dc5", "AQAAAAEAACcQAAAAEBzZ5zxJDOkJ5kHmDERYALhutp5PtnuO5vgaAmdkz3ubMzg0vjN30Mr0XcQyGTOz1g==", "ad26bf98-ca33-49d7-98ed-d768f3903ae9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c5d057f3-9607-4f33-af58-10bfad7f664c"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1d924208-eab1-4728-ad12-0ec15891be11", "AQAAAAEAACcQAAAAEF9ydqLEJfs4uSeHd7Z70ycGaa6jiV9P/5rFbcKpWlM8L0YrwcdtROJE2lUTqnqSqg==", "d1d42fc3-18d0-4041-93f5-0d536f12b966" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0eb98344-6ba2-40b7-a21f-c774e718cd96"),
                column: "CreatedDate",
                value: new DateTime(2023, 4, 18, 18, 53, 51, 506, DateTimeKind.Local).AddTicks(6496));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("abc8d06f-0a4b-4724-ba66-bf978e15b82d"),
                column: "CreatedDate",
                value: new DateTime(2023, 4, 18, 18, 53, 51, 506, DateTimeKind.Local).AddTicks(6486));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("529fa156-f272-45de-9f77-28e33b37b85c"),
                column: "CreatedDate",
                value: new DateTime(2023, 4, 18, 18, 53, 51, 506, DateTimeKind.Local).AddTicks(6640));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("abc8d06f-0a4b-4724-ba66-bf978e15b82d"),
                column: "CreatedDate",
                value: new DateTime(2023, 4, 18, 18, 53, 51, 506, DateTimeKind.Local).AddTicks(6637));
        }
    }
}
