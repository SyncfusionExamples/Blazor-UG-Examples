using Microsoft.EntityFrameworkCore;

namespace GanttMySql.Data
{
    /// <summary>
    /// DbContext for task_data table.
    /// Manages DB connection and entity configuration for TaskDataModel.
    /// </summary>
    public class TaskDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TaskDbContext"/>.
        /// </summary>
        /// <param name="options">The options for this context.</param>
        public TaskDbContext(DbContextOptions<TaskDbContext> options)
            : base(options)
        {
        }

         /// <summary>
        /// Gets the <see cref="DbSet{TaskDataModel}"/> representing the task_data table.
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

                // Auto-increment for Primary Key
                entity.Property(e => e.TaskId)
                      .ValueGeneratedOnAdd();

                // TaskName (NOT NULL, VARCHAR(50))
                entity.Property(e => e.TaskName)
                      .HasMaxLength(50)
                      .IsRequired();

                // StartDate (DATETIME, nullable)
                entity.Property(e => e.StartDate)
                      .HasColumnType("datetime")
                      .IsRequired(false);

                // EndDate (DATETIME, nullable)
                entity.Property(e => e.EndDate)
                      .HasColumnType("datetime")
                      .IsRequired(false);

                // ParentId (INT, nullable)
                entity.Property(e => e.ParentId)
                      .IsRequired(false);

                // Predecessor (VARCHAR(100), nullable)
                entity.Property(e => e.Predecessor)
                      .HasMaxLength(100)
                      .IsRequired(false);

                // Duration (NOT NULL, VARCHAR(10))
                entity.Property(e => e.Duration)
                      .HasMaxLength(10)
                      .IsRequired();

                // Progress (NOT NULL, INT)
                entity.Property(e => e.Progress)
                      .HasColumnType("int")
                      .IsRequired();

                // Helpful indexes
                entity.HasIndex(e => e.ParentId).HasDatabaseName("IX_Task_ParentId");
                entity.HasIndex(e => e.StartDate).HasDatabaseName("IX_Task_StartDate");
            });
        }
    }
}

