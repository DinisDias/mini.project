using Microsoft.EntityFrameworkCore;
using Mini.Projeto.Models;
using Mini.Projeto.Repositories;


namespace Mini.Projeto.Data
{
    public class BooksDBContex : DbContext
    {
        public BooksDBContex(DbContextOptions<BooksDBContex> options)
            : base(options)
        {

        }
        public DbSet<BookModel> Book { get; set; }
        public DbSet<AuthorModel> Author { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookMap());
            modelBuilder.ApplyConfiguration(new AuthorMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}