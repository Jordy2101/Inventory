using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Web.Models.Transactions.Entrada
{
    public class EntradaViewModel
    {
        public int ID_ENTRADA { get; set; }
        public int ID_PROVEEDOR { get; set; }
        public string PROVEEDOR { get; set; }
        public int ID_USUARIO { get; set; }
        public string USUARIO { get; set; }
        public string TIPO_COMPROBANTE { get; set; }
        public string SERIE_COMPROBANTE { get; set; }
        public string NUM_COMPROBANTE { get; set; }
        public DateTime FECHA_HORA { get; set; }
        public decimal IMPUESTO { get; set; }
        public decimal TOTAL { get; set; }
        public string ESTADO { get; set; }
    }
}
