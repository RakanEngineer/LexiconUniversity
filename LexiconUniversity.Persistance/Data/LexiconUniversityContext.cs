using LexiconUniversity.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiconUniversity.Persistance.Data
{
    public class LexiconUniversityContext : DbContext
    {        
        public LexiconUniversityContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; } = default!;
        public DbSet<Address> Addresses { get; set; } = default!;
        public DbSet<Course> Courses { get; set; } = default!;

        public DbSet<Enrollment> Enrollments { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Enrollment>().HasKey(e => new { e.CourseId, e.StudentId });

            modelBuilder.Entity<Student>()
               .HasMany(s => s.Courses)
               .WithMany(c => c.Students)
               .UsingEntity<Enrollment>(
               e => e.HasOne(e => e.Course).WithMany(c => c.Enrollments),
               e => e.HasOne(e => e.Student).WithMany(s => s.Enrollments),
               e => e.HasKey(e => new { e.CourseId, e.StudentId }));
        }
    }
}
