using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Troupon.Catalog.Core.Domain.Entities;
using System;

namespace relese_notes_server_dot_net
{
    public class DealConfiguration : IEntityTypeConfiguration<DealEntity>
    {
        public void Configure(EntityTypeBuilder<DealEntity> builder)
        {
            builder.HasKey(v => v.Id);
            builder.Property(p => p.Major)
                .IsRequired();
            builder.Property(p => p.Minor)
                .IsRequired();
            builder.Property(p => p.Patch)
                .IsRequired();

            builder.HasData(
                new DealEntity { Id = Guid.Parse("0d53c6ce-6181-42a8-8616-03f86f883112"), Major = "0", Minor = "0", Patch = "1" },
                new DealEntity { Id = Guid.NewGuid(), Major = "1", Minor = "1", Patch = "0" },
                new DealEntity { Id = Guid.NewGuid(), Major = "1", Minor = "2", Patch = "0" },
                new DealEntity { Id = Guid.NewGuid(), Major = "1", Minor = "3", Patch = "0" },
                new DealEntity { Id = Guid.NewGuid(), Major = "1", Minor = "4", Patch = "0" },
                new DealEntity { Id = Guid.NewGuid(), Major = "1", Minor = "5", Patch = "0" },
                new DealEntity { Id = Guid.NewGuid(), Major = "1", Minor = "6", Patch = "0" },
                new DealEntity { Id = Guid.NewGuid(), Major = "1", Minor = "7", Patch = "1" });
        }
    }
}
