namespace BuildVersionsApi.Features.Persistance.Configuration;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BuildVersionsApi.Features.Model;

internal sealed class BuildVersionConfiguration : IEntityTypeConfiguration<BuildVersion>
{
  public void Configure(EntityTypeBuilder<BuildVersion> builder)
  {
    //Detta är samma som du kan göra i OnModelCreating i DbContext
    //Skillnaden är att här har man separation mellan entiteterna
    //En konfigurationsklass per entitet

    _ = builder.ToTable("BuildVersions");

    _ = builder.HasKey(x => x.Id);

    //Om man vill fylla upp lite sampledata så kan man göra så här:
    //_ = builder.HasData(
    //    new Person { Id = new Guid("96EE628C-56B2-4F06-9049-668EF56C6D0B"), Name = "Nisse Hult" },
    //    new Person { Id = new Guid("AD9137C0-83BE-40C3-AB9E-D845C23561EA"), Name = "Hasse Hansson" },
    //    new Person { Id = new Guid("0B1E1F35-5477-4E3D-A7F3-444DF85EF1E7"), Name = "Kalle Karlsson" },
    //    new Person { Id = new Guid("EF7B215F-7D19-4921-A5CB-2C19D6C049A9"), Name = "Svenne Svensson" },
    //    new Person { Id = new Guid("A3BA5544-0B26-4CA4-AC24-141BFC1D051A"), Name = "Lasse Larsson" });
  }
}