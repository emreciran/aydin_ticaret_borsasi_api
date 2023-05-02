using EntitiesLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Context
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Announcement> Announcements { get; set; }

        public DbSet<News> News { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<RequestSuggestion> RequestSuggestions { get; set; }
    }
}

