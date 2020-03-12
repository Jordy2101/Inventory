using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Inventory.Entity.Users;

namespace Inventory.Entity.Transactions.Salida
{
    public class SALIDA
    {

        public int ID_SALIDA { get; set; }
        [Required]
        public int ID_CLIENTE { get; set; }
        [Required]
        public int ID_USUARIO  { get; set; }
        [Required]
        public string TIPO_COMPROBANTE { get; set; }
        [Required]
        public string SERIE_COMPROBANTE { get; set; }
        [Required]
        public string NUM_COMPROBANTE { get; set; }
        [Required]
        public DateTime FECHA_HORA { get; set; }
        [Required]
        public decimal IMPUESTO { get; set; }
        [Required]
        public decimal TOTAL { get; set; }
        [Required]
        public string ESTADO { get; set; }


        public ICollection<DETALLE_SALIDA> detalles { get; set; }
        public USUARIO usuario { get; set; }
        public PERSONA persona { get; set; }


    }
}
