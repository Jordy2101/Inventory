using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Web.Models.Warehouse.Category
{
    public class CategoriaViewModel
    {
        public int ID_CATEGORIA { get; set; }   
        public string NOMBRE_CATEGORIA { get; set; }
        public string DESCRIPCION { get; set; }
        public bool ESTADO { get; set; }
    }
}

