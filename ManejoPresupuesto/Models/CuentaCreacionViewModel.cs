using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManejoPresupuesto.Models
{
    public class CuentaCreacionViewModel:Cuenta
    {
        /*Este metodo hereda de cuenta para ocupar todos sus atributos, pero la caracteritica especial es que este tipo de cuentas lo recoge como IEnumerable*/
        public IEnumerable<SelectListItem> TiposCuentas {  get; set; }

    }
}
