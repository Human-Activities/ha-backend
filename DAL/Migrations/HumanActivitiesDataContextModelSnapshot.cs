﻿// <auto-generated />
using System;
using DAL.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(HumanActivitiesDataContext))]
    partial class HumanActivitiesDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DAL.DataEntities.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid>("ActivityGuid")
                        .HasColumnType("uuid");

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int?>("GroupId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("DAL.DataEntities.Bill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountBillNumber")
                        .HasColumnType("integer");

                    b.Property<Guid>("BillGuid")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<int?>("GroupId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("TotalValue")
                        .HasColumnType("double precision");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("DAL.DataEntities.BillItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BillId")
                        .HasColumnType("integer");

                    b.Property<Guid>("BillItemGuid")
                        .HasColumnType("uuid");

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("TotalValue")
                        .HasColumnType("double precision");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BillId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("BillItem");
                });

            modelBuilder.Entity("DAL.DataEntities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsActivityCategory")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("Value")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("DAL.DataEntities.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<Guid>("GroupGuid")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("DAL.DataEntities.Section", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("SectionGuid")
                        .HasColumnType("uuid");

                    b.Property<int>("ToDoListId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ToDoListId");

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("DAL.DataEntities.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDone")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Notes")
                        .HasColumnType("text");

                    b.Property<int>("Priority")
                        .HasColumnType("integer");

                    b.Property<int>("SectionId")
                        .HasColumnType("integer");

                    b.Property<Guid>("TaskGuid")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SectionId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("DAL.DataEntities.ToDoList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int?>("GroupId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsFavourite")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ToDoListGuid")
                        .HasColumnType("uuid");

                    b.Property<int>("ToDoListType")
                        .HasColumnType("integer");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("ToDoList");
                });

            modelBuilder.Entity("DAL.DataEntities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserGuid")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DAL.DataEntities.UserGroups", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("GroupId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("UserGroups");
                });

            modelBuilder.Entity("DAL.DataEntities.UserIdentity", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserGuid")
                        .HasColumnType("uuid");

                    b.HasKey("UserId");

                    b.ToTable("UserIdentities");
                });

            modelBuilder.Entity("DAL.DataEntities.UserRefreshToken", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserGuid")
                        .HasColumnType("uuid");

                    b.HasKey("UserId");

                    b.ToTable("UserRefreshTokens");
                });

            modelBuilder.Entity("DAL.DataEntities.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("DAL.DataEntities.Activity", b =>
                {
                    b.HasOne("DAL.DataEntities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.DataEntities.Group", "Group")
                        .WithMany("Activities")
                        .HasForeignKey("GroupId");

                    b.HasOne("DAL.DataEntities.User", "User")
                        .WithMany("Activities")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_Activities_User_UserId");

                    b.Navigation("Category");

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.DataEntities.Bill", b =>
                {
                    b.HasOne("DAL.DataEntities.Group", "Group")
                        .WithMany("Bills")
                        .HasForeignKey("GroupId");

                    b.HasOne("DAL.DataEntities.User", "User")
                        .WithMany("Bills")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.DataEntities.BillItem", b =>
                {
                    b.HasOne("DAL.DataEntities.Bill", "Bill")
                        .WithMany("BillItems")
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.DataEntities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.DataEntities.User", "User")
                        .WithMany("BillItems")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bill");

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.DataEntities.Section", b =>
                {
                    b.HasOne("DAL.DataEntities.ToDoList", null)
                        .WithMany("Sections")
                        .HasForeignKey("ToDoListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DAL.DataEntities.Task", b =>
                {
                    b.HasOne("DAL.DataEntities.Section", "Section")
                        .WithMany("Tasks")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Section");
                });

            modelBuilder.Entity("DAL.DataEntities.ToDoList", b =>
                {
                    b.HasOne("DAL.DataEntities.Group", "Group")
                        .WithMany("ToDoLists")
                        .HasForeignKey("GroupId");

                    b.HasOne("DAL.DataEntities.User", "User")
                        .WithMany("ToDoLists")
                        .HasForeignKey("UserId");

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.DataEntities.User", b =>
                {
                    b.HasOne("DAL.DataEntities.UserRole", "UserRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .IsRequired()
                        .HasConstraintName("FK_user_user_role_id");

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("DAL.DataEntities.UserGroups", b =>
                {
                    b.HasOne("DAL.DataEntities.Group", "Group")
                        .WithMany("UserGroups")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.DataEntities.User", "User")
                        .WithMany("UserGroups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.DataEntities.UserIdentity", b =>
                {
                    b.HasOne("DAL.DataEntities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.DataEntities.UserRefreshToken", b =>
                {
                    b.HasOne("DAL.DataEntities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.DataEntities.Bill", b =>
                {
                    b.Navigation("BillItems");
                });

            modelBuilder.Entity("DAL.DataEntities.Group", b =>
                {
                    b.Navigation("Activities");

                    b.Navigation("Bills");

                    b.Navigation("ToDoLists");

                    b.Navigation("UserGroups");
                });

            modelBuilder.Entity("DAL.DataEntities.Section", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("DAL.DataEntities.ToDoList", b =>
                {
                    b.Navigation("Sections");
                });

            modelBuilder.Entity("DAL.DataEntities.User", b =>
                {
                    b.Navigation("Activities");

                    b.Navigation("BillItems");

                    b.Navigation("Bills");

                    b.Navigation("ToDoLists");

                    b.Navigation("UserGroups");
                });

            modelBuilder.Entity("DAL.DataEntities.UserRole", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
