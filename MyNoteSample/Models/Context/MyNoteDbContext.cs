using Microsoft.EntityFrameworkCore;
using MyNoteSample.Models.Entities;

namespace MyNoteSample.Models.Context
{
    public class MyNoteDbContext : DbContext
    {
        private readonly string ConnectionString = "Server=localhost; Database=NotesDb; Integrated Security=true;";
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<LikedNotes> LikedNotes { get; set; }
        public DbSet<EmailMembership> EmailMemberships { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
                optionsBuilder.UseLazyLoadingProxies();
            }
        }
    }
}
