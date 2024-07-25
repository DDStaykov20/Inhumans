using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomIdentity.Context
{
    /// <inheritdoc />
    public partial class UpdateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Payment",
                table: "Assignments",
                newName: "Grade");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Grade",
                table: "Assignments",
                newName: "Payment");
        }
    }
}
