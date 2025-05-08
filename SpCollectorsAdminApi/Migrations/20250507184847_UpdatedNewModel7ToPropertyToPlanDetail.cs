using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpCollectorsAdminApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedNewModel7ToPropertyToPlanDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CollAdvance",
                table: "PlanDetail");

            migrationBuilder.DropColumn(
                name: "CollDue",
                table: "PlanDetail");

            migrationBuilder.DropColumn(
                name: "ORDate",
                table: "PlanDetail");

            migrationBuilder.DropColumn(
                name: "ORNo",
                table: "PlanDetail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CollAdvance",
                table: "PlanDetail",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CollDue",
                table: "PlanDetail",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ORDate",
                table: "PlanDetail",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ORNo",
                table: "PlanDetail",
                type: "TEXT",
                nullable: true);
        }
    }
}
