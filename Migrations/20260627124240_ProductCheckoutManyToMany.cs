using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class ProductCheckoutManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Checkouts_CheckoutEntityId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CheckoutEntityId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CheckoutEntityId",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "CheckoutEntityProductEntity",
                columns: table => new
                {
                    CheckoutsId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckoutEntityProductEntity", x => new { x.CheckoutsId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_CheckoutEntityProductEntity_Checkouts_CheckoutsId",
                        column: x => x.CheckoutsId,
                        principalTable: "Checkouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CheckoutEntityProductEntity_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckoutEntityProductEntity_ProductsId",
                table: "CheckoutEntityProductEntity",
                column: "ProductsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckoutEntityProductEntity");

            migrationBuilder.AddColumn<int>(
                name: "CheckoutEntityId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CheckoutEntityId",
                table: "Products",
                column: "CheckoutEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Checkouts_CheckoutEntityId",
                table: "Products",
                column: "CheckoutEntityId",
                principalTable: "Checkouts",
                principalColumn: "Id");
        }
    }
}
