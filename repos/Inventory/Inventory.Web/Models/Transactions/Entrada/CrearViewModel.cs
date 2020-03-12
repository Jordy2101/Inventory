using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Web.Models.Transactions.Entrada
{
    public class CrearViewModel
    {
        //Propiedades maestro
        [Required]
        public int ID_PROVEEDOR { get; set; }
        [Required]
        public int ID_USUARIO { get; set; }
        [Required]
        public string TIPO_COMPROBANTE { get; set; }
        public string SERIE_COMPROBANTE { get; set; }
        [Required]
        public string NUM_COMPROBANTE { get; set; }
        [Required]
        public decimal IMPUESTO { get; set; }
        [Required]
        public decimal TOTAL { get; set; }
        //Propiedades detalle
        [Required]
        public List<DetalleViewModel> detalles { get; set; }
    }
}
