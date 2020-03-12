using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Inventory.Entity.Transactions;


namespace Inventory.Entity.Users
{
    public class PERSONA
    {
        public int ID_PERSONA { get; set; }
        public string TIPO_PERSONA { get; set; }
        [Required]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "El nombre de la persona no puedo ser menor de 3 letras o mas de 100 letras")]
        public string NOMBRE { get; set; }
        [StringLength(20)]
        public string TIPO_DOCUMENTO { get; set; }
        [StringLength(20)]
        public string NUM_DOCUMENTO { get; set; }
        [StringLength(70)]
        public string DIRECCION { get; set; }
        [StringLength(20)]
        public string TELEFONO { get; set; }
        [StringLength(50)]
        public string EMAIL { get; set;  }
        public ICollection<ENTRADA> ingresos { get; set; }

    }
}
