using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagerDuplicate.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedstatustoToDoTaskModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ToDoTask",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "ToDoTask");
        }
    }
}
