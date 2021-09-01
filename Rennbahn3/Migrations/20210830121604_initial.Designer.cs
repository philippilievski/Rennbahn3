﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Rennbahn3;

namespace Rennbahn3.Migrations
{
    [DbContext(typeof(RennbahnContext))]
    [Migration("20210830121604_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Rennbahn3.Model.Driver", b =>
                {
                    b.Property<int>("DriverID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SaisonID")
                        .HasColumnType("int");

                    b.Property<int?>("TeamID")
                        .HasColumnType("int");

                    b.HasKey("DriverID");

                    b.HasIndex("SaisonID");

                    b.HasIndex("TeamID");

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("Rennbahn3.Model.Race", b =>
                {
                    b.Property<int>("RaceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SaisonID")
                        .HasColumnType("int");

                    b.HasKey("RaceID");

                    b.HasIndex("SaisonID");

                    b.ToTable("Races");
                });

            modelBuilder.Entity("Rennbahn3.Model.Saison", b =>
                {
                    b.Property<int>("SaisonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SaisonID");

                    b.ToTable("Saisons");
                });

            modelBuilder.Entity("Rennbahn3.Model.Team", b =>
                {
                    b.Property<int>("TeamID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TeamID");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Rennbahn3.Model.Driver", b =>
                {
                    b.HasOne("Rennbahn3.Model.Saison", "Saison")
                        .WithMany("Drivers")
                        .HasForeignKey("SaisonID");

                    b.HasOne("Rennbahn3.Model.Team", "Team")
                        .WithMany("Drivers")
                        .HasForeignKey("TeamID");

                    b.Navigation("Saison");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Rennbahn3.Model.Race", b =>
                {
                    b.HasOne("Rennbahn3.Model.Saison", "Saison")
                        .WithMany("Races")
                        .HasForeignKey("SaisonID");

                    b.Navigation("Saison");
                });

            modelBuilder.Entity("Rennbahn3.Model.Saison", b =>
                {
                    b.Navigation("Drivers");

                    b.Navigation("Races");
                });

            modelBuilder.Entity("Rennbahn3.Model.Team", b =>
                {
                    b.Navigation("Drivers");
                });
#pragma warning restore 612, 618
        }
    }
}
