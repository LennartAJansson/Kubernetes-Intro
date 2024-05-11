#nullable disable

namespace Auth.Module.Migrations
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
          name: "AspNetRoles",
          columns: table => new
          {
            Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
            Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4")
          },
          constraints: table =>
          {
            _ = table.PrimaryKey("PK_AspNetRoles", x => x.Id);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      _ = migrationBuilder.CreateTable(
          name: "AspNetUsers",
          columns: table => new
          {
            Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
            UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
            PasswordHash = table.Column<string>(type: "longtext", nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            SecurityStamp = table.Column<string>(type: "longtext", nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            PhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
            TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
            LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
            LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
            AccessFailedCount = table.Column<int>(type: "int", nullable: false)
          },
          constraints: table =>
          {
            _ = table.PrimaryKey("PK_AspNetUsers", x => x.Id);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      _ = migrationBuilder.CreateTable(
          name: "AspNetRoleClaims",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            RoleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
            ClaimType = table.Column<string>(type: "longtext", nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4")
          },
          constraints: table =>
          {
            _ = table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
            _ = table.ForeignKey(
                      name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                      column: x => x.RoleId,
                      principalTable: "AspNetRoles",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      _ = migrationBuilder.CreateTable(
          name: "AspNetUserClaims",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
            ClaimType = table.Column<string>(type: "longtext", nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4")
          },
          constraints: table =>
          {
            _ = table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
            _ = table.ForeignKey(
                      name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                      column: x => x.UserId,
                      principalTable: "AspNetUsers",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      _ = migrationBuilder.CreateTable(
          name: "AspNetUserLogins",
          columns: table => new
          {
            LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
          },
          constraints: table =>
          {
            _ = table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
            _ = table.ForeignKey(
                      name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                      column: x => x.UserId,
                      principalTable: "AspNetUsers",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      _ = migrationBuilder.CreateTable(
          name: "AspNetUserRoles",
          columns: table => new
          {
            UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
            RoleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
          },
          constraints: table =>
          {
            _ = table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
            _ = table.ForeignKey(
                      name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                      column: x => x.RoleId,
                      principalTable: "AspNetRoles",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
            _ = table.ForeignKey(
                      name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                      column: x => x.UserId,
                      principalTable: "AspNetUsers",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      _ = migrationBuilder.CreateTable(
          name: "AspNetUserTokens",
          columns: table => new
          {
            UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
            LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            Name = table.Column<string>(type: "varchar(255)", nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            Value = table.Column<string>(type: "longtext", nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4")
          },
          constraints: table =>
          {
            _ = table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
            _ = table.ForeignKey(
                      name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                      column: x => x.UserId,
                      principalTable: "AspNetUsers",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      _ = migrationBuilder.CreateTable(
          name: "AuthRoleAuthUser",
          columns: table => new
          {
            RolesId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
            UsersId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
          },
          constraints: table =>
          {
            _ = table.PrimaryKey("PK_AuthRoleAuthUser", x => new { x.RolesId, x.UsersId });
            _ = table.ForeignKey(
                      name: "FK_AuthRoleAuthUser_AspNetRoles_RolesId",
                      column: x => x.RolesId,
                      principalTable: "AspNetRoles",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
            _ = table.ForeignKey(
                      name: "FK_AuthRoleAuthUser_AspNetUsers_UsersId",
                      column: x => x.UsersId,
                      principalTable: "AspNetUsers",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      _ = migrationBuilder.CreateIndex(
          name: "IX_AspNetRoleClaims_RoleId",
          table: "AspNetRoleClaims",
          column: "RoleId");

      _ = migrationBuilder.CreateIndex(
          name: "RoleNameIndex",
          table: "AspNetRoles",
          column: "NormalizedName",
          unique: true);

      _ = migrationBuilder.CreateIndex(
          name: "IX_AspNetUserClaims_UserId",
          table: "AspNetUserClaims",
          column: "UserId");

      _ = migrationBuilder.CreateIndex(
          name: "IX_AspNetUserLogins_UserId",
          table: "AspNetUserLogins",
          column: "UserId");

      _ = migrationBuilder.CreateIndex(
          name: "IX_AspNetUserRoles_RoleId",
          table: "AspNetUserRoles",
          column: "RoleId");

      _ = migrationBuilder.CreateIndex(
          name: "EmailIndex",
          table: "AspNetUsers",
          column: "NormalizedEmail");

      _ = migrationBuilder.CreateIndex(
          name: "UserNameIndex",
          table: "AspNetUsers",
          column: "NormalizedUserName",
          unique: true);

      _ = migrationBuilder.CreateIndex(
          name: "IX_AuthRoleAuthUser_UsersId",
          table: "AuthRoleAuthUser",
          column: "UsersId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      _ = migrationBuilder.DropTable(
          name: "AspNetRoleClaims");

      _ = migrationBuilder.DropTable(
          name: "AspNetUserClaims");

      _ = migrationBuilder.DropTable(
          name: "AspNetUserLogins");

      _ = migrationBuilder.DropTable(
          name: "AspNetUserRoles");

      _ = migrationBuilder.DropTable(
          name: "AspNetUserTokens");

      _ = migrationBuilder.DropTable(
          name: "AuthRoleAuthUser");

      _ = migrationBuilder.DropTable(
          name: "AspNetRoles");

      _ = migrationBuilder.DropTable(
          name: "AspNetUsers");
    }
  }
}