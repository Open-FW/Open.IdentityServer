using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Open.IdentityServer.Migrations.PostgreSQL.AppPersistedGrant
{
    public partial class Identity4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ConsumedTime",
                table: "PersistedGrant",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PersistedGrant",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SessionId",
                table: "PersistedGrant",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "DeviceFlowCodes",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SessionId",
                table: "DeviceFlowCodes",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrant_SubjectId_SessionId_Type",
                table: "PersistedGrant",
                columns: new[] { "SubjectId", "SessionId", "Type" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PersistedGrant_SubjectId_SessionId_Type",
                table: "PersistedGrant");

            migrationBuilder.DropColumn(
                name: "ConsumedTime",
                table: "PersistedGrant");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "PersistedGrant");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "PersistedGrant");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "DeviceFlowCodes");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "DeviceFlowCodes");
        }
    }
}
