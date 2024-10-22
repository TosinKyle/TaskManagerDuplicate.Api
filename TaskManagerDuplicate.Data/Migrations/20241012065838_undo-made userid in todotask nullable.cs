using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagerDuplicate.Data.Migrations
{
    /// <inheritdoc />
    public partial class undomadeuseridintodotasknullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoTask_User_UserId",
                table: "ToDoTask");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ToDoTask",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoTask_User_UserId",
                table: "ToDoTask",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoTask_User_UserId",
                table: "ToDoTask");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ToDoTask",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoTask_User_UserId",
                table: "ToDoTask",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
