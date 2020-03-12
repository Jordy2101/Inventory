using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Entity.Users
{
    public class ROL
    {
        public int ID_ROL { get; set;}
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "El nombre del ROL no puede tener menos de 3 letras o mas de 30 letras")]
        public string NOMBRE { get; set;}
        [StringLength(100)]
        public string DESCRIPCION { get; set; }
        public bool CONDICION { get; set; }

        public ICollection<USUARIO> usuario { get; set; }

    }
}
