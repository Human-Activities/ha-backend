﻿// <auto-generated />
using System;
using DAL.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(HumanActivitiesDataContext))]
    [Migration("20230117222205_InitialDatabase")]
    partial class InitialDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DAL.DataEntities.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid>("ActivityGuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("activity_guid");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<bool>("IsPrivate")
                        .HasColumnType("boolean")
                        .HasColumnName("is_private");

                    b.Property<bool>("IsTemplate")
                        .HasColumnType("boolean")
                        .HasColumnName("is_template");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_activities");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_activities_user_id");

                    b.ToTable("activities", (string)null);
                });

            modelBuilder.Entity("DAL.DataEntities.Calendar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid>("CalendarGuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("calendar_guid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_calendars");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_calendars_user_id");

                    b.ToTable("calendars", (string)null);
                });

            modelBuilder.Entity("DAL.DataEntities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ActivityId")
                        .HasColumnType("integer")
                        .HasColumnName("activity_id");

                    b.Property<Guid>("CategoryGuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("category_guid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_categories");

                    b.HasIndex("ActivityId")
                        .HasDatabaseName("ix_categories_activity_id");

                    b.ToTable("categories", (string)null);
                });

            modelBuilder.Entity("DAL.DataEntities.Cost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid>("CostGuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("cost_guid");

                    b.Property<int>("CostType")
                        .HasColumnType("integer")
                        .HasColumnName("cost_type");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<double>("Value")
                        .HasColumnType("double precision")
                        .HasColumnName("value");

                    b.HasKey("Id")
                        .HasName("pk_costs");

                    b.ToTable("costs", (string)null);
                });

            modelBuilder.Entity("DAL.DataEntities.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CalendarId")
                        .HasColumnType("integer")
                        .HasColumnName("calendar_id");

                    b.Property<int>("Day")
                        .HasColumnType("integer")
                        .HasColumnName("day");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("end_time");

                    b.Property<Guid>("EventGuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("event_guid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_time");

                    b.HasKey("Id")
                        .HasName("pk_events");

                    b.HasIndex("CalendarId")
                        .HasDatabaseName("ix_events_calendar_id");

                    b.ToTable("events", (string)null);
                });

            modelBuilder.Entity("DAL.DataEntities.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<Guid>("GroupGuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("group_guid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_groups");

                    b.ToTable("groups", (string)null);
                });

            modelBuilder.Entity("DAL.DataEntities.Section", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid>("SectionGuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("section_guid");

                    b.Property<int>("ToDoListId")
                        .HasColumnType("integer")
                        .HasColumnName("to_do_list_id");

                    b.Property<int?>("ToDoListTemplateId")
                        .HasColumnType("integer")
                        .HasColumnName("to_do_list_template_id");

                    b.HasKey("Id")
                        .HasName("pk_sections");

                    b.HasIndex("ToDoListTemplateId")
                        .HasDatabaseName("ix_sections_to_do_list_template_id");

                    b.ToTable("sections", (string)null);
                });

            modelBuilder.Entity("DAL.DataEntities.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDone")
                        .HasColumnType("boolean")
                        .HasColumnName("is_done");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Notes")
                        .HasColumnType("text")
                        .HasColumnName("notes");

                    b.Property<int>("SectionId")
                        .HasColumnType("integer")
                        .HasColumnName("section_id");

                    b.Property<Guid>("TaskGuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("task_guid");

                    b.HasKey("Id")
                        .HasName("pk_tasks");

                    b.HasIndex("SectionId")
                        .HasDatabaseName("ix_tasks_section_id");

                    b.ToTable("tasks", (string)null);
                });

            modelBuilder.Entity("DAL.DataEntities.ToDoListTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date_time");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<Guid>("ToDoListTemplateGuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("to_do_list_template_guid");

                    b.Property<int>("ToDoListType")
                        .HasColumnType("integer")
                        .HasColumnName("to_do_list_type");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_to_do_list_templates");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_to_do_list_templates_user_id");

                    b.ToTable("to_do_list_templates", (string)null);
                });

            modelBuilder.Entity("DAL.DataEntities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_of_birth");

                    b.Property<string>("LastName")
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("login");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer")
                        .HasColumnName("role_id");

                    b.Property<Guid>("UserGuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("user_guid");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_users_role_id");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("DAL.DataEntities.UserCosts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CostId")
                        .HasColumnType("integer")
                        .HasColumnName("cost_id");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_user_costs");

                    b.HasIndex("CostId")
                        .HasDatabaseName("ix_user_costs_cost_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_user_costs_user_id");

                    b.ToTable("user_costs", (string)null);
                });

            modelBuilder.Entity("DAL.DataEntities.UserGroups", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("GroupId")
                        .HasColumnType("integer")
                        .HasColumnName("group_id");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_user_groups");

                    b.HasIndex("GroupId")
                        .HasDatabaseName("ix_user_groups_group_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_user_groups_user_id");

                    b.ToTable("user_groups", (string)null);
                });

            modelBuilder.Entity("DAL.DataEntities.UserIdentity", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("salt");

                    b.Property<Guid>("UserGuid")
                        .HasColumnType("uuid")
                        .HasColumnName("user_guid");

                    b.HasKey("UserId")
                        .HasName("pk_user_identities");

                    b.ToTable("user_identities", (string)null);
                });

            modelBuilder.Entity("DAL.DataEntities.UserRefreshToken", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("token");

                    b.Property<Guid>("UserGuid")
                        .HasColumnType("uuid")
                        .HasColumnName("user_guid");

                    b.HasKey("UserId")
                        .HasName("pk_user_refresh_tokens");

                    b.ToTable("user_refresh_tokens", (string)null);
                });

            modelBuilder.Entity("DAL.DataEntities.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_user_roles");

                    b.ToTable("user_roles", (string)null);
                });

            modelBuilder.Entity("DAL.DataEntities.Activity", b =>
                {
                    b.HasOne("DAL.DataEntities.User", "User")
                        .WithMany("Activities")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_Activities_User_UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.DataEntities.Calendar", b =>
                {
                    b.HasOne("DAL.DataEntities.User", "User")
                        .WithMany("Calendars")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_Calendars_User_UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.DataEntities.Category", b =>
                {
                    b.HasOne("DAL.DataEntities.Activity", "Activity")
                        .WithMany("Categories")
                        .HasForeignKey("ActivityId")
                        .IsRequired()
                        .HasConstraintName("FK_Category_Activity_ActivityId");

                    b.Navigation("Activity");
                });

            modelBuilder.Entity("DAL.DataEntities.Event", b =>
                {
                    b.HasOne("DAL.DataEntities.Calendar", "Calendar")
                        .WithMany("Events")
                        .HasForeignKey("CalendarId")
                        .IsRequired()
                        .HasConstraintName("FK_Category_Calendar_CalendarId");

                    b.Navigation("Calendar");
                });

            modelBuilder.Entity("DAL.DataEntities.Section", b =>
                {
                    b.HasOne("DAL.DataEntities.ToDoListTemplate", null)
                        .WithMany("Sections")
                        .HasForeignKey("ToDoListTemplateId")
                        .HasConstraintName("fk_sections_to_do_list_templates_to_do_list_template_id");
                });

            modelBuilder.Entity("DAL.DataEntities.Task", b =>
                {
                    b.HasOne("DAL.DataEntities.Section", "Section")
                        .WithMany("Tasks")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_tasks_sections_section_id");

                    b.Navigation("Section");
                });

            modelBuilder.Entity("DAL.DataEntities.ToDoListTemplate", b =>
                {
                    b.HasOne("DAL.DataEntities.User", "User")
                        .WithMany("ToDoListTemplates")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_to_do_list_templates_users_user_id");

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

            modelBuilder.Entity("DAL.DataEntities.UserCosts", b =>
                {
                    b.HasOne("DAL.DataEntities.Cost", "Cost")
                        .WithMany("UserCosts")
                        .HasForeignKey("CostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_costs_costs_cost_id");

                    b.HasOne("DAL.DataEntities.User", "User")
                        .WithMany("UserCosts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_costs_users_user_id");

                    b.Navigation("Cost");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.DataEntities.UserGroups", b =>
                {
                    b.HasOne("DAL.DataEntities.Group", "Group")
                        .WithMany("UserGroups")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_groups_groups_group_id");

                    b.HasOne("DAL.DataEntities.User", "User")
                        .WithMany("UserGroups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_groups_users_user_id");

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.DataEntities.UserIdentity", b =>
                {
                    b.HasOne("DAL.DataEntities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_identities_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.DataEntities.UserRefreshToken", b =>
                {
                    b.HasOne("DAL.DataEntities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_refresh_tokens_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.DataEntities.Activity", b =>
                {
                    b.Navigation("Categories");
                });

            modelBuilder.Entity("DAL.DataEntities.Calendar", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("DAL.DataEntities.Cost", b =>
                {
                    b.Navigation("UserCosts");
                });

            modelBuilder.Entity("DAL.DataEntities.Group", b =>
                {
                    b.Navigation("UserGroups");
                });

            modelBuilder.Entity("DAL.DataEntities.Section", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("DAL.DataEntities.ToDoListTemplate", b =>
                {
                    b.Navigation("Sections");
                });

            modelBuilder.Entity("DAL.DataEntities.User", b =>
                {
                    b.Navigation("Activities");

                    b.Navigation("Calendars");

                    b.Navigation("ToDoListTemplates");

                    b.Navigation("UserCosts");

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
