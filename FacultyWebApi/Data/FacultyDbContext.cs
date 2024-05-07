using FacultetApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace FacultetApi.Data
{
    public class FacultyDbContext:DbContext
    {
        public DbSet<College> Colleges { get; set; }
        public DbSet<County> Countys { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Faculty> Facultys { get; set; }
        public DbSet<FacultyStudy> FacultyStudys { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentCollege> StudentColleges { get; set; }
        public DbSet<Study> Studys { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=FacultyDb;");
        }
       

    }
}
