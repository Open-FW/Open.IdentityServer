using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Open.IdentityServer.Migrations.MSSQL.AppConfiguration
{
    public partial class Identity4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApiScope_ApiResource_ApiResourceId",
                table: "ApiScope");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiScopeClaim_ApiScope_ApiScopeId",
                table: "ApiScopeClaim");

            migrationBuilder.DropTable(
                name: "ApiSecret");

            migrationBuilder.DropTable(
                name: "IdentityClaim");

            migrationBuilder.DropIndex(
                name: "IX_ApiScopeClaim_ApiScopeId",
                table: "ApiScopeClaim");

            migrationBuilder.DropIndex(
                name: "IX_ApiScope_ApiResourceId",
                table: "ApiScope");

            migrationBuilder.DropColumn(
                name: "ApiScopeId",
                table: "ApiScopeClaim");

            migrationBuilder.DropColumn(
                name: "ApiResourceId",
                table: "ApiScope");

            migrationBuilder.AddColumn<string>(
                name: "AllowedIdentityTokenSigningAlgorithms",
                table: "Client",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RequireRequestObject",
                table: "Client",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ScopeId",
                table: "ApiScopeClaim",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "ApiScope",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "AllowedAccessTokenSigningAlgorithms",
                table: "ApiResource",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ShowInDiscoveryDocument",
                table: "ApiResource",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ApiResourceScope",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Scope = table.Column<string>(maxLength: 200, nullable: false),
                    ApiResourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiResourceScope", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiResourceScope_ApiResource_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalTable: "ApiResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiResourceSecret",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Value = table.Column<string>(maxLength: 4000, nullable: false),
                    Expiration = table.Column<DateTime>(nullable: true),
                    Type = table.Column<string>(maxLength: 250, nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    ApiResourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiResourceSecret", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiResourceSecret_ApiResource_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalTable: "ApiResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiScopeProperty",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(maxLength: 250, nullable: false),
                    Value = table.Column<string>(maxLength: 2000, nullable: false),
                    ScopeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiScopeProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiScopeProperty_ApiScope_ScopeId",
                        column: x => x.ScopeId,
                        principalTable: "ApiScope",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityResourceClaim",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(maxLength: 200, nullable: false),
                    IdentityResourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityResourceClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityResourceClaim_IdentityResource_IdentityResourceId",
                        column: x => x.IdentityResourceId,
                        principalTable: "IdentityResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApiScopeClaim_ScopeId",
                table: "ApiScopeClaim",
                column: "ScopeId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiResourceScope_ApiResourceId",
                table: "ApiResourceScope",
                column: "ApiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiResourceSecret_ApiResourceId",
                table: "ApiResourceSecret",
                column: "ApiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiScopeProperty_ScopeId",
                table: "ApiScopeProperty",
                column: "ScopeId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityResourceClaim_IdentityResourceId",
                table: "IdentityResourceClaim",
                column: "IdentityResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApiScopeClaim_ApiScope_ScopeId",
                table: "ApiScopeClaim",
                column: "ScopeId",
                principalTable: "ApiScope",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApiScopeClaim_ApiScope_ScopeId",
                table: "ApiScopeClaim");

            migrationBuilder.DropTable(
                name: "ApiResourceScope");

            migrationBuilder.DropTable(
                name: "ApiResourceSecret");

            migrationBuilder.DropTable(
                name: "ApiScopeProperty");

            migrationBuilder.DropTable(
                name: "IdentityResourceClaim");

            migrationBuilder.DropIndex(
                name: "IX_ApiScopeClaim_ScopeId",
                table: "ApiScopeClaim");

            migrationBuilder.DropColumn(
                name: "AllowedIdentityTokenSigningAlgorithms",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "RequireRequestObject",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "ScopeId",
                table: "ApiScopeClaim");

            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "ApiScope");

            migrationBuilder.DropColumn(
                name: "AllowedAccessTokenSigningAlgorithms",
                table: "ApiResource");

            migrationBuilder.DropColumn(
                name: "ShowInDiscoveryDocument",
                table: "ApiResource");

            migrationBuilder.AddColumn<int>(
                name: "ApiScopeId",
                table: "ApiScopeClaim",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ApiResourceId",
                table: "ApiScope",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ApiSecret",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApiResourceId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiSecret", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiSecret_ApiResource_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalTable: "ApiResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityResourceId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityClaim_IdentityResource_IdentityResourceId",
                        column: x => x.IdentityResourceId,
                        principalTable: "IdentityResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApiScopeClaim_ApiScopeId",
                table: "ApiScopeClaim",
                column: "ApiScopeId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiScope_ApiResourceId",
                table: "ApiScope",
                column: "ApiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiSecret_ApiResourceId",
                table: "ApiSecret",
                column: "ApiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityClaim_IdentityResourceId",
                table: "IdentityClaim",
                column: "IdentityResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApiScope_ApiResource_ApiResourceId",
                table: "ApiScope",
                column: "ApiResourceId",
                principalTable: "ApiResource",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApiScopeClaim_ApiScope_ApiScopeId",
                table: "ApiScopeClaim",
                column: "ApiScopeId",
                principalTable: "ApiScope",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
