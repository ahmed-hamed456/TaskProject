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
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        private readonly ApplicationDbContext _context;
        public ContactRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Contact contact)
        {
            var contactInDb = _context.Contacts.FirstOrDefault(c=>c.Id == contact.Id);

            if (contactInDb != null)
            {
                contactInDb.Name = contact.Name;
                contactInDb.Phone = contact.Phone;
                contactInDb.Address = contact.Address;
                contactInDb.Notes = contact.Notes;
            }
        }
    }
}
