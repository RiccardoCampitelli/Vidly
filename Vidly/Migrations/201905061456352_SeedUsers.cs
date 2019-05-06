namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'03ffded5-e269-4925-b13a-ac38d6e59ae6', N'admin@vidly.com', 0, N'ACCyBa8xb/vOvGEuDZ5R2EJGMFtkW4uTfu/T8OK8X79smSeSI2WEK6V36z3uSnNEFg==', N'278e989c-434e-47eb-ab3d-e31d3696dadd', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'fef84dfd-be32-4fc0-99cc-391c8702571e', N'guest@vidly.com', 0, N'ANUn2TeVuHEz1phM5gLWX/lywOSDdgOauvclhEWpQ9HduTacpfhUoEh3+y4V8MPMNg==', N'22cac351-354c-490e-a5c1-9315236df9ff', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')

                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [Discriminator]) VALUES (N'ba2fe2d4-7e58-44c6-b2ff-ec9fa10f3ca5', N'CanManageMovies', N'IdentityRole')

                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'03ffded5-e269-4925-b13a-ac38d6e59ae6', N'ba2fe2d4-7e58-44c6-b2ff-ec9fa10f3ca5')
                ");
        }
        
        public override void Down()
        {
        }
    }
}
