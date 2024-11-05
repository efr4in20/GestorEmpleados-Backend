using System.ComponentModel.DataAnnotations;

namespace MiWebAPI.Models
{
    public class Empleado
    {
        [Key]
        public int Id { get; set; }
        public string Producto { get; set; }
        public int Canitdad { get; set; }
        public decimal Precio { get; set; }
        public decimal Total { get; set; }

        public decimal Subtotal ;
        public string Cliente { get; set; }
    }
}
