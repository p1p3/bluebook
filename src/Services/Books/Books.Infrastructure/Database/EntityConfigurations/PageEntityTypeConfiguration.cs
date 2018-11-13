using Books.Core.Models.BookAggregate;
using Books.Core.Models.Fields;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Books.Infrastructure.Database.EntityConfigurations
{
    internal class PageEntityTypeConfiguration : IEntityTypeConfiguration<Page>
    {
        public void Configure(EntityTypeBuilder<Page> pageBuilder)
        {
            pageBuilder.ToTable("pages", BooksManagmentContext.DEFAULT_SCHEMA);
            pageBuilder.HasKey(page => page.Id);

            var navigation = pageBuilder.Metadata.FindNavigation(nameof(Page.Fields));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            pageBuilder
                .HasMany<PageField>(page => page.Fields)
                .WithOne()
                .IsRequired(true);
 
        }
    }
}
