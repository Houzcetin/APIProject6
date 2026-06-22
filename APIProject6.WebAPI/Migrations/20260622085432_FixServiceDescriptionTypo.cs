using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIProject6.WebAPI.Migrations
{
    public partial class FixServiceDescriptionTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descriptiom",
                table: "Services",
                newName: "Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Services",
                newName: "Descriptiom");
        }
    }
}
