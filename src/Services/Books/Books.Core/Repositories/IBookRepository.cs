using Books.Core.Models.BookAggregate;
using Books.Core.SeedWork;
using System;
using System.Threading.Tasks;

namespace Books.Core.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<Book> AddAsync(Book book);

        Task UpdateAsync(Book book);

        Task<Book> GetAsync(Guid bookId);
    }
}

