using Microsoft.EntityFrameworkCore.Migrations;

namespace RxApp.Migrations
{
    public partial class AllowAddRecipesProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AllowAddingRecipes",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowAddingRecipes",
                table: "AspNetUsers");
        }
    }
}
