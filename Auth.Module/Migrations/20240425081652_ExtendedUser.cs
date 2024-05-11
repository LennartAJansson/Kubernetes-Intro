#nullable disable

namespace Auth.Module.Migrations
{
  using Microsoft.EntityFrameworkCore.Metadata;
  using Microsoft.EntityFrameworkCore.Migrations;

  /// <inheritdoc />
  public partial class ExtendedUser : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      _ = migrationBuilder.AddColumn<string>(
          name: "Firstname",
          table: "AspNetUsers",
          type: "longtext",
          nullable: true)
          .Annotation("MySql:CharSet", "utf8mb4");

      _ = migrationBuilder.AddColumn<string>(
          name: "Lastname",
          table: "AspNetUsers",
          type: "longtext",
          nullable: true)
          .Annotation("MySql:CharSet", "utf8mb4");

      _ = migrationBuilder.AddColumn<string>(
          name: "ProfileImage",
          table: "AspNetUsers",
          type: "longtext",
          nullable: true)
          .Annotation("MySql:CharSet", "utf8mb4");

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
      _ = migrationBuilder.DropColumn(
          name: "Firstname",
          table: "AspNetUsers");

      _ = migrationBuilder.DropColumn(
          name: "Lastname",
          table: "AspNetUsers");

      _ = migrationBuilder.DropColumn(
          name: "ProfileImage",
          table: "AspNetUsers");

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