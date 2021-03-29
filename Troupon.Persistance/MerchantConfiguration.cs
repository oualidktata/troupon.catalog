using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Troupon.Catalog.Core.Domain.Entities;
using System;

namespace Troupon.Catalog.Infra.Persistence
{
    public class MerchantConfiguration : IEntityTypeConfiguration<MerchantEntity>
    {
        public void Configure(EntityTypeBuilder<MerchantEntity> builder)
        {
            builder.HasKey(v => v.Id);
            builder.Property(p => p.Name)
                .IsRequired();
            builder.HasData(
                new MerchantEntity { Id = Guid.Parse("5e448b39-db5b-42a4-bc12-52f34dcd5c14"), Name = "Awsome Goods Plus",ImageUri= "https://picsum.photos/id/1023/200/300" },
                new MerchantEntity { Id = Guid.Parse("532e8ec2-121d-4a86-bfe2-8812c2c27232"), Name = "Masso Relax Inc" , ImageUri = "https://picsum.photos/id/1003/200/300" },
                new MerchantEntity { Id = Guid.Parse("83c1dce6-97d5-4a35-afb7-4eb86577160c"), Name = "Antirouille la magouille", ImageUri = "https://picsum.photos/id/1012/200/300" },
                new MerchantEntity { Id = Guid.Parse("042038de-1e60-427d-bdce-7d683ffc8bf5"), Name = "Bronsage & Debosselage Reuni", ImageUri = "https://picsum.photos/id/1011/200/300" }
                );
        }
    }
}
