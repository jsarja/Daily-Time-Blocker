using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Planner.Domain.Entities;

namespace Planner.Infrastructure.Data.EntityFramework.Configurations
{
    public class TodoItemCategoryConfiguration : IEntityTypeConfiguration<TodoItemCategory>
    {
        public void Configure(EntityTypeBuilder<TodoItemCategory> builder)
        {
            // Set column constrictions.
            builder.Property(d=> d.TodoItemCategoryId).IsRequired().ValueGeneratedOnAdd();
            builder.Property(d=> d.Title).IsRequired().HasMaxLength(150);
            builder.Property(d=> d.Description).IsRequired();
            builder.Property(d => d.OwnerId).IsRequired();
            
            // Ignore collection reference.
            builder.Ignore(t => t.TodoItemSet);
        }
    }
}