using LinkIo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkIo.Infrastructure.Data.Configurations;
public class LinkReferrerConfiguration : IEntityTypeConfiguration<LinkReferrer>
{
    public void Configure(EntityTypeBuilder<LinkReferrer> builder)
    {
        builder.HasOne(e => e.Link)
            .WithMany(e => e.Referrers);
    }
}
