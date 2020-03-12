using System;
using System.Collections.Generic;
using System.Text;
using Inventory.Entity.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Data.Mapping.Users
{
    public class PersonaMap : IEntityTypeConfiguration<PERSONA>
    {
        public void Configure(EntityTypeBuilder<PERSONA> builder)
        {
            builder.ToTable("PERSONA").HasKey(c=> c.ID_PERSONA);
        }
    }
}
