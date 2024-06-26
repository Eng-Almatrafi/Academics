﻿// <auto-generated />
using System;
using Academic.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Academic.DataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240323195336_update-officetbale")]
    partial class updateofficetbale
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Academics.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR");

                    b.Property<int>("HoursToComplete")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasPrecision(15, 2)
                        .HasColumnType("decimal(15,2)");

                    b.HasKey("Id");

                    b.ToTable("Courses", (string)null);
                });

            modelBuilder.Entity("Academics.Models.Enrollment", b =>
                {
                    b.Property<int>("SectionId")
                        .HasColumnType("int");

                    b.Property<int>("ParticipantId")
                        .HasColumnType("int");

                    b.HasKey("SectionId", "ParticipantId");

                    b.HasIndex("ParticipantId");

                    b.ToTable("Enrollments", (string)null);
                });

            modelBuilder.Entity("Academics.Models.Instructor", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("FName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("LName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR");

                    b.Property<int?>("OfficeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OfficeId")
                        .IsUnique()
                        .HasFilter("[OfficeId] IS NOT NULL");

                    b.ToTable("Instructors", (string)null);
                });

            modelBuilder.Entity("Academics.Models.Office", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<bool?>("OfficeEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("OfficeLocation")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("OfficeName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR");

                    b.HasKey("Id");

                    b.ToTable("Offices", (string)null);
                });

            modelBuilder.Entity("Academics.Models.Participant", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("FName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("LName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR");

                    b.HasKey("Id");

                    b.ToTable("Particpants", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Academics.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Feedback")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Reviews", (string)null);
                });

            modelBuilder.Entity("Academics.Models.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<bool>("FRI")
                        .HasColumnType("bit");

                    b.Property<bool>("MON")
                        .HasColumnType("bit");

                    b.Property<bool>("SAT")
                        .HasColumnType("bit");

                    b.Property<bool>("SUN")
                        .HasColumnType("bit");

                    b.Property<string>("ScheduleType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("THU")
                        .HasColumnType("bit");

                    b.Property<bool>("TUE")
                        .HasColumnType("bit");

                    b.Property<bool>("WED")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Schedules", (string)null);
                });

            modelBuilder.Entity("Academics.Models.Section", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("InstructorId")
                        .HasColumnType("int");

                    b.Property<int>("ScheduleId")
                        .HasColumnType("int");

                    b.Property<string>("SectionName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("InstructorId");

                    b.HasIndex("ScheduleId");

                    b.ToTable("Sections", (string)null);
                });

            modelBuilder.Entity("Academics.Models.Corporate", b =>
                {
                    b.HasBaseType("Academics.Models.Participant");

                    b.Property<string>("Company")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JobTitle")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Coporates", (string)null);
                });

            modelBuilder.Entity("Academics.Models.Individual", b =>
                {
                    b.HasBaseType("Academics.Models.Participant");

                    b.Property<bool>("IsIntern")
                        .HasColumnType("bit");

                    b.Property<string>("University")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("YearOfGraduation")
                        .HasColumnType("int");

                    b.ToTable("Individuals", (string)null);
                });

            modelBuilder.Entity("Academics.Models.Enrollment", b =>
                {
                    b.HasOne("Academics.Models.Participant", "Participant")
                        .WithMany()
                        .HasForeignKey("ParticipantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Academics.Models.Section", "Section")
                        .WithMany()
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Participant");

                    b.Navigation("Section");
                });

            modelBuilder.Entity("Academics.Models.Instructor", b =>
                {
                    b.HasOne("Academics.Models.Office", "Office")
                        .WithOne("Instructor")
                        .HasForeignKey("Academics.Models.Instructor", "OfficeId");

                    b.Navigation("Office");
                });

            modelBuilder.Entity("Academics.Models.Review", b =>
                {
                    b.HasOne("Academics.Models.Course", "Course")
                        .WithMany("Reviews")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("Academics.Models.Section", b =>
                {
                    b.HasOne("Academics.Models.Course", "Course")
                        .WithMany("Sections")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Academics.Models.Instructor", "Instructor")
                        .WithMany("Sections")
                        .HasForeignKey("InstructorId");

                    b.HasOne("Academics.Models.Schedule", "Schedule")
                        .WithMany("Sections")
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Academics.Models.DateRange", "DateRange", b1 =>
                        {
                            b1.Property<int>("SectionId")
                                .HasColumnType("int");

                            b1.Property<DateTime>("EndDate")
                                .HasColumnType("date")
                                .HasColumnName("EndDate");

                            b1.Property<DateTime>("StartDate")
                                .HasColumnType("date")
                                .HasColumnName("StartDate");

                            b1.HasKey("SectionId");

                            b1.ToTable("Sections");

                            b1.WithOwner()
                                .HasForeignKey("SectionId");
                        });

                    b.OwnsOne("Academics.Models.TimeSlot", "TimeSlot", b1 =>
                        {
                            b1.Property<int>("SectionId")
                                .HasColumnType("int");

                            b1.Property<TimeSpan>("EndTime")
                                .HasColumnType("time(0)")
                                .HasColumnName("EndTime");

                            b1.Property<TimeSpan>("StartTime")
                                .HasColumnType("time(0)")
                                .HasColumnName("StartTime");

                            b1.HasKey("SectionId");

                            b1.ToTable("Sections");

                            b1.WithOwner()
                                .HasForeignKey("SectionId");
                        });

                    b.Navigation("Course");

                    b.Navigation("DateRange")
                        .IsRequired();

                    b.Navigation("Instructor");

                    b.Navigation("Schedule");

                    b.Navigation("TimeSlot")
                        .IsRequired();
                });

            modelBuilder.Entity("Academics.Models.Corporate", b =>
                {
                    b.HasOne("Academics.Models.Participant", null)
                        .WithOne()
                        .HasForeignKey("Academics.Models.Corporate", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Academics.Models.Individual", b =>
                {
                    b.HasOne("Academics.Models.Participant", null)
                        .WithOne()
                        .HasForeignKey("Academics.Models.Individual", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Academics.Models.Course", b =>
                {
                    b.Navigation("Reviews");

                    b.Navigation("Sections");
                });

            modelBuilder.Entity("Academics.Models.Instructor", b =>
                {
                    b.Navigation("Sections");
                });

            modelBuilder.Entity("Academics.Models.Office", b =>
                {
                    b.Navigation("Instructor");
                });

            modelBuilder.Entity("Academics.Models.Schedule", b =>
                {
                    b.Navigation("Sections");
                });
#pragma warning restore 612, 618
        }
    }
}
