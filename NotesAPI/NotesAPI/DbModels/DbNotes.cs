using Microsoft.EntityFrameworkCore;
using System;


namespace NotesAPI.DbModels
{
    public class DbNotes : DbContext
    {
        public DbSet<TNote> Notes { get; set; }
        public DbSet<TFolder> Folders { get; set; }
        public DbSet<TUser> Users { get; set; }
        public DbSet<TNoteVisibility> NoteVisibilities { get; set; }
        public DbSet<TNoteBody> NoteBody { get; set; }

        public DbNotes(DbContextOptions<DbNotes> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TUser>().ToTable("Users");
            modelBuilder.Entity<TFolder>().ToTable("Folders");
            modelBuilder.Entity<TNote>().ToTable("Notes");
            modelBuilder.Entity<TNoteVisibility>().ToTable("NoteVisibilites");
            modelBuilder.Entity<TNoteBody>().ToTable("NoteBodies");
        }
    }
}
