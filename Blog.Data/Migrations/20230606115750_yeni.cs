using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Blog.Data.Migrations
{
    /// <inheritdoc />
    public partial class yeni : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("628e949f-6c31-4b07-a092-1ddae387bee7"));

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("80529066-ca20-431c-9d25-8253c35614c2"));

            migrationBuilder.RenameColumn(
                name: "Sifre",
                table: "Logins",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "KullaniciAdi",
                table: "Logins",
                newName: "Password");

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "Content", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "ImageId", "IsDeleted", "ModifiedBy", "ModifiedDate", "Title", "UserId", "ViewCount" },
                values: new object[,]
                {
                    { new Guid("976a39c0-7488-4b07-9428-95f8060ec3ec"), new Guid("0eb98344-6ba2-40b7-a21f-c774e718cd96"), "Blog Sayfamıza hoşgeldiniz.2", "Admin Test", new DateTime(2023, 6, 6, 14, 57, 50, 119, DateTimeKind.Local).AddTicks(3469), null, null, new Guid("3509133c-5a93-4c02-9a55-ad7e02eaa1fd"), false, null, null, "Asp.net Core Blog2", new Guid("c5d057f3-9607-4f33-af58-10bfad7f664c"), 15 },
                    { new Guid("d813b373-49e3-49ce-8a20-a566cfa32f1f"), new Guid("abc8d06f-0a4b-4724-ba66-bf978e15b82d"), "Blog Sayfamıza hoşgeldiniz.", "Admin Test", new DateTime(2023, 6, 6, 14, 57, 50, 119, DateTimeKind.Local).AddTicks(3463), null, null, new Guid("abc8d06f-0a4b-4724-ba66-bf978e15b82d"), false, null, null, "Asp.net Core Blog", new Guid("36e8f3d6-0e25-4d65-8ca6-81ca01c49461"), 15 }
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0c2d5f25-251c-468d-8bcb-8af1ee12a89d"),
                column: "ConcurrencyStamp",
                value: "4ffc4b08-6837-41ae-ab7f-7a9e4c4fa5bb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2420f0b7-ca5f-4721-8bcb-3c334928dc8d"),
                column: "ConcurrencyStamp",
                value: "20981a6f-0bbc-4a16-981e-86c53a3c858d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ea39bc82-671f-42fe-a370-b92abd43c5ac"),
                column: "ConcurrencyStamp",
                value: "dec71086-17d4-4879-902b-98e758c06f31");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("36e8f3d6-0e25-4d65-8ca6-81ca01c49461"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5e32b9b8-6a21-46af-8916-15f2c1b07f9a", "AQAAAAEAACcQAAAAEDmYYTd6wgYEyqgUqaTSnQn8YcXK1tdrlQIhSAfpOkRVFYXwd40S4bHVMaJAaC+iIQ==", "a4ae6767-1664-482c-9658-b77965bc383c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c5d057f3-9607-4f33-af58-10bfad7f664c"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4bec34fd-c076-4628-bedc-8f4deb697195", "AQAAAAEAACcQAAAAEEhrB0PozPQOnGeYAEP43qY4Ve1AR/RCQ37kCrodBLVQ8e/0cOFs0K8nXsWhjMhAMA==", "91196e8b-51f4-4aa9-a536-95bcb2bd6d3d" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0eb98344-6ba2-40b7-a21f-c774e718cd96"),
                column: "CreatedDate",
                value: new DateTime(2023, 6, 6, 14, 57, 50, 119, DateTimeKind.Local).AddTicks(3664));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("abc8d06f-0a4b-4724-ba66-bf978e15b82d"),
                column: "CreatedDate",
                value: new DateTime(2023, 6, 6, 14, 57, 50, 119, DateTimeKind.Local).AddTicks(3661));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("529fa156-f272-45de-9f77-28e33b37b85c"),
                column: "CreatedDate",
                value: new DateTime(2023, 6, 6, 14, 57, 50, 119, DateTimeKind.Local).AddTicks(3769));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("abc8d06f-0a4b-4724-ba66-bf978e15b82d"),
                column: "CreatedDate",
                value: new DateTime(2023, 6, 6, 14, 57, 50, 119, DateTimeKind.Local).AddTicks(3766));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("976a39c0-7488-4b07-9428-95f8060ec3ec"));

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("d813b373-49e3-49ce-8a20-a566cfa32f1f"));

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Logins",
                newName: "Sifre");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Logins",
                newName: "KullaniciAdi");

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "Content", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "ImageId", "IsDeleted", "ModifiedBy", "ModifiedDate", "Title", "UserId", "ViewCount" },
                values: new object[,]
                {
                    { new Guid("628e949f-6c31-4b07-a092-1ddae387bee7"), new Guid("abc8d06f-0a4b-4724-ba66-bf978e15b82d"), "Blog Sayfamıza hoşgeldiniz.", "Admin Test", new DateTime(2023, 6, 6, 14, 51, 49, 221, DateTimeKind.Local).AddTicks(6963), null, null, new Guid("abc8d06f-0a4b-4724-ba66-bf978e15b82d"), false, null, null, "Asp.net Core Blog", new Guid("36e8f3d6-0e25-4d65-8ca6-81ca01c49461"), 15 },
                    { new Guid("80529066-ca20-431c-9d25-8253c35614c2"), new Guid("0eb98344-6ba2-40b7-a21f-c774e718cd96"), "Blog Sayfamıza hoşgeldiniz.2", "Admin Test", new DateTime(2023, 6, 6, 14, 51, 49, 221, DateTimeKind.Local).AddTicks(6978), null, null, new Guid("3509133c-5a93-4c02-9a55-ad7e02eaa1fd"), false, null, null, "Asp.net Core Blog2", new Guid("c5d057f3-9607-4f33-af58-10bfad7f664c"), 15 }
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0c2d5f25-251c-468d-8bcb-8af1ee12a89d"),
                column: "ConcurrencyStamp",
                value: "7a057593-ce7c-462f-8659-b516212b97b3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2420f0b7-ca5f-4721-8bcb-3c334928dc8d"),
                column: "ConcurrencyStamp",
                value: "95ef7c70-73de-4e61-80e9-1bf29ed4e2b0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ea39bc82-671f-42fe-a370-b92abd43c5ac"),
                column: "ConcurrencyStamp",
                value: "5405ed08-477c-4e45-8ad1-519554f7b885");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("36e8f3d6-0e25-4d65-8ca6-81ca01c49461"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f2907cc8-de2f-4e01-99ff-4748a9e2bf1c", "AQAAAAEAACcQAAAAEF6Ecx4ToJH2kx1mwh0ILqlZv6LwliET8TMemoI1g6ipdRZa7wXUl3d8mhrdLpk37w==", "0bc52b52-d24d-4959-99fc-85559e23d076" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c5d057f3-9607-4f33-af58-10bfad7f664c"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "37887e9d-462b-454e-9c92-dfbbc3e8a10a", "AQAAAAEAACcQAAAAEBru4Murmj/KDwbv+g810A1wH3EuVsvTBkTz6eKf1TyVE+MNeeGsNf1j7z/jaPczdA==", "2ed0e2b1-3da7-461e-b930-601d09b133a5" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0eb98344-6ba2-40b7-a21f-c774e718cd96"),
                column: "CreatedDate",
                value: new DateTime(2023, 6, 6, 14, 51, 49, 221, DateTimeKind.Local).AddTicks(7130));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("abc8d06f-0a4b-4724-ba66-bf978e15b82d"),
                column: "CreatedDate",
                value: new DateTime(2023, 6, 6, 14, 51, 49, 221, DateTimeKind.Local).AddTicks(7128));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("529fa156-f272-45de-9f77-28e33b37b85c"),
                column: "CreatedDate",
                value: new DateTime(2023, 6, 6, 14, 51, 49, 221, DateTimeKind.Local).AddTicks(7218));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("abc8d06f-0a4b-4724-ba66-bf978e15b82d"),
                column: "CreatedDate",
                value: new DateTime(2023, 6, 6, 14, 51, 49, 221, DateTimeKind.Local).AddTicks(7214));
        }
    }
}
