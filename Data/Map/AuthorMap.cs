using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mini.Projeto.Models;
using static Mini.Projeto.Models.AuthorModel;

namespace Mini.Projeto.Data
{
    public class AuthorMap : IEntityTypeConfiguration<AuthorModel>
    {
        public void Configure(EntityTypeBuilder<AuthorModel> builder)
        {
            builder.HasKey(x => x.idAuthor);
            builder.Property(x => x.authorName).IsRequired();

        }
    }
}