using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpCollectorsAdminApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CollectorEntry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CollectorName = table.Column<string>(type: "TEXT", nullable: true),
                    CollectorCode = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectorEntry", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ContractNo = table.Column<string>(type: "TEXT", nullable: true),
                    Planholder = table.Column<string>(type: "TEXT", nullable: true),
                    Plan = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    EffectiveDate = table.Column<string>(type: "TEXT", nullable: true),
                    DueDate = table.Column<string>(type: "TEXT", nullable: true),
                    QuotaNComm = table.Column<string>(type: "TEXT", nullable: true),
                    CBI = table.Column<string>(type: "TEXT", nullable: true),
                    InstNo = table.Column<string>(type: "TEXT", nullable: true),
                    Aging = table.Column<string>(type: "TEXT", nullable: true),
                    Balance = table.Column<string>(type: "TEXT", nullable: true),
                    Tax = table.Column<string>(type: "TEXT", nullable: true),
                    Ins = table.Column<string>(type: "TEXT", nullable: true),
                    ORNo = table.Column<string>(type: "TEXT", nullable: true),
                    ORDate = table.Column<string>(type: "TEXT", nullable: true),
                    CollDue = table.Column<string>(type: "TEXT", nullable: true),
                    CollAdvance = table.Column<string>(type: "TEXT", nullable: true),
                    CollectorEntryId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanDetail_CollectorEntry_CollectorEntryId",
                        column: x => x.CollectorEntryId,
                        principalTable: "CollectorEntry",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanDetail_CollectorEntryId",
                table: "PlanDetail",
                column: "CollectorEntryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanDetail");

            migrationBuilder.DropTable(
                name: "CollectorEntry");
        }
    }
}
