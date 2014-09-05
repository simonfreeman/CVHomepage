using CVHomepage.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace CVHomepage.DAL
{
    public class CVContext : DbContext
    {
        public CVContext() : base("CVContext")
        {
        }

        public DbSet<Skill> Skills { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Filter> Filters { get; set; }

    }
}