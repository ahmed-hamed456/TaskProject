using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.DataAccess.Data;
using TaskProject.Entities.Models;
using TaskProject.Utilities;

namespace TaskProject.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public DbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public void Initialize()
        {
            //Migration
            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                }
            }
            catch (Exception)
            {

                throw;
            }

            //Roles
            if (!_roleManager.RoleExistsAsync(SD.AdminRole).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.AdminRole)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.CustomerRole)).GetAwaiter().GetResult();



                //User

                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "user1",
                    Email = "Admin@mycontact.com",
                    Name = "Administrator",
                    PhoneNumber = "01234567891",
                }, "P@$$w0rd").GetAwaiter().GetResult();

                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "user2",
                    Email = "Admin@mycontact.com",
                    Name = "Administrator",
                    PhoneNumber = "01119479408",
                }, "P@$$w0rd").GetAwaiter().GetResult();

                ApplicationUser user1 = _context.ApplicationUsers.FirstOrDefault(u => u.Email == "Admin@mycontact.com");
                ApplicationUser user2 = _context.ApplicationUsers.FirstOrDefault(u => u.Email == "Admin2@mycontact.com");

                _userManager.AddToRoleAsync(user1, SD.AdminRole).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(user2, SD.AdminRole).GetAwaiter().GetResult();
            }

            return;
        }
    }
}

