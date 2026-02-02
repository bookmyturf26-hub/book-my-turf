using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BookMyTurfwebservices.Models.Entities;

namespace BookMyTurfwebservices.Data.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(p => p.BookingId)
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(p => p.OrderId)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.TransactionId)
            .HasMaxLength(100);

        builder.Property(p => p.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(p => p.Status)
            .HasMaxLength(20)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(p => p.PaymentMethod)
            .HasMaxLength(50);

        builder.Property(p => p.Currency)
            .HasMaxLength(3)
            .HasDefaultValue("INR");

        builder.Property(p => p.CustomerEmail)
            .HasMaxLength(100);

        builder.Property(p => p.CustomerPhone)
            .HasMaxLength(15);

        builder.Property(p => p.GatewayResponse)
            .HasMaxLength(2000);

        builder.Property(p => p.RefundAmount)
            .HasPrecision(18, 2);

        builder.Property(p => p.RefundReason)
            .HasMaxLength(500);

        builder.Property(p => p.Notes)
            .HasMaxLength(1000);

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.Property(p => p.PaidAt);
        builder.Property(p => p.RefundedAt);
        builder.Property(p => p.ExpiresAt);

        // Indexes
        builder.HasIndex(p => p.BookingId);
        builder.HasIndex(p => p.OrderId).IsUnique();
        builder.HasIndex(p => p.TransactionId).IsUnique();
        builder.HasIndex(p => p.CreatedAt);
        builder.HasIndex(p => p.Status);
        builder.HasIndex(p => new { p.CustomerEmail, p.CreatedAt });
        builder.HasIndex(p => p.PaidAt);

        // Relationships
        builder.HasMany(p => p.Transactions)
            .WithOne(t => t.Payment)
            .HasForeignKey(t => t.PaymentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.RefundRequests)
            .WithOne(r => r.Payment)
            .HasForeignKey(r => r.PaymentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}