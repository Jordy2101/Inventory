using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Inventory.Web.Models.Warehouse.Category
{
    public class CrearViewModel
    {
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre de la categoria no puedo ser menor de 3 letras o mas de 50 letras")]
        public string NOMBRE_CATEGORIA { get; set; }
        [StringLength(200)]
        public string DESCRIPCION { get; set; }
    }
}
