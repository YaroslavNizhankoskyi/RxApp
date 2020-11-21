using Microsoft.EntityFrameworkCore.Migrations;

namespace RxApp.Migrations
{
    public partial class RemoveOptionsIncluded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Allergies_AspNetUsers_CustomerId",
                table: "Allergies");

            migrationBuilder.DropForeignKey(
                name: "FK_Drugs_PharmGroups_PharmGroupId",
                table: "Drugs");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeletedForMedic",
                table: "Recipes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeletedForPacient",
                table: "Recipes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "PharmGroupId",
                table: "Drugs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Allergies_AspNetUsers_CustomerId",
                table: "Allergies",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Drugs_PharmGroups_PharmGroupId",
                table: "Drugs",
                column: "PharmGroupId",
                principalTable: "PharmGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Allergies_AspNetUsers_CustomerId",
                table: "Allergies");

            migrationBuilder.DropForeignKey(
                name: "FK_Drugs_PharmGroups_PharmGroupId",
                table: "Drugs");

            migrationBuilder.DropColumn(
                name: "IsDeletedForMedic",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "IsDeletedForPacient",
                table: "Recipes");

            migrationBuilder.AlterColumn<int>(
                name: "PharmGroupId",
                table: "Drugs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Allergies_AspNetUsers_CustomerId",
                table: "Allergies",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Drugs_PharmGroups_PharmGroupId",
                table: "Drugs",
                column: "PharmGroupId",
                principalTable: "PharmGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
