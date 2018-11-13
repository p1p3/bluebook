using Books.Core.Models.BookAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Books.Infrastructure.Database.EntityConfigurations
{
    internal class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> bookBuilder)
        {
            bookBuilder.ToTable("books", BooksManagmentContext.DEFAULT_SCHEMA);
            bookBuilder.HasKey(o => o.Id);
            
            bookBuilder.Property<string>(book => book.Title).IsRequired();
      
            bookBuilder.HasMany<Chapter>(book => book.Chapters)
                .WithOne()
                .IsRequired(false);
        }
    }
}
