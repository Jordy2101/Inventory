using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Inventory.Entity.Transactions.Salida; 


namespace Inventory.Data.Mapping.Transactions
{
    public class SalidaMap : IEntityTypeConfiguration<SALIDA>
    {
        public void Configure(EntityTypeBuilder<SALIDA> builder)
        {
            builder.ToTable("SALIDA")
                 .HasKey(v => v.ID_SALIDA);
          /*  builder.HasOne(v => v.persona)
                .WithMany(p => p.TIPO_DOCUMENTO)
                .HasForeignKey(v => v.idcliente);*/
        }
    }
}
