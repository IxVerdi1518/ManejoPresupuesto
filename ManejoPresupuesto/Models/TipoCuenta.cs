using System.ComponentModel.DataAnnotations;

namespace ManejoPresupuesto.Models
{
    public class TipoCuenta
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public int UsuarioUd { get; set; }
        public int Orden { get; set; }
    }
}
