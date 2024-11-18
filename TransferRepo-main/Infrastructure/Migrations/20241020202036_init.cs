using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransferStatus",
                columns: table => new
                {
                    TransferStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferStatus", x => x.TransferStatusId);
                });

            migrationBuilder.CreateTable(
                name: "TransferType",
                columns: table => new
                {
                    TransferTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferType", x => x.TransferTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Transfer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    SrcAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DestAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transfer_TransferStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "TransferStatus",
                        principalColumn: "TransferStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transfer_TransferType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "TransferType",
                        principalColumn: "TransferTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TransferStatus",
                columns: new[] { "TransferStatusId", "Status" },
                values: new object[,]
                {
                    { 1, "Pending" },
                    { 2, "Accepted" },
                    { 3, "Denied" }
                });

            migrationBuilder.InsertData(
                table: "TransferType",
                columns: new[] { "TransferTypeId", "Name" },
                values: new object[,]
                {
                    { 1, "Varios" },
                    { 2, "Alquileres" },
                    { 3, "Cuotas" },
                    { 4, "Facturas" },
                    { 5, "Seguros" },
                    { 6, "Honorarios" },
                    { 7, "Prestamos" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transfer_StatusId",
                table: "Transfer",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfer_TypeId",
                table: "Transfer",
                column: "TypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transfer");

            migrationBuilder.DropTable(
                name: "TransferStatus");

            migrationBuilder.DropTable(
                name: "TransferType");
        }
    }
}
