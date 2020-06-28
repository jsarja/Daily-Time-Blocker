using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Planner.Application.Common.Models;
using Planner.Domain.Entities;

namespace Planner.Infrastructure.Data.EntityFramework.Configurations
{
    public class TodoItemCategoryJoinTableConfiguration : IEntityTypeConfiguration<TodoItemCategoryJoinTable>
    {
        public void Configure(EntityTypeBuilder<TodoItemCategoryJoinTable> builder)
        {
            // Configure Primary key
            builder.HasKey(t => new {t.TodoItemId, t.CategoryId});
            
            // Configure Foreign key.
            builder.HasOne<TodoItem>().WithMany().HasForeignKey(jt => jt.TodoItemId);
            builder.HasOne<TodoItemCategory>().WithMany().HasForeignKey(jt => jt.CategoryId);

            
            builder.Ignore(jt => jt.TodoItem);
            builder.Ignore(jt => jt.Category);
        }
    }
}