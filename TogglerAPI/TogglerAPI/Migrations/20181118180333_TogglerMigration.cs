using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace TogglerAPI.Migrations
{
    public partial class TogglerMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(nullable: false),
                    Username = table.Column<string>(maxLength: 32, nullable: false),
                    Password = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey("FK_Users_RoleId", x => x.RoleId, "Roles", "RoleId", onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ServiceId = table.Column<Guid>(nullable: false, defaultValueSql: "newsequentialid()"),
                    Name = table.Column<string>(nullable: false),
                    Version = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceId);
                });

            migrationBuilder.CreateTable(
                name: "Toggles",
                columns: table => new
                {
                    ToggleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Toggles", x => x.ToggleId);
                });

            migrationBuilder.CreateTable(
                name: "ToggleServicePermissions",
                columns: table => new
                {
                    ToggleId = table.Column<int>(nullable: false),
                    ServiceId = table.Column<int>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    OverridenValue = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToggleServicePermissions_ToggleIdServiceId", x => new { x.ToggleId, x.ServiceId });
                    table.ForeignKey("FK_ToggleServicePermissions_ToggleId", x => x.ToggleId, "Toggles", "ToggleId", onDelete: ReferentialAction.Cascade);
                    table.ForeignKey("FK_ToggleServicePermissions_ServiceId", x => x.ServiceId, "Services", "ServiceId", onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "ToggleServicePermissions");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Toggles");
        }
    }
}