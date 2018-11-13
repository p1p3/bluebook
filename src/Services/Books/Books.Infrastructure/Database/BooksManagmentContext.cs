using Books.Core.Models.BookAggregate;
using Books.Core.Models.Fields;
using Books.Core.SeedWork;
using Books.Infrastructure.Database.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Threading;
using System.Threading.Tasks;

namespace Books.Infrastructure.Database
{
    internal class BooksManagmentContext : DbContext, IUnitOfWork
    {
        internal const string DEFAULT_SCHEMA = "books_managment";

        internal DbSet<Book> Books { get; private set; }
        internal DbSet<Chapter> Chapters { get; private set; }
        internal DbSet<Page> Pages { get; private set; }
        internal DbSet<PageField> PageFields { get; private set; }
        internal DbSet<FieldType> PageFieldTypes { get; private set; }

        public BooksManagmentContext(DbContextOptions<BooksManagmentContext> options) : base(options)
        {
            System.Diagnostics.Debug.WriteLine("OrderingContext::ctor ->" + this.GetHashCode());

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ChapterEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PageEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PageFieldEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FieldTypeEntityTypeConfiguration());
        }

        public Task CommitChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.SaveChangesAsync();
        }
    }

    //internal class OrderingContextDesignFactory : IDesignTimeDbContextFactory<BooksManagmentContext>
    //{
    //    public BooksManagmentContext CreateDbContext(string[] args)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<BooksManagmentContext>()
    //            .UseSqlServer("Server=.;Initial Catalog=Microsoft.eShopOnContainers.Services.BooksManagmentDb;Integrated Security=true");

    //        return new BooksManagmentContext(optionsBuilder.Options);
    //    }
    //}
}
