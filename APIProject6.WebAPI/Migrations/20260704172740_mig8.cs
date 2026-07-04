using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIProject6.WebAPI.Migrations
{
    public partial class mig8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTask_Chefs_ChefId",
                table: "EmployeeTask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeTask",
                table: "EmployeeTask");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeTask_ChefId",
                table: "EmployeeTask");

            migrationBuilder.DropColumn(
                name: "ChefId",
                table: "EmployeeTask");

            migrationBuilder.RenameTable(
                name: "EmployeeTask",
                newName: "EmployeeTasks");

            migrationBuilder.AlterColumn<string>(
                name: "TaskName",
                table: "EmployeeTasks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeTasks",
                table: "EmployeeTasks",
                column: "EmployeeTaskId");

            migrationBuilder.CreateTable(
                name: "EmployeeTaskChefs",
                columns: table => new
                {
                    EmployeeTaskId = table.Column<int>(type: "int", nullable: false),
                    ChefId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTaskChefs", x => new { x.EmployeeTaskId, x.ChefId });
                    table.ForeignKey(
                        name: "FK_EmployeeTaskChefs_Chefs_ChefId",
                        column: x => x.ChefId,
                        principalTable: "Chefs",
                        principalColumn: "ChefId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeTaskChefs_EmployeeTasks_EmployeeTaskId",
                        column: x => x.EmployeeTaskId,
                        principalTable: "EmployeeTasks",
                        principalColumn: "EmployeeTaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTaskChefs_ChefId",
                table: "EmployeeTaskChefs",
                column: "ChefId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeTaskChefs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeTasks",
                table: "EmployeeTasks");

            migrationBuilder.RenameTable(
                name: "EmployeeTasks",
                newName: "EmployeeTask");

            migrationBuilder.AlterColumn<int>(
                name: "TaskName",
                table: "EmployeeTask",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ChefId",
                table: "EmployeeTask",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeTask",
                table: "EmployeeTask",
                column: "EmployeeTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTask_ChefId",
                table: "EmployeeTask",
                column: "ChefId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTask_Chefs_ChefId",
                table: "EmployeeTask",
                column: "ChefId",
                principalTable: "Chefs",
                principalColumn: "ChefId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
