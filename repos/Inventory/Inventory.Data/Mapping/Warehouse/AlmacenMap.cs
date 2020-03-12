using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Inventory.Entity.Warehouse;

namespace Inventory.Data.Mapping.Warehouse
{
    public class AlmacenMap : IEntityTypeConfiguration<Almacen>
    {
        public void Configure(EntityTypeBuilder<Almacen> builder)
        {
            builder.ToTable("ALMACEN").HasKey(c=> c.ID_ALMACEN);
            builder.Property(c => c.DESCRIPCION)
              .HasMaxLength(50);
            builder.Property(c => c.CAPACIDAD)
                .HasMaxLength(256);
        }
    }
}
