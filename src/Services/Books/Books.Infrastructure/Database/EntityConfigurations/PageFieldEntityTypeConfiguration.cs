using Books.Core.Models.BookAggregate;
using Books.Core.Models.Fields;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Books.Infrastructure.Database.EntityConfigurations
{
    internal class PageFieldEntityTypeConfiguration : IEntityTypeConfiguration<PageField>
    {
        public void Configure(EntityTypeBuilder<PageField> pageFieldBuilder)
        {
            pageFieldBuilder.ToTable("page_fields", BooksManagmentContext.DEFAULT_SCHEMA);
            pageFieldBuilder.HasKey(pageField => pageField.Identifier);

            pageFieldBuilder.Property<string>(pageField => pageField.Description).IsRequired();
            pageFieldBuilder.Property<bool>(pageField => pageField.Required).IsRequired();

            pageFieldBuilder.HasOne(o => o.Type)
               .WithMany()
               .IsRequired();

        }
    }
}
