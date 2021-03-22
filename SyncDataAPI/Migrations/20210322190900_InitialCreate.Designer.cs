﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SyncDataAPI;

namespace SyncDataAPI.Migrations
{
    [DbContext(typeof(FormsContext))]
    [Migration("20210322190900_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.4");

            modelBuilder.Entity("SyncDataAPI.Field", b =>
                {
                    b.Property<int>("FieldId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("FormId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("InventoryCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("InventoryDesc")
                        .HasColumnType("TEXT");

                    b.HasKey("FieldId");

                    b.HasIndex("FormId");

                    b.ToTable("Fields");
                });

            modelBuilder.Entity("SyncDataAPI.Form", b =>
                {
                    b.Property<int>("FormId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FormName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("TEXT");

                    b.HasKey("FormId");

                    b.ToTable("Forms");
                });

            modelBuilder.Entity("SyncDataAPI.LogData", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("FieldId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FormId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("InventoryCountNew")
                        .HasColumnType("INTEGER");

                    b.Property<int>("InventoryCountPrevious")
                        .HasColumnType("INTEGER");

                    b.Property<string>("InventoryDescNew")
                        .HasColumnType("TEXT");

                    b.Property<string>("InventoryDescPrevious")
                        .HasColumnType("TEXT");

                    b.Property<int>("SubApplicationId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("LogDatas");
                });

            modelBuilder.Entity("SyncDataAPI.Field", b =>
                {
                    b.HasOne("SyncDataAPI.Form", null)
                        .WithMany("Fields")
                        .HasForeignKey("FormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SyncDataAPI.Form", b =>
                {
                    b.Navigation("Fields");
                });
#pragma warning restore 612, 618
        }
    }
}