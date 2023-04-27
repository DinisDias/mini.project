using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Mini.Projeto.Repositories.Interfaces;

namespace Mini.Projeto.Models
{
    public class NonNegativePriceAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            decimal price = (decimal)value;
            if (price < 0)
            {
                return new ValidationResult("The price value cannot be negative.");
            }
            return ValidationResult.Success;
        }
    }

    public class UniqueIsbnAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var isbn = value as string;
            if (isbn == null)
            {
                return new ValidationResult("Isbn is not valid.");
            }

            var bookRepository = (IBookRepository)validationContext.GetService(typeof(IBookRepository));
            var existingBook = bookRepository.SearchBookIsbn(isbn).Result;

            if (existingBook != null && existingBook.isbn != ((BookModel)validationContext.ObjectInstance).isbn)
            {
                return new ValidationResult("The ISBN is already in use.", new[] { validationContext.MemberName });
            }

            return ValidationResult.Success;
        }
    }



    public class BookModel
    {
        [UniqueIsbn]
        public string? isbn { get; set; }

        public string? bookName { get; set; }

        public string? author { get; set; }

        [NonNegativePrice]
        public decimal price { get; set; }

        public bool isDeleted { get; set; } = false;
    }
}