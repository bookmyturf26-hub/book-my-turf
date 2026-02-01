using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BookMyTurfwebservices.Models.Entities;

namespace BookMyTurfwebservices.Data.Configurations;

public class RefundRequestConfiguration : IEntityTypeConfiguration<RefundRequest>
{
    public void Configure(EntityTypeBuilder<RefundRequest> builder)
    {
        builder.ToTable("RefundRequests");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(r => r.PaymentId)
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(r => r.RefundId)
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(r => r.BookingId)
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(r => r.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(r => r.RefundAmount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(r => r.Currency)
            .HasMaxLength(3)
            .HasDefaultValue("INR");

        builder.Property(r => r.Status)
            .HasMaxLength(20)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(r => r.Type)
            .HasMaxLength(10)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(r => r.Reason)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(r => r.ReasonCode)
            .HasConversion<string>();

        builder.Property(r => r.Notes)
            .HasMaxLength(1000);

        builder.Property(r => r.GatewayRefundId)
            .HasMaxLength(100);

        builder.Property(r => r.GatewayResponse)
            .HasMaxLength(2000);

        builder.Property(r => r.Speed)
            .HasMaxLength(10)
            .HasConversion<string>();

        builder.Property(r => r.InitiatedBy)
            .HasMaxLength(20)
            .HasConversion<string>();

        builder.Property(r => r.InitiatedByUser)
            .HasMaxLength(100);

        builder.Property(r => r.EstimatedSettlementDate);
        builder.Property(r => r.RefundedAt);

        // Indexes
        builder.HasIndex(r => r.PaymentId);
        builder.HasIndex(r => r.RefundId).IsUnique();
        builder.HasIndex(r => r.BookingId);
        builder.HasIndex(r => r.CreatedAt);
        builder.HasIndex(r => r.Status);
        builder.HasIndex(r => r.Type);
        builder.HasIndex(r => r.GatewayRefundId).IsUnique();
        builder.HasIndex(r => new { r.Status, r.CreatedAt });

        // Query filter for soft delete
        builder.HasQueryFilter(r => !r.IsDeleted);
    }
}