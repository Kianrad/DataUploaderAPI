using DataUploadAPI.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataUploadAPI.Data.Configurations
{
    public class CategoryConfiguration
    {
        public CategoryConfiguration(EntityTypeBuilder<Category> entity)
        {
            
            entity.HasMany(d => d.Products)
                .WithOne(d => d.Category)
                .HasForeignKey(d => d.CategoryId);

        }
    }
}