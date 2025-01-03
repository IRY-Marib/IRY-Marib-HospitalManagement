using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalManagement.Migrations
{
    public partial class EMERQ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emergencies_Departments_DepartmentID",
                table: "Emergencies");

            migrationBuilder.RenameColumn(
                name: "IsEmailSent",
                table: "Emergencies",
                newName: "IsSent");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Emergencies",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "DatePosted",
                table: "Emergencies",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "EmergencyID",
                table: "Emergencies",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Emergencies",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentID",
                table: "Emergencies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Emergencies_Departments_DepartmentID",
                table: "Emergencies",
                column: "DepartmentID",
                principalTable: "Departments",
                principalColumn: "DepartmentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emergencies_Departments_DepartmentID",
                table: "Emergencies");

            migrationBuilder.RenameColumn(
                name: "IsSent",
                table: "Emergencies",
                newName: "IsEmailSent");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Emergencies",
                newName: "DatePosted");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Emergencies",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Emergencies",
                newName: "EmergencyID");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Emergencies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentID",
                table: "Emergencies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Emergencies_Departments_DepartmentID",
                table: "Emergencies",
                column: "DepartmentID",
                principalTable: "Departments",
                principalColumn: "DepartmentID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
