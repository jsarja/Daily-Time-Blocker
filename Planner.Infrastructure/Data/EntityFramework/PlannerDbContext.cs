using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Planner.Application.Common.Models;
using Planner.Application.Common.Interfaces;
using Planner.Domain.Entities;

namespace Planner.Infrastructure.Data.EntityFramework
{
    public class PlannerDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<DailyTodoItem> DailyTodoItems { get; set; }
        public DbSet<DailyTodoItemBlock> DailyTodoItemBlocks { get; set; }
        public DbSet<TodoItemCategory> TodoItemCategories  { get; set; }
        public DbSet<TodoItemCategoryJoinTable> TodoItemCategoryJoin { get; set; }

        public PlannerDbContext(DbContextOptions<PlannerDbContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // https://www.learnentityframeworkcore.com/configuration/fluent-api
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        
        // Creating/updating database:
        // cd Planner.Infrastructure
        // dotnet ef --startup-project ../Planner.BlazorServer/ migrations add InitialMigration -o EntityFramework/Migrations
        // dotnet ef --startup-project ../Planner.BlazorServer/ database update
        
    }
}