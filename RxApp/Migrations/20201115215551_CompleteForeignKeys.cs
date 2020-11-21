using Microsoft.EntityFrameworkCore.Migrations;

namespace RxApp.Migrations
{
    public partial class CompleteForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActiveComponentId",
                table: "Allergies",
                newName: "ActiveIngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Drugs_PharmGroupId",
                table: "Drugs",
                column: "PharmGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugActiveIngredients_ActiveIngredientId",
                table: "DrugActiveIngredients",
                column: "ActiveIngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Allergies_ActiveIngredientId",
                table: "Allergies",
                column: "ActiveIngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Allergies_ActiveIngredients_ActiveIngredientId",
                table: "Allergies",
                column: "ActiveIngredientId",
                principalTable: "ActiveIngredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DrugActiveIngredients_ActiveIngredients_ActiveIngredientId",
                table: "DrugActiveIngredients",
                column: "ActiveIngredientId",
                principalTable: "ActiveIngredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Drugs_PharmGroups_PharmGroupId",
                table: "Drugs",
                column: "PharmGroupId",
                principalTable: "PharmGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Allergies_ActiveIngredients_ActiveIngredientId",
                table: "Allergies");

            migrationBuilder.DropForeignKey(
                name: "FK_DrugActiveIngredients_ActiveIngredients_ActiveIngredientId",
                table: "DrugActiveIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Drugs_PharmGroups_PharmGroupId",
                table: "Drugs");

            migrationBuilder.DropIndex(
                name: "IX_Drugs_PharmGroupId",
                table: "Drugs");

            migrationBuilder.DropIndex(
                name: "IX_DrugActiveIngredients_ActiveIngredientId",
                table: "DrugActiveIngredients");

            migrationBuilder.DropIndex(
                name: "IX_Allergies_ActiveIngredientId",
                table: "Allergies");

            migrationBuilder.RenameColumn(
                name: "ActiveIngredientId",
                table: "Allergies",
                newName: "ActiveComponentId");
        }
    }
}
