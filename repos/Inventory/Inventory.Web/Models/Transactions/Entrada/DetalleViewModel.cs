using System.ComponentModel.DataAnnotations;

namespace Inventory.Web.Models.Transactions.Entrada
{
    public class DetalleViewModel
    {

        [Required]
        public int ID_ARTICULO { get; set; }
        public string articulo { get; set; }
        [Required]
        public int CANTIDAD { get; set; }
        [Required]
        public decimal PRECIO { get; set; }
    }
}
