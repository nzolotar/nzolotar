using Microsoft.EntityFrameworkCore;

namespace DeduplicationStore.Data;

public sealed class DeduplicationDbContext : DbContext
{
    public DeduplicationDbContext(DbContextOptions<DeduplicationDbContext> options)
        : base(options) { }

    public DbSet<ProcessedMessage> ProcessedMessages => Set<ProcessedMessage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProcessedMessage>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.MessageId)
                  .IsRequired()
                  .HasMaxLength(512);

            // This unique constraint is the database-level deduplication guarantee.
            // Inserting a duplicate MessageId will throw a DbUpdateException.
            entity.HasIndex(e => e.MessageId)
                  .IsUnique()
                  .HasDatabaseName("IX_ProcessedMessages_MessageId_Unique");

            entity.Property(e => e.ProcessedAt)
                  .IsRequired();
        });
    }
}
