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
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasColumnName("Name");
            builder.Property(x => x.Heigth).HasColumnName("Heigth");
            builder.Property(x => x.Weigth).HasColumnName("Weigth");
            builder.Property(x => x.Width).HasColumnName("Width");
            builder.Property(x => x.Explanation).HasColumnName("Explanation");
            builder.Property(x => x.AddedDate).HasColumnName("AddedDate");
            builder.Property(x => x.AddedBy).HasColumnName("AddedBy");
            builder.Property(x => x.CategoryId).HasColumnName("CategoryId");
        }
    }
}
