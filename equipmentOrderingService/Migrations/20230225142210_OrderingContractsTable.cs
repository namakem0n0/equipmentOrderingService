using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace equipmentOrderingService.Migrations
{
    /// <inheritdoc />
    public partial class OrderingContractsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Area",
                table: "IndustrialPremises",
                newName: "TotalArea");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderingContractId",
                table: "TechnicalEquipment",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FreeArea",
                table: "IndustrialPremises",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "OrderingContracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EquipmentQuantity = table.Column<int>(type: "int", nullable: false),
                    PremisesId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderingContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderingContracts_IndustrialPremises_PremisesId",
                        column: x => x.PremisesId,
                        principalTable: "IndustrialPremises",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalEquipment_OrderingContractId",
                table: "TechnicalEquipment",
                column: "OrderingContractId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderingContracts_PremisesId",
                table: "OrderingContracts",
                column: "PremisesId");

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicalEquipment_OrderingContracts_OrderingContractId",
                table: "TechnicalEquipment",
                column: "OrderingContractId",
                principalTable: "OrderingContracts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TechnicalEquipment_OrderingContracts_OrderingContractId",
                table: "TechnicalEquipment");

            migrationBuilder.DropTable(
                name: "OrderingContracts");

            migrationBuilder.DropIndex(
                name: "IX_TechnicalEquipment_OrderingContractId",
                table: "TechnicalEquipment");

            migrationBuilder.DropColumn(
                name: "OrderingContractId",
                table: "TechnicalEquipment");

            migrationBuilder.DropColumn(
                name: "FreeArea",
                table: "IndustrialPremises");

            migrationBuilder.RenameColumn(
                name: "TotalArea",
                table: "IndustrialPremises",
                newName: "Area");
        }
    }
}
