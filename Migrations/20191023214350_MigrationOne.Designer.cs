﻿// <auto-generated />
using System;
using HobbyHub.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HobbyHub.Migrations
{
    [DbContext(typeof(HobbyHubContext))]
    [Migration("20191023214350_MigrationOne")]
    partial class MigrationOne
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("HobbyHub.Models.AddedToPersonalHobbies", b =>
                {
                    b.Property<int>("AddedToPersonalHobbiesId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("HobbyId");

                    b.Property<int>("UserId");

                    b.HasKey("AddedToPersonalHobbiesId");

                    b.HasIndex("HobbyId");

                    b.HasIndex("UserId");

                    b.ToTable("AddedToPersonalHobbies");
                });

            modelBuilder.Entity("HobbyHub.Models.Hobby", b =>
                {
                    b.Property<int>("HobbyId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int>("HobbyCreatorId");

                    b.Property<string>("HobbyType")
                        .IsRequired();

                    b.HasKey("HobbyId");

                    b.HasIndex("HobbyCreatorId");

                    b.ToTable("Hobby");
                });

            modelBuilder.Entity("HobbyHub.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HobbyHub.Models.AddedToPersonalHobbies", b =>
                {
                    b.HasOne("HobbyHub.Models.Hobby", "hobby")
                        .WithMany("Enthusiasts")
                        .HasForeignKey("HobbyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HobbyHub.Models.User", "user")
                        .WithMany("MyHobbies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HobbyHub.Models.Hobby", b =>
                {
                    b.HasOne("HobbyHub.Models.User", "HobbyCreator")
                        .WithMany("Hobby")
                        .HasForeignKey("HobbyCreatorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}