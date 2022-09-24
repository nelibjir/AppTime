using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AppTime.Entities
{
    public class DataContext : DbContext
    {
        private readonly IDbConnectionConfiguration fConfiguration;
        private readonly ILoggerFactory fLoggerFactory;

        public DataContext(IDbConnectionConfiguration configuration, ILoggerFactory loggerFactory)
        {
            fConfiguration = configuration;
            fLoggerFactory = loggerFactory;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLoggerFactory(fLoggerFactory);
                optionsBuilder.UseSqlServer(fConfiguration.ConnectionString);
            }
        }

        public virtual DbSet<FileExtension> FileExtension { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.UserId)
                    .HasName("IX_user-user_id")
                    .IsUnique();
            });

        }
    }
}
