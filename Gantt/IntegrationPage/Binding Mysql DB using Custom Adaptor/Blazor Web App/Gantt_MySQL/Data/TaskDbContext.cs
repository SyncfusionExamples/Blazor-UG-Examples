using Microsoft.EntityFrameworkCore;

namespace Gantt_MySQL.Data
{

    /// <summary>
    /// DbContext for task_data table.
    /// Manages DB connection and entity configuration for TaskDataModel.
    /// </summary>
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// DbSet for Task entities
        /// </summary>
        public DbSet<TaskDataModel> TaskData => Set<TaskDataModel>();

        /// <summary>
        /// Configures entity mappings and constraints.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaskDataModel>(entity =>
            {
                // Table name
                entity.ToTable("task_data");

                // Primary Key
                entity.HasKey(e => e.TaskId);

                // Auto-increment for PK
                entity.Property(e => e.TaskId)
                      .ValueGeneratedOnAdd();

                // ParentId (Hierarchy)
                entity.Property(e => e.ParentId)
                      .IsRequired(false);

                // TaskName (NOT NULL, VARCHAR(50))
                entity.Property(e => e.TaskName)
                      .HasMaxLength(50)
                      .IsRequired();

                // StartDate (datetime, default CURRENT_TIMESTAMP in DB)
                entity.Property(e => e.StartDate)
                      .HasColumnType("datetime")
                      .IsRequired(false);

                // EndDate (datetime, nullable)
                entity.Property(e => e.EndDate)
                      .HasColumnType("datetime")
                      .IsRequired(false);

                // Duration (NOT NULL, VARCHAR(10))
                entity.Property(e => e.Duration)
                      .HasMaxLength(10)
                      .IsRequired();

                // Progress (NOT NULL, DECIMAL(18,2))
                entity.Property(e => e.Progress)
                      .HasPrecision(18, 2)
                      .IsRequired();

                // Helpful indexes (sample-friendly)
                entity.HasIndex(e => e.ParentId).HasDatabaseName("IX_Task_ParentId");
                entity.HasIndex(e => e.StartDate).HasDatabaseName("IX_Task_StartDate");
            });
        }
    }
}

