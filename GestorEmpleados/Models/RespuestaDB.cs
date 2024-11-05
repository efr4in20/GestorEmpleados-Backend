using System.ComponentModel.DataAnnotations;

namespace GestorEmpleados.API.Models
{
    public class RespuestaDB
    {
        [Key]
        public int TipoError {  get; set; }
        public string? Mensaje { get; set; }
    }
}
