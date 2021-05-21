namespace RaisingHeltons.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using RaisingHeltons.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RaisingHeltons.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(RaisingHeltons.Models.ApplicationDbContext context)
        {
            #region Role Creation
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));

            if(!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }
            if (!context.Roles.Any(r => r.Name == "Moderator"))
            {
                roleManager.Create(new IdentityRole { Name = "Moderator" });
            }
            #endregion

            #region User Creation
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "mfrierson64@gmail.com"))
            {
                userManager.Create(new ApplicationUser()
                {
                    UserName = "mfrierson64@gmail.com",
                    Email = "mfrierson64@gmail.com",
                    FirstName = "Mercedes",
                    LastName = "Helton",
                    DisplayName = "Mercedes H."
                }, "Abc&123");
            }
            if (!context.Users.Any(u => u.Email == "kayla_mcgraw@hotmail.com"))
            {
                userManager.Create(new ApplicationUser()
                {
                    UserName = "kayla_mcgraw@hotmail.com",
                    Email = "kayla_mcgraw@hotmail.com",
                    FirstName = "Kayla",
                    LastName = "McGraw",
                    DisplayName = "Kayla M."
                }, "$Imone2410");
            }
            #endregion

            #region Role Assignment
            var adminId = userManager.FindByEmail("mfrierson64@gmail.com").Id;
            userManager.AddToRole(adminId, "Admin");

            var admin2Id = userManager.FindByEmail("kayla_mcgraw@hotmail.com").Id;
            userManager.AddToRole(admin2Id, "Admin");
            #endregion
        }
    }
}
