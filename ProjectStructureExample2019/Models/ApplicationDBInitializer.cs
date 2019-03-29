using BusinessModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace ProjectStructureExample2019.Models
{
    public class ApplicationDBInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        public ApplicationDBInitializer()
        {
        }
        protected override void Seed(ApplicationDbContext context)
        {
            var manager =
                new UserManager<ApplicationUser>(
                    new UserStore<ApplicationUser>(context));

            context.Roles.AddOrUpdate(r => r.Name,
                new IdentityRole { Name = "Admin" }
                );
            context.Roles.AddOrUpdate(r => r.Name,
                new IdentityRole { Name = "Account Manager" }
                );
            context.Roles.AddOrUpdate(r => r.Name,
                new IdentityRole { Name = "Authorised Account Holder" }
                );

            PasswordHasher ps = new PasswordHasher();

            context.Users.AddOrUpdate(a => new { a.FirstName, a.SecondName },
                new ApplicationUser[] {
               new ApplicationUser
                {
                    UserName = "powell.paul@itsligo.ie",
                    Email = "powell.paul@itsligo.ie",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    EmailConfirmed = true,
                    FirstName = "Paul",
                    SecondName = "Powell",
                    PasswordHash = ps.HashPassword("Ppowell$1")
                },
               new ApplicationUser
                {
                    UserName = "bowles.lionie@itsligo.ie",
                    Email = "bowles.lionie@itsligo.ie",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    EmailConfirmed = true,
                    FirstName = "Lionie",
                    SecondName = "Bowles",
                    PasswordHash = ps.HashPassword("LBowles$1")
                },
               new ApplicationUser
                {
                    UserName = "blithe.regina@itsligo.ie",
                    Email = "blithe.regina@itsligo.ie",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    EmailConfirmed = true,
                    FirstName = "Regina",
                    SecondName = "Blithe",
                    PasswordHash = ps.HashPassword("RBlithe$1")
                },
                });

            context.SaveChanges();

            // Assign Roles
            ApplicationUser ChosenClubAdmin = manager.FindByEmail("powell.paul@itsligo.ie");

            if (ChosenClubAdmin != null)
            {
                manager.AddToRoles(ChosenClubAdmin.Id, new string[] { "Admin" });
            }

            ChosenClubAdmin = manager.FindByEmail("bowles.lionie@itsligo.ie");
            if (ChosenClubAdmin != null)
            {
                manager.AddToRoles(ChosenClubAdmin.Id, new string[] { "Account Manager" });
            }
            ChosenClubAdmin = manager.FindByEmail("blithe.regina@itsligo.ie");
            if (ChosenClubAdmin != null)
            {
                manager.AddToRoles(ChosenClubAdmin.Id, new string[] { "Account Manager" });
            }

            SeedAccountManagers(context);

            base.Seed(context);
        }

        private void SeedAccountManagers(ApplicationDbContext context)
        {
            BusinessContext bctx = new BusinessContext(); // Note this call will trigger the database initialiser for BusinessContext
                                                          // The first time around
                                                          // Create and Attach Account Managers to the Accounts

            var roles = context.Roles.Where(r => r.Name == "Account Manager");
            if (roles.Any())
            {
                var users = context.Users.ToList();
                // Get users in roles
                var AccountManagers = (from c in users
                                       where c.Roles.Any(r => r.RoleId == roles.First().Id)
                                       select c);
                // Assign a random user to a company
                foreach (var company in bctx.Accounts)
                {
                    // Select a random account manager id
                    company.AccountManagerID = AccountManagers
                        .Select(m => new { id = m.Id, order = Guid.NewGuid().ToString() })
                        .OrderBy(o => o.order)
                        .Select(r => r.id)
                        .First();
                }
            }
            bctx.SaveChanges();
        }
    }
}