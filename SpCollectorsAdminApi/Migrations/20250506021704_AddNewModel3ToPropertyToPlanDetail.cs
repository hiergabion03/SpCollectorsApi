using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpCollectorsAdminApi.Migrations
{
    /// <inheritdoc />
    public partial class AddNewModel3ToPropertyToPlanDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanDetail_CollectorEntry_CollectorEntryId",
                table: "PlanDetail");

            migrationBuilder.AlterColumn<int>(
                name: "CollectorEntryId",
                table: "PlanDetail",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Period",
                table: "CollectorEntry",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanDetail_CollectorEntry_CollectorEntryId",
                table: "PlanDetail",
                column: "CollectorEntryId",
                principalTable: "CollectorEntry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanDetail_CollectorEntry_CollectorEntryId",
                table: "PlanDetail");

            migrationBuilder.DropColumn(
                name: "Period",
                table: "CollectorEntry");

            migrationBuilder.AlterColumn<int>(
                name: "CollectorEntryId",
                table: "PlanDetail",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanDetail_CollectorEntry_CollectorEntryId",
                table: "PlanDetail",
                column: "CollectorEntryId",
                principalTable: "CollectorEntry",
                principalColumn: "Id");
        }
    }
}
