using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class Fixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CartEntityId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CheckoutEntityId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "Identites",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Checkouts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Carts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Products_CartEntityId",
                table: "Products",
                column: "CartEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CheckoutEntityId",
                table: "Products",
                column: "CheckoutEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Identites_CartId",
                table: "Identites",
                column: "CartId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Checkouts_UserId",
                table: "Checkouts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Checkouts_Users_UserId",
                table: "Checkouts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Identites_Carts_CartId",
                table: "Identites",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Carts_CartEntityId",
                table: "Products",
                column: "CartEntityId",
                principalTable: "Carts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Checkouts_CheckoutEntityId",
                table: "Products",
                column: "CheckoutEntityId",
                principalTable: "Checkouts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checkouts_Users_UserId",
                table: "Checkouts");

            migrationBuilder.DropForeignKey(
                name: "FK_Identites_Carts_CartId",
                table: "Identites");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Carts_CartEntityId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Checkouts_CheckoutEntityId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CartEntityId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CheckoutEntityId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Identites_CartId",
                table: "Identites");

            migrationBuilder.DropIndex(
                name: "IX_Checkouts_UserId",
                table: "Checkouts");

            migrationBuilder.DropColumn(
                name: "CartEntityId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CheckoutEntityId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "Identites");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Checkouts");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Carts");
        }
    }
}
