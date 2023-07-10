using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiProject.Delivery.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddFileExtension : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "file",
                table: "attachments");

            migrationBuilder.AddColumn<string>(
                name: "fileExtension",
                table: "attachments",
                type: "character varying(6)",
                maxLength: 6,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fileExtension",
                table: "attachments");

            migrationBuilder.AddColumn<bool>(
                name: "file",
                table: "attachments",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
