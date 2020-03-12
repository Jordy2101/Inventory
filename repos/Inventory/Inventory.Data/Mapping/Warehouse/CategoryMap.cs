using Inventory.Entity.Warehouse;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Data.Mapping.Warehouse
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("CATEGORIA")
                .HasKey(c => c.ID_CATEGORIA);
            builder.Property(c => c.NOMBRE_CATEGORIA)
               .HasMaxLength(50);
            builder.Property(c => c.DESCRIPCION)
                .HasMaxLength(256);

        }
    }
}
