using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Troupon.Catalog.Core.Domain.Entities;
using System;

namespace TR.Catalog
{
    public class DealConfiguration : IEntityTypeConfiguration<DealEntity>
    {
        public void Configure(EntityTypeBuilder<DealEntity> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Name)
                .IsRequired();
            builder.Property(d => d.Description)
                .IsRequired();
            builder.Property(d => d.Details)
                .IsRequired();
            builder.HasOne<MerchantEntity>(d=>d.Merchant)
                .WithMany(m=>m.Deals)
                .HasForeignKey(d=>d.MerchantId);

            builder.HasData(
                new DealEntity { Id = Guid.Parse("0d53c6ce-6181-42a8-8616-03f86f883112"), Name = "0", Description = "0", Details = "1",MerchantId= Guid.Parse("5e448b39-db5b-42a4-bc12-52f34dcd5c14") },
                new DealEntity { Id = Guid.NewGuid(), Name = "1", Description = "1", Details = "0", MerchantId = Guid.Parse("5e448b39-db5b-42a4-bc12-52f34dcd5c14") },
                new DealEntity { Id = Guid.NewGuid(), Name = "1", Description = "2", Details = "0", MerchantId = Guid.Parse("5e448b39-db5b-42a4-bc12-52f34dcd5c14") },
                new DealEntity { Id = Guid.NewGuid(), Name = "1", Description = "3", Details = "0", MerchantId = Guid.Parse("5e448b39-db5b-42a4-bc12-52f34dcd5c14") },
                new DealEntity { Id = Guid.NewGuid(), Name = "1", Description = "4", Details = "0", MerchantId = Guid.Parse("532e8ec2-121d-4a86-bfe2-8812c2c27232") },
                new DealEntity { Id = Guid.NewGuid(), Name = "1", Description = "5", Details = "0", MerchantId = Guid.Parse("532e8ec2-121d-4a86-bfe2-8812c2c27232") },
                new DealEntity { Id = Guid.NewGuid(), Name = "1", Description = "6", Details = "0", MerchantId = Guid.Parse("83c1dce6-97d5-4a35-afb7-4eb86577160c") },
                new DealEntity { Id = Guid.NewGuid(), Name = "1", Description = "7", Details = "1", MerchantId = Guid.Parse("83c1dce6-97d5-4a35-afb7-4eb86577160c") });
        }
    }
}
