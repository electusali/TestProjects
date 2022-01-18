using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProjects.Entity.Concrete;

namespace TestProjects.DataAccess.Concrete.EntitiyFramworkCore.Mappings
{
    public class ProductImageMap : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.AddedBy).HasColumnName("AddedBy");
            builder.Property(x => x.AddedDate).HasColumnName("AddedDate");
            builder.Property(x => x.FileName).HasColumnName("FileName");
            builder.Property(x => x.FilePath).HasColumnName("FilePath");
            builder.Property(x => x.ProductId).HasColumnName("ProductId");
        }
    }
}
