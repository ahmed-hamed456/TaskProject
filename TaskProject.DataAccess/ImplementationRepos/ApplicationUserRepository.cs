using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.DataAccess.Data;
using TaskProject.Entities.Models;
using TaskProject.Entities.Repositories.Contract;

namespace TaskProject.DataAccess.ImplementationRepos
{
    public class ApplicationUserRepository : GenericRepository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(ApplicationUser applicationUser)
        {
            var userInDb =  _context.ApplicationUsers.FirstOrDefault(u=>u.Id == applicationUser.Id);

            if (userInDb != null)
            {
                userInDb.Name = applicationUser.Name;
                userInDb.Email = applicationUser.Email;
                userInDb.PhoneNumber = applicationUser.PhoneNumber;
            }
        }
    }
}
