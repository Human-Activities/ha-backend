using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedGroupToActivityTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Activities",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Activities_GroupId",
                table: "Activities",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Groups_GroupId",
                table: "Activities",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Groups_GroupId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_GroupId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Activities");
        }
    }
}
