using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Entities.Models;

namespace TaskProject.Entities.Repositories.Contract
{
    public interface IApplicationUserRepository : IGenericRepository<ApplicationUser>
    {
       void Update(ApplicationUser applicationUser);
    }
}
