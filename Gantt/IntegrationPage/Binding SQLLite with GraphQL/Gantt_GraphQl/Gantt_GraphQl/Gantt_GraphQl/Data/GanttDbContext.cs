using Gantt_GraphQl.Model;
using Microsoft.EntityFrameworkCore;

namespace Gantt_GraphQl.Data
{
    public class GanttDbContext : DbContext
    {
        public GanttDbContext(DbContextOptions<GanttDbContext> options) : base(options) { }

        public DbSet<TaskEntity> Tasks => Set<TaskEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskEntity>()
                .ToTable("task_data")
                .HasKey(t => t.TaskId);

            modelBuilder.Entity<TaskEntity>()
                .Property(t => t.TaskId).HasColumnName("TaskId");

            modelBuilder.Entity<TaskEntity>()
                .Property(t => t.TaskName).HasColumnName("TaskName").IsRequired();

            modelBuilder.Entity<TaskEntity>()
                .Property(t => t.StartDate).HasColumnName("StartDate");

            modelBuilder.Entity<TaskEntity>()
                .Property(t => t.EndDate).HasColumnName("EndDate");

            modelBuilder.Entity<TaskEntity>()
                .Property(t => t.ParentId).HasColumnName("ParentId");

            modelBuilder.Entity<TaskEntity>()
                .Property(t => t.Duration).HasColumnName("Duration").IsRequired();

            modelBuilder.Entity<TaskEntity>()
                .Property(t => t.Progress).HasColumnName("Progress").IsRequired();
        }
    }
}


