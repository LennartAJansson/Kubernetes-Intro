#nullable disable

namespace Persistance.Migrations
{
  using Microsoft.EntityFrameworkCore.Migrations;

  /// <inheritdoc />
  public partial class SoftDelete : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) => migrationBuilder.AddColumn<bool>(
            name: "IsDeleted",
            table: "BuildVersions",
            type: "tinyint(1)",
            nullable: false,
            defaultValue: false);

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) => migrationBuilder.DropColumn(
            name: "IsDeleted",
            table: "BuildVersions");
  }
}