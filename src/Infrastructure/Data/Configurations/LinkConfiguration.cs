﻿using LinkIo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkIo.Infrastructure.Data.Configurations;
public class LinkConfiguration : IEntityTypeConfiguration<Link>
{
    public void Configure(EntityTypeBuilder<Link> builder)
    {
        builder.Property(x => x.ShortUrlCode)
            .HasMaxLength(15);
        builder.HasIndex(x => x.ShortUrlCode)
            .IsUnique();
    }
}
