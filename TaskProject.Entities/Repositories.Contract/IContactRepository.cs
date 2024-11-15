using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Entities.Models;

namespace TaskProject.Entities.Repositories.Contract
{
    public interface IContactRepository : IGenericRepository<Contact>
    {
        void Update(Contact contact);   
    }
}
