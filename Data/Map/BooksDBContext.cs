using Microsoft.EntityFrameworkCore;
using Mini.Projeto.Models;
using Mini.Projeto.Repositories;
using static Mini.Projeto.Models.AuthorModel;

namespace Mini.Projeto.Data.Map
{
    public class BooksDBContext : DbContext
    {
        public DbSet<BookModel> Book { get; set; }
        public DbSet<AuthorModel> Author { get; set; }

        public BooksDBContext(DbContextOptions<BooksDBContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookMap());
            modelBuilder.ApplyConfiguration(new AuthorMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}