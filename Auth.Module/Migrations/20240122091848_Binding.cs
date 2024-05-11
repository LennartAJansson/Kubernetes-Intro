#nullable disable

namespace Auth.Module.Migrations
{
  using System;

  using Microsoft.EntityFrameworkCore.Migrations;

  /// <inheritdoc />
  public partial class Binding : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) => migrationBuilder.DropTable(
            name: "AuthRoleAuthUser");

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
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
          name: "IX_AuthRoleAuthUser_UsersId",
          table: "AuthRoleAuthUser",
          column: "UsersId");
    }
  }
}