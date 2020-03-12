using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Inventory.Entity.Warehouse
{
    public class Category
    {
        public int ID_CATEGORIA { get; set; }
        [Required]
        [StringLength(50, MinimumLength =3, ErrorMessage ="El nombre de la categoria no puedo ser menor de 3 letras o mas de 50 letras")]
        public string NOMBRE_CATEGORIA { get; set; }
        [StringLength(200)]
        public string DESCRIPCION { get; set; }
        public bool ESTADO { get; set;}

        public ICollection<Articulo> articulos { get; set; }
    }
}
