using System.Data.Entity;
using GMS.Domian.APIMS.Entities;

namespace GMS.Domian.APIMS.Concrete
{
    public class EFAPIMSDbContext : DbContext
    {
        public DbSet<APIType> APITypes { get; set; }

        public DbSet<APIClassification> APIClassifications { get; set; }

        public DbSet<APIItem> APIItems { get; set; }

        public DbSet<InputParameter> InputParameters { get; set; }

        public DbSet<Result> Results { get; set; }

        public DbSet<CustomerClass> CustomerClasses { get; set; }

        public DbSet<ClassProperty> ClassProperties { get; set; }
    }
}
