using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NextHouse.Domain.Entities.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Persistence.Configurations
{
    public class PropertyRequestConfiguration
    : IEntityTypeConfiguration<PropertyRequest>
    {
        public void Configure(EntityTypeBuilder<PropertyRequest> builder)
        {
            builder.ToTable("PropertyRequests");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Message)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(x => x.Status)
                .HasConversion<string>();

            builder.HasOne(x => x.Property)
                .WithMany(x => x.Requests)
                .HasForeignKey(x => x.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Tenant)
                .WithMany(x => x.Requests)
                .HasForeignKey(x => x.TenantId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
