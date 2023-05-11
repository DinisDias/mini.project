using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mini.Projeto.Models;
using Microsoft.AspNetCore.Mvc;
using Mini.Projeto.Repositories.Interfaces;
using System;
using Mini.Projeto.Data.Map;

namespace Mini.Projeto.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BooksDBContext _dbContext;
        public AuthorRepository(BooksDBContext booksDBContex)
        {
            _dbContext = booksDBContex;
        }
        public async Task<AuthorModel> SearchAuthorId(int idauthor)
        {
            return await _dbContext.Author.FirstOrDefaultAsync(x => x.idAuthor == idauthor);
        }

        public async Task<List<AuthorModel>> SearchAllAuthors()
        {
            return await _dbContext.Author.ToListAsync();
        }
        public async Task<AuthorModel> Add(AuthorModel author)
        {
            var existingAuthor = await SearchAuthorId(author.idAuthor);
            if (existingAuthor != null)
            {
                throw new ArgumentException($"Id '{author.idAuthor}' already exists.");
            }

            _dbContext.Author.Add(author);
            await _dbContext.SaveChangesAsync();

            return author;
        }
        public async Task<AuthorModel> Update(int idAuthor, AuthorModel author)
        {
            AuthorModel idauthor = await SearchAuthorId(idAuthor);

            if (idauthor == null)
            {
                throw new Exception($"Author for id: {idAuthor} Not Found. ");
            }

            idauthor.authorName = author.authorName;

            await _dbContext.SaveChangesAsync();

            return idauthor;
        }

        public async Task<bool> Delete(int idAuthor)
        {
            var author = await _dbContext.Author.FindAsync(idAuthor);

            if (author == null)
            {
                return false; // retornar falso para indicar que o registro não foi excluído
            }

            author.isDeleted = true; // atualizar a coluna "isDeleted" para verdadeiro
            await _dbContext.SaveChangesAsync();

            return true; // retornar verdadeiro para indicar que o registro foi excluído com sucesso
        }

    }
}