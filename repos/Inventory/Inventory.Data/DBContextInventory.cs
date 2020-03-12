using Inventory.Data.Mapping.Warehouse;
using Inventory.Data.Mapping.Users;
using Inventory.Data.Mapping.Transactions;
using Inventory.Entity.Warehouse;
using Inventory.Entity.Transactions;
using Inventory.Entity.Transactions.Entrada;
using Inventory.Entity.Users;
using Inventory.Entity.Transactions.Salida;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Data
{
    public class DBContextInventory :DbContext
    {
        public DbSet<Category> Categorias { get; set; }
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<ROL> Roles { get; set; }
        public DbSet<USUARIO> Usuarios { get; set; }
        public DbSet<PERSONA> Persona { get; set; }
        public DbSet<ENTRADA> Entrada { get; set; }
        public DbSet<DETALLE_ENTRADA> detalle { get; set; }
        public DbSet<SALIDA> salida { get; set; }
        public DbSet<DETALLE_SALIDA> detallesalida { get; set; }

        public DbSet<Almacen> almacen { get; set; }


        public DBContextInventory(DbContextOptions<DBContextInventory> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new ArticuloMap());
            modelBuilder.ApplyConfiguration(new RolMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new PersonaMap());
            modelBuilder.ApplyConfiguration(new EntradaMap());
            modelBuilder.ApplyConfiguration(new DetalleEntradamap());
            modelBuilder.ApplyConfiguration(new SalidaMap());
            modelBuilder.ApplyConfiguration(new DetalleSalidaMap());
            modelBuilder.ApplyConfiguration(new AlmacenMap());


        }



    }
}
