using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Inventory.Entity.Transactions.Salida;

namespace Inventory.Data.Mapping.Transactions
{
    public class DetalleSalidaMap : IEntityTypeConfiguration<DETALLE_SALIDA>
    {
        public void Configure(EntityTypeBuilder<DETALLE_SALIDA> builder)
        {
            builder.ToTable("DETALLE_SALIDA").HasKey(d=> d.iddetalle_venta);
        }
    }
}
