using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rad3012223.ClubData.ClassLibrary.Week5.Models
{
    public class Week5ClubContext :DbContext
    {
        public DbSet<Club> Club { get; set; }
        public DbSet<Member> Member { get; set; }
        public DbSet<Student> Student { get; set; }

        public Week5ClubContext()
            : base("name=Week52223Connection")
        {
        }

        public static Week5ClubContext Create()
        {
            return new Week5ClubContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
