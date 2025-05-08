using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpCollectorsAdminApi.Migrations
{
    /// <inheritdoc />
    public partial class AddNewModel4ToPropertyToPlanDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CollectorCode",
                table: "PlanDetail",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CollectorName",
                table: "PlanDetail",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Period",
                table: "PlanDetail",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CollectorCode",
                table: "PlanDetail");

            migrationBuilder.DropColumn(
                name: "CollectorName",
                table: "PlanDetail");

            migrationBuilder.DropColumn(
                name: "Period",
                table: "PlanDetail");
        }
    }
}
