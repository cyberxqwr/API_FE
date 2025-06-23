using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Paslauga.Migrations
{
    /// <inheritdoc />
    public partial class big01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "Storage",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnitPrice",
                table: "Storage",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "RAM",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnitPrice",
                table: "RAM",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "CPU",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnitPrice",
                table: "CPU",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "HardwareSuppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwareSuppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Provider",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provider", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProviderResources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProviderId = table.Column<int>(type: "INTEGER", nullable: false),
                    HardwareType = table.Column<int>(type: "INTEGER", nullable: false),
                    HardwareId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProviderResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProviderResources_CPU_HardwareId",
                        column: x => x.HardwareId,
                        principalTable: "CPU",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProviderResources_Provider_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Provider",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProviderResources_RAM_HardwareId",
                        column: x => x.HardwareId,
                        principalTable: "RAM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProviderResources_Storage_HardwareId",
                        column: x => x.HardwareId,
                        principalTable: "Storage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Storage_SupplierId",
                table: "Storage",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_RAM_SupplierId",
                table: "RAM",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_CPU_SupplierId",
                table: "CPU",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderResources_HardwareId",
                table: "ProviderResources",
                column: "HardwareId");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderResources_ProviderId",
                table: "ProviderResources",
                column: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_CPU_HardwareSuppliers_SupplierId",
                table: "CPU",
                column: "SupplierId",
                principalTable: "HardwareSuppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RAM_HardwareSuppliers_SupplierId",
                table: "RAM",
                column: "SupplierId",
                principalTable: "HardwareSuppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Storage_HardwareSuppliers_SupplierId",
                table: "Storage",
                column: "SupplierId",
                principalTable: "HardwareSuppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CPU_HardwareSuppliers_SupplierId",
                table: "CPU");

            migrationBuilder.DropForeignKey(
                name: "FK_RAM_HardwareSuppliers_SupplierId",
                table: "RAM");

            migrationBuilder.DropForeignKey(
                name: "FK_Storage_HardwareSuppliers_SupplierId",
                table: "Storage");

            migrationBuilder.DropTable(
                name: "HardwareSuppliers");

            migrationBuilder.DropTable(
                name: "ProviderResources");

            migrationBuilder.DropTable(
                name: "Provider");

            migrationBuilder.DropIndex(
                name: "IX_Storage_SupplierId",
                table: "Storage");

            migrationBuilder.DropIndex(
                name: "IX_RAM_SupplierId",
                table: "RAM");

            migrationBuilder.DropIndex(
                name: "IX_CPU_SupplierId",
                table: "CPU");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "Storage");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "Storage");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "RAM");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "RAM");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "CPU");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "CPU");
        }
    }
}
 
