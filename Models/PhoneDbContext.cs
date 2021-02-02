using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Models
{
    public class PhoneDbContext : DbContext
    {
        public DbSet<Persons> Persons { get; set; }
        public DbSet<Contacts> Contacts { get; set; }
        public PhoneDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
