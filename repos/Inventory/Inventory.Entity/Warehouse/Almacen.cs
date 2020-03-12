using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Inventory.Entity.Warehouse
{
    public class Almacen
    {
        public int ID_ALMACEN { get; set; }
        [Required]
        [StringLength(200)]
        public string DESCRIPCION { get; set; }
        public int CAPACIDAD { get; set; }
        public bool ESTADO { get; set; }
        [StringLength(200)]
        public string UBICACION { get; set; }

        public ICollection<Articulo> articulos { get; set; }
    }
}
