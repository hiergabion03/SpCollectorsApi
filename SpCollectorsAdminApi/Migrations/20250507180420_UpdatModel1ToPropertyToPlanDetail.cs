using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpCollectorsAdminApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdatModel1ToPropertyToPlanDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Ins",
                table: "PlanDetail",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Ins",
                table: "PlanDetail",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
