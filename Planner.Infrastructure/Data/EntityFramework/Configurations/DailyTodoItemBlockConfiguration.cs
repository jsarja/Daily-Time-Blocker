using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Planner.Domain.Entities;

namespace Planner.Infrastructure.Data.EntityFramework.Configurations
{
    public class DailyTodoItemBlockConfiguration : IEntityTypeConfiguration<DailyTodoItemBlock>
    {
        public void Configure(EntityTypeBuilder<DailyTodoItemBlock> builder)
        {
            // Configure Foreign key.
            builder.Property<int>("DTodoItemId");
            builder.HasOne(b => b.DTodoItem)
                .WithMany()
                .HasForeignKey("DTodoItemId")
                .IsRequired();
            
            // Set column constrictions.
            builder.Property(d=> d.DailyTodoItemBlockId).IsRequired().ValueGeneratedOnAdd();
            builder.Property(d=> d.StartTime).IsRequired();
            builder.Property(d=> d.EndTime).IsRequired();
            builder.Property(d=> d.IsCompleted).IsRequired().HasDefaultValue(false);

            // Ignore collection reference.
            builder.Ignore(b => b.DTodoItem);
        }
    }
}