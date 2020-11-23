using Microsoft.EntityFrameworkCore.Migrations;

namespace RxApp.Migrations
{
    public partial class IncompatibleDrugs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IncompatibleDrugs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActiveIngredientSecondId = table.Column<int>(type: "int", nullable: true),
                    ActiveIngredientFirstId = table.Column<int>(type: "int", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncompatibleDrugs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IncompatibleDrugs_ActiveIngredients_ActiveIngredientFirstId",
                        column: x => x.ActiveIngredientFirstId,
                        principalTable: "ActiveIngredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IncompatibleDrugs_ActiveIngredients_ActiveIngredientSecondId",
                        column: x => x.ActiveIngredientSecondId,
                        principalTable: "ActiveIngredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IncompatibleDrugs_ActiveIngredientFirstId",
                table: "IncompatibleDrugs",
                column: "ActiveIngredientFirstId");

            migrationBuilder.CreateIndex(
                name: "IX_IncompatibleDrugs_ActiveIngredientSecondId",
                table: "IncompatibleDrugs",
                column: "ActiveIngredientSecondId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IncompatibleDrugs");
        }
    }
}
