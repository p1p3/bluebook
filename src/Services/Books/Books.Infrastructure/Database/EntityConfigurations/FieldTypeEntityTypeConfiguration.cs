using Books.Core.Models.Fields;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Books.Infrastructure.Database.EntityConfigurations
{
    internal class FieldTypeEntityTypeConfiguration : IEntityTypeConfiguration<FieldType>
    {
        public void Configure(EntityTypeBuilder<FieldType> fieldTypeBuilder)
        {

            fieldTypeBuilder.ToTable("fieldtypes", BooksManagmentContext.DEFAULT_SCHEMA);

            fieldTypeBuilder.HasKey(ct => ct.Id);

            fieldTypeBuilder.Property(ct => ct.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            fieldTypeBuilder.Property(ct => ct.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
