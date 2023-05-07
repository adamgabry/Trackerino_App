using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trackerino.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddNavProperiesProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StarDateTime",
                table: "Activities",
                newName: "StartDateTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDateTime",
                table: "Activities",
                newName: "StarDateTime");
        }
    }
}
