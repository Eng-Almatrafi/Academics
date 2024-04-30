using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academic.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateofficetbale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "OfficeEnabled",
                table: "Offices",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OfficeEnabled",
                table: "Offices");
        }
    }
}
