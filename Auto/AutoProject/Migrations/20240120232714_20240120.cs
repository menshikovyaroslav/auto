using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Front.Migrations
{
    /// <inheritdoc />
    public partial class _20240120 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Photos_CarId",
                table: "Photos",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Cars_CarId",
                table: "Photos",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Cars_CarId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_CarId",
                table: "Photos");
        }
    }
}
