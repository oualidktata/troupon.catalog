using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Troupon.Catalog.Core.Domain.Entities;
using System;

namespace Troupon.Catalog.Infra.Persistence
{
    public class ApplicationConfiguration : IEntityTypeConfiguration<Application>
    {
        public void Configure(EntityTypeBuilder<Application> builder)
        {
            builder.HasKey(v => v.Id);
            builder.Property(p => p.Name)
                .IsRequired();
            builder.HasData(
                new Application { Id = Guid.NewGuid(), Name = "AGATE" },
                new Application { Id = Guid.NewGuid(), Name = "ADP" },
                new Application { Id = Guid.NewGuid(), Name = "Facturation" }
                );
        }
    }
}
