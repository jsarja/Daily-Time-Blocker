﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Planner.Infrastructure.Data.EntityFramework;

namespace Planner.Infrastructure.Data.EntityFramework.Migrations
{
    [DbContext(typeof(PlannerDbContext))]
    partial class PlannerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0-preview.4.20220.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Planner.Application.Common.DbModels.TodoItemCategoryJoinTable", b =>
                {
                    b.Property<int>("TodoItemId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.HasKey("TodoItemId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("TodoItemCategoryJoin");
                });

            modelBuilder.Entity("Planner.Domain.Entities.DailyTodoItem", b =>
                {
                    b.Property<int>("DailyTodoItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<TimeSpan?>("TimeReservedForTodo")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("TimeUsedForTodo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("time")
                        .HasDefaultValue(new TimeSpan(0, 0, 0, 0, 0));

                    b.Property<DateTime>("TodoDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TodoItemId")
                        .HasColumnType("int");

                    b.HasKey("DailyTodoItemId");

                    b.HasIndex("TodoItemId");

                    b.ToTable("DailyTodoItems");
                });

            modelBuilder.Entity("Planner.Domain.Entities.DailyTodoItemBlock", b =>
                {
                    b.Property<int>("DailyTodoItemBlockId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DTodoItemId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsCompleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("DailyTodoItemBlockId");

                    b.HasIndex("DTodoItemId");

                    b.ToTable("DailyTodoItemBlocks");
                });

            modelBuilder.Entity("Planner.Domain.Entities.TodoItem", b =>
                {
                    b.Property<int>("TodoItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsUserFavorite")
                        .HasColumnType("bit");

                    b.Property<string>("Owner")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.HasKey("TodoItemId");

                    b.ToTable("TodoItems");
                });

            modelBuilder.Entity("Planner.Domain.Entities.TodoItemCategory", b =>
                {
                    b.Property<int>("TodoItemCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Owner")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.HasKey("TodoItemCategoryId");

                    b.ToTable("TodoItemCategories");
                });

            modelBuilder.Entity("Planner.Application.Common.DbModels.TodoItemCategoryJoinTable", b =>
                {
                    b.HasOne("Planner.Domain.Entities.TodoItemCategory", null)
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Planner.Domain.Entities.TodoItem", null)
                        .WithMany()
                        .HasForeignKey("TodoItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Planner.Domain.Entities.DailyTodoItem", b =>
                {
                    b.HasOne("Planner.Domain.Entities.TodoItem", null)
                        .WithMany()
                        .HasForeignKey("TodoItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Planner.Domain.Entities.DailyTodoItemBlock", b =>
                {
                    b.HasOne("Planner.Domain.Entities.DailyTodoItem", null)
                        .WithMany()
                        .HasForeignKey("DTodoItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
