using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Web.Models.Warehouse.Almacen
{
    public class ActualizarViewModel
    {

        [Required]
        public int ID_ALMACEN { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "El nombre de la categoria no puedo ser menor de 3 letras o mas de 50 letras")]
        public string DESCRIPCION { get; set; }
        [Required]
        public int CAPACIDAD { get; set; }
        [StringLength(200)]
        public string UBICACION { get; set; }
    }
}
