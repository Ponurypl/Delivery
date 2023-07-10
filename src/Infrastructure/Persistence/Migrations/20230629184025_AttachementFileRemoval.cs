using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiProject.Delivery.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AttachementFileRemoval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "payload",
                table: "attachments");

            migrationBuilder.AddColumn<bool>(
                name: "file",
                table: "attachments",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "file",
                table: "attachments");

            migrationBuilder.AddColumn<byte[]>(
                name: "payload",
                table: "attachments",
                type: "bytea",
                nullable: true);
        }
    }
}
