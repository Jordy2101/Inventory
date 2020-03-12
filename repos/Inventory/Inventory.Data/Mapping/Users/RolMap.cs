using System;
using System.Collections.Generic;
using System.Text;
using Inventory.Entity.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Data.Mapping.Users
{
    public class RolMap : IEntityTypeConfiguration<ROL>
    {
        public void Configure(EntityTypeBuilder<ROL> builder)
        {
            builder.ToTable("ROL").HasKey(c => c.ID_ROL);
        }
    }
}
