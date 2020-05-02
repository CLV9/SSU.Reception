using System.Data.Entity;

namespace SSU.Reception.Models
{
    public class SchoolContext : DbContext
    {
        public SchoolContext() : base("DbConnection") { }

        public DbSet<School> Schools { get; set; }
    }
}