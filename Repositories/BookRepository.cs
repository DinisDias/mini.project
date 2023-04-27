using Microsoft.EntityFrameworkCore;
using Mini.Projeto.Models;
using Mini.Projeto.Data;
using Mini.Projeto.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mini.Projeto.Repositories
{
    namespace Mini.Projeto.Repositories
    {
        public class BookRepository : IBookRepository
        {
            private readonly BooksDBContex _dbContext;

            public BookRepository(BooksDBContex booksDBContex)
            {
                _dbContext = booksDBContex;
            }

            public async Task<List<BookModel>> SearchAllBooks()
            {
                return await _dbContext.Book.ToListAsync();
            }

            public async Task<BookModel> SearchBookIsbn(string isbn)
            {
                return await _dbContext.Book.FirstOrDefaultAsync(x => x.isbn == isbn && !x.isDeleted);
            }

            public async Task<BookModel> Add(BookModel book)
            {
                var existingBook = await SearchBookIsbn(book.isbn);
                if (existingBook != null)
                {
                    throw new ArgumentException($"Isbn '{book.isbn}' already exists. ");
                }

                _dbContext.Book.Add(book);
                await _dbContext.SaveChangesAsync();

                return book;
            }


            public async Task<BookModel> Update(string isbn, BookModel book)
            {
                BookModel bookIsbn = await SearchBookIsbn(isbn);

                if (bookIsbn == null)
                {
                    throw new Exception($"Book for isbn: {isbn} Not Found! ");
                }

                bookIsbn.bookName = book.bookName;
                bookIsbn.author = book.author;
                bookIsbn.price = book.price;

                await _dbContext.SaveChangesAsync();

                return bookIsbn;
            }

            public async Task<bool> Delete(string isbn)
            {
                BookModel book = await _dbContext.Book.FirstOrDefaultAsync(x => x.isbn == isbn && !x.isDeleted);

                if (book == null)
                {
                    throw new Exception($"Book with ID: {isbn} Not Found!");
                }

                book.isDeleted = true;
                await _dbContext.SaveChangesAsync();

                return true;
            }
        }
    }
}