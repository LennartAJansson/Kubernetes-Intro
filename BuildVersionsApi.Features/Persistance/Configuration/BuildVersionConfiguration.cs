namespace BuildVersionsApi.Features.Persistance.Configuration;

using BuildVersionsApi.Features.Domain.Model;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class BuildVersionConfiguration
  : IEntityTypeConfiguration<BuildVersion>
{
  public void Configure(EntityTypeBuilder<BuildVersion> builder)
  {
    //Detta är samma som du kan göra i OnModelCreating i DbContext
    //Skillnaden är att här har man separation mellan entiteterna
    //En konfigurationsklass per entitet

    _ = builder.ToTable("BuildVersions");

    _ = builder.HasKey(x => x.Id);
    _ = builder.Property(x => x.Id).ValueGeneratedOnAdd();
    _ = builder.HasIndex(x => x.ProjectName);
  }
}