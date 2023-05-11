using Mini.Projeto.Models;

namespace Mini.Projeto.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        Task<List<AuthorModel>> SearchAllAuthors();
        Task<AuthorModel> SearchAuthorId(int idAuthor);
        Task<AuthorModel> Add(AuthorModel authorName);
        Task<AuthorModel> Update(int idAuthor, AuthorModel authorName);
        Task<bool> Delete(int idAuthor);

    }
}