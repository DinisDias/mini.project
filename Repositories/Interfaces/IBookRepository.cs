using Mini.Projeto.Models;


namespace Mini.Projeto.Repositories.Interfaces

    {
        public interface IBookRepository
        {
            Task<List<BookModel>> SearchAllBooks();
            Task<BookModel> SearchBookIsbn(string isbn);
            Task<BookModel> Add(BookModel book);
            Task<BookModel> Update(string isbn, BookModel book);
            Task<bool> Delete(string isbn);

        }
    }


