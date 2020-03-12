using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Inventory.Entity.Transactions;
using Inventory.Entity.Transactions.Entrada;

namespace Inventory.Data.Mapping.Transactions
{
    public class DetalleEntradamap : IEntityTypeConfiguration<DETALLE_ENTRADA>
    {
        public void Configure(EntityTypeBuilder<DETALLE_ENTRADA> builder)
        {
            builder.ToTable("DETALLE_ENTRADA").HasKey(c => c.ID_DETALLE_ENTRADA); 
        }
    }
}
