using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Planner.Domain.Entities;

namespace Planner.Infrastructure.Data.EntityFramework.Configurations
{
    public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
    {
        public void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            // Set column constrictions.
            builder.Property(t => t.TodoItemId).IsRequired().ValueGeneratedOnAdd();
            builder.Property(t=> t.Title).IsRequired().HasMaxLength(150);
            builder.Property(t=> t.OwnerId).IsRequired();
            builder.Property(t=> t.Description).IsRequired();
            builder.Property(t=> t.IsUserFavorite).IsRequired();
            
            // Ignore collection reference.
            builder.Ignore(t => t.CategorySet);
        }
    }
}