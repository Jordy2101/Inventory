using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Entity.Warehouse
{
    public class Articulo
    {
        public int ID_ARTICULO { get; set; }
        [ForeignKey("CATEGORIA")]
        public int ID_CATEGORIA { get; set; }
        [ForeignKey("ALMACEN")]
        public int ID_ALMACEN { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre del Articulo no puede tener menos de 3 letras o mas de 50 letras")]
        public string NOMBRE_ARTICULO { get; set; } 
        [StringLength(200)]
        public string DESCRIPCION { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "La marca del Articulo no puede tener menos de 3 letras o mas de 50 letras")]
        public string MARCA { get; set; }
        public decimal PRECIO { get; set;}
        public bool ESTADO { get; set; }
        public int STOCK { get; set; }

      public Almacen almacen { get; set;}
      public Category category { get; set; }

    }
}
