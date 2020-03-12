using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Inventory.Entity.Warehouse;

namespace Inventory.Entity.Transactions.Salida
{
    public class DETALLE_SALIDA
    {
        public int iddetalle_venta { get; set; }
        [Required]
        public int idventa { get; set; }
        [Required]
        public int idarticulo { get; set; }
        [Required]
        public int cantidad { get; set; }
        [Required]
        public decimal precio { get; set; }
        [Required]
        public decimal descuento { get; set; }

        public ENTRADA venta { get; set; }
        public Articulo articulo { get; set; }

    }
}
