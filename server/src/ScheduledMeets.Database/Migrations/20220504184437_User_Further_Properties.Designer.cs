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
    [Migration("20220504184437_User_Further_Properties")]
    partial class User_Further_Properties
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseHiLo(modelBuilder, "EntityFrameworkHiLoSequence");

            modelBuilder.HasSequence("EntityFrameworkHiLoSequence")
                .IncrementsBy(10);

            modelBuilder.Entity("ScheduledMeets.Core.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Nickname")
                        .HasColumnType("text")
                        .HasColumnName("nickname");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasDatabaseName("ix_users_username");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("ScheduledMeets.Core.User", b =>
                {
                    b.OwnsOne("ScheduledMeets.Core.PersonName", "Name", b1 =>
                        {
                            b1.Property<long>("UserId")
                                .HasColumnType("bigint")
                                .HasColumnName("id");

                            b1.Property<string>("FirstName")
                                .HasColumnType("text")
                                .HasColumnName("name_first_name");

                            b1.Property<string>("LastName")
                                .HasColumnType("text")
                                .HasColumnName("name_last_name");

                            b1.HasKey("UserId");

                            b1.ToTable("users");

                            b1.WithOwner()
                                .HasForeignKey("UserId")
                                .HasConstraintName("fk_users_users_id");
                        });

                    b.Navigation("Name")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
