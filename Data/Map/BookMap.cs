using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mini.Projeto.Models;

namespace Mini.Projeto.Data
{
    public class BookMap : IEntityTypeConfiguration<BookModel>
    {
        public void Configure(EntityTypeBuilder<BookModel> builder)
        {
            builder.HasKey(x => x.isbn);
            builder.Property(x => x.bookName).IsRequired();
            builder.Property(x => x.author).IsRequired();
            builder.Property(x => x.price).IsRequired();
        }
    }
}