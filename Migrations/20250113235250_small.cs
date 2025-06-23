using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Paslauga.Migrations
{
    /// <inheritdoc />
    public partial class small : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VDC_NetworkPool_NetworkPoolId",
                table: "VDC");

            migrationBuilder.AddForeignKey(
                name: "FK_VDC_NetworkPool_NetworkPoolId",
                table: "VDC",
                column: "NetworkPoolId",
                principalTable: "NetworkPool",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VDC_NetworkPool_NetworkPoolId",
                table: "VDC");

            migrationBuilder.AddForeignKey(
                name: "FK_VDC_NetworkPool_NetworkPoolId",
                table: "VDC",
                column: "NetworkPoolId",
                principalTable: "NetworkPool",
                principalColumn: "Id");
        }
    }
}
 
