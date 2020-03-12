using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Web.Models.Transactions.Salida
{
    public class DetalleViewModel
    {


        [Required]
        public int idarticulo { get; set; }
        public string articulo { get; set; }
        [Required]
        public int cantidad { get; set; }
        [Required]
        public decimal precio { get; set; }
        [Required]
        public decimal descuento { get; set; }



    }
}
