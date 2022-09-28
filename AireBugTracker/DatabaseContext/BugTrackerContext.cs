using DatabaseContext.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DatabaseContext
{
    public class BugTrackerContext : DbContext
    {
        public BugTrackerContext() : base("BugTrackerContext")
        {
        }

        public DbSet<Bug> Bugs { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Keep table names singular
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}