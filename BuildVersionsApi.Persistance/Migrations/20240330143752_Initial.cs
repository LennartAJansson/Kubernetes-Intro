#nullable disable

namespace Persistance.Migrations
{
  using System;

  using Microsoft.EntityFrameworkCore.Metadata;
  using Microsoft.EntityFrameworkCore.Migrations;

  /// <inheritdoc />
  public partial class Initial : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      _ = migrationBuilder.AlterDatabase()
          .Annotation("MySql:CharSet", "utf8mb4");

      _ = migrationBuilder.CreateTable(
          name: "BuildVersions",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            ProjectName = table.Column<string>(type: "varchar(255)", nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            Major = table.Column<int>(type: "int", nullable: false),
            Minor = table.Column<int>(type: "int", nullable: false),
            Build = table.Column<int>(type: "int", nullable: false),
            Revision = table.Column<int>(type: "int", nullable: false),
            SemanticVersionText = table.Column<string>(type: "longtext", nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            Username = table.Column<string>(type: "longtext", nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            Created = table.Column<DateTime>(type: "datetime(6)", nullable: true),
            Changed = table.Column<DateTime>(type: "datetime(6)", nullable: true)
          },
          constraints: table =>
          {
            _ = table.PrimaryKey("PK_BuildVersions", x => x.Id);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      _ = migrationBuilder.CreateIndex(
          name: "IX_BuildVersions_ProjectName",
          table: "BuildVersions",
          column: "ProjectName");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) => migrationBuilder.DropTable(
            name: "BuildVersions");
  }
}