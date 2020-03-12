using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Web.Models.Warehouse.Almacen
{
    public class AlmacenViewModel
    {

        public int ID_ALMACEN { get; set; }
        public string NOMBRE_ALMACEN { get; set; }
        public string DESCRIPCION { get; set; }
        public int CAPACIDAD { get; set; }
        public string UBICACION { get; set; } 
        public bool ESTADO { get; set; }
    }
}
