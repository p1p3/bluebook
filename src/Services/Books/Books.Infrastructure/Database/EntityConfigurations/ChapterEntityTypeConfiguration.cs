using Books.Core.Models.BookAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Books.Infrastructure.Database.EntityConfigurations
{
    internal class ChapterEntityTypeConfiguration : IEntityTypeConfiguration<Chapter>
    {
        public void Configure(EntityTypeBuilder<Chapter> chapterBuilder)
        {
            chapterBuilder.ToTable("chapters", BooksManagmentContext.DEFAULT_SCHEMA);
            chapterBuilder.HasKey(chapter => chapter.Id);

            chapterBuilder.Property<string>(chapter => chapter.Title).IsRequired();
            chapterBuilder.Property<int>(chapter => chapter.ChapterNumber).IsRequired();

            chapterBuilder.HasMany<Page>(chapter => chapter.Pages)
                .WithOne()
                .IsRequired(false);
        }
    }
}
