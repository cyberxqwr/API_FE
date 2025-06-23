using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Paslauga.Migrations
{
    /// <inheritdoc />
    public partial class small02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VLANId",
                table: "VM",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_VM_VLANId",
                table: "VM",
                column: "VLANId");

            migrationBuilder.AddForeignKey(
                name: "FK_VM_VLAN_VLANId",
                table: "VM",
                column: "VLANId",
                principalTable: "VLAN",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VM_VLAN_VLANId",
                table: "VM");

            migrationBuilder.DropIndex(
                name: "IX_VM_VLANId",
                table: "VM");

            migrationBuilder.DropColumn(
                name: "VLANId",
                table: "VM");
        }
    }
}
 
