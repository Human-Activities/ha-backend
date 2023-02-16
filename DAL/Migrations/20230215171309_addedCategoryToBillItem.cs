using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class addedCategoryToBillItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "BillItem",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BillItem_CategoryId",
                table: "BillItem",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillItem_Categories_CategoryId",
                table: "BillItem",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillItem_Categories_CategoryId",
                table: "BillItem");

            migrationBuilder.DropIndex(
                name: "IX_BillItem_CategoryId",
                table: "BillItem");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "BillItem");
        }
    }
}
