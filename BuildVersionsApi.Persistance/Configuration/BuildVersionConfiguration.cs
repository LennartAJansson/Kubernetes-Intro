namespace BuildVersionsApi.Persistance.Configuration;

using BuildVersionsApi.Domain.Model;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class BuildVersionConfiguration
  : IEntityTypeConfiguration<BuildVersion>
{
  public void Configure(EntityTypeBuilder<BuildVersion> builder)
  {
    _ = builder.ToTable("BuildVersions");

    _ = builder.HasKey(x => x.Id);
    _ = builder.Property(x => x.Id).ValueGeneratedOnAdd();
    _ = builder.HasIndex(x => x.ProjectName);
  }
}