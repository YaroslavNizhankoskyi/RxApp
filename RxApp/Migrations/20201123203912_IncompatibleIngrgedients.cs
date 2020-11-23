using Microsoft.EntityFrameworkCore.Migrations;

namespace RxApp.Migrations
{
    public partial class IncompatibleIngrgedients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncompatibleDrugs_ActiveIngredients_ActiveIngredientFirstId",
                table: "IncompatibleDrugs");

            migrationBuilder.DropForeignKey(
                name: "FK_IncompatibleDrugs_ActiveIngredients_ActiveIngredientSecondId",
                table: "IncompatibleDrugs");

            migrationBuilder.DropIndex(
                name: "IX_IncompatibleDrugs_ActiveIngredientSecondId",
                table: "IncompatibleDrugs");

            migrationBuilder.DropColumn(
                name: "ActiveIngredientSecondId",
                table: "IncompatibleDrugs");

            migrationBuilder.CreateIndex(
                name: "IX_IncompatibleDrugs_ActiveIngredintSecondId",
                table: "IncompatibleDrugs",
                column: "ActiveIngredintSecondId");

            migrationBuilder.AddForeignKey(
                name: "FK_IncompatibleDrugs_ActiveIngredients_ActiveIngredientFirstId",
                table: "IncompatibleDrugs",
                column: "ActiveIngredientFirstId",
                principalTable: "ActiveIngredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IncompatibleDrugs_ActiveIngredients_ActiveIngredintSecondId",
                table: "IncompatibleDrugs",
                column: "ActiveIngredintSecondId",
                principalTable: "ActiveIngredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncompatibleDrugs_ActiveIngredients_ActiveIngredientFirstId",
                table: "IncompatibleDrugs");

            migrationBuilder.DropForeignKey(
                name: "FK_IncompatibleDrugs_ActiveIngredients_ActiveIngredintSecondId",
                table: "IncompatibleDrugs");

            migrationBuilder.DropIndex(
                name: "IX_IncompatibleDrugs_ActiveIngredintSecondId",
                table: "IncompatibleDrugs");

            migrationBuilder.AddColumn<int>(
                name: "ActiveIngredientSecondId",
                table: "IncompatibleDrugs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IncompatibleDrugs_ActiveIngredientSecondId",
                table: "IncompatibleDrugs",
                column: "ActiveIngredientSecondId");

            migrationBuilder.AddForeignKey(
                name: "FK_IncompatibleDrugs_ActiveIngredients_ActiveIngredientFirstId",
                table: "IncompatibleDrugs",
                column: "ActiveIngredientFirstId",
                principalTable: "ActiveIngredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IncompatibleDrugs_ActiveIngredients_ActiveIngredientSecondId",
                table: "IncompatibleDrugs",
                column: "ActiveIngredientSecondId",
                principalTable: "ActiveIngredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
