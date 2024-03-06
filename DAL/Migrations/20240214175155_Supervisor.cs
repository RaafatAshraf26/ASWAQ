using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class Supervisor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'fa4ad6ab-f4ec-4ebb-ad66-36cdffc2ee3d', N'Raafat', N'Ashraf', N'Raafat', N'RAAFAT', N'raafat.ashraf.17@gmail.com', N'RAAFAT.ASHRAF.17@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEBbbQHvWVGmdklzzRg5yMLundqVKj+ZTWlJJSQpinY/xG7CmLasMn3g9IGCsBlf+HQ==', N'ASCNOD363AJ2CW37JXXZDL5QCF63TP72', N'f19059e7-e7d2-4fde-b13b-40daf879bc29', N'01122742408', 0, 0, NULL, 1, 0)\r\n");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from [dbo].[AspNetUsers] where Id = 'fa4ad6ab-f4ec-4ebb-ad66-36cdffc2ee3d' ");
        }
    }
}
