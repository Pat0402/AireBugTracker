using DatabaseContext.Models;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DatabaseContext
{
    public class BugTrackerContext : DbContext
    {
        public BugTrackerContext() : base("BugTrackerContext") { }
        public BugTrackerContext(string connectionString) : base(connectionString) { }
        public BugTrackerContext(DbConnection connection) : base(connection, true) { }

        public DbSet<Bug> Bugs { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Keep table names singular
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}