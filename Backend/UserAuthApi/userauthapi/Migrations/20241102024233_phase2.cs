using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace userauthapi.Migrations
{
    /// <inheritdoc />
    public partial class phase2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConsumerName",
                table: "Consumers");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Consumers",
                newName: "ConsumerEmail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ConsumerEmail",
                table: "Consumers",
                newName: "UserName");

            migrationBuilder.AddColumn<string>(
                name: "ConsumerName",
                table: "Consumers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
