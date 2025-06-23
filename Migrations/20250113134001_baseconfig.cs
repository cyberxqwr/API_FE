using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Paslauga.Migrations
{
    /// <inheritdoc />
    public partial class baseconfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NetworkPoolId",
                table: "VDC",
                type: "INTEGER",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganisationId",
                table: "VDC",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VCPUAllocated",
                table: "VDC",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VCPUMax",
                table: "VDC",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VMemoryAllocated",
                table: "VDC",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VMemoryMax",
                table: "VDC",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VStorageMax",
                table: "VDC",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VStorageUsed",
                table: "VDC",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProviderName",
                table: "Cloud",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "NetworkPool",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NetworkPool", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organisation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organisation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VDCId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    OSName = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VM_VDC_VDCId",
                        column: x => x.VDCId,
                        principalTable: "VDC",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VLAN",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NetworkPoolId = table.Column<int>(type: "INTEGER", nullable: true, defaultValue: 0),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    GatewayCIDR = table.Column<string>(type: "TEXT", nullable: false),
                    StaticIPBegin = table.Column<string>(type: "TEXT", nullable: false),
                    StaticIPEnd = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VLAN", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VLAN_NetworkPool_NetworkPoolId",
                        column: x => x.NetworkPoolId,
                        principalTable: "NetworkPool",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AvailableIPs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IPAddress = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    VLANId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailableIPs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AvailableIPs_VLAN_VLANId",
                        column: x => x.VLANId,
                        principalTable: "VLAN",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VDC_NetworkPoolId",
                table: "VDC",
                column: "NetworkPoolId");

            migrationBuilder.CreateIndex(
                name: "IX_VDC_OrganisationId",
                table: "VDC",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_AvailableIPs_VLANId",
                table: "AvailableIPs",
                column: "VLANId");

            migrationBuilder.CreateIndex(
                name: "IX_VLAN_NetworkPoolId",
                table: "VLAN",
                column: "NetworkPoolId");

            migrationBuilder.CreateIndex(
                name: "IX_VM_VDCId",
                table: "VM",
                column: "VDCId");

            migrationBuilder.AddForeignKey(
                name: "FK_VDC_NetworkPool_NetworkPoolId",
                table: "VDC",
                column: "NetworkPoolId",
                principalTable: "NetworkPool",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VDC_Organisation_OrganisationId",
                table: "VDC",
                column: "OrganisationId",
                principalTable: "Organisation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VDC_NetworkPool_NetworkPoolId",
                table: "VDC");

            migrationBuilder.DropForeignKey(
                name: "FK_VDC_Organisation_OrganisationId",
                table: "VDC");

            migrationBuilder.DropTable(
                name: "AvailableIPs");

            migrationBuilder.DropTable(
                name: "Organisation");

            migrationBuilder.DropTable(
                name: "VM");

            migrationBuilder.DropTable(
                name: "VLAN");

            migrationBuilder.DropTable(
                name: "NetworkPool");

            migrationBuilder.DropIndex(
                name: "IX_VDC_NetworkPoolId",
                table: "VDC");

            migrationBuilder.DropIndex(
                name: "IX_VDC_OrganisationId",
                table: "VDC");

            migrationBuilder.DropColumn(
                name: "NetworkPoolId",
                table: "VDC");

            migrationBuilder.DropColumn(
                name: "OrganisationId",
                table: "VDC");

            migrationBuilder.DropColumn(
                name: "VCPUAllocated",
                table: "VDC");

            migrationBuilder.DropColumn(
                name: "VCPUMax",
                table: "VDC");

            migrationBuilder.DropColumn(
                name: "VMemoryAllocated",
                table: "VDC");

            migrationBuilder.DropColumn(
                name: "VMemoryMax",
                table: "VDC");

            migrationBuilder.DropColumn(
                name: "VStorageMax",
                table: "VDC");

            migrationBuilder.DropColumn(
                name: "VStorageUsed",
                table: "VDC");

            migrationBuilder.DropColumn(
                name: "ProviderName",
                table: "Cloud");
        }
    }
}
 
