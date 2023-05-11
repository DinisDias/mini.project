using Mini.Projeto.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Mini.Projeto.Models
{
    public class AuthorModel
    {
        public int idAuthor { get; set; }

        public string? authorName { get; set; }

        public bool isDeleted { get; set; } = false;
    }
}