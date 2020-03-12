using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Web.Models.Warehouse.Articulo
{
    public class ArticuloViewModel
    {
        public int idarticulo { get; set; }
        public int idcategoria { get; set; }
        public int idalmacen { get; set; }
        public string almacen { get; set; }
        public string category { get; set; }
        public string nombre { get; set; }
        public decimal precio_venta { get; set; }
        public int stock { get; set; }
        public string descripcion { get; set; }
        public bool condicion { get; set; }
    }
}
