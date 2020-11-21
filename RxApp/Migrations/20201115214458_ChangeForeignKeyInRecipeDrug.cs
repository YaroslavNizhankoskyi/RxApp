using Microsoft.EntityFrameworkCore.Migrations;

namespace RxApp.Migrations
{
    public partial class ChangeForeignKeyInRecipeDrug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_AspNetUsers_CustomerId",
                table: "Recipes");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Recipes",
                newName: "PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Recipes_CustomerId",
                table: "Recipes",
                newName: "IX_Recipes_PatientId");

            migrationBuilder.AddColumn<bool>(
                name: "IsSold",
                table: "RecipeDrugs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeDrugs_DrugId",
                table: "RecipeDrugs",
                column: "DrugId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeDrugs_RecipeId",
                table: "RecipeDrugs",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeDrugs_Drugs_DrugId",
                table: "RecipeDrugs",
                column: "DrugId",
                principalTable: "Drugs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeDrugs_Recipes_RecipeId",
                table: "RecipeDrugs",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_AspNetUsers_PatientId",
                table: "Recipes",
                column: "PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeDrugs_Drugs_DrugId",
                table: "RecipeDrugs");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeDrugs_Recipes_RecipeId",
                table: "RecipeDrugs");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_AspNetUsers_PatientId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_RecipeDrugs_DrugId",
                table: "RecipeDrugs");

            migrationBuilder.DropIndex(
                name: "IX_RecipeDrugs_RecipeId",
                table: "RecipeDrugs");

            migrationBuilder.DropColumn(
                name: "IsSold",
                table: "RecipeDrugs");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "Recipes",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Recipes_PatientId",
                table: "Recipes",
                newName: "IX_Recipes_CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_AspNetUsers_CustomerId",
                table: "Recipes",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
