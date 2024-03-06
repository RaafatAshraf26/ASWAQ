using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class AssignSuperVisorToAllRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into AspNetUserRoles(UserId,RoleId) select 'fa4ad6ab-f4ec-4ebb-ad66-36cdffc2ee3d' , [Id] from AspNetRoles");

            migrationBuilder.Sql("insert into Employees(Id,Salary,Age,Address) values('fa4ad6ab-f4ec-4ebb-ad66-36cdffc2ee3d',10000,20,'Fayoum')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from AspNetUserRoles where UserId = 'fa4ad6ab-f4ec-4ebb-ad66-36cdffc2ee3d' ");

            migrationBuilder.Sql("delete from Employees where Id = 'fa4ad6ab-f4ec-4ebb-ad66-36cdffc2ee3d' ");
        }
    }
}
