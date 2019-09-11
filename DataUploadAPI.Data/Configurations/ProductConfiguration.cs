using DataUploadAPI.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataUploadAPI.Data.Configurations
{
    public class ProductConfiguration
    {
        public ProductConfiguration(EntityTypeBuilder<Product> entity)
        {
            entity.Property(e => e.Price)
               .IsRequired();

            entity.HasOne(d => d.Category);

        }
    }
}