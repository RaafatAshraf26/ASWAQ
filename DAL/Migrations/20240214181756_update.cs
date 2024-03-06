using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_SuperVisor_UserName",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "SuperVisor_UserName",
                table: "Employees",
                newName: "SuperVisor_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_SuperVisor_UserName",
                table: "Employees",
                newName: "IX_Employees_SuperVisor_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_SuperVisor_Id",
                table: "Employees",
                column: "SuperVisor_Id",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_SuperVisor_Id",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "SuperVisor_Id",
                table: "Employees",
                newName: "SuperVisor_UserName");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_SuperVisor_Id",
                table: "Employees",
                newName: "IX_Employees_SuperVisor_UserName");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_SuperVisor_UserName",
                table: "Employees",
                column: "SuperVisor_UserName",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}
