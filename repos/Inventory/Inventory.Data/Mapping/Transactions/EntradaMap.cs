using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Inventory.Entity.Transactions;

namespace Inventory.Data.Mapping.Transactions
{
    public class EntradaMap : IEntityTypeConfiguration<ENTRADA>
    {
        public void Configure(EntityTypeBuilder<ENTRADA> builder)
        {
            builder.ToTable("ENTRADA").HasKey(c=> c.ID_ENTRADA);
            builder.HasOne(i => i.persona)
                    .WithMany(p => p.ingresos).HasForeignKey(i => i.ID_PROVEEDOR);
        }
    }
}
