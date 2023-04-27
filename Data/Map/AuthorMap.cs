using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mini.Projeto.Models;


namespace Mini.Projeto.Data
{
    public class AuthorMap : IEntityTypeConfiguration<AuthorModel>
    {
        public void Configure(EntityTypeBuilder<AuthorModel> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.authorName).IsRequired();
        }
    }
}