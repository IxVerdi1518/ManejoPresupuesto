using ManejoPresupuesto.Validaciones;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ManejoPresupuesto.Models
{
    public class TipoCuenta
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El campo {0} es requerido")]
        //[StringLength(maximumLength:50,MinimumLength =3, ErrorMessage ="La longitud del campo {0} debe de estar entre {2} y {1}")]
        //[Display(Name ="Nombre del Tipo Cuenta")]
        [PrimeraLetraMayuscula]
        /*Lo que ocupa aqui es validar directamente como javacript sin enviar la peticion*/
        [Remote(action:"VerificarExisteTipoCuenta", controller:"TiposCuentas")]
        public string Nombre { get; set; }
        public int UsuarioId { get; set; }
        public int Orden { get; set; }

        //Pruebas de otros tipos de validaciones
        //[Required(ErrorMessage = "El campo {0} es requerido")]
        //[EmailAddress(ErrorMessage ="El campo debe de ser un campo electronico")]
        //public string Email { get; set; }
        //[Range(minimum:18,maximum:130,ErrorMessage ="El valor debe de estar entre {1} y{2}")]
        //public int Edad { get; set; }
        //[Url(ErrorMessage ="El campo debe de ser una url valida")]
        //public string URL { get; set; }
        //[CreditCard(ErrorMessage ="La tarjeta de credito no es valida")]
        //[Display(Name ="Tarjeta de Credito")]
        //public string TarjetaDeCredito { get; set; }
    }
}
