using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Web.Models.Warehouse.Almacen
{
    public class CrearViewModel
    {
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre de la categoria no puedo ser menor de 3 letras o mas de 50 letras")]
        public string NOMBRE_ALMACEN { get; set; }
        [StringLength(200)]
        public string DESCRIPCION { get; set; }
        public int CAPACIDAD { get; set; }
        public string UBICACION { get; set; }
    }
}
