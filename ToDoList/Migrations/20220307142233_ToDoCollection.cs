using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.Migrations
{
    public partial class ToDoCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ToDoCollectionId",
                table: "ToDos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ToDoCollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoCollection", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToDos_ToDoCollectionId",
                table: "ToDos",
                column: "ToDoCollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDos_ToDoCollection_ToDoCollectionId",
                table: "ToDos",
                column: "ToDoCollectionId",
                principalTable: "ToDoCollection",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDos_ToDoCollection_ToDoCollectionId",
                table: "ToDos");

            migrationBuilder.DropTable(
                name: "ToDoCollection");

            migrationBuilder.DropIndex(
                name: "IX_ToDos_ToDoCollectionId",
                table: "ToDos");

            migrationBuilder.DropColumn(
                name: "ToDoCollectionId",
                table: "ToDos");
        }
    }
}
