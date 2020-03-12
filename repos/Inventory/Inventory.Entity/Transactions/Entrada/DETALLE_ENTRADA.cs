using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Inventory.Entity.Warehouse;

namespace Inventory.Entity.Transactions.Entrada
{
    public class DETALLE_ENTRADA
    {
        public int ID_DETALLE_ENTRADA { get; set; }
        [Required]
        public int ID_INGRESO { get; set; }
        [Required]
        public int ID_ARTICULO { get; set; }
        [Required]
        public int CANTIDAD { get; set; }
        [Required]
        public decimal PRECIO { get; set; }

        public ENTRADA ingreso { get; set; }
        public Articulo articulo { get; set; }

    }
}
