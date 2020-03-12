using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Inventory.Entity.Transactions;

namespace Inventory.Entity.Users
{
    public class USUARIO
    {
        public int ID_USUARIO { get; set; }
        [Required]
        public int ID_ROL { get; set; } 
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre del usuario no puede tener menos de 3 letras o mas de 50 letras")]
        public string NOMBRE { get; set; }
        [StringLength(20)]
        public string TIPO_OCUMENTO { get; set; }
        [StringLength(20)]
        public string NUM_DOCUMENTO { get; set; }
        [StringLength(70)]
        public string DIRECCION { get; set; }
        [StringLength(20)]
        public string TELEFONO { get; set; }
        [StringLength(70)]
        [Required]
        public string EMAIL { get; set;}
        [Required]
        public byte [] PASSWORD_HAS { get; set; }
        [Required]
        public byte[] PASSWORD_SALT { get; set; }
        public bool CONDICION { get; set; }


        public ROL rol { get; set; }

        public ICollection<ENTRADA> ingresos { get; set; }

    }
}
