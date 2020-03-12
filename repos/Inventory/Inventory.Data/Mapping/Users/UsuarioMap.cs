using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Inventory.Entity.Users; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Data.Mapping.Users
{
    public class UsuarioMap : IEntityTypeConfiguration<USUARIO>
    {
        public void Configure(EntityTypeBuilder<USUARIO> builder)
        {
            builder.ToTable("USUARIO").HasKey(c => c.ID_USUARIO);
        }
    }
}
