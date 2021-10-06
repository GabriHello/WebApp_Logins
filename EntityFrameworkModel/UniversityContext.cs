using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Nome.EntityFrameworkModel
{
    public partial class UniversityContext : DbContext
    {
        public UniversityContext()
            : base("name=UniversityContext")
        {
        }

        public virtual DbSet<StudentsUni> StudentsUni { get; set; }
        public virtual DbSet<Subject> Subject { get; set; }
        public virtual DbSet<Teacher> Teacher { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentsUni>()
                .Property(e => e.IdNumber)
                .IsFixedLength();

            modelBuilder.Entity<StudentsUni>()
                .HasMany(e => e.Subject)
                .WithMany(e => e.StudentsUni)
                .Map(m => m.ToTable("Student_Subject").MapLeftKey("IdStudent").MapRightKey("IdSubject"));

            modelBuilder.Entity<Teacher>()
                .Property(e => e.IdNumber)
                .IsFixedLength();

            modelBuilder.Entity<Teacher>()
                .HasMany(e => e.Subject)
                .WithOptional(e => e.Teacher)
                .HasForeignKey(e => e.IdTeacher);
        }
    }
}
