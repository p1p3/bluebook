using System;
using System.Threading.Tasks;
using Books.Core.Models.BookAggregate;
using Books.Core.Repositories;
using Books.Core.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Books.Infrastructure.Database.Repositories
{
    internal class BookRepository : IBookRepository
    {

        private readonly BooksManagmentContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public BookRepository(BooksManagmentContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<Book> AddAsync(Book book)
        {
            var addedBook = _context.Books.Add(book).Entity;
            return Task.FromResult(addedBook);
        }

        public async Task<Book> GetAsync(Guid bookId)
        {
            var book = await _context.Books
                .Include(b => b.Chapters)
                    .ThenInclude(c => c.Pages)
                .FirstOrDefaultAsync(b => b.Id == bookId);

            return book;
        }

        public Task UpdateAsync(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}
