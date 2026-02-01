using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BookMyTurfwebservices.Models.Entities;

namespace BookMyTurfwebservices.Data.Configurations;

public class PaymentTransactionConfiguration : IEntityTypeConfiguration<PaymentTransaction>
{
    public void Configure(EntityTypeBuilder<PaymentTransaction> builder)
    {
        builder.ToTable("PaymentTransactions");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(t => t.PaymentId)
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(t => t.TransactionType)
            .HasMaxLength(20)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(t => t.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(t => t.Status)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(t => t.GatewayTransactionId)
            .HasMaxLength(100);

        builder.Property(t => t.GatewayResponse)
            .HasMaxLength(2000);

        builder.Property(t => t.Notes)
            .HasMaxLength(1000);

        builder.Property(t => t.CreatedAt)
            .IsRequired();

        builder.Property(t => t.ProcessedAt);

        // Indexes
        builder.HasIndex(t => t.PaymentId);
        builder.HasIndex(t => t.GatewayTransactionId).IsUnique();
        builder.HasIndex(t => t.CreatedAt);
        builder.HasIndex(t => t.TransactionType);
        builder.HasIndex(t => t.Status);
        builder.HasIndex(t => new { t.PaymentId, t.CreatedAt });
    }
}