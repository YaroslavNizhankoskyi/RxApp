using Microsoft.EntityFrameworkCore.Migrations;

namespace RxApp.Migrations
{
    public partial class ExtendIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDeletedForPacient",
                table: "Recipes",
                newName: "IsDeletedForPatient");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SecondName",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "IsDeletedForPatient",
                table: "Recipes",
                newName: "IsDeletedForPacient");

            migrationBuilder.AddColumn<int>(
                name: "ActiveIngredientId",
                table: "IncompatibleIngredients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IncompatibleIngredients_ActiveIngredientId",
                table: "IncompatibleIngredients",
                column: "ActiveIngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_IncompatibleIngredients_ActiveIngredients_ActiveIngredientId",
                table: "IncompatibleIngredients",
                column: "ActiveIngredientId",
                principalTable: "ActiveIngredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
