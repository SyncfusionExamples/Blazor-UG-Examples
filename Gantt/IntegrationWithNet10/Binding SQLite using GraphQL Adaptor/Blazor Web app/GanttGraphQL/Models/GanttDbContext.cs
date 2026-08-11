using Microsoft.EntityFrameworkCore;

namespace GanttGraphQL.Models
{
    public class GanttDbContext : DbContext
    {
        public GanttDbContext(DbContextOptions<GanttDbContext> options) : base(options) { }

        public DbSet<TaskDataModel> Tasks => Set<TaskDataModel>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskDataModel>()
                .ToTable("TaskData")
                .HasKey(t => t.TaskID);

            modelBuilder.Entity<TaskDataModel>()
                .Property(t => t.TaskID).HasColumnName("TaskID").IsRequired();

            modelBuilder.Entity<TaskDataModel>()
                .Property(t => t.TaskName).HasColumnName("TaskName").IsRequired();

            modelBuilder.Entity<TaskDataModel>()
                .Property(t => t.StartDate).HasColumnName("StartDate");

            modelBuilder.Entity<TaskDataModel>()
                .Property(t => t.EndDate).HasColumnName("EndDate");

            modelBuilder.Entity<TaskDataModel>()
                .Property(t => t.Duration).HasColumnName("Duration");

            modelBuilder.Entity<TaskDataModel>()
                .Property(t => t.Progress).HasColumnName("Progress");

            modelBuilder.Entity<TaskDataModel>()
               .Property(t => t.Predecessor).HasColumnName("Predecessor");

            modelBuilder.Entity<TaskDataModel>()
                .Property(t => t.ParentID).HasColumnName("ParentID");
        }
    }
}

