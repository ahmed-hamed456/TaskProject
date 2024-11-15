using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.DataAccess.Data;
using TaskProject.DataAccess.Handlers;
using TaskProject.Entities.Models;
using TaskProject.Entities.Repositories.Contract;

namespace TaskProject.DataAccess.ImplementationRepos
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IContactRepository Contact { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            ApplicationUser = new ApplicationUserRepository(context);
            Contact = new ContactRepository(context);
        }


        public int Complete()
            =>_context.SaveChanges();

        public void Dispose()
            =>_context.Dispose();
    }
}
