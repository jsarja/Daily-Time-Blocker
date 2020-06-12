using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Planner.Application.Common.Models;
using Planner.Domain.Entities;

namespace Planner.Application.Common.Interfaces
{
    public interface IApplicationDbContext : IDisposable, IAsyncDisposable
    {
        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<DailyTodoItem> DailyTodoItems { get; set; }
        public DbSet<DailyTodoItemBlock> DailyTodoItemBlocks { get; set; }
        public DbSet<TodoItemCategory> TodoItemCategories  { get; set; }
        public DbSet<TodoItemCategoryJoinTable> TodoItemCategoryJoin { get; set; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}