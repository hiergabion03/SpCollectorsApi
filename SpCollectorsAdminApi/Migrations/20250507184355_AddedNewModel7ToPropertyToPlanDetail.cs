using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpCollectorsAdminApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewModel7ToPropertyToPlanDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ORNo = table.Column<string>(type: "TEXT", nullable: true),
                    ORDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CollDue = table.Column<double>(type: "REAL", nullable: true),
                    CollAdvance = table.Column<double>(type: "REAL", nullable: true),
                    PlanDetailId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentDetail_PlanDetail_PlanDetailId",
                        column: x => x.PlanDetailId,
                        principalTable: "PlanDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDetail_PlanDetailId",
                table: "PaymentDetail",
                column: "PlanDetailId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentDetail");
        }
    }
}
