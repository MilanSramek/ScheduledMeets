﻿// <auto-generated />
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
    [Migration("20230301212232_User_Model")]
    partial class User_Model
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
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

            modelBuilder.Entity("ScheduledMeets.Persistance.Model.Meets", b =>
                {
                    b.Navigation("Attendees");
                });

            modelBuilder.Entity("ScheduledMeets.Persistance.Model.User", b =>
                {
                    b.Navigation("Attendees");
                });
#pragma warning restore 612, 618
        }
    }
}
