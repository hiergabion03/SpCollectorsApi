using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpCollectorsAdminApi.Migrations
{
    /// <inheritdoc />
    public partial class AddNewModel6ToPropertyToPlanDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Tax",
                table: "PlanDetail",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "REAL",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "CBI",
                table: "PlanDetail",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Tax",
                table: "PlanDetail",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CBI",
                table: "PlanDetail",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "REAL",
                oldNullable: true);
        }
    }
}
