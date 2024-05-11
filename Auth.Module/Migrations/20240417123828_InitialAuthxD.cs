#nullable disable

namespace Auth.Module.Migrations
{
  using Microsoft.EntityFrameworkCore.Metadata;
  using Microsoft.EntityFrameworkCore.Migrations;

  /// <inheritdoc />
  public partial class InitialAuthxD : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      _ = migrationBuilder.AlterColumn<int>(
          name: "Id",
          table: "AspNetUserClaims",
          type: "int",
          nullable: false,
          oldClrType: typeof(int),
          oldType: "int")
          .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

      _ = migrationBuilder.AlterColumn<int>(
          name: "Id",
          table: "AspNetRoleClaims",
          type: "int",
          nullable: false,
          oldClrType: typeof(int),
          oldType: "int")
          .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      _ = migrationBuilder.AlterColumn<int>(
          name: "Id",
          table: "AspNetUserClaims",
          type: "int",
          nullable: false,
          oldClrType: typeof(int),
          oldType: "int")
          .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

      _ = migrationBuilder.AlterColumn<int>(
          name: "Id",
          table: "AspNetRoleClaims",
          type: "int",
          nullable: false,
          oldClrType: typeof(int),
          oldType: "int")
          .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);
    }
  }
}