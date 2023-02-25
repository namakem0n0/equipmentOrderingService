using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace equipmentOrderingService.Migrations
{
    /// <inheritdoc />
    public partial class OrderingContractUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TechnicalEquipment_OrderingContracts_OrderingContractId",
                table: "TechnicalEquipment");

            migrationBuilder.DropIndex(
                name: "IX_TechnicalEquipment_OrderingContractId",
                table: "TechnicalEquipment");

            migrationBuilder.DropColumn(
                name: "OrderingContractId",
                table: "TechnicalEquipment");

            migrationBuilder.AddColumn<Guid>(
                name: "EquipmentTypeId",
                table: "OrderingContracts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderingContracts_EquipmentTypeId",
                table: "OrderingContracts",
                column: "EquipmentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderingContracts_TechnicalEquipment_EquipmentTypeId",
                table: "OrderingContracts",
                column: "EquipmentTypeId",
                principalTable: "TechnicalEquipment",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderingContracts_TechnicalEquipment_EquipmentTypeId",
                table: "OrderingContracts");

            migrationBuilder.DropIndex(
                name: "IX_OrderingContracts_EquipmentTypeId",
                table: "OrderingContracts");

            migrationBuilder.DropColumn(
                name: "EquipmentTypeId",
                table: "OrderingContracts");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderingContractId",
                table: "TechnicalEquipment",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalEquipment_OrderingContractId",
                table: "TechnicalEquipment",
                column: "OrderingContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicalEquipment_OrderingContracts_OrderingContractId",
                table: "TechnicalEquipment",
                column: "OrderingContractId",
                principalTable: "OrderingContracts",
                principalColumn: "Id");
        }
    }
}
