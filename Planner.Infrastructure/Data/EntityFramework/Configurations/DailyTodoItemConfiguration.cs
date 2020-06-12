using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Planner.Domain.Entities;

namespace Planner.Infrastructure.Data.EntityFramework.Configurations
{
    public class DailyTodoItemConfiguration : IEntityTypeConfiguration<DailyTodoItem>
    {
        public void Configure(EntityTypeBuilder<DailyTodoItem> builder)
        {
            // Configure Foreign key.
            builder.Property<int>("TodoItemId");
            builder.HasOne(d => d.TodoInfo)
                .WithMany()
                .HasForeignKey("TodoItemId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Set column constrictions.
            builder.Property(d=> d.DailyTodoItemId).IsRequired().ValueGeneratedOnAdd();
            builder.Property(d=> d.TodoDate).IsRequired();
            builder.Property(d=> d.TimeUsedForTodo).IsRequired().HasDefaultValue(new TimeSpan(0));

            // Ignore object reference.
            builder.Ignore(t => t.TodoInfo);
        }
    }
}