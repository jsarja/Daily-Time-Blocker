using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Planner.Infrastructure.Data.EntityFramework.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TodoItemCategories",
                columns: table => new
                {
                    TodoItemCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItemCategories", x => x.TodoItemCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "TodoItems",
                columns: table => new
                {
                    TodoItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsUserFavorite = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItems", x => x.TodoItemId);
                });

            migrationBuilder.CreateTable(
                name: "DailyTodoItems",
                columns: table => new
                {
                    DailyTodoItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TodoDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeUsedForTodo = table.Column<TimeSpan>(type: "time", nullable: false, defaultValue: new TimeSpan(0, 0, 0, 0, 0)),
                    TimeReservedForTodo = table.Column<TimeSpan>(type: "time", nullable: true),
                    TodoItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyTodoItems", x => x.DailyTodoItemId);
                    table.ForeignKey(
                        name: "FK_DailyTodoItems_TodoItems_TodoItemId",
                        column: x => x.TodoItemId,
                        principalTable: "TodoItems",
                        principalColumn: "TodoItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TodoItemCategoryJoin",
                columns: table => new
                {
                    TodoItemId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItemCategoryJoin", x => new { x.TodoItemId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_TodoItemCategoryJoin_TodoItemCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "TodoItemCategories",
                        principalColumn: "TodoItemCategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TodoItemCategoryJoin_TodoItems_TodoItemId",
                        column: x => x.TodoItemId,
                        principalTable: "TodoItems",
                        principalColumn: "TodoItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DailyTodoItemBlocks",
                columns: table => new
                {
                    DailyTodoItemBlockId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DTodoItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyTodoItemBlocks", x => x.DailyTodoItemBlockId);
                    table.ForeignKey(
                        name: "FK_DailyTodoItemBlocks_DailyTodoItems_DTodoItemId",
                        column: x => x.DTodoItemId,
                        principalTable: "DailyTodoItems",
                        principalColumn: "DailyTodoItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyTodoItemBlocks_DTodoItemId",
                table: "DailyTodoItemBlocks",
                column: "DTodoItemId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyTodoItems_TodoItemId",
                table: "DailyTodoItems",
                column: "TodoItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TodoItemCategoryJoin_CategoryId",
                table: "TodoItemCategoryJoin",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyTodoItemBlocks");

            migrationBuilder.DropTable(
                name: "TodoItemCategoryJoin");

            migrationBuilder.DropTable(
                name: "DailyTodoItems");

            migrationBuilder.DropTable(
                name: "TodoItemCategories");

            migrationBuilder.DropTable(
                name: "TodoItems");
        }
    }
}
