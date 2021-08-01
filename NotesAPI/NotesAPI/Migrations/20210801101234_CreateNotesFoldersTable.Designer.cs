﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NotesAPI.DbModels;

namespace NotesAPI.Migrations
{
    [DbContext(typeof(DbNotes))]
    [Migration("20210801101234_CreateNotesFoldersTable")]
    partial class CreateNotesFoldersTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.8");

            modelBuilder.Entity("NotesAPI.DbModels.TFolder", b =>
                {
                    b.Property<int>("IdFolder")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("IdUser")
                        .HasColumnType("int");

                    b.Property<DateTime>("InsertedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("IdFolder");

                    b.HasIndex("IdUser");

                    b.ToTable("Folders");
                });

            modelBuilder.Entity("NotesAPI.DbModels.TNote", b =>
                {
                    b.Property<int>("IdNote")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("IdNoteBodyType")
                        .HasColumnType("int");

                    b.Property<int>("IdNoteVisibility")
                        .HasColumnType("int");

                    b.Property<int>("IdUser")
                        .HasColumnType("int");

                    b.Property<DateTime>("InsertedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.HasKey("IdNote");

                    b.HasIndex("IdNoteBodyType");

                    b.HasIndex("IdNoteVisibility");

                    b.HasIndex("IdUser");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("NotesAPI.DbModels.TNoteBody", b =>
                {
                    b.Property<int>("IdNoteBody")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Body")
                        .HasColumnType("longtext");

                    b.Property<int>("IdNote")
                        .HasColumnType("int");

                    b.HasKey("IdNoteBody");

                    b.HasIndex("IdNote");

                    b.ToTable("NoteBodies");
                });

            modelBuilder.Entity("NotesAPI.DbModels.TNoteBodyType", b =>
                {
                    b.Property<int>("IdNoteBodyType")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.HasKey("IdNoteBodyType");

                    b.ToTable("NoteBodyTypes");
                });

            modelBuilder.Entity("NotesAPI.DbModels.TNoteFolder", b =>
                {
                    b.Property<int>("IdNote")
                        .HasColumnType("int");

                    b.Property<int>("IdFolder")
                        .HasColumnType("int");

                    b.Property<int>("IdNoteFolder")
                        .HasColumnType("int");

                    b.HasKey("IdNote", "IdFolder");

                    b.HasIndex("IdFolder");

                    b.ToTable("NotesFolders");
                });

            modelBuilder.Entity("NotesAPI.DbModels.TNoteVisibility", b =>
                {
                    b.Property<int>("IdVisibility")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.HasKey("IdVisibility");

                    b.ToTable("NoteVisibilites");
                });

            modelBuilder.Entity("NotesAPI.DbModels.TUser", b =>
                {
                    b.Property<int>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("IdUser");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("NotesAPI.DbModels.TFolder", b =>
                {
                    b.HasOne("NotesAPI.DbModels.TUser", "User")
                        .WithMany()
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("NotesAPI.DbModels.TNote", b =>
                {
                    b.HasOne("NotesAPI.DbModels.TNoteBodyType", "NoteBodyType")
                        .WithMany()
                        .HasForeignKey("IdNoteBodyType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NotesAPI.DbModels.TNoteVisibility", "NoteVisibility")
                        .WithMany()
                        .HasForeignKey("IdNoteVisibility")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NotesAPI.DbModels.TUser", "User")
                        .WithMany()
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NoteBodyType");

                    b.Navigation("NoteVisibility");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NotesAPI.DbModels.TNoteBody", b =>
                {
                    b.HasOne("NotesAPI.DbModels.TNote", "Note")
                        .WithMany("NoteBodies")
                        .HasForeignKey("IdNote")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Note");
                });

            modelBuilder.Entity("NotesAPI.DbModels.TNoteFolder", b =>
                {
                    b.HasOne("NotesAPI.DbModels.TFolder", "Folder")
                        .WithMany()
                        .HasForeignKey("IdFolder")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NotesAPI.DbModels.TNote", "Note")
                        .WithMany()
                        .HasForeignKey("IdNote")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Folder");

                    b.Navigation("Note");
                });

            modelBuilder.Entity("NotesAPI.DbModels.TNote", b =>
                {
                    b.Navigation("NoteBodies");
                });
#pragma warning restore 612, 618
        }
    }
}
