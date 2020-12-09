using Microsoft.EntityFrameworkCore.Migrations;

namespace RxApp.Migrations
{
    public partial class AllowedAddRecipesProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AllowAddingRecipes",
                table: "AspNetUsers",
                newName: "AllowedAddingRecipes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AllowedAddingRecipes",
                table: "AspNetUsers",
                newName: "AllowAddingRecipes");
        }
    }
}
