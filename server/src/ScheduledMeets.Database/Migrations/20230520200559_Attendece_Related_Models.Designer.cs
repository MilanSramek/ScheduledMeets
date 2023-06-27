﻿// <auto-generated />
using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

using ScheduledMeets.Persistence;

#nullable disable

namespace ScheduledMeets.Database.Migrations
{
    [DbContext(typeof(AccessContext))]
    [Migration("20230520200559_Attendece_Related_Models")]
    partial class Attendece_Related_Models
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseHiLo(modelBuilder, "EntityFrameworkHiLoSequence");

            modelBuilder.HasSequence("EntityFrameworkHiLoSequence")
                .IncrementsBy(10);

            modelBuilder.Entity("ScheduledMeets.Persistance.Model.Attendee", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<long>("Id"));

                    b.Property<bool>("IsOwner")
                        .HasColumnType("boolean")
                        .HasColumnName("is_owner");

                    b.Property<long>("MeetsId")
                        .HasColumnType("bigint")
                        .HasColumnName("meets_id");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_attendee");

                    b.HasIndex("MeetsId")
                        .HasDatabaseName("ix_attendee_meets_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_attendee_user_id");

                    b.ToTable("attendee", (string)null);
                });

            modelBuilder.Entity("ScheduledMeets.Persistance.Model.Attendence", b =>
                {
                    b.Property<long>("AttendeeId")
                        .HasColumnType("bigint")
                        .HasColumnName("attendee_id");

                    b.Property<long>("MeetId")
                        .HasColumnType("bigint")
                        .HasColumnName("meet_id");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.HasKey("AttendeeId", "MeetId")
                        .HasName("pk_attendence");

                    b.HasIndex("AttendeeId")
                        .HasDatabaseName("ix_attendence_attendee_id");

                    b.HasIndex("MeetId")
                        .HasDatabaseName("ix_attendence_meet_id");

                    b.ToTable("attendence", (string)null);
                });

            modelBuilder.Entity("ScheduledMeets.Persistance.Model.Meet", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<long>("Id"));

                    b.Property<DateTimeOffset>("From")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("from");

                    b.Property<long>("MeetsId")
                        .HasColumnType("bigint")
                        .HasColumnName("meets_id");

                    b.Property<DateTimeOffset>("To")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("to");

                    b.HasKey("Id")
                        .HasName("pk_meet");

                    b.HasIndex("MeetsId")
                        .HasDatabaseName("ix_meet_meets_id");

                    b.ToTable("meet", (string)null);
                });

            modelBuilder.Entity("ScheduledMeets.Persistance.Model.Meets", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_meets");

                    b.ToTable("meets", (string)null);
                });

            modelBuilder.Entity("ScheduledMeets.Persistance.Model.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<string>("Nickname")
                        .HasColumnType("text")
                        .HasColumnName("nickname");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("pk_user");

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasDatabaseName("ix_user_username");

                    b.ToTable("user", (string)null);
                });

            modelBuilder.Entity("ScheduledMeets.Persistance.Model.Attendee", b =>
                {
                    b.HasOne("ScheduledMeets.Persistance.Model.Meets", "Meets")
                        .WithMany("Attendees")
                        .HasForeignKey("MeetsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_attendee_meets_meets_id");

                    b.HasOne("ScheduledMeets.Persistance.Model.User", "User")
                        .WithMany("Attendees")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_attendee_user_user_id");

                    b.Navigation("Meets");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ScheduledMeets.Persistance.Model.Attendence", b =>
                {
                    b.HasOne("ScheduledMeets.Persistance.Model.Attendee", "Attendee")
                        .WithMany("Attendences")
                        .HasForeignKey("AttendeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_attendence_attendee_attendee_id");

                    b.HasOne("ScheduledMeets.Persistance.Model.Meet", "Meet")
                        .WithMany("Attendences")
                        .HasForeignKey("MeetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_attendence_meet_meet_id");

                    b.Navigation("Attendee");

                    b.Navigation("Meet");
                });

            modelBuilder.Entity("ScheduledMeets.Persistance.Model.Meet", b =>
                {
                    b.HasOne("ScheduledMeets.Persistance.Model.Meets", "Meets")
                        .WithMany("ContainedMeets")
                        .HasForeignKey("MeetsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_meet_meets_meets_id");

                    b.Navigation("Meets");
                });

            modelBuilder.Entity("ScheduledMeets.Persistance.Model.Attendee", b =>
                {
                    b.Navigation("Attendences");
                });

            modelBuilder.Entity("ScheduledMeets.Persistance.Model.Meet", b =>
                {
                    b.Navigation("Attendences");
                });

            modelBuilder.Entity("ScheduledMeets.Persistance.Model.Meets", b =>
                {
                    b.Navigation("Attendees");

                    b.Navigation("ContainedMeets");
                });

            modelBuilder.Entity("ScheduledMeets.Persistance.Model.User", b =>
                {
                    b.Navigation("Attendees");
                });
#pragma warning restore 612, 618
        }
    }
}
